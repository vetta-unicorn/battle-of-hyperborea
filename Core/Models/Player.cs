namespace BoH.Models;

using BoH.Interfaces;
using System.Collections.ObjectModel;

/// <summary>
/// Представляет игрока и управляет его юнитами в ходе игры.
/// </summary>
public class Player : IPlayer
{
    /// <inheritdoc/>
    public string Id { get; } = Guid.NewGuid().ToString();

    /// <inheritdoc/>
    public string Team { get; }

    /// <inheritdoc/>
    public List<IUnit> Units { get; set; } = new();

    /// <inheritdoc/>
    public bool HasAliveUnits => Units.Any(u => !u.IsDead);

    /// <summary>
    /// Инициализирует нового игрока.
    /// </summary>
    /// <param name="team">Название команды игрока.</param>
    /// <exception cref="ArgumentException">
    /// Выбрасывается, если название команды пустое или содержит только пробелы.
    /// </exception>
    public Player(string team)
    {
        if (string.IsNullOrWhiteSpace(team))
            throw new ArgumentException("Team name can't be null", nameof(team));

        Team = team;
    }

    public void ResetUnitsForNewTurn()
    {
        foreach (var unit in Units.Where(u => !u.IsDead))
        {
            unit.ResetTurnState();
            unit.IsStunned = false;
        }
    }
}