using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using battle_GUI.ViewModels;
using battle_GUI.Views;
using System.Collections.Generic;

namespace battle_GUI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainView = new MainView();
            List <object> RadioButtons = mainView.CreateElementsList();
            desktop.MainWindow = new MainWindow
            {
                
                DataContext = new MainViewModel(mainView.FindControl<Grid>("MainGrid"), RadioButtons, mainView.FindControl<TextBlock>("Errors")) // Передаем Grid
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            var mainView = new MainView();
            List <object> RadioButtons = mainView.CreateElementsList();
            singleViewPlatform.MainView = mainView;
            mainView.DataContext = new MainViewModel(mainView.FindControl<Grid>("MainGrid"), RadioButtons, mainView.FindControl<TextBlock>("Errors")); // Передаем Grid
        }

        base.OnFrameworkInitializationCompleted();
    }


}
