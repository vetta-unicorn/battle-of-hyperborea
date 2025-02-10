using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using battle_GUI.ViewModels;
using BoH.GameLogic;
using BoH.Interfaces;
using BoH.Models;
using BoH.Services;
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
        DataContext = new MainViewModel(mainGrid); // Установка DataContext
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    //private void CreateGameBoard(int width, int height)
    //{
    //    _gameBoard = new GameBoard(width, height);
    //}

    //private void PopulateGrid()
    //{
    //    MainGrid.RowDefinitions.Clear();
    //    MainGrid.ColumnDefinitions.Clear();

    //    for (int i = 0; i < _gameBoard.Height; i++)
    //    {
    //        MainGrid.RowDefinitions.Add(new RowDefinition());
    //    }

    //    for (int j = 0; j < _gameBoard.Width; j++)
    //    {
    //        MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
    //    }

        
    //    // Далее добавляем кнопки как было описано ранее...
    //    for (int y = 0; y < _gameBoard.Height; y++)
    //    {
    //        for (int x = 0; x < _gameBoard.Width; x++)
    //        {
    //            var cell = _gameBoard.Cells[x, y];
    //            var button = new Button
    //            {
    //                Content = $"Cell {cell.Position.X}, {cell.Position.Y}",
    //                Tag = cell, // Сохраняем ссылку на объект Cell
    //                Width = 62, // Установите желаемую ширину
    //                Height = 62 // Установите желаемую высоту
    //            };
    //            //button.Click += Button_Click;

    //            Grid.SetColumn(button, x);
    //            Grid.SetRow(button, y);
    //            MainGrid.Children.Add(button);
    //        }
    //    }
    //}

    
    //private void Button_Click(object sender, RoutedEventArgs e)
    //{

    //    var button = sender as Button;
    //    var cell = button.Tag as Cell;

    //    //тут координаты надо посмотреть CLI

    //    if (cell != null)
    //    {


    //        int index = ActionsFlag();


    //        if (this.FindControl<TextBlock>("ActionText").IsVisible == false)
    //        {
                
    //            button.Background = new SolidColorBrush(Colors.Green);

    //            try { SelectUnit(ICell unitCell)}
    //            catch(InvalidOperationException) 
    //            {
    //                this.FindControl<TextBlock>("Errors").Text="The unit is unavailable for selection.";
    //            }

    //            catch (ArgumentNullException)
    //            {
    //                this.FindControl<TextBlock>("Errors").Text = "There was no unit in the cage.";
    //            }

    //            finally 
    //            {
    //                RadioVisible(true);
    //                //и показываются характеристики персонажа
    //                //и область???
    //                // в turnManager.SelectUnit(gameBoard[0, 0]); передать координаты


    //            }




    //        }
    //        else
    //        {
    //            switch (index)
    //            {
    //                case 0: //бездействие
    //                    {
    //                        break;
    //                    }

    //                case 3:  //Ability
    //                    {

    //                        //тут ну action это (ActionType)index), лист клеток это тот который отсканирован? цель это клетка на коробую нажали? 
    //                        //нужны координаты
    //                        // выбор абилити доделыается
    //                        turnManager.ProcessPlayerAction((ActionType)index), List<ICell> ? availableCells, object ? target , IAbility ? usedAbility )
    //                        break;
    //                    }

    //                case 5: //end of the round
    //                    {

    //                        if (gameController.CheckVictoryCondition(players))
    //                        {
    //                            //тут либо надо что то сделать...мб удалить доску?
    //                        }
    //                        else
    //                        {
    //                            playnow = (playnow + 1) % 2;
    //                            turnManager.StartNewRound(players[playnow]);
    //                        }
                            
    //                        break;
    //                    }

    //                default: //go and attack
    //                    {
    //                        //(выше конкретней)
    //                        turnManager.ProcessPlayerAction((ActionType)index), List<ICell> ? availableCells, object ? target );
    //                        break;
    //                    }


                    
    //                    RadioVisible(false);

    //            }

    //        }

    //        //тут обновление поля  
    //    }
    //}



    //private int ActionsFlag()
    //{
    //    var actions = new Dictionary<string, int>
    // {
    //    { "None", 0 },
    //    { "Go", 1 },
    //    { "Attack", 2 },
    //    { "Ability", 3 },
    //    { "Skip", 4 },
    //    { "End", 5 }
    // };

    //    foreach (var action in actions)
    //    {
                        
    //      return action.Value;
            
    //    }
    //    return 0;

    //}

    //private void RadioVisible(bool isVisible)
    //{
    //    var Text = this.FindControl<TextBlock>("ActionText");
    //    Text.IsVisible = isVisible;
    //    var None = this.FindControl<RadioButton>("None");
    //    None.IsVisible = isVisible;
    //    var Go = this.FindControl<RadioButton>("Go");
    //    Go.IsVisible = isVisible;
    //    var Attack = this.FindControl<RadioButton>("Attack");
    //    Attack.IsVisible = isVisible;
    //    var Ability = this.FindControl<RadioButton>("Ability");
    //    Ability.IsVisible = isVisible;
    //    var Skip = this.FindControl<RadioButton>("Skip");
    //    Skip.IsVisible = isVisible;
    //    var End = this.FindControl<RadioButton>("End");
    //    End.IsVisible = isVisible;
    //}

    //private async void StartGame_Click(object sender, RoutedEventArgs e)
    //{
    //    var button = sender as Button;
    //    button.Background = new SolidColorBrush(Colors.Pink);
    //    // Сетап игры
    //    // ------------------------------------------------------------------------------------------------------
    //    GameBoardService gameBoardService = new GameBoardService();
    //    GameController gameController = new GameController(gameBoardService);
    //    await Task.Delay(100);
        
    //    List<IUnit> units = new(){
    //        new RusArcher(),
    //        new RusWarrior(),
    //        new LizardArcher(),
    //        new LizardWarrior(),
    //        new RusArcher(),
    //        new RusWarrior(),
    //        new LizardArcher(),
    //        new LizardWarrior(),

    //    };

    //    players[0] = new Player("Rus");
    //    players[1] = new Player("Lizard");
    //    playnow = 0;

    //    GameBoard gameBoard = (GameBoard)gameBoardService.GenerateGameBoard(8, 8, units, players);

    //    ActionHandler actionHandler = new(gameBoard);
    //    ScannerHandler scannerHandler = new(gameBoard);
    //    List<ICell> scannedCells = new();

    //    Render(gameBoard);


       
    //}



     //верка на пустое
    //                if (cell.Content == null)
    //                {
    //                    button.Background = new SolidColorBrush(Colors.LightGray);
    //                    button.Content = " ";

    //                }

    //                // проверка что юнит
    //                else if (cell.Content is IUnit)
    //                {
    //                    // ящеры
    //                    if(cell.Icon == "S")
    //                    {
    //                        button.Background = new SolidColorBrush(Colors.Green);
    //                        button.Content = $"{cell.Icon}";
    //                    }

    //                    else if (cell.Icon == "2")
    //                    {
    //                        button.Background = new SolidColorBrush(Colors.LightGreen);
    //                        button.Content = button.Content = $"{cell.Icon}";
    //                    }

    //                    // русы
    //                    else if(cell.Icon == "R")
    //                    {
    //                        button.Background = new SolidColorBrush(Colors.Pink);
    //                        button.Content = button.Content = $"{cell.Icon}";
    //                    }

    //                    else if (cell.Icon == "Я")
    //                    {
    //                        button.Background = new SolidColorBrush(Colors.LightPink);
    //                        button.Content = button.Content = $"{cell.Icon}";
    //                    }

                        
    //                }
    //                else
    //                    {
    //                        button.Background = new SolidColorBrush(Colors.Black);
    //                        button.Content = button.Content = $"{cell.Icon}";
    //                    }
    //            }
    //        }
    //    }
    //}

}

