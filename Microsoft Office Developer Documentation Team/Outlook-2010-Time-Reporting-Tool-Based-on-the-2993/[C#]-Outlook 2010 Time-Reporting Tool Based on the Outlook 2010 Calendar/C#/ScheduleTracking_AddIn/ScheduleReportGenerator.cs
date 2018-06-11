using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace ScheduleTracking_AddIn
{
    public static class ScheduleReportGenerator
    {
        /// <summary>Default format string for displaying appointment durration
        /// in the report. </summary>
        /// <remarks>Appointment durration is stored as a double, representing
        /// the durration in hours.</remarks>
        private static readonly string durationFormat = "f2";

        /// <summary>Default format string for displaying percentage values in
        /// the report.</summary>
        private static readonly string percentFormat = "p1";

        /// <summary>The separator character used in the category list.</summary>
        private static readonly string[] listSeparator =
            new string[] { CultureInfo.CurrentCulture.TextInfo.ListSeparator };

        /// <summary>Gets the set of categories assigned to an Outlook calendar
        /// item.</summary>
        /// <param name="appointment">The item from which to get the categories.
        /// </param>
        /// <returns>The categories assigned to the item.</returns>
        private static HashSet<string> GetAppointmentCategories(
            Outlook.AppointmentItem appointment)
        {
            if (string.IsNullOrEmpty(appointment.Categories))
            {
                return new HashSet<string>();
            }

            string[] rawCategories = appointment.Categories.Split(
                listSeparator, StringSplitOptions.RemoveEmptyEntries);

            HashSet<string> categories = new HashSet<string>();
            foreach (string rawCategory in rawCategories)
            {
                string category = rawCategory.Trim();
                if (category.Length > 0)
                {
                    categories.Add(category);
                }
            }
            return categories;
        }

        /// <summary>Returns an escaped string that represents the original
        /// value in CSV format.</summary>
        /// <param name="s">The string value to encode.</param>
        /// <remarks>Adds leading and trailing quotation marks and escapes
        /// internal quotation marks by replacing them with two consecutive
        /// quotation marks. This is necessary when a field contains any
        /// quotation marks or commas.
        /// </remarks>
        private static string CsvEscape(string s)
        {
            return string.Format("\"{0}\"", s.Replace("\"", "\"\""));
        }

        /// <summary>Represents an Outlook calendar item or appointment within
        /// the context of the report to generate.</summary>
        internal class ScheduleItem
        {
            private ScheduleReport report;
            private string subject;
            private DateTime start;
            private DateTime end;
            private HashSet<string> relevantCategories;
            private double duration;

            public ScheduleItem(
                ScheduleReport report, Outlook.AppointmentItem appointment)
            {
                this.report = report;
                this.subject = appointment.Subject;
                this.start = appointment.Start;
                this.end = appointment.End;
                this.relevantCategories = GetAppointmentCategories(appointment);
                this.relevantCategories.IntersectWith(
                    report.ReportingCategories);
                this.duration = appointment.Duration / 60.0;
            }

            /// <summary>Returns the appointment information in CSV format.
            /// </summary>
            /// <remarks>Outputs subject, hours per category (multiple),
            /// (empty), reported hours, (empty), start date, start time,
            /// end date, end time, appointment duration.</remarks>
            public string ToCsvString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(CsvEscape(subject));
                foreach (string category in report.ReportingCategories)
                {
                    double categoryHours =
                        relevantCategories.Contains(category)
                        ? HoursToReportPerCategory : 0.0;
                    sb.Append("," + categoryHours.ToString(durationFormat));
                }
                sb.Append(",," +
                    HoursWithinReportingPeriod.ToString(durationFormat));
                sb.Append(",," + start.ToString("MM/dd/yyyy"));
                sb.Append("," + start.ToString("HH:mm"));
                sb.Append("," + end.ToString("MM/dd/yyyy"));
                sb.Append("," + end.ToString("HH:mm"));
                sb.Append("," + duration.ToString(durationFormat));

                return sb.ToString();
            }

            public string Subject { get { return subject; } }

            /// <summary>Gets the starting time of the appointment, clipped
            /// to the start date of the report.</summary>
            public DateTime ClippedStart
            {
                get
                {
                    return (start > report.Start) ? start : report.Start;
                }
            }

            /// <summary>Gets the ending time of the appointment, clipped
            /// to the end date of the report.</summary>
            public DateTime ClippedEnd
            {
                get
                {
                    return (end < report.End) ? end : report.End;
                }
            }

            public double HoursWithinReportingPeriod
            {
                get
                {
                    return (ClippedEnd - ClippedStart).TotalHours;
                }
            }

            public HashSet<string> RelevantCategories
            {
                get { return relevantCategories; }
            }

            public double HoursToReportPerCategory
            {
                get
                {
                    return (this.relevantCategories.Count > 0) ?
                        HoursWithinReportingPeriod / this.relevantCategories.Count : 0;
                }
            }
        }

        /// <summary>Represents the data and logic for generating a time
        /// tracking report.</summary>
        internal class ScheduleReport
        {
            /// <summary>The header row for appointment information, in CSV
            /// format.</summary>
            private readonly string csvHeader;

            private DateTime reportStart;
            private DateTime reportEnd;
            private List<string> reportingCategories;
            private List<ScheduleItem> items;
            private bool includeDailySubtotals;

            /// <summary>Initializes a new time tracking report.</summary>
            /// <param name="reportStart">The beginning date and time for the
            /// report.</param>
            /// <param name="reportEnd">The ending date and time for the
            /// report.</param>
            /// <param name="categories">The Outlook categories for which to 
            /// collect data.</param>
            /// <param name="includeDailySubtotals">Indicates whether to
            /// include daily subtotals in the generated report.</param>
            public ScheduleReport(DateTime reportStart, DateTime reportEnd,
                HashSet<string> reportingCategories, bool includeDailySubtotals)
            {
                this.reportStart = reportStart;
                this.reportEnd = reportEnd;

                this.reportingCategories =
                    new List<string>(reportingCategories);
                this.reportingCategories.Sort();

                this.items = new List<ScheduleItem>();

                this.includeDailySubtotals = includeDailySubtotals;

                // Construct the CSV header string.
                StringBuilder sb = new StringBuilder("Categories");
                foreach (string category in reportingCategories)
                {
                    sb.AppendFormat(",{0}", CsvEscape(category));
                }
                sb.Append(
                    ",,Reported Hours,,Start Date,Start Time,End Date,End Time,Duration");
                csvHeader = sb.ToString();
            }

            public DateTime Start { get { return reportStart; } }
            public DateTime End { get { return reportEnd; } }
            public List<string> ReportingCategories
            {
                get { return reportingCategories; }
            }

            public void Add(ScheduleItem item)
            {
                if (item != null)
                {
                    items.Add(item);
                }
            }

            internal void CreateCsvFile(string filepath)
            {
                try
                {
                    using (TextWriter writer = new StreamWriter(filepath))
                    {
                        GenerateReport(writer);
                    }
                }
                catch
                {
                    // Insert exception handling code here.
                }
            }

            private void GenerateReport(TextWriter writer)
            {
                writer.WriteLine("Hours by Category from {0} through {1}",
                    reportStart, reportEnd);
                writer.WriteLine();
                writer.WriteLine(csvHeader);
                writer.WriteLine();
                if (items.Count > 0)
                {
                    Dictionary<string, double> grandTotals =
                        new Dictionary<string, double>();
                    foreach (string category in reportingCategories)
                    {
                        grandTotals[category] = 0.0;
                    }

                    Dictionary<int, Dictionary<string, double>> dailySubtotals =
                        new Dictionary<int, Dictionary<string, double>>();
                    for (int dayOffset = 0;
                        dayOffset <= (reportEnd - reportStart).Days; dayOffset++)
                    {
                        dailySubtotals[dayOffset] = new Dictionary<string, double>();
                        foreach (string category in reportingCategories)
                        {
                            dailySubtotals[dayOffset][category] = 0.0;
                        }
                    }

                    foreach (ScheduleItem item in items)
                    {
                        AddItemHoursToTotals(grandTotals, dailySubtotals, item);
                    }

                    WriteReportData(writer, grandTotals, dailySubtotals);
                }
                else
                {
                    writer.WriteLine("No items");
                }
                writer.Close();
            }

            private void WriteReportData(
                TextWriter writer,
                Dictionary<string, double> grandTotals,
                Dictionary<int, Dictionary<string, double>> dailySubtotals)
            {
                double totalHoursInReportedInPeriod =
                    grandTotals.Sum(x => x.Value);

                WriteTotalsForPeriod(
                    writer, grandTotals, totalHoursInReportedInPeriod);
                WritePercentsPerPeriod(
                    writer, grandTotals, totalHoursInReportedInPeriod);

                List<int> dayOffsets = new List<int>(dailySubtotals.Keys);
                dayOffsets.Sort();
                if (includeDailySubtotals)
                {
                    WriteDailySubtotals(writer, dailySubtotals, dayOffsets);
                }

                writer.WriteLine();
                WriteAppointmentDetails(writer);
            }

            private void WriteTotalsForPeriod(TextWriter writer,
                Dictionary<string, double> grandTotals,
                double totalHoursInReportedInPeriod)
            {
                writer.Write("Period Totals");
                foreach (string category in reportingCategories)
                {
                    writer.Write(",{0}",
                        grandTotals[category].ToString(durationFormat));
                }
                writer.WriteLine(",,{0}",
                    totalHoursInReportedInPeriod.ToString(durationFormat));
            }

            private void WritePercentsPerPeriod(TextWriter writer,
                Dictionary<string, double> grandTotals,
                double totalHoursInReportedInPeriod)
            {
                writer.Write("% per Period");
                double runningTotal = 0.0;
                foreach (string category in reportingCategories)
                {
                    double itemPercent = (totalHoursInReportedInPeriod > 0) ?
                        grandTotals[category] / totalHoursInReportedInPeriod : 0;
                    runningTotal += itemPercent;

                    writer.Write(",{0}", itemPercent.ToString(percentFormat));
                }
                writer.WriteLine(",,{0}", (totalHoursInReportedInPeriod > 0) ?
                    runningTotal.ToString(percentFormat) : "n/a");
            }

            private void WriteDailySubtotals(TextWriter writer,
                Dictionary<int, Dictionary<string, double>> dailySubtotals,
                List<int> dayOffsets)
            {
                writer.WriteLine();
                foreach (int day in dayOffsets)
                {
                    writer.Write("Day of {0}",
                        reportStart.AddDays(day).ToString("MM/dd/yyyy"));
                    foreach (string category in reportingCategories)
                    {
                        writer.Write(",{0}",
                            dailySubtotals[day][category].ToString(durationFormat));
                    }
                    double totalForDay = dailySubtotals[day].Sum(x => x.Value);
                    writer.WriteLine(",,{0}", totalForDay.ToString(durationFormat));
                }
            }

            private void WriteAppointmentDetails(TextWriter writer)
            {
                writer.WriteLine("Appointment Detail");
                DateTime dayToReportUpon = reportStart.AddDays(-1);
                foreach (ScheduleItem item in items)
                {
                    if (item.RelevantCategories.Count == 0) continue;

                    if (item.ClippedStart.Date > dayToReportUpon)
                    {
                        dayToReportUpon = item.ClippedStart.Date;
                        writer.WriteLine(dayToReportUpon.ToString("MM/dd/yyyy"));
                    }
                    writer.WriteLine(item.ToCsvString());
                }
            }

            private void AddItemHoursToTotals(
                Dictionary<string, double> grandTotals,
                Dictionary<int, Dictionary<string, double>> dailySubtotals,
                ScheduleItem item)
            {
                if (includeDailySubtotals)
                {
                    int startDay = (item.ClippedStart - reportStart).Days;
                    int endDay = (item.ClippedEnd - reportStart).Days;
                    if (startDay == endDay)
                    {
                        CalulateDailySubtotalsForSingleDayAppointment(
                            dailySubtotals, item, startDay);
                    }
                    else
                    {
                        for (int day = startDay; day <= endDay; day++)
                        {
                            CalculateDailySubtotalsForMultiDayAppointment(
                                dailySubtotals, item, startDay, endDay, day);
                        }
                    }
                }

                foreach (string category in item.RelevantCategories)
                {
                    grandTotals[category] += item.HoursToReportPerCategory;
                }
            }

            private void CalculateDailySubtotalsForMultiDayAppointment(
                Dictionary<int, Dictionary<string, double>> dailySubtotals,
                ScheduleItem item, int startDay, int endDay, int day)
            {
                double startHourForDay = (day > startDay) ? 0.0 :
                    (item.ClippedStart - item.ClippedStart.Date).Hours;
                double endHourForDay = (day < endDay) ? 24.0 :
                    (item.ClippedEnd - item.ClippedEnd.Date).Hours;
                double hours = endHourForDay - startHourForDay;
                double percentOfMeetingWithinDay =
                    hours / item.HoursWithinReportingPeriod;
                foreach (string category in item.RelevantCategories)
                {
                    dailySubtotals[day][category] +=
                        item.HoursToReportPerCategory * percentOfMeetingWithinDay;
                }
            }

            private void CalulateDailySubtotalsForSingleDayAppointment(
                Dictionary<int, Dictionary<string, double>> dailySubtotals,
                ScheduleItem item, int startDay)
            {
                foreach (string category in item.RelevantCategories)
                {
                    dailySubtotals[startDay][category] +=
                        item.HoursToReportPerCategory;
                }
            }
        }

        public static Outlook.Items GetCalendarItemsInTimeFrame(
            DateTime reportStart, DateTime reportEnd)
        {
            Outlook.NameSpace session = Globals.ThisAddIn.Application.Session;
            Outlook.Folder calendar = session.GetDefaultFolder(
                    Outlook.OlDefaultFolders.olFolderCalendar)
                    as Outlook.Folder;

            // Specify the filter this way to include appointments that
            // overlap with the specified date range but do not necessarily
            // fall entirely within the date range.
            // Date values in filter must not include seconds.
            string filter = string.Format(
                "[Start] < '{0}' AND [End] > '{1}'",
                reportEnd.ToString("MM/dd/yyyy hh:mm tt"),
                reportStart.ToString("MM/dd/yyyy hh:mm tt"));

            // Include recurring calendar items.
            Outlook.Items calendarItems = calendar.Items;
            calendarItems.Sort("[Start]", Type.Missing);
            calendarItems.IncludeRecurrences = true;
            calendarItems = calendarItems.Restrict(filter);

            return calendarItems;
        }

        /// <summary>Generates a time tracking report and saves it in CSV
        /// format.</summary>
        /// <param name="reportStart">The begining date and time for the
        /// report.</param>
        /// <param name="reportEnd">The ending date and time for the report.
        /// </param>
        /// <param name="categories">The Outlook categories for which to
        /// collect data.</param>
        /// <param name="includeDailySubtotals">Indicates whether to include
        /// daily subtotals in the generated report.</param>
        /// <param name="filepath">The file to which to save the report.
        /// </param>
        internal static void GenerateReport(
            DateTime reportStart, DateTime reportEnd,
            HashSet<string> categories, bool includeDailySubtotals,
            string filepath)
        {
            ScheduleReport report = new ScheduleReport(
                reportStart, reportEnd, categories, includeDailySubtotals);

            Outlook.Items calendarItems =
                GetCalendarItemsInTimeFrame(reportStart, reportEnd);
            foreach (Outlook.AppointmentItem appointment in calendarItems)
            {
                report.Add(new ScheduleItem(report, appointment));
            }

            report.CreateCsvFile(filepath);
        }
    }
}
