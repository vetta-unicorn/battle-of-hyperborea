using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using BoH.Interfaces;
using BoH.Models;
using System;


namespace battle_GUI.Views;

public partial class MainView : UserControl
{
    private GameBoard _gameBoard;

    public MainView()
    {
        InitializeComponent();
        CreateGameBoard(8, 8); // Например, 5x5
        PopulateGrid();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        MainGrid = this.FindControl<Grid>("MainGrid");
    }

    private void CreateGameBoard(int width, int height)
    {
        _gameBoard = new GameBoard(width, height);
    }

    private void PopulateGrid()
    {
        MainGrid.RowDefinitions.Clear();
        MainGrid.ColumnDefinitions.Clear();

        for (int i = 0; i < _gameBoard.Height; i++)
        {
            MainGrid.RowDefinitions.Add(new RowDefinition());
        }

        for (int j = 0; j < _gameBoard.Width; j++)
        {
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        
// Далее добавляем кнопки как было описано ранее...
        for (int x = 0; x < _gameBoard.Width; x++)
        {
            for (int y = 0; y < _gameBoard.Height; y++)
            {
                var cell = _gameBoard.Cells[x, y];
                var button = new Button
                {
                    Content = $"Cell {cell.Position.X}, {cell.Position.Y}",
                    Tag = cell, // Сохраняем ссылку на объект Cell
                    Width = 62, // Установите желаемую ширину
                    Height = 62 // Установите желаемую высоту
                };
                button.Click += Button_Click;
                Grid.SetColumn(button, x);
                Grid.SetRow(button, y);
                MainGrid.Children.Add(button);
            }
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var cell = button.Tag as Cell;

        if (cell != null)
        {
            button.Background = new SolidColorBrush(Colors.Pink);
        }
    }
}

