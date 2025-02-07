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
        CreateGameBoard(8, 8); 
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


    //возможно придется все перенести в функцию
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var cell = button.Tag as Cell;

        //тут координаты надо посмотреть CLI

        if (cell != null)
        {
            

            int i = ActionsFlag();
            

            if (this.FindControl<TextBlock>("ActionText").IsVisible == false)
            {
                RadioVisible(true);
                button.Background = new SolidColorBrush(Colors.Green);

                //тут красится выбранная клетка
                //и показываются характеристики персонажа
                //и область???
                //посмотри AbilityNotification в папке CLI



            }
            else
            {
                switch (i)
                {
                    case 0:
                        {
                            
                            button.Background = new SolidColorBrush(Colors.LightGray);

                            //тут скрытие характеристик
                            RadioVisible(false);

                            break;
                        }
                    case 1000:
                        {
                            throw new Exception("Ты не выбрал кнопку"); //ну или вывод сообщения?!
                        }
                    
                        
                    case 5:
                        {
                            //конец одной из команд, если flag=5 то конец хода.
                            //передача прав другому игроку (додумать, т.к. мдам)
                            //turnManager.EndTurn();
                            //gameController.CheckVictoryCondition(players);
                            //turnManager.StartNewRound(players[1]);

                            RadioVisible(false);
                            break;
                        }
                    
                    default:
                        //тут действие из TurnManager
                        //обновление поля

                        RadioVisible(false);
                        break;





                }

            }

          //тут обновление поля  
        }
    }

   

    private int ActionsFlag () //попробовать улучшить???
    {
        int flag;
        if (this.FindControl<RadioButton>("None").IsChecked == true) flag = 0;
        if (this.FindControl<RadioButton>("Go").IsChecked==true) flag = 1;
        if (this.FindControl<RadioButton>("Attack").IsChecked == true) flag = 2;
        if (this.FindControl<RadioButton>("Ability").IsChecked == true) flag = 3;
        if (this.FindControl<RadioButton>("Skip").IsChecked == true) flag = 4;
        if (this.FindControl<RadioButton>("End").IsChecked == true) flag = 5;
        else flag = 1000;

        return (flag);
    }

    private void RadioVisible(bool isVisible)
    {
        var Text = this.FindControl<TextBlock >("ActionText");
        Text.IsVisible= isVisible;
        var None = this.FindControl<RadioButton>("None");
        None.IsVisible = isVisible;
        var Go = this.FindControl<RadioButton>("Go");
        Go.IsVisible = isVisible;
        var Attack = this.FindControl<RadioButton>("Attack");
        Attack.IsVisible = isVisible;
        var Ability = this.FindControl<RadioButton>("Ability");
        Ability.IsVisible = isVisible;
        var Skip = this.FindControl<RadioButton>("Skip");
        Skip.IsVisible = isVisible;
        var End = this.FindControl<RadioButton>("End");
        End.IsVisible = isVisible;
    }
     
    private void Start_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        

        if (button != null)
        {
            button.Background = new SolidColorBrush(Colors.Pink);
            RadioVisible(true);
        }

        

    }

    
}

