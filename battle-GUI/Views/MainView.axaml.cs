using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using battle_GUI.ViewModels;
using BoH.GameLogic;
using BoH.Interfaces;
using BoH.Models;
using BoH.Services;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;


namespace battle_GUI.Views;

public partial class MainView : UserControl
{

    public MainView()
    {
        InitializeComponent();
        var mainGrid = this.FindControl<Grid>("MainGrid");
        var TextErrors = this.FindControl<TextBlock>("Errors");
        List<object> RadioButtons = CreateElementsList();

        if (mainGrid != null && RadioButtons != null && TextErrors != null)
        {
            DataContext = new MainViewModel(mainGrid, RadioButtons, TextErrors); // Установка DataContext 
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }


    private void StartGame_Click(object sender, RoutedEventArgs e)
    {
        // Получите доступ к ViewModel и вызовите метод
        if (DataContext != null)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.StartGame_Click(sender, e);
        }
    }

    public void ScannerVisible(object sender, RoutedEventArgs e)
    {
        // Получите доступ к ViewModel и вызовите метод
        if (DataContext != null)
        {
            var viewModel = (MainViewModel)DataContext;
            viewModel.ScannerVisible(sender, e);
        }
    }

    public List <object> CreateElementsList()
    {
        List<object> elements = new List<object>();

        var StackPanel = this.FindControl<StackPanel>("Objects");

        if (StackPanel != null)
        {

            foreach (var child in StackPanel.Children)
            {
                elements.Add(child);


            }
        }
        return elements;
    }


}
    


