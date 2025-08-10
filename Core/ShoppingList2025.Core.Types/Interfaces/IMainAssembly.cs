using System.Reflection;

namespace ShoppingList2025.Core.Types;

public interface IMainAssembly
{
    string Company { get; }
    string Configuration { get; }
    string Copyright { get; }
    string Description { get; }
    Assembly? EntryAssembly { get; set; }
    string FileName { get; }
    string FileNameWithoutExtension { get; }
    string FileVersion { get; }
    string ProductName { get; }
    string ProductVersion { get; }
    string? StartupPath { get; }
    string Title { get; }
    string Trademark { get; }
}
