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

namespace battle_GUI.ViewModels;

public class MainViewModel : ViewModelBase
{
    // доска
    private GameBoard _gameBoard { get; set; }

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

    // словарь цветов юнитов
    Dictionary<string, Color>? colorMapping { get; set; }


    public MainViewModel(Grid mainGrid)
    {
        // создаем доску
        _gameBoard = new GameBoard(8, 8);

        // создаем лист юнитов
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

        // заполняем сетку игрового поля кнопками
        MainGrid = mainGrid;
        SetGrid();

        // какие-то системные штуки, РАЗОБРАТЬСЯ
        gameBoardService = new GameBoardService();
        gameController = new GameController(gameBoardService);
        actionHandler = new(_gameBoard);
        scannerHandler = new(_gameBoard);
        scannedCells = new();
        turnManager = new TurnManager(_gameBoard, players, actionHandler, scannerHandler);

        // добавляем словарь цветов
        UnitColors unitColors = new UnitColors();
        colorMapping = UnitColors.UnitColorMapping;
    }

    // НЕ РАБОТАЕТ
    //// кнопка начала игры
    //public void StartGame_Click(object sender, RoutedEventArgs e)
    //{
    //    // генерируем игровую доску
    //    _gameBoard = new GameBoard(8, 8);
    //    _gameBoard = (GameBoard)gameBoardService.GenerateGameBoard(8, 8, _unitList, players);

    //    // очищаем доску
    //    Cleaner();
    //    SetGrid();

    //    // заполняет сетку цветами и иконками в зависимости от содержания
    //    Renderer();
    //}

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
                        Tag = (x, y)
                    };
                    button.Click += Button_Click;
                    Grid.SetColumn(button, x);
                    Grid.SetRow(button, y);
                    MainGrid.Children.Add(button);
                }

            }
        }
    }

    // при начале игры сначала очищает сетку от прошлой окраски
    public void Cleaner()
    {
        // Убедимся, что мы очищаем все кнопки в MainGrid
        foreach (var child in MainGrid.Children)
        {
            if (child is Button button)
            {
                button.Content = " "; // Очищаем текст кнопки
                button.Background = new SolidColorBrush(Colors.LightGray); // Устанавливаем светло-серый фон
            }
        }
    }

    public void Renderer()
    {
        int size = _gameBoard.Width;
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if (_gameBoard is not null && _gameBoard[x, y] is Cell cell && cell is not null)
                {
                    var button = MainGrid.Children[y * 8 + x] as Button;

                    if (button != null)
                    {
                        button.Content = _gameBoard?.Cells[x, y]?.Content?.Icon;

                        if (cell.Content is null || colorMapping is null)
                        {
                            button.Background = new SolidColorBrush(Colors.LightGray);
                        }

                        else
                        {
                            // проверка на нахождение в словаре IIconHolder
                            if (colorMapping.ContainsKey(cell.Content.Icon))
                            {
                                button.Background = new SolidColorBrush(colorMapping[cell.Content.Icon]);
                            }
                        }
                    }

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
        if (button != null)
        {
            // Например, можно изменить текст кнопки
            button.Content = "Clicked!";
        }
    }


} 



