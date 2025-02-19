namespace BoH.Models;

using BoH.Interfaces;
public class SelfCare : IAbility
{
    /// <inheritdoc/> 
    public string AbilityId { get; }

    /// <inheritdoc/> 
    public string Name { get; } = "SelfCare";

    /// <inheritdoc/> 
    public string Description { get; } = "В конце хода юнит лечит себя на 1-6 здоровья";

    /// <inheritdoc/> 
    public bool IsActive { get; } = false;

    /// <inheritdoc/> 
    public event Action<IAbility>? OnAbilityUsed;

    /// <inheritdoc/> 
    public event Action<IAbility>? OnCooldown = null;

    /// <inheritdoc/> 
    public int Coolown { set; get; } = 0;

    /// <inheritdoc/> 
    public bool Activate(IUnit user, IUnit? target = null)
    {
        if (Coolown != 0)
        {
            OnCooldown?.Invoke(this);
            return false;
        }

        Random rnd = new Random();
        target = user;
        user.Heal(rnd.Next(1, 7));
        OnAbilityUsed?.Invoke(this);
        return true;
    }

    /// <inheritdoc/> 
    public void Update()
    {

    }

    public SelfCare()
    {
        AbilityId = Guid.NewGuid().ToString();
    }

}