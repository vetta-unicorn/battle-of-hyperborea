namespace BoH.Models;

using BoH.Interfaces;
using System.ComponentModel;

/// <inheritdoc/>
public class Cell : ICell, IIconHolder//, INotifyPropertyChanged
{
    private (int X, int Y) _position;
    /// <inheritdoc/>
    public (int X, int Y) Position
    {
        get => _position;
        private set
        {
            if (_position != value)
            {
                _position = value;
            }
        }
    }


    public string PositionString => $"({Position.X}, {Position.Y})";

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">
    /// Выбрасывается, если символ некорректен (например, не является печатным).
    /// </exception>
    /// 

    public Cell(int x, int y)
    {
        Position = (x, y);
    }

    public string Icon
    {
        get => _icon;
        private set
        {
            _icon = value;
        }
    }
    private string _icon = " ";

    /// <inheritdoc/>
    public IIconHolder? Content { get; set; } = null;

    /// <inheritdoc/>
    /// <returns>true, если клетка занята; иначе false.</returns>
    public bool IsOccupied() => Content != null;

    /// <inheritdoc/>
    public void Clear()
    {
        Content = null;
    }


    /// <inheritdoc/>
    /// <exception cref="ArgumentException"/>
    public void UpdateIcon()
    {
        Icon = Content?.Icon ?? " ";
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Cell"/> с указанной позицией.
    /// </summary>
    /// <param name="Position">Позиция клетки на игровом поле в формате (X, Y).</param>
    /// <exception cref="ArgumentException">Генерируется, если координаты отрицательные.</exception>
    public Cell((int X, int Y) Position)
    {
        if (Position.X < 0 || Position.Y < 0)
        {
            throw new ArgumentException("Coordinates can't be negative.");
        }
        this.Position = Position;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Cell other)
            return false;
        return Position.X == other.Position.X && Position.Y == other.Position.Y;
    }

    public override int GetHashCode() => HashCode.Combine(Position.X, Position.Y);

    //public event PropertyChangedEventHandler? PropertyChanged;

    //protected virtual void OnPropertyChanged(string propertyName)
    //{
    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //}
}
