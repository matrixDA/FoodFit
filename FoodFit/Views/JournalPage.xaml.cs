using System.Collections.ObjectModel;

namespace FoodFit.Views;

public partial class JournalPage : ContentPage
{
    public ObservableCollection<string> JournalEntries { get; set; } = new ObservableCollection<string>();

    public JournalPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void OnSaveEntryClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(EntryEditor.Text))
        {
            JournalEntries.Insert(0, EntryEditor.Text);
            EntryEditor.Text = string.Empty; 
        }
    }
}
