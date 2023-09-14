using System.Linq.Expressions;

namespace Calculator;

public partial class History : ContentPage
{
	List<HistoryCalculations> historyCalculations = new List<HistoryCalculations>();

    public History()
	{
		InitializeComponent();
	}
    private async void Button_Clicked(object sender, EventArgs e)
    {
        int result = await App.HistoryVM.ClearHistory();
        CalculationsList.ItemsSource = new List<HistoryCalculations>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        historyCalculations = await App.HistoryVM.GetHistoryItems();
        CalculationsList.ItemsSource = historyCalculations;

    }

}
