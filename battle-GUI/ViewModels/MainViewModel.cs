using BoH.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using BoH.Interfaces;
using System.Collections.Generic;
using System;
using BoH.Services;
using System.Reactive;
using Avalonia.Controls;
using BoH.GameLogic;
using DynamicData;
using Avalonia.Interactivity;
using Avalonia.Media;
using HarfBuzzSharp;
using battle_GUI.Views;
using System.Windows.Input;
using Avalonia.Rendering;
using Avalonia.Input;

namespace battle_GUI.ViewModels;

public class MainViewModel : ViewModelBase
{
    // доска
    private GameBoard _gameBoard { get; set; }
    private List<object> _RadioButtonsAction { get; set; }
    private List<object> _RadioButtonsAbility { get; set; }

    // листы юнитов и игроков
    private List<IUnit> _unitList { get; set; }
    private Player[] players { get; set; }
    private int playnow;

    // что-то сервисное
    private GameBoardService gameBoardService { get; set; }
    private GameController gameController { get; set; }
    ActionHandler actionHandler {  get; set; }

    ScannerHandler scannerHandler { get; set; }
    List<ICell> scannedCells { get; set; }
    TurnManager turnManager {  get; set; }

    // сетка игрового поля
    public Grid MainGrid { get; private set; }
    public List<object> RadioButtons {  get; set; }
    public TextBlock TextErrors { get; set; }
    public TextBlock Info { get; set; }

    // словарь цветов юнитов и перекрасок
    Dictionary<string, Color>? colorMapping { get; set; }
    Dictionary<string, Color>? scannerMapping { get; set; }

    public MainViewModel(Grid mainGrid, List<object> _RadioButtons, TextBlock _TextErrors, TextBlock _Info)
    {
        // создаем доску
        _gameBoard = new GameBoard(8, 8);

        // создаем лист игроков
        players = new Player[]
        {
            new Player("Rus"),
            new Player("Lizard")
        };

        _unitList = new List<IUnit>
        {
            new RusArcher(),
            new LizardArcher(),
            new RusWarrior(),
            new LizardWarrior(),
            new RusArcher(),
            new LizardArcher(),
            new RusWarrior(),
            new LizardWarrior()
        };

        // какие-то системные штуки, РАЗОБРАТЬСЯ
        gameBoardService = new GameBoardService();
        gameController = new GameController(gameBoardService);

        // заполняем сетку игрового поля кнопками
        MainGrid = mainGrid;
        RadioButtons = _RadioButtons;
        TextErrors = _TextErrors;
        Info = _Info;

        playnow = 0;
        SetGrid();

        // добавляем словарь цветов
        UnitColors unitColors = new UnitColors();
        colorMapping = UnitColors.UnitColorMapping;

        // добавляем словарь перекраски для сканера
        scannerMapping = UnitColors.ScannerColorMapping;
        Renderer_ViewModels renderer = new Renderer_ViewModels();

        MainGrid.IsEnabled = false;
    }


    // кнопка начала игры
    public void StartGame_Click(object sender, RoutedEventArgs e)
    {
        MainGrid.IsEnabled = true;

        RB_ViewModel RB = new RB_ViewModel();
        RB.RadioVisible(RadioButtons, false);
        Renderer_ViewModels renderer = new Renderer_ViewModels();

        // Здесь мы ЗАНОВО создаем доску и игроков тк юниты добавляют к игракам СВЕРХУ
        _gameBoard = new GameBoard(8, 8);
        players = new Player[]
        {
            new Player("Rus"),
            new Player("Lizard")
        };
        
        _unitList = new List<IUnit>
        {
            new RusArcher(),
            new LizardArcher(),
            new RusWarrior(),
            new LizardWarrior(),
            new RusArcher(),
            new LizardArcher(),
            new RusWarrior(),
            new LizardWarrior()
        };

        _gameBoard = (GameBoard)gameBoardService.GenerateGameBoard(8, 8, _unitList, players);

        ActionHandler actionHandler = new(_gameBoard);
        ScannerHandler scannerHandler = new(_gameBoard);

        turnManager = new TurnManager(_gameBoard, players, actionHandler, scannerHandler);
        // заполняет сетку цветами и иконками в зависимости от содержания
        renderer.Renderer(_gameBoard, MainGrid, colorMapping);
        turnManager.StartNewRound(players[0]);
    }

    // заполняем сетку кнопками
    public void SetGrid()
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

