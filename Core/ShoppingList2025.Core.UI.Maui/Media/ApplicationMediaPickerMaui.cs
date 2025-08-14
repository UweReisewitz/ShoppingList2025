using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Core.UI.Maui;
public class ApplicationMediaPickerMaui : IApplicationMediaPicker
{
    public bool IsCaptureSupported => false;

    public async Task<string?> CapturePhotoAsync(ApplicationMediaPickerOptions? options = null)
    {
        var mediaPickerOptions = new MediaPickerOptions() { Title = options?.Title };
        var fileResult = await MediaPicker.CapturePhotoAsync(mediaPickerOptions);
        return fileResult?.FullPath;
    }

    public async Task<string?> CaptureVideoAsync(ApplicationMediaPickerOptions? options = null)
    {
        var mediaPickerOptions = new MediaPickerOptions() { Title = options?.Title };
        var fileResult = await MediaPicker.CaptureVideoAsync(mediaPickerOptions);
        return fileResult?.FullPath;
    }

    public async Task<string?> PickPhotoAsync(ApplicationMediaPickerOptions? options = null)
    {
        var mediaPickerOptions = new MediaPickerOptions() { Title = options?.Title };
        var fileResult = await MediaPicker.PickPhotoAsync(mediaPickerOptions);
        return fileResult?.FullPath;
    }

    public async Task<string?> PickVideoAsync(ApplicationMediaPickerOptions? options = null)
    {
        var mediaPickerOptions = new MediaPickerOptions() { Title = options?.Title };
        var fileResult = await MediaPicker.PickVideoAsync(mediaPickerOptions);
        return fileResult?.FullPath;
    }
}
