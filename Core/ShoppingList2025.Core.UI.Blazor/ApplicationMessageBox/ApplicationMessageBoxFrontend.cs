using System.ComponentModel;
using MudBlazor;
using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Core.UI.Blazor;

public class ApplicationMessageBoxFrontend(IMainAssembly mainAssembly,
                                           IDialogService dialogService)
    : IApplicationMessageBoxFrontend
{
    private readonly string productName = mainAssembly.ProductName;

    public IApplicationMessageBoxParameter CreateMessageBoxParameter() => new ApplicationMessageBoxParameter(this.productName);

    public Task ShowErrorMessageAsync(string errorMessage) => this.ShowMessageAsync(this.productName, errorMessage, ApplicationMessageBoxMessageMode.Error);
    public Task ShowErrorMessageAsync(string title, string errorMessage) => this.ShowMessageAsync(title, errorMessage, ApplicationMessageBoxMessageMode.Error);
    public Task ShowMessageAsync(string message, ApplicationMessageBoxMessageMode messageMode) => this.ShowMessageAsync(this.productName, message, messageMode);
    public Task ShowMessageAsync(string title, string message, ApplicationMessageBoxMessageMode messageMode)
    {
        var messageBoxParameter = this.CreateMessageBoxParameter();

        messageBoxParameter.Title = title;
        messageBoxParameter.Prompt = message;
        messageBoxParameter.Buttons = ApplicationMessageBoxButtons.Ok;
        messageBoxParameter.Symbol = messageMode switch
        {
            ApplicationMessageBoxMessageMode.Warning => ApplicationMessageBoxSymbol.Exclamation,
            ApplicationMessageBoxMessageMode.Error => ApplicationMessageBoxSymbol.Stop,
            ApplicationMessageBoxMessageMode.Information => ApplicationMessageBoxSymbol.Information,
            _ => throw new InvalidEnumArgumentException(messageMode.ToString()),
        };

        return this.ShowCustomMessageAsync(messageBoxParameter);
    }

    public async Task<ApplicationMessageBoxButtons> ShowCustomMessageAsync(IApplicationMessageBoxParameter parameter)
    {
        var parameters = new DialogParameters { [nameof(parameter)] = parameter };
        var options = new DialogOptions
        {
            CloseButton = true,
            BackdropClick = false
        };
        var dialog = await dialogService.ShowAsync<ApplicationMessageBox>(parameter.Title, parameters, options);
        var dialogResult = await dialog.Result;

        return dialogResult == null
            ? ApplicationMessageBoxButtons.Cancel
            : dialogResult.Canceled
                ? ApplicationMessageBoxButtons.Cancel
                : (ApplicationMessageBoxButtons)dialogResult.Data!;
    }
}
