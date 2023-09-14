using ListViewDemos.ViewModels;

namespace Calculator;

public partial class HistoryPage : ContentPage
{
	public HistoryPage()
	{
		InitializeComponent();
    }
    void clearHistory(object sender, EventArgs e)
    {
        //GlobalStorage.Instance.clearAll();P
        //ListViewContainer.ItemsSource = GlobalStorage.Instance.getOperations();

        removeFromDB();
    }
    async Task removeFromDB()
    {
        SqLite sql = new SqLite();
        await sql.Init();
        await sql.RemoveAll();
        this.BindingContext = new HistoryViewModel();
    }
    void onLoad()
    {
        ListViewContainer.ItemsSource = GlobalStorage.Instance.getOperations();
    }
}