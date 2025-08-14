namespace ShoppingList2025.Core.Types;

public class ApplicationMediaPickerOptions
{
    public string Title { get; set; } = string.Empty;
}
public interface IApplicationMediaPicker
{
    bool IsCaptureSupported { get; }

    Task<string?> PickPhotoAsync(ApplicationMediaPickerOptions? options = null);

    Task<string?> CapturePhotoAsync(ApplicationMediaPickerOptions? options = null);

    Task<string?> PickVideoAsync(ApplicationMediaPickerOptions? options = null);

    Task<string?> CaptureVideoAsync(ApplicationMediaPickerOptions? options = null);
}
