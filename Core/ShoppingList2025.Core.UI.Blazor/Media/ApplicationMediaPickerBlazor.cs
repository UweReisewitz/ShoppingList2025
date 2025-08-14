using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Core.UI.Blazor;
public class ApplicationMediaPickerBlazor(IMainAssembly mainAssembly,
                                          IApplicationMessageBoxFrontend messageBoxFrontend)
    : IApplicationMediaPicker
{
    public bool IsCaptureSupported => false;

    public async Task<string?> CapturePhotoAsync(ApplicationMediaPickerOptions? options = null)
    {
        await messageBoxFrontend.ShowErrorMessageAsync(mainAssembly.ProductName, "Es können keine Fotos aufgenommen werden");
        return null;
    }

    public async Task<string?> CaptureVideoAsync(ApplicationMediaPickerOptions? options = null)
    {
        await messageBoxFrontend.ShowErrorMessageAsync(mainAssembly.ProductName, "Es können keine Videos aufgenommen werden");
        return null;
    }

    public async Task<string?> PickPhotoAsync(ApplicationMediaPickerOptions? options = null)
    {
        await messageBoxFrontend.ShowErrorMessageAsync(mainAssembly.ProductName, "Es können keine Fotos ausgewählt werden");
        return null;
    }

    public async Task<string?> PickVideoAsync(ApplicationMediaPickerOptions? options = null)
    {
        await messageBoxFrontend.ShowErrorMessageAsync(mainAssembly.ProductName, "Es können keine Videos ausgewählt werden");
        return null;
    }
}
