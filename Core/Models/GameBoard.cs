namespace BoH.Models;

using BoH.Interfaces;
using System;

/// <summary>
/// Реализация игрового поля.
/// </summary>
public class GameBoard : IGameBoard
{
    /// <summary>
    /// Размер игрового поля по ширине (количество столбцов).
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Размер игрового поля по высоте (количество строк).
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Двумерный массив клеток, представляющих игровое поле.
    /// </summary>
    public ICell[,] Cells { get; }

    /// <summary>
    /// Индексатор для доступа к клетке игрового поля по координатам.
    /// </summary>
    /// <param name="x">Координата X.</param>
    /// <param name="y">Координата Y.</param>
    /// <returns>Клетка игрового поля по указанным координатам.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если координаты выходят за пределы игрового поля.</exception>
    public ICell this[int x, int y]
    {
        get
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                throw new ArgumentOutOfRangeException("Coordinates are out of the gameboard.");
            }
            return Cells[x, y];
        }
    }

    /// <summary>
    /// Инициализирует новый экземпляр игрового поля с заданными размерами.
    /// </summary>
    /// <param name="width">Ширина игрового поля (количество столбцов).</param>
    /// <param name="height">Высота игрового поля (количество строк).</param>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если ширина или высота меньше 1.</exception>
    public GameBoard(int width, int height)
    {
        if (width < 1 || height < 1)
        {
            throw new ArgumentOutOfRangeException("Size of the gameboard must be bigger than 0.");
        }

        Width = width;
        Height = height;
        Cells = new ICell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cells[x, y] = new Cell((x, y));
            }
        }
    }

    /// <summary>
    /// Проверяет, является ли ячейка доступной для перемещения.
    /// </summary>
    /// <param name="x">Координата X.</param>
    /// <param name="y">Координата Y.</param>
    /// <returns>true, если ячейка доступна для перемещения; иначе false.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если координаты выходят за пределы игрового поля.</exception>
    public bool IsCellAvailable(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            throw new ArgumentOutOfRangeException("Coordinates are out of the gameboard.");
        }

        return !Cells[x, y].IsOccupied();
    }
}
