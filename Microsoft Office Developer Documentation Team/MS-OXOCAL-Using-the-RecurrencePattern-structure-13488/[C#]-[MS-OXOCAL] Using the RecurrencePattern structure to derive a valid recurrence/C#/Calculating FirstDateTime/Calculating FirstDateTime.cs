using System;
using System.Globalization;

namespace CalculatingFirstDateTime
{
    public class SamplesCalculateFirstDateTime
    {
        public enum RECUR_FREQUENCY // period is stored as minutes for a daily recurrence, weeks for a weekly recurrence, and months for a monthly or yearly recurrence.
        {
            DAYS_IN_MINUTES,    // For daily recurrences the period stored as minutes in a whole number of days. 
                                // For example, a recurrence of every two days has a value of 2880 minutes.
            WEEKS,              // The period is stored in weeks.
            MONTHS              // For monthly or yearly, the period is stored in months. For a yearly recurrence, period = 12.
        };

        public static HebrewCalendar myHebrewCal = new HebrewCalendar();
        public static GregorianCalendar myCal = new GregorianCalendar();

        // The minimum valid date in the Gregorian Calendar is January 1st, 1601
        public static DateTime minDateGregorian = new DateTime(1601, 1, 1, new GregorianCalendar());
        
        // The minimum valid date in the Hebrew Calendar is (Gregorian) September 27th, 1601
        public static DateTime minDateHebrew = new DateTime(5362, 1, 1, myHebrewCal);

        public static void Main()
        {
            DateTime myStartDate;
            int myPeriod;

            for (RECUR_FREQUENCY myRecurrenceFrequency = RECUR_FREQUENCY.DAYS_IN_MINUTES; myRecurrenceFrequency <= RECUR_FREQUENCY.MONTHS; myRecurrenceFrequency++)
            {
                switch (myRecurrenceFrequency)
                {
                    case RECUR_FREQUENCY.DAYS_IN_MINUTES:
                        myPeriod = 4320;                            // every 3 days
                        myStartDate = DateTime.Today;               // Use today's date
                        break;
                    case RECUR_FREQUENCY.WEEKS:
                        myPeriod = 2;                               // every 2 weeks
                        myStartDate = DateTime.Today.AddYears(-1);  // Use a year ago from today
                        break;
                    case RECUR_FREQUENCY.MONTHS:                   
                        myPeriod = 12;                               // every 12 months
                        myStartDate = new DateTime(2008, 4, 6);     // Use April 6, 2008
                        break;
                    default:
                        myPeriod = 1;
                        myStartDate = DateTime.Today;
                        break;
                }
             
                RecurrenceValues myRecurrenceValues = new RecurrenceValues(myStartDate, myPeriod, myRecurrenceFrequency);

                DateTime validRecurrence = CalculateValidRecurrence(myRecurrenceValues, myRecurrenceValues.startDate);

                if (myRecurrenceFrequency == RECUR_FREQUENCY.MONTHS)
                {
                    break;
                }
                else
                {
                    DisplayValues(myCal, myRecurrenceValues.startDate, myRecurrenceValues, validRecurrence);
                }
            }
        }

        public static void DisplayValues(Calendar myCal, DateTime myDT, RecurrenceValues myRecurrenceValues, DateTime validRecurrence)
        {
            Console.WriteLine("Start Date: {0}/{1}/{2}", myCal.GetMonth(myDT), myCal.GetDayOfMonth(myDT), myCal.GetYear(myDT));
            Console.WriteLine("Recurrence Frequency: {0}", myRecurrenceValues.recurrenceFrequency);
            Console.WriteLine("Period: Every {0} " + "(" + myRecurrenceValues.recurrenceFrequency + ")", myRecurrenceValues.Period);
            Console.WriteLine("{0}/{1}/{2} is a valid recurrence date in the series.", myCal.GetMonth(validRecurrence), myCal.GetDayOfMonth(validRecurrence), myCal.GetYear(validRecurrence));
            Console.WriteLine();
        }

        public static int CalculateNumberOfMonthsBetweenTwoDates(DateTime myDate1, DateTime myDate2)
        {
            int numMonths;
            // Returns the number of months between two dates.         
            numMonths = myDate1 > myDate2 ? (12 * (myDate1.Year - myDate2.Year) + (myDate1.Month - myDate2.Month)) :
                                            (12 * (myDate2.Year - myDate1.Year) + (myDate2.Month - myDate1.Month));
            return numMonths;
        }

        public static DateTime CalculateDailyRecurrence(RecurrenceValues myRecurrenceValues)
        {
            double minutesBetweenMinDateAndStartDate = myRecurrenceValues.startDate.Subtract(minDateGregorian).TotalMinutes;

            DateTime myRecurrence;

            // We can use FirstDateTime to find a valid date given a "clip start date" that falls close to dates in our series
            // For daily recurrences, FirstDateTime will always be a multiple of 1440 (the number of minutes in a day).
            // We calculate FirstDateTime by taking the start date (as expressed in minutes between minimum date and start date)  and modulo the period in minutes. 
            double FirstDateTime = minutesBetweenMinDateAndStartDate % myRecurrenceValues.Period;

            // Finding a valid recurrence date given a clip start date:
            Random myRnd = new Random();
            DateTime inputDate = myRecurrenceValues.startDate.AddDays(myRnd.Next(1, 28));

            double clipStartDateInMinutes = inputDate.Subtract(minDateGregorian).TotalMinutes;
            double dateOffsetInMinutes = (clipStartDateInMinutes - FirstDateTime) % myRecurrenceValues.Period;
            if (dateOffsetInMinutes == 0)
            {
                myRecurrence = inputDate;
            }
            else
            {
                myRecurrence = inputDate.AddMinutes(-dateOffsetInMinutes);
            }

            return myRecurrence;
        }

        private static DateTime CalculateWeeklyRecurrence(RecurrenceValues myRecurrenceValues)
        {

            // Find the date of the first day of the week prior to the start date.
            DateTime modifiedStartDate = myRecurrenceValues.startDate.AddDays(-(int)myRecurrenceValues.startDate.DayOfWeek);

            // Calculate the number of minutes between midnight that day and midnight January 1, 1601.
            double modifiedStartDateInMinutes = modifiedStartDate.Subtract(minDateGregorian).TotalMinutes;

            // Take that value modulo the value of the Period field x 10080 (# of minutes in a week) to get the value of FirstDateTime.
            double FirstDateTime = modifiedStartDateInMinutes % (myRecurrenceValues.Period * 10080);

            // Get an input date close to the start date. (Add 3 to 7 weeks in days to the start date.)
            Random myRnd = new Random();
            DateTime inputDate = myRecurrenceValues.startDate.AddDays(myRnd.Next(21, 49));

            // Adjust to the start of the week since we're only looking for a valid week...
            inputDate = inputDate.AddDays(-(int)inputDate.DayOfWeek);

            // Get the input date in minutes
            double inputDateInMinutes = inputDate.Subtract(minDateGregorian).TotalMinutes;

            DateTime myRecurrence;

            // If the date offset is zero, the week is valid. Otherwise we subtract the offset from the input date
            // to find a valid week.

            double dateOffsetInMinutes = (inputDateInMinutes - FirstDateTime) % (myRecurrenceValues.Period * 10080);
            if (dateOffsetInMinutes == 0)
            {
                // if zero, the week is valid
                myRecurrence = inputDate;
            }
            else
            {
                // If non-zero, this is not a valid week. This value must be subtracted from the input date to return a valid week.
                myRecurrence = inputDate.AddMinutes(-dateOffsetInMinutes);
            }

            // Note that finding a valid day in a multi-days/week pattern requires using the PatternTypeSpecific value.
            // We're just assuming a 1x / week pattern based on the start date.
            myRecurrence = myRecurrence.AddDays((int)myRecurrenceValues.startDate.DayOfWeek);

            return myRecurrence;
        }

        public static DateTime CalculateMonthlyOrYearlyRecurrence(RecurrenceValues myRecurrenceValues)
        {
            // Find the first day of the month of the start date
            DateTime modifiedStartDate = myRecurrenceValues.startDate.AddDays(-myHebrewCal.GetDayOfMonth(myRecurrenceValues.startDate)+1);

            // Find the number of months between 9/27/1601 and our start date
            int monthsBetweenTwoDates = CalculateNumberOfMonthsBetweenTwoDates(minDateHebrew, modifiedStartDate);

            // Take that number of months and modulo the period to get an offset to add to the minimum Hebrew Calendar date (9/27/1601)
            int offsetInMonths = monthsBetweenTwoDates % (myRecurrenceValues.Period);

            // Add that number of Hebrew Lunar months to 9/27/1601.
            DateTime recurrenceOffsetDate = new DateTime();
            recurrenceOffsetDate = myHebrewCal.AddMonths(minDateHebrew, offsetInMonths);

            // Calculate the number of minutes between midnight that day and midnight, January 1, 1601. This is our recurrence offset (FirstDateTime), in minutes. 
            double FirstDateTime = new TimeSpan(recurrenceOffsetDate.Ticks - minDateGregorian.Ticks).TotalMinutes;

            // Since we're just calculating FirstDateTime, just return the start date

            return myRecurrenceValues.startDate;
        }


        public static DateTime CalculateValidRecurrence(RecurrenceValues myRecurrenceValues, DateTime myRecurrence)
        {
            switch (myRecurrenceValues.recurrenceFrequency)    // This will be daily, weekly, or monthly/yearly.
            {
                case RECUR_FREQUENCY.DAYS_IN_MINUTES:
                    return CalculateDailyRecurrence(myRecurrenceValues);

                case RECUR_FREQUENCY.WEEKS:
                    return CalculateWeeklyRecurrence(myRecurrenceValues);

                case RECUR_FREQUENCY.MONTHS:
                    return CalculateMonthlyOrYearlyRecurrence(myRecurrenceValues);

                default:
                    return new DateTime();
            }
        }

        public class RecurrenceValues
        {
            public RecurrenceValues(DateTime myStartDate, int myPeriod, RECUR_FREQUENCY myRecurrenceFrequency)
            {
                startdate = myStartDate;
                period = myPeriod;
                recurrencefrequency = myRecurrenceFrequency;
            }

            // The period is how often the appointment recurs, in months. A yearly recurrence always has a value of 12.
            private int period;
            public int Period
            {
                get { return period; }
                set { period = Period; }
            }

            // The start date is the date of the first appointment
            private DateTime startdate;
            public DateTime startDate
            {
                get { return startdate; }
                set { startdate = startDate; }
            }

            // The recurrence frequency is either daily, weekly, or monthly/yearly.
            private RECUR_FREQUENCY recurrencefrequency;
            public RECUR_FREQUENCY recurrenceFrequency
            {
                get { return recurrencefrequency; }
                set { recurrencefrequency = recurrenceFrequency; }
            }
        }
    }
}
