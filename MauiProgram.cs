namespace Calculator;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        string dbPath = FileAccessHelpers.GetLocalFilePath("CalculationHistory.db3");
        builder.Services.AddSingleton<HistoryVM>(s => ActivatorUtilities.CreateInstance<HistoryVM>(s, dbPath));


        return builder.Build();
	}
}
