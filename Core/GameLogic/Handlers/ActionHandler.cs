namespace BoH.GameLogic;

using BoH.Interfaces;

/// <summary>
/// Реализация обработчика действий юнитов.
/// </summary>
public class ActionHandler : IActionHandler
{
    private IGameBoard _gameBoard;

    /// <inheritdoc/>
    public event Action<IGameBoard>? OnUpdatingGameBoard;

    /// <summary>
    /// Создает экземпляр обработчика действий.
    /// </summary>
    /// <param name="gameBoard">Игровое поле.</param>
    public ActionHandler(IGameBoard gameBoard)
    {
        _gameBoard = gameBoard;
    }

    /// <inheritdoc/>
    public void HandleMovement(IUnit movingUnit, ICell destination, List<ICell> legalMoves)
    {
        if (movingUnit.OccupiedCell == null) throw new ArgumentNullException("The unit was not placed on the gameboard.");
        if (legalMoves.Count == 0) throw new ArgumentException("The list of available coordinates is empty.");
        if (!legalMoves.Any(cell => cell.Position.X == destination.Position.X && cell.Position.Y == destination.Position.Y))
            throw new InvalidOperationException("The cell is not located within the movement radius or there an obstacle in the cage.");

        if (destination.IsOccupied()) throw new InvalidOperationException("The cage is occupied, movement is impossible.");

        ICell originalCell = movingUnit.OccupiedCell;
        (int x, int y) originalPosition = originalCell.Position;
        _gameBoard[originalPosition.x, originalPosition.y].Content = null;
        originalCell.UpdateIcon();

        movingUnit.OccupiedCell = destination;
        destination.Content = movingUnit as IIconHolder;
        destination.UpdateIcon();

        movingUnit.ChangeTurnPhase();
        OnUpdatingGameBoard?.Invoke(_gameBoard);
    }

    /// <inheritdoc/>
    public void HandleAttack(IUnit attacker, ICell targetedCell, List<ICell> legalAttackLocations)
    {
        if (targetedCell.Content is IObstacle) throw new InvalidOperationException("You can't attack an obstacle.");
        if (!legalAttackLocations.Contains(targetedCell)) throw new InvalidOperationException("The cell is not in the attack range.");
        if (targetedCell.Content is null) throw new InvalidOperationException("An empty cell is selected.");
        if (targetedCell.Content is IUnit target)
        {
            attacker.Attack(target);
            attacker.ChangeTurnPhase();
            OnUpdatingGameBoard?.Invoke(_gameBoard);
        }
        else throw new InvalidDataException("Unknown type of object in the cage.");
    }

    /// <inheritdoc/>
    public void HandleAbility(IUnit attacker, IAbility usedAbility, ICell targetedCell, List<ICell> legalAttackLocations)
    {
        if (targetedCell.Content is IObstacle) throw new InvalidOperationException("You can't attack an obstacle.");
        if (!legalAttackLocations.Contains(targetedCell)) throw new InvalidOperationException("The cell is not in the attack range.");
        if (!attacker.Abilities.Contains(usedAbility)) throw new InvalidOperationException("The ability is missing from the unit.");   
        if (targetedCell.Content is null) throw new InvalidOperationException("An empty cell is selected.");

        if (targetedCell.Content is IUnit target)
        {
            usedAbility.Activate(attacker, target);
            attacker.ChangeTurnPhase();
            OnUpdatingGameBoard?.Invoke(_gameBoard);
        }
        else throw new InvalidDataException("Unknown type of object in the cage.");
    }

    /// <inheritdoc/>
    public void HandleSkip(IUnit unit)
    {
        if (unit.CurrentTurnPhase != TurnPhase.End) unit.ChangeTurnPhase();
        OnUpdatingGameBoard?.Invoke(_gameBoard);
    }
}