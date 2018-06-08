using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

using System.Diagnostics;  // For Trace

namespace WebRole1
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            try
            {
                // For information on handling configuration changes
                // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

                DiagnosticMonitorConfiguration config = DiagnosticMonitor.GetDefaultInitialConfiguration();

                // Display information about the default configuration.
                ShowConfig(config);

                // Add in configuration settings for several performance counters.
                config.PerformanceCounters.ScheduledTransferPeriod = TimeSpan.FromMinutes(2D);

                config.PerformanceCounters.BufferQuotaInMB = 10;

                // Use 30 seconds for the perf counter sample rate.
                TimeSpan perfSampleRate = TimeSpan.FromSeconds(30D);

                config.PerformanceCounters.DataSources.Add(new PerformanceCounterConfiguration()
                {
                    CounterSpecifier = @"\Memory\Available Bytes",
                    SampleRate = perfSampleRate
                });

                config.PerformanceCounters.DataSources.Add(new PerformanceCounterConfiguration()
                {
                    CounterSpecifier = @"\Processor(_Total)\% Processor Time",
                    SampleRate = perfSampleRate
                });

                config.PerformanceCounters.DataSources.Add(new PerformanceCounterConfiguration()
                {
                    CounterSpecifier = @"\ASP.NET\Applications Running",
                    SampleRate = perfSampleRate
                });

                // Display information about the changed configuration.
                ShowConfig(config);

                // Apply the updated configuration to the diagnostic monitor.
                // The first parameter is for the connection string configuration setting.
                DiagnosticMonitor.Start("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString", config);

            }
            catch (Exception e)
            {
                Trace.WriteLine("Exception during WebRole1.OnStart: " + e.ToString());
                // Take other action as needed.
            }

            return base.OnStart();

        }

        private void ShowConfig(DiagnosticMonitorConfiguration config)
        {

            try
            {

                if (null == config)
                {
                    Trace.WriteLine("Null configuration passed to ShowConfig");
                    return;
                }

                // Display the general settings of the configuration
                Trace.WriteLine("*** General configuration settings ***");
                Trace.WriteLine("Config change poll interval: " + config.ConfigurationChangePollInterval.ToString());
                Trace.WriteLine("Overall quota in MB: " + config.OverallQuotaInMB);

                // Display the diagnostic infrastructure logs
                Trace.WriteLine("*** Diagnostic infrastructure settings ***");
                Trace.WriteLine("DiagnosticInfrastructureLogs buffer quota in MB: " + config.DiagnosticInfrastructureLogs.BufferQuotaInMB);
                Trace.WriteLine("DiagnosticInfrastructureLogs scheduled transfer log filter: " + config.DiagnosticInfrastructureLogs.ScheduledTransferLogLevelFilter);
                Trace.WriteLine("DiagnosticInfrastructureLogs transfer period: " + config.DiagnosticInfrastructureLogs.ScheduledTransferPeriod.ToString());

                // List the Logs info
                Trace.WriteLine("*** Logs configuration settings ***");
                Trace.WriteLine("Logs buffer quota in MB: " + config.Logs.BufferQuotaInMB);
                Trace.WriteLine("Logs scheduled transfer log level filter: " + config.Logs.ScheduledTransferLogLevelFilter);
                Trace.WriteLine("Logs transfer period: " + config.Logs.ScheduledTransferPeriod.ToString());

                // List the Directories info
                Trace.WriteLine("*** Directories configuration settings ***");
                Trace.WriteLine("Directories buffer quota in MB: " + config.Directories.BufferQuotaInMB);
                Trace.WriteLine("Directories scheduled transfer period: " + config.Directories.ScheduledTransferPeriod.ToString());
                int count = config.Directories.DataSources.Count, index;
                if (0 == count)
                {
                    Trace.WriteLine("No data sources for Directories");
                }
                else
                {
                    for (index = 0; index < count; index++)
                    {
                        Trace.WriteLine("Directories configuration data source:");
                        Trace.WriteLine("\tContainer: " + config.Directories.DataSources[index].Container);
                        Trace.WriteLine("\tDirectory quota in MB: " + config.Directories.DataSources[index].DirectoryQuotaInMB);
                        Trace.WriteLine("\tPath: " + config.Directories.DataSources[index].Path);
                        Trace.WriteLine("");
                    }
                }

                // List the event log info
                Trace.WriteLine("*** Event log configuration settings ***");
                Trace.WriteLine("Event log buffer quota in MB: " + config.WindowsEventLog.BufferQuotaInMB);
                count = config.WindowsEventLog.DataSources.Count;
                if (0 == count)
                {
                    Trace.WriteLine("No data sources for event log");
                }
                else
                {
                    for (index = 0; index < count; index++)
                    {
                        Trace.WriteLine("Event log configuration data source:" + config.WindowsEventLog.DataSources[index]);
                    }
                }
                Trace.WriteLine("Event log scheduled transfer log level filter: " + config.WindowsEventLog.ScheduledTransferLogLevelFilter);
                Trace.WriteLine("Event log scheduled transfer period: " + config.WindowsEventLog.ScheduledTransferPeriod.ToString());

                // List the performance counter info
                Trace.WriteLine("*** Performance counter configuration settings ***");
                Trace.WriteLine("Performance counter buffer quota in MB: " + config.PerformanceCounters.BufferQuotaInMB);
                Trace.WriteLine("Performance counter scheduled transfer period: " + config.PerformanceCounters.ScheduledTransferPeriod.ToString());
                count = config.PerformanceCounters.DataSources.Count;
                if (0 == count)
                {
                    Trace.WriteLine("No data sources for PerformanceCounters");
                }
                else
                {
                    for (index = 0; index < count; index++)
                    {
                        Trace.WriteLine("PerformanceCounters configuration data source:");
                        Trace.WriteLine("\tCounterSpecifier: " + config.PerformanceCounters.DataSources[index].CounterSpecifier);
                        Trace.WriteLine("\tSampleRate: " + config.PerformanceCounters.DataSources[index].SampleRate.ToString());
                        Trace.WriteLine("");
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("Exception during ShowConfig: " + e.ToString());
                // Take other action as needed.
            }
        }
    }
}
