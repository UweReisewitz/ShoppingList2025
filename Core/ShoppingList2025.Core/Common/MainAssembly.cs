using System.Reflection;
using ShoppingList2025.Core.Types;

namespace ShoppingList2025.Core;

public class MainAssembly : IMainAssembly
{
    private bool arePropertiesInitialized;

    public MainAssembly()
    {
        this.EntryAssembly = Assembly.GetEntryAssembly();
    }

    private Assembly? entryAssembly;
    public Assembly? EntryAssembly
    {
        get => this.entryAssembly;
        set
        {
            this.entryAssembly = value;
            if (this.entryAssembly != null)
            {
                this.InitializeProperties();
            }
        }
    }

    private void InitializeProperties()
    {
        if (this.EntryAssembly != null)
        {
            this.ProductVersion = this.GetAssemblyAttribute<AssemblyInformationalVersionAttribute>(a => a.InformationalVersion);

            this.FileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.EntryAssembly.ManifestModule.FullyQualifiedName);
            this.FileName = Path.GetFileName(this.EntryAssembly.ManifestModule.FullyQualifiedName);
            this.StartupPath = Path.GetDirectoryName(this.EntryAssembly.ManifestModule.FullyQualifiedName);

            this.FileVersion = this.GetAssemblyAttribute<AssemblyFileVersionAttribute>(a => a.Version);
            this.Company = this.GetAssemblyAttribute<AssemblyCompanyAttribute>(a => a.Company);
            this.ProductName = this.GetAssemblyAttribute<AssemblyProductAttribute>(a => a.Product).Trim();
            this.Copyright = this.GetAssemblyAttribute<AssemblyCopyrightAttribute>(a => a.Copyright);
            this.Trademark = this.GetAssemblyAttribute<AssemblyTrademarkAttribute>(a => a.Trademark);
            this.Title = this.GetAssemblyAttribute<AssemblyTitleAttribute>(a => a.Title);
            this.Description = this.GetAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description);
            this.Configuration = this.GetAssemblyAttribute<AssemblyDescriptionAttribute>(a => a.Description);

            this.arePropertiesInitialized = true;
        }
        else
        {
            this.arePropertiesInitialized = false;
        }
    }

    /// <summary>
    /// Liefert die nach formatierte Dateiversion des Executables zurück.
    /// </summary>
    /// <returns></returns>

    private string productVersion = string.Empty;
    public string ProductVersion
    {
        get => this.arePropertiesInitialized ? this.productVersion : throw new NotInitializedException();
        set => this.productVersion = value;
    }

    private string fileNameWithoutExtension = string.Empty;
    public string FileNameWithoutExtension
    {
        get => this.arePropertiesInitialized ? this.fileNameWithoutExtension : throw new NotInitializedException();
        set => this.fileNameWithoutExtension = value;
    }

    private string fileName = string.Empty;
    public string FileName
    {
        get => this.arePropertiesInitialized ? this.fileName : throw new NotInitializedException();
        set => this.fileName = value;
    }

    private string? startupPath = string.Empty;
    public string? StartupPath
    {
        get => this.arePropertiesInitialized ? this.startupPath : throw new NotInitializedException();
        set => this.startupPath = value;
    }

    private string company = string.Empty;
    public string Company
    {
        get => this.arePropertiesInitialized ? this.company : throw new NotInitializedException();
        set => this.company = value;
    }

    private string productName = string.Empty;
    public string ProductName
    {
        get => this.arePropertiesInitialized ? this.productName : throw new NotInitializedException();
        set => this.productName = value;
    }
    private string copyright = string.Empty;
    public string Copyright
    {
        get => this.arePropertiesInitialized ? this.copyright : throw new NotInitializedException();
        set => this.copyright = value;
    }

    private string trademark = string.Empty;
    public string Trademark
    {
        get => this.arePropertiesInitialized ? this.trademark : throw new NotInitializedException();
        set => this.trademark = value;
    }
    private string title = string.Empty;
    public string Title
    {
        get => this.arePropertiesInitialized ? this.title : throw new NotInitializedException();
        set => this.title = value;
    }

    private string description = string.Empty;
    public string Description
    {
        get => this.arePropertiesInitialized ? this.description : throw new NotInitializedException();
        set => this.description = value;
    }

    private string configuration = string.Empty;
    public string Configuration
    {
        get => this.arePropertiesInitialized ? this.configuration : throw new NotInitializedException();
        set => this.configuration = value;
    }

    private string fileVersion = string.Empty;
    public string FileVersion
    {
        get => this.arePropertiesInitialized ? this.fileVersion : throw new NotInitializedException();
        set => this.fileVersion = value;
    }

    private string GetAssemblyAttribute<T>(Func<T, string> value) where T : Attribute
    {
        if (this.EntryAssembly == null)
        {
            return string.Empty;
        }
        var attribute = CustomAttributeExtensions.GetCustomAttribute<T>(this.EntryAssembly);
        return attribute == null
            ? string.Empty
            : value.Invoke(attribute);
    }
}
