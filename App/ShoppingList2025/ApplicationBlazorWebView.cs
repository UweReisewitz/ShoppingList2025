using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.FileProviders;

namespace ShoppingList2025;

//https://stackoverflow.com/questions/72513093/how-to-display-local-image-as-well-as-resources-image-in-net-maui-blazor
public class ApplicationBlazorWebView : BlazorWebView
{
    public override IFileProvider CreateFileProvider(string contentRootDir)
    {
        var physicalFilesFolder = new PhysicalFileProvider("/");
        return new CompositeFileProvider(physicalFilesFolder, base.CreateFileProvider(contentRootDir));
    }
}
