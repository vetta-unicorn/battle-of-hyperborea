using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using BoH.GameLogic;
using BoH.Interfaces;
using BoH.Models;
using BoH.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;


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
        for (int y = 0; y < _gameBoard.Height; y++)
        {
            for (int x = 0; x < _gameBoard.Width; x++)
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

    //private void Button_Click(object sender, RoutedEventArgs e)
    //{
    //    var button = sender as Button;
    //    var cell = button.Tag as Cell;

    //    if (cell != null)
    //    {
    //        button.Background = new SolidColorBrush(Colors.Pink);
    //    }

    //    // сюда сделаем передачу координат как-нибудь

    //}

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

                            button.Background = new SolidColorBrush(Colors.Gray);

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



    private int ActionsFlag() //попробовать улучшить???
    {
        int flag;
        if (this.FindControl<RadioButton>("None").IsChecked == true) flag = 0;
        if (this.FindControl<RadioButton>("Go").IsChecked == true) flag = 1;
        if (this.FindControl<RadioButton>("Attack").IsChecked == true) flag = 2;
        if (this.FindControl<RadioButton>("Ability").IsChecked == true) flag = 3;
        if (this.FindControl<RadioButton>("Skip").IsChecked == true) flag = 4;
        if (this.FindControl<RadioButton>("End").IsChecked == true) flag = 5;
        else flag = 1000;

        return (flag);
    }

    private void RadioVisible(bool isVisible)
    {
        var Text = this.FindControl<TextBlock>("ActionText");
        Text.IsVisible = isVisible;
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

    private async void StartGame_Click(object sender, RoutedEventArgs e)
    {
        // Сетап игры
        // ------------------------------------------------------------------------------------------------------
        GameBoardService gameBoardService = new GameBoardService();
        GameController gameController = new GameController(gameBoardService);
        await Task.Delay(100);
        Player[] players = new Player[2];
        List<IUnit> units = new(){
            new RusArcher(),
            new RusWarrior(),
            new LizardArcher(),
            new LizardWarrior(),
            new RusArcher(),
            new RusWarrior(),
            new LizardArcher(),
            new LizardWarrior(),

        };

        players[0] = new Player("Rus");
        players[1] = new Player("Lizard");

        GameBoard gameBoard = (GameBoard)gameBoardService.GenerateGameBoard(8, 8, units, players);

        ActionHandler actionHandler = new(gameBoard);
        ScannerHandler scannerHandler = new(gameBoard);
        List<ICell> scannedCells = new();

        Render(gameBoard);
    }

    // графический рендер
    public void Render(IGameBoard gameBoard)
    {
        int size = gameBoard.Width;
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if (gameBoard[x, y] is Cell cell)
                {
                    var button = MainGrid.Children[y * 8 + x] as Button;
                    // проверка на пустое
                    if (cell.Content == null)
                    {
                        button.Background = new SolidColorBrush(Colors.LightGray);
                        button.Content = "";

                    }

                    // проверка что юнит
                    else if (cell.Content is IUnit)
                    {
                        // ящеры
                        if(cell.Icon == "S")
                        {
                            button.Background = new SolidColorBrush(Colors.Green);
                            button.Content = $"{cell.Icon}";
                        }

                        else if (cell.Icon == "2")
                        {
                            button.Background = new SolidColorBrush(Colors.LightGreen);
                            button.Content = button.Content = $"{cell.Icon}";
                        }

                        // русы
                        else if(cell.Icon == "R")
                        {
                            button.Background = new SolidColorBrush(Colors.Pink);
                            button.Content = button.Content = $"{cell.Icon}";
                        }

                        else if (cell.Icon == "Я")
                        {
                            button.Background = new SolidColorBrush(Colors.LightPink);
                            button.Content = button.Content = $"{cell.Icon}";
                        }
                    }
                }
            }
        }
    }

    //public void ScanRender(IGameBoard gameBoard, List<ICell> scannedCells)
    //{
    //    int size = gameBoard.Width;
    //    PrintHorizontalBorder(size);
    //    for (int y = 0; y < size; y++)
    //    {
    //        for (int x = 0; x < gameBoard.Height; x++)
    //        {
    //            if (gameBoard[x, y] is Cell cell)
    //            {
    //                if (scannedCells.Contains(cell))
    //                {
    //                    // Выводим специальный символ, если клетка отсканирована: "!" для юнита, "#" для других объектов
    //                    if (cell.Content is IUnit)
    //                        Console.Write("| ! ");
    //                    else
    //                        Console.Write("| # ");
    //                }
    //                else
    //                {
    //                    Console.Write($"| {cell.Icon} ");
    //                }
    //            }
    //        }
    //        Console.WriteLine("|");
    //        PrintHorizontalBorder(size);
    //    }
    //}
}

