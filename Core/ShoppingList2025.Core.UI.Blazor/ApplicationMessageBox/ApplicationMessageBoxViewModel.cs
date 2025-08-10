using System.Collections.ObjectModel;
using AsyncAwaitBestPractices.MVVM;
using MudBlazor;
using PropertyChanged;
using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Core.UI.Blazor;

[AddINotifyPropertyChangedInterface]
public class ApplicationMessageBoxViewModel(IMainAssembly mainAssembly) 
    : BlazorDialogViewModelBase(), IApplicationMessageBoxViewModel, IDisposable
{
    public virtual Task InitializeAsync(IApplicationMessageBoxParameter parameter)
    {
        this.Title = parameter.Title;
        this.Prompt = parameter.Prompt.Trim();

        this.IsIconVisible = parameter.Symbol != ApplicationMessageBoxSymbol.None;
        //this.IconType = GetMessageBoxIcon(parameter.Symbol);

        var buttons = InitializeButtons(parameter);
        this.MessageBoxButtonItems = new ObservableCollection<ApplicationMessageBoxButtonItem>(buttons);

        return Task.CompletedTask;
    }

    private static List<ApplicationMessageBoxButtonItem> InitializeButtons(IApplicationMessageBoxParameter parameter)
    {
        var retval = new List<ApplicationMessageBoxButtonItem>();

        if ((parameter.Buttons & ApplicationMessageBoxButtons.Ok) != 0)
        {
            AddButton(retval, ApplicationMessageBoxButtons.Ok, "Ok");
        }
        if ((parameter.Buttons & ApplicationMessageBoxButtons.Yes) != 0)
        {
            AddButton(retval, ApplicationMessageBoxButtons.Yes, "Ja");
        }
        if ((parameter.Buttons & ApplicationMessageBoxButtons.No) != 0)
        {
            AddButton(retval, ApplicationMessageBoxButtons.No, "Nein");
        }
        if ((parameter.Buttons & ApplicationMessageBoxButtons.Abort) != 0)
        {
            AddButton(retval, ApplicationMessageBoxButtons.Abort, "Abbrechen");
        }
        if ((parameter.Buttons & ApplicationMessageBoxButtons.Retry) != 0)
        {
            AddButton(retval, ApplicationMessageBoxButtons.Retry, "Wiederholen");
        }
        if ((parameter.Buttons & ApplicationMessageBoxButtons.Ignore) != 0)
        {
            AddButton(retval, ApplicationMessageBoxButtons.Ignore, "Ignorieren");
        }
        if ((parameter.Buttons & ApplicationMessageBoxButtons.Cancel) != 0)
        {
            AddButton(retval, ApplicationMessageBoxButtons.Cancel, "Abbrechen");
        }
        if ((parameter.Buttons & ApplicationMessageBoxButtons.Help) != 0)
        {
            AddButton(retval, ApplicationMessageBoxButtons.Help, "Hilfe");
        }

        return retval;
    }

    private static void AddButton(List<ApplicationMessageBoxButtonItem> buttons, ApplicationMessageBoxButtons buttontype, string text)
    {
        var item = new ApplicationMessageBoxButtonItem
        {
            Text = text,
            ButtonType = buttontype
        };

        buttons.Add(item);
    }

    public string Title { get; private set; } = mainAssembly.ProductName;
    public string Prompt { get; private set; } = string.Empty;
    public bool IsIconVisible { get; private set; }
    public int RemainingTime { get; private set; }

    public ObservableCollection<ApplicationMessageBoxButtonItem> MessageBoxButtonItems { get; private set; } = [];

    public Task PerformMessageBoxButtonAsync(ApplicationMessageBoxButtons item)
    {
        return this.CloseDialogAsync(item);
    }

    private Task CloseDialogAsync(ApplicationMessageBoxButtons resultButton)
    {
        return this.DialogInstance == null
            ? throw new NullReferenceException(nameof(this.DialogInstance))
            : this.BlazorComponentInvokeAsync == null
                ? throw new NullReferenceException(nameof(this.BlazorComponentInvokeAsync))
                : this.BlazorComponentInvokeAsync.InvokeAsync(() => this.DialogInstance.Close(DialogResult.Ok(resultButton)));
    }

    private bool isDisposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.isDisposed)
        {
            if (disposing)
            {
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            this.isDisposed = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~GuiMessageBoxViewModel()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

}
