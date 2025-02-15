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
//using System.Drawing;
using Avalonia.Media;
using HarfBuzzSharp;
using battle_GUI.Views;

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

    // что-то сервисное
    private GameBoardService gameBoardService { get; set; }
    private GameController gameController { get; set; }
    ActionHandler actionHandler {  get; set; }

    ScannerHandler scannerHandler { get; set; }
    List<ICell> scannedCells { get; set; }
    TurnManager turnManager {  get; set; }

    // сетка игрового поля
    public Grid MainGrid { get; private set; }
    public List<object> _RadioButtons {  get; set; }

    // словарь цветов юнитов и перекрасок
    Dictionary<string, Color>? colorMapping { get; set; }
    Dictionary<string, Color>? scannerMapping { get; set; }

    TurnManager turnManager { get; set; }


    public MainViewModel(Grid mainGrid, List<object> RadioButtons)
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

        // создаем лист игроков
        players = new Player[]
        {
            new Player("Rus"),
            new Player("Lizard")
        };

        // какие-то системные штуки, РАЗОБРАТЬСЯ
        gameBoardService = new GameBoardService();
        gameController = new GameController(gameBoardService);

        // заполняем сетку игрового поля кнопками
        MainGrid = mainGrid;
        _RadioButtons = RadioButtons;
        SetGrid();

        // генерируем игровую доску
        _gameBoard = (GameBoard)gameBoardService.GenerateGameBoard(8, 8, _unitList, players);
        ActionHandler actionHandler = new(_gameBoard);
        ScannerHandler scannerHandler = new(_gameBoard);
        TurnManager turnManager = new TurnManager(_gameBoard, players, actionHandler, scannerHandler);

        // добавляем словарь цветов
        UnitColors unitColors = new UnitColors();
        colorMapping = UnitColors.UnitColorMapping;

        // добавляем словарь перекраски для сканера
        scannerMapping = UnitColors.ScannerColorMapping;
    }


    // кнопка начала игры
    public void StartGame_Click(object sender, RoutedEventArgs e)
    {
        Renderer_ViewModels renderer = new Renderer_ViewModels();

        // Здесь мы ЗАНОВО создаем доску и игроков тк юниты добавляют к игракам СВЕРХУ
        _gameBoard = new GameBoard(8, 8);
        players = new Player[]
        {
            new Player("Rus"),
            new Player("Lizard")
        };

        _gameBoard = (GameBoard)gameBoardService.GenerateGameBoard(8, 8, _unitList, players);

        // заполняет сетку цветами и иконками в зависимости от содержания
        renderer.Renderer(_gameBoard, MainGrid, colorMapping);
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
                    button.Click += Button_Click;
                    Grid.SetColumn(button, x);
                    Grid.SetRow(button, y);
                    MainGrid.Children.Add(button);
                }

            }
        }
    }


    // НАПИСАТЬ NOTIFICATION ACTION / UNIT ДЛЯ GUI

    public void Scanner()
    {

    }

    // тестовая функция нажатия кнопки
    public void Button_Click(object sender, RoutedEventArgs e)
    {
        // Логика обработки нажатия кнопки
        var button = sender as Button;

        //var coordinates = (ValueTuple<int, int>)button.Tag;
        

        if (button != null)
        {
            // Например, можно изменить текст кнопки
            button.Content = "Clicked!";
        }
    }


} 



