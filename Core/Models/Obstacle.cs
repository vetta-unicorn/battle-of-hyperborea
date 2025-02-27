namespace BoH.Models;

using System;
using BoH.Interfaces;

/// <summary>
/// Реализация препятствия на игровом поле.
/// </summary>
public class Obstacle : IObstacle, IIconHolder
{
    /// <summary>
    /// Уникальный идентификатор препятствия. Генерируется автоматически.
    /// </summary>
    public string ObstacleId { get; }

    /// <inheritdoc/>
    /// <exception cref="ArgumentException">
    /// Выбрасывается, если символ некорректен (например, не является печатным).
    /// </exception>
    public string Icon
    {
        get => _icon;
        private set
        {
            if (value.Length != 1 ||
               !char.IsLetterOrDigit(value[0]) &&
               !char.IsSymbol(value[0]) &&
               !char.IsPunctuation(value[0]))
            {
                throw new ArgumentException("Unit's icon must be 1 symbol.");
            }
            _icon = value;
        }
    }
    private string _icon = "T";

    /// <summary>
    /// Инициализирует новый экземпляр препятствия с автоматически сгенерированным идентификатором.
    /// </summary>
    /// <param name="icon">Иконка препятствия (необязательный параметр, по умолчанию 'B').</param>
    public Obstacle(char icon = 'B')
    {
        ObstacleId = Guid.NewGuid().ToString();
        Icon = icon.ToString();
    }
}
