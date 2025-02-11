namespace BoH.Models;

using BoH.Interfaces;

public class MadDash : IAbility
{
    /// <inheritdoc/> 
    public string AbilityId { get; }

    /// <inheritdoc/> 
    public string Name { get; } = "MadDash";

    /// <inheritdoc/> 
    public string Description { get; } = "Вернитесь на фазу передвижения. Потеряйте 5 здоровья (не применится, если у применяющего юнита меньше 6 здоровья).";

    /// <inheritdoc/> 
    public bool IsActive { get; } = true;

    /// <inheritdoc/> 
    public int Coolown { set; get; } = 0;

    /// <inheritdoc/> 
    public event Action<IAbility>? OnAbilityUsed;

    /// <inheritdoc/> 
    public event Action<IAbility>? OnCooldown;

    /// <inheritdoc/> 
    public bool Activate(IUnit user, IUnit? target = null)
    {
        if (Coolown != 0) {
            OnCooldown?.Invoke(this);
            return false;
        }

        else if(user.Hp < 6)
        {
            return false;
        }
        else
        {
            user.CurrentTurnPhase = TurnPhase.End;
            user.Hp -= 5;
            Coolown = 3;
            OnAbilityUsed?.Invoke(this);
            return true;
        }
    }

    /// <inheritdoc/> 
    public void Update()
    {
        if (Coolown != 0)
            --Coolown;
    }

    public MadDash()
    {
        AbilityId = Guid.NewGuid().ToString();
    }

}