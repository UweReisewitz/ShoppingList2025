using Microsoft.Extensions.Logging;

namespace ShoppingList2025.Core.Types;

public interface IApplicationLogging
{
    string TraceFile { get; }

    string TraceFileName { get; set; }

    string ConfigFileName { get; set; }

    LogLevel DefaultLogLevel { get; set; }

    void Init();
    void SetLogLevel(LogLevel logLevel);
}