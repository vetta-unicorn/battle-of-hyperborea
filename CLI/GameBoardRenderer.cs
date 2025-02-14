namespace BoH.CLI;

using BoH.Interfaces;
using BoH.Models;

public class ConsoleGameBoardRenderer : IGameBoardRenderer
{
    public void Render(IGameBoard gameBoard)
    {
        int size = gameBoard.Width;
        PrintHorizontalBorder(size);
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if (gameBoard[x, y] is Cell cell)
                {
                    // Центрирование иконки, если она занимает 1 символ (можно изменить формат при необходимости)
                    Console.Write($"| {cell.Icon} ");
                }
            }
            Console.WriteLine("|"); // закрывающая вертикальная граница строки
            PrintHorizontalBorder(size);
        }
    }

    public void ScanRender(IGameBoard gameBoard, List<ICell> scannedCells)
    {
        int size = gameBoard.Width;
        PrintHorizontalBorder(size);
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < gameBoard.Height; x++)
            {
                if (gameBoard[x, y] is Cell cell)
                {
                    if (scannedCells.Contains(cell))
                    {
                        // Выводим специальный символ, если клетка отсканирована: "!" для юнита, "#" для других объектов
                        if (cell.Content is IUnit)
                            Console.Write("| ! ");
                        else
                            Console.Write("| # ");
                    }
                    else
                    {
                        Console.Write($"| {cell.Icon} ");
                    }
                }
            }
            Console.WriteLine("|");
            PrintHorizontalBorder(size);
        }
    }

    /// <summary>
    /// Выводит горизонтальную границу для игрового поля.
    /// Например, для поля шириной 3 будет выведено: +---+---+---+
    /// </summary>
    private void PrintHorizontalBorder(int width)
    {
        for (int i = 0; i < width; i++)
        {
            Console.Write("+---");
        }
        Console.WriteLine("+");
    }
}
