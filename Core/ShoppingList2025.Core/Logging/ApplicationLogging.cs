using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Layouts;
using NLog.Targets;
using ShoppingList2025.Core.Types;
using LogLevel = NLog.LogLevel;

namespace ShoppingList2025.Core;

public class ApplicationLogging(IMainAssembly mainAssembly) : IApplicationLogging
{
    public ILoggerFactory LoggerFactory { get; private set; } = new LoggerFactory();

    private readonly LogFactory logFactoryNLog = new();

    public void Init()
    {
        if (string.IsNullOrEmpty(this.TraceFileName))
        {
            this.TraceFileName = mainAssembly.ProductName;
        }

        if (string.IsNullOrEmpty(this.TraceFolder))
        {
            this.TraceFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), mainAssembly.ProductName, "Log");
        }

        if (File.Exists(this.ConfigFileName))
        {
            this.logFactoryNLog.Setup().LoadConfigurationFromFile(this.ConfigFileName);
        }
        else
        {
            var loggingConfiguration = new LoggingConfiguration();
            loggingConfiguration.AddRule(this.defaultLogLevel, NLog.LogLevel.Fatal, this.GetCsvFileTarget("csvlogfile"));
            loggingConfiguration.AddRule(this.defaultLogLevel, NLog.LogLevel.Fatal, GetConsoleTarget("logconsole"));
            this.logFactoryNLog.Configuration = loggingConfiguration;
        }

        LogManager.Configuration = this.logFactoryNLog.Configuration;
        LogManager.Setup().SetupExtensions(ext => {
            ext.RegisterTarget<NLog.Targets.ConcurrentFileTarget>();
        });
        this.LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder.AddNLog());
    }

    public string ConfigFileName { get; set; } = "nlog.config";

    public Microsoft.Extensions.Logging.LogLevel DefaultLogLevel { get; set; } = Microsoft.Extensions.Logging.LogLevel.Information;

    private NLog.LogLevel defaultLogLevel => ConvertToNLogLevel(this.DefaultLogLevel);
    internal string TraceFolder { get; set; } = string.Empty;
    private static string TraceExtension => "log";
    public string TraceFileName { get; set; } = string.Empty;
    public string TraceFile => Path.Combine(this.TraceFolder, $"{this.TraceFileName}.{TraceExtension}");

    public void SetLogLevel(Microsoft.Extensions.Logging.LogLevel logLevel)
    {
        var nlogLevel = ConvertToNLogLevel(logLevel);
        this.SetLogLevel(nlogLevel);
    }

    internal void SetLogLevel(LogLevel logLevel)
    {
        if (logLevel == LogLevel.Off)
        {
            this.logFactoryNLog.SuspendLogging();
        }
        else
        {
            if (!this.logFactoryNLog.IsLoggingEnabled())
            {
                this.logFactoryNLog.ResumeLogging();
            }

            if (this.logFactoryNLog.Configuration != null)
            {
                foreach (var rule in this.logFactoryNLog.Configuration.LoggingRules)
                {
                    rule.DisableLoggingForLevels(NLog.LogLevel.Trace, NLog.LogLevel.Fatal);
                    for (var i = logLevel.Ordinal; i <= 5; i++)
                    {
                        rule.EnableLoggingForLevel(LogLevel.FromOrdinal(i));
                    }
                }
            }
        }

        LogManager.ReconfigExistingLoggers();
    }

    private FileTarget GetCsvFileTarget(string name)
    {
        var layout = new CsvLayout()
        {
            Quoting = CsvQuotingMode.All,
            WithHeader = true,
            Delimiter = CsvColumnDelimiterMode.Pipe,
        };
        layout.Columns.Add(new CsvColumn() { Name = "Date", Layout = "${longdate}", Quoting = CsvQuotingMode.Nothing });
        layout.Columns.Add(new CsvColumn() { Name = "User", Layout = "${environment-user}", Quoting = CsvQuotingMode.Nothing });
        layout.Columns.Add(new CsvColumn() { Name = "Machine", Layout = "${machinename}", Quoting = CsvQuotingMode.Nothing });
        layout.Columns.Add(new CsvColumn() { Name = "ProcessId", Layout = "${processid}", Quoting = CsvQuotingMode.Nothing });
        layout.Columns.Add(new CsvColumn() { Name = "Loglevel", Layout = "${level}", Quoting = CsvQuotingMode.Nothing });
        layout.Columns.Add(new CsvColumn() { Name = "Callsite", Layout = "${callsite}", Quoting = CsvQuotingMode.Nothing });
        layout.Columns.Add(new CsvColumn() { Name = "Version", Layout = "${assembly-version}", Quoting = CsvQuotingMode.Nothing });
        layout.Columns.Add(new CsvColumn() { Name = "Message", Layout = "${message}", Quoting = CsvQuotingMode.All });
        layout.Columns.Add(new CsvColumn() { Name = "Info", Layout = "${all-event-properties} ${exception:format=tostring}", Quoting = CsvQuotingMode.All });
        return new FileTarget(name)
        {
            FileName = this.TraceFile,
            Layout = layout,
            MaxArchiveDays = 14,
            CreateDirs = true,
            ArchiveSuffixFormat = "_{0:0000}",
            ArchiveAboveSize = 312500,
            ArchiveOldFileOnStartup = false,
            KeepFileOpen = false,
        };
    }

    private static ConsoleTarget GetConsoleTarget(string name) => new(name)
    {
        Layout = new SimpleLayout("${longdate}|${level}|${environment-user}|${machinename}|${processid}|${callsite}|${callsite-linenumber}|${message}|${all-event-properties} ${exception:format=tostring}"),
    };

    private static NLog.LogLevel ConvertToNLogLevel(Microsoft.Extensions.Logging.LogLevel logLevel) => logLevel switch
    {
        Microsoft.Extensions.Logging.LogLevel.Trace => NLog.LogLevel.Trace,
        Microsoft.Extensions.Logging.LogLevel.Debug => NLog.LogLevel.Debug,
        Microsoft.Extensions.Logging.LogLevel.Information => NLog.LogLevel.Info,
        Microsoft.Extensions.Logging.LogLevel.Warning => NLog.LogLevel.Warn,
        Microsoft.Extensions.Logging.LogLevel.Error => NLog.LogLevel.Error,
        Microsoft.Extensions.Logging.LogLevel.Critical => NLog.LogLevel.Fatal,
        Microsoft.Extensions.Logging.LogLevel.None => NLog.LogLevel.Off,
        _ => NLog.LogLevel.Trace,
    };
}


