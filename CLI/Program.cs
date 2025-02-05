
namespace BoH.CLI;

using BoH.Interfaces;
using BoH.Services;
using BoH.Models;
using BoH.GameLogic;
using System.Diagnostics;




public class Program
{
    [STAThread]

    /// <summary>
    /// Точка входа в приложение. Отвечает за запуск игры и обработку игровых раундов.
    /// </summary>
    /// <param name="args">Аргументы командной строки (не используются).</param>
    public static async Task Main(string[] args)
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
            new RusArcher(),
            new RusWarrior(),
            new LizardArcher(),
            new LizardWarrior()
        };

        players[0] = new Player("Rus");
        players[1] = new Player("Lizard");

        GameBoard gameBoard = (GameBoard)gameBoardService.GenerateGameBoard(8, 8, units, players);
        ActionHandler actionHandler = new(gameBoard);
        ScannerHandler scannerHandler = new(gameBoard);
        List<ICell> scannedCells = new();

        ConsoleGameBoardRenderer renderer = new ConsoleGameBoardRenderer();
        actionHandler.OnUpdatingGameBoard += renderer.Render;
        scannerHandler.OnGameBoardScanned += renderer.ScanRender;

        ConsoleAbilityNotifications abilityNotifications = new ConsoleAbilityNotifications();
        ConsoleUnitNotifications unitNotifications = new ConsoleUnitNotifications();

        TurnManager turnManager = new TurnManager(gameBoard, players, actionHandler, scannerHandler);

        turnManager.OnUnitSelected += unitNotifications.Notify_UnitSelected;
        // ------------------------------------------------------------------------------------------------------

        // Ход игрока 1
        turnManager.StartNewRound(players[0]);
        turnManager.SelectUnit(gameBoard[0, 0]);
        scannedCells = turnManager.ProcessScanner(ActionType.Move);
        turnManager.ProcessPlayerAction(ActionType.Move, scannedCells, gameBoard[1, 1]);
        turnManager.SelectUnit(gameBoard[1, 1]);
        turnManager.ProcessPlayerAction(ActionType.Skip);

        // ... Проходит ход...

        // Ход игрока 2
        turnManager.EndTurn();
        gameController.CheckVictoryCondition(players);
        turnManager.StartNewRound(players[1]);
        turnManager.SelectUnit(gameBoard[7, 7]);

        // ... Проходит ход...

        // ...
    }


}
