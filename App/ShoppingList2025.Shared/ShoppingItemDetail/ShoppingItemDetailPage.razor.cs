namespace ShoppingList2025.Shared;

public partial class ShoppingItemDetailPage
{
    private async Task<IEnumerable<string>> GetSuggestedNamesAsync(string value, CancellationToken token)
    {
        await this.ViewModel.UpdateSuggestedNamesAsync(value);
        return this.ViewModel.SuggestedNames;
    }
}
