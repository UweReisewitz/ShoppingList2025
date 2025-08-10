namespace ShoppingList2025.Core.Types;

public enum ApplicationMessageBoxMessageMode
{
    Information = 0,
    Warning = 1,
    Error = 2
}

public enum ApplicationMessageBoxSymbol
{
    None = 0,
    Stop = 1,
    Question = 2,
    Exclamation = 3,
    Information = 4
}

[Flags]
public enum ApplicationMessageBoxButtons
{
    None = 0,
    Ok = 1,
    Cancel = 2,
    Abort = 4,
    Retry = 8,
    Ignore = 16,
    Yes = 32,
    No = 64,
    User1 = 128,
    User2 = 256,
    User3 = 512,
    Help = 8192,

    OkCancel = 3,
    AbortRetryIgnore = 28,
    YesNoCancel = 98,
    YesNo = 96,
    RetryCancel = 10
}
