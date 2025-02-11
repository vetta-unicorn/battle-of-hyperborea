namespace BoH.Models;

using BoH.Interfaces;

public class StunningBlow : IAbility
{
    
    /// <inheritdoc/> 
    public string AbilityId { get; }

    /// <inheritdoc/> 
    public string Name { get; } = "StunningBlow";

    /// <inheritdoc/> 
    public string Description { get; } = "Оглушает противника при ударе. Бросается только один кубик";

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

        if (target == null)
        {
            return false;
        }
        else
        {
            target.IsStunned = true;
            var userDices = user.DamageDices;
            user.DamageDices = 1;
            user.Attack(target);
            user.DamageDices = userDices;
            Coolown = 2;
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

    public StunningBlow()
    {
        AbilityId = Guid.NewGuid().ToString();
    }
}