        // двигаемся по сетке и в каждую ячейку добавляем по кнопке
        for (int y = 0; y < _gameBoard.Height; y++)
        {
            for (int x = 0; x < _gameBoard.Width; x++)
            {
                if (_gameBoard[x, y] is not null)
                {
                    var button = new Button
                    {
                        Width = 62,
                        Height = 62,
                        Tag = (X: x, Y: y)
                    };

                    if (button != null && Button_Click != null && Button_PointerEnter != null)
                    {
                        button.Click += Button_Click;
                        button.PointerEntered += Button_PointerEnter;
                    }
                    Grid.SetColumn(button, x);
                    Grid.SetRow(button, y);
                    MainGrid.Children.Add(button);
                }

            }
        }
    }

    public void Button_PointerEnter(object sender, PointerEventArgs e)
    {
        var button = sender as Button;
        int X = -1;
        int Y = -1;
        if (button != null)
        {
            var coordinates = button.Tag;
            if (coordinates is (int x, int y))
            {
                X = x; Y = y;
            }

            // Логика для получения информации о клетке
            var cellInfo = GetCellInfo(X, Y);
            DisplayInfo(cellInfo); // Метод для отображения информации
        }
    }

    public void DisplayInfo(string textInfo)
    {
        Info.Text = textInfo;
    }


    private string GetCellInfo(int x, int y)
    {
        string st = "";

        if (_gameBoard != null && _gameBoard[x, y] != null && _gameBoard[x, y] is Cell cell)
        {
            if (cell.Content is Obstacle)
            {
                st = "Obstacle!";
            }

            else if (cell.Content is IUnit unit)
            {
                st = $"Team: {unit.Team}\nName: {unit.UnitName}\n" +
                    $"Hp: {unit.Hp}\nDefense: {unit.Defence}\n";

                if (unit.IsDead == true)
                {
                    st += "Unit is dead!";
                }
            }
        }

        return st;
    }


    
    public void Button_Click(object sender, RoutedEventArgs e)
    {
        TextErrors.Text = " ";

        // Логика обработки нажатия кнопки
        var button = sender as Button;
        if (button != null)
        {
        RoundViewModel progress = new RoundViewModel();
        int X = -1;
        int Y = -1;
        var coordinates = button.Tag;
        if (coordinates is (int x, int y))
        {
            X = x; Y = y;
        }
            if (RadioButtons[0] is TextBlock text)
            {
                if (!text.IsVisible)
                {
                    //тут функция которая при первом нажатии
                    progress.TheFirstChoice(button, turnManager, _gameBoard[X, Y], TextErrors, RadioButtons);

                }

                else
                {
                    //тут функция для второго нажатия
                    progress.TheSecondChoice(RadioButtons, TextErrors, turnManager, gameController, players, playnow, _gameBoard[X, Y], MainGrid);
                    Renderer_ViewModels Renders = new Renderer_ViewModels();
                    Renders.Renderer(_gameBoard, MainGrid, colorMapping);
                }
            }
        }

    }



    public void ScannerVisible(object sender, RoutedEventArgs e)
    {
        Renderer_ViewModels Renders = new Renderer_ViewModels();
        
        var radioButton = sender as RadioButton;
        if (radioButton != null)
        {
            switch (radioButton.Name)
            {
                case "Move":
                    {
                        Renders.Renderer(_gameBoard, MainGrid, colorMapping);
                        List<ICell> scannedCells = turnManager.ProcessScanner(ActionType.Move);
                        Renders.ScanRenderer(_gameBoard, MainGrid, scannedCells, scannerMapping);
                        break;
                    }
                case "Attack":
                    {
                        Renders.Renderer(_gameBoard, MainGrid, colorMapping);
                        List<ICell> scannedCells = turnManager.ProcessScanner(ActionType.Attack);
                        Renders.ScanRenderer(_gameBoard, MainGrid, scannedCells, scannerMapping);
                        break;
                    }
                case "Ability":
                    {
                        Renders.Renderer(_gameBoard, MainGrid, colorMapping);
                        List<ICell> scannedCells = turnManager.ProcessScanner(ActionType.Ability);
                        Renders.ScanRenderer(_gameBoard, MainGrid, scannedCells, scannerMapping);
                        break;
                    }
                case "None":
                case "Skip":
                case "End":
                    {
                        Renders.Renderer(_gameBoard, MainGrid, colorMapping);
                        break;
                    }
            }
        }
    }
} 



