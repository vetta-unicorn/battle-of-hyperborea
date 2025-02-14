namespace BoH.Services;

using BoH.Interfaces;
using BoH.Models;
using System.Threading.Tasks;

/// <summary>
/// Сервис для управления игровым полем.
/// </summary>
public class GameBoardService : IGameBoardService
{
    /// <inheritdoc/>
    public IGameBoard GenerateGameBoard(int width, int height, List<IUnit> units, IPlayer[] players)
    {
        // Валидация входных параметров
        if (players == null || players.Length != 2)
            throw new ArgumentException("Требуется ровно два игрока", nameof(players));

        if (units == null)
            throw new ArgumentNullException(nameof(units));

        // Проверка уникальных команд
        var uniqueTeams = units.Select(u => u.Team).Distinct().ToList();
        if (uniqueTeams.Count != 2)
            throw new ArgumentException("Должно быть ровно две уникальные команды", nameof(units));

        var gameBoard = new GameBoard(width, height);
        var rnd = new Random();

        // Распределение юнитов по игрокам
        foreach (var unit in units)
        {
            if (unit.Team == uniqueTeams[0])
                players[0].Units.Add(unit);
            else if (unit.Team == uniqueTeams[1])
                players[1].Units.Add(unit);
            else
                throw new InvalidOperationException($"Юнит принадлежит неизвестной команде: {unit.Team}");
        }

        // Проверка вместимости поля
        int maxUnitsPerRow = Math.Max(
            players[0].Units.Count,
            players[1].Units.Count
        );

        if (maxUnitsPerRow > width || height < 2)
            throw new InvalidOperationException(
                $"Поле {width}x{height} слишком мало для размещения юнитов");

        if (units.Count == 8)
        {
            // Размещение юнитов первого игрока (верхняя строка)
            int column = 0;
            foreach (var unit in players[0].Units)
            {
                if (column >= width) break;
                var cell = gameBoard[column, 0];
                AddObjectToGameBoard(unit as IIconHolder, cell);
                cell.UpdateIcon();
                column+=2;
            }

            // Размещение юнитов второго игрока (нижняя строка)
            column = width - 1;
            foreach (var unit in players[1].Units)
            {
                if (column < 0) break;
                var cell = gameBoard[column, height - 1];
                AddObjectToGameBoard(unit as IIconHolder, cell);
                cell.UpdateIcon();
                column-=2;
            }
        }

        else
        {
            // Размещение юнитов первого игрока (верхняя строка)
            int column = 0;
            foreach (var unit in players[0].Units)
            {
                if (column >= width) break;
                var cell = gameBoard[column, 0];
                AddObjectToGameBoard(unit as IIconHolder, cell);
                cell.UpdateIcon();
                column++;
            }

            // Размещение юнитов второго игрока (нижняя строка)
            column = width - 1;
            foreach (var unit in players[1].Units)
            {
                if (column < 0) break;
                var cell = gameBoard[column, height - 1];
                AddObjectToGameBoard(unit as IIconHolder, cell);
                cell.UpdateIcon();
                column--;
            }
        }

        // Генерация препятствий
        var availableCells = new List<ICell>();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (!gameBoard[x, y].IsOccupied())
                    availableCells.Add(gameBoard[x, y]);
            }
        }

        int obstacleCount = rnd.Next(0, Math.Min(width, availableCells.Count));
        for (int i = 0; i < obstacleCount; i++)
        {
            var index = rnd.Next(availableCells.Count);
            var cell = availableCells[index];
            cell.Content = new Obstacle(); 
            cell.UpdateIcon();
            availableCells.RemoveAt(index);
        }

        return gameBoard;
    }

    /// <inheritdoc/>
    public void AddObjectToGameBoard(IIconHolder? obj, ICell cell)
    {
        if (obj == null) return;
        cell.Content = obj;
    }

    /// <inheritdoc/>
    public void RemoveObjectFromGameBoard(IIconHolder? obj, ICell cell)
    {
        if (obj == null) return;
        cell.Content = null;
    }

    /// <inheritdoc/>
    public async Task SaveGameBoardAsync()
    {
        // TODO
        await Task.Delay(0);
    }

    /// <inheritdoc/>
    public async Task LoadGameBoardAsync()
    {
        // TODO
        await Task.Delay(0);
    }

    /// <inheritdoc/>
    public async Task DeleteGameBoardAsync()
    {
        // TODO
        await Task.Delay(0);
    }
}
