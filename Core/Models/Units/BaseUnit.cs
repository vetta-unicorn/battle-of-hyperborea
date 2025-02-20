namespace BoH.Models;

using System.ComponentModel;
using System.Xml.Linq;
using BoH.Interfaces;

/// <summary>
/// Базовая реализация боевого юнита.
/// </summary>
public class BaseUnit : IUnit, IIconHolder, INotifyPropertyChanged
{
    // штуки для свойств 
    private string name = "Unit";
    private int hp = 100;
    private bool isDead = false;
    private bool isStunned = false;

    /// <summary>
    /// Максимально возможное здоровье юнита.
    /// </summary>
    protected virtual int MaxHealth { get; } = 100;

    /// <inheritdoc/>
    public string UnitId { get; }

    /// <inheritdoc/>
    public string UnitName
    {
        get => name;
        set { name = value; OnPropertyChanged(nameof(UnitName)); }
    }


    /// <inheritdoc/>
    public string Team { get; } = "Dev";

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
                throw new ArgumentException("Иконка юнита должна быть одним печатным символом.");
            }
            _icon = value;
        }
    }
    private string _icon = "T";

    /// <inheritdoc/>
    public int Hp
    {
        get => hp;
        set { hp = value; OnPropertyChanged(nameof(Hp)); }
    }


    /// <inheritdoc/>
    public int Speed { get; set; } = 3;

    /// <inheritdoc/>
    public int DamageDices { get; set; } = 3;

    /// <inheritdoc/>
    public int Defence { get; set; } = 0;

    /// <inheritdoc/>
    public int Range { get; protected set; } = 1;

    /// <inheritdoc/>
    public UnitType UnitType { get; } = UnitType.Melee;

    /// <inheritdoc/>
    public bool IsStunned
    {
        get => isStunned;
        set { isStunned = value; OnPropertyChanged(nameof(IsStunned)); }
    }

    /// <inheritdoc/>
    public bool IsDead
    {
        get => isDead;
        set { isDead = value; OnPropertyChanged(nameof(IsDead)); }
    }

    /// <inheritdoc/>
    public TurnPhase CurrentTurnPhase { get; set; }

    /// <inheritdoc/>
    public List<IAbility> Abilities { get; } = new();

    /// <inheritdoc/>
    public ICell? OccupiedCell { get; set; } = null;

    /// <inheritdoc/>
    public event Action<IUnit>? OnDeath;

    /// <inheritdoc/>
    public event Action<IUnit>? OnStunned;

    /// <inheritdoc/>
    public event Action<IUnit>? OnMove;

    /// <inheritdoc/>
    public event Action<IUnit>? OnAttack;

    /// <inheritdoc/>
    public event Action<IUnit>? OnTakingDamage;

    /// <inheritdoc/>
    public event Action<IUnit>? OnHealed;

    /// <summary>
    /// Инициализирует новый экземпляр юнита.
    /// </summary>
    /// <param name="icon">Иконка препятствия (необязательный параметр, по умолчанию 'B').</param>
    /// <param name="team">Фракция юнита (необязательный параметр, по умолчанию "Dev").</param>
    /// <param name="type">Тип атаки персонажа (необязательный параметр, по умолчанию Melee).</param>
    public BaseUnit(string unitName, char icon = 'T', string team = "Dev", UnitType type = UnitType.Melee)
    {
        UnitName = unitName;
        UnitId = Guid.NewGuid().ToString();
        UnitType = type;
        Team = team;
        Icon = icon.ToString();
    }

    /// <inheritdoc/>
    /// <param name="amount">Количество урона для применения.</param>
    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        int effectiveDamage = Math.Max(0, amount - Defence);
        Hp -= effectiveDamage;
        OnTakingDamage?.Invoke(this);

        if (Hp <= 0)
        {
            Hp = 0;
            IsDead = true;
            OnDeath?.Invoke(this);
        }
    }

    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException">Выбрасывается, если юнит мёртв.</exception>
    public void Heal(int amount)
    {
        if (IsDead) throw new InvalidOperationException("Мертвый юнит не может быть вылечен.");

        Hp = Math.Min(MaxHealth, Hp + amount);
        OnHealed?.Invoke(this);
    }

    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException"></exception>
    public void PlaceUnit(ICell newPosition)
    {
        if (IsDead) throw new InvalidOperationException("Мертвый юнит не может двигаться.");
        if (IsStunned) throw new InvalidOperationException("Оглушенный юнит не может двигаться.");
        if (CurrentTurnPhase != TurnPhase.Movement) throw new InvalidOperationException("Юнит не в фазе передвижения.");
        if (newPosition.Content != null) throw new InvalidOperationException("Клетка занята, передвижение невозможно.");

        if (OccupiedCell != null) OccupiedCell.Content = null;
        OccupiedCell = newPosition;
        newPosition.Content = this;
        ChangeTurnPhase();
    }

    /// <inheritdoc/>
    /// <returns>true, если перемещение возможно; иначе false.</returns>
    public bool CanMove() => !IsDead && !IsStunned && CurrentTurnPhase == TurnPhase.Movement;

    /// <inheritdoc/>
    /// <returns>Значение урона. Если юнит мёртв, возвращает 0.</returns>
    public int CalculateAttackDamage()
    {
        if (!IsDead)
        {
            int calculatedDamage = 0;
            Random rnd = new();
            for (int i = 0; i < DamageDices; i++)
            {
                calculatedDamage += rnd.Next(1, 7);
            }
            return calculatedDamage;
        }
        else return 0;
    }

    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException"/>
    public void Attack(IUnit target)
    {
        if (IsDead) throw new InvalidOperationException("Мертвый юнит не может атаковать.");
        if (CurrentTurnPhase != TurnPhase.Action) throw new InvalidOperationException("Юнит не в фазе действия.");
        if (target.IsDead) throw new InvalidOperationException("Нельзя атаковать мертвого юнита.");

        target.TakeDamage(CalculateAttackDamage());
        OnAttack?.Invoke(this);
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Полностью сбрасывает состояние хода, устанавливая фазу в <see cref="TurnPhase.Movement"/>
    /// </remarks>
    public void ResetTurnState()
    {
        CurrentTurnPhase = TurnPhase.Movement;
    }

    /// <inheritdoc/>
    /// <exception cref="InvalidEnumArgumentException">
    /// Выбрасывается, если текущая фаза содержит недопустимое значение.
    /// </exception>
    public void ChangeTurnPhase()
    {
        CurrentTurnPhase = CurrentTurnPhase switch
        {
            TurnPhase.Movement => TurnPhase.Action,
            TurnPhase.Action => TurnPhase.End,
            TurnPhase.End => TurnPhase.Movement,
            _ => throw new InvalidEnumArgumentException(
                $"Недопустимое значение фазы хода: {CurrentTurnPhase}")
        };
    }

    // события
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}