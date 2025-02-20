namespace BoH.Models;

public class LizardWarrior : BaseUnit
{
    protected override int MaxHealth { get; } = 10;
    public LizardWarrior() : base("Lizard-Warrior", 'S', "Lizard")
    {
        Abilities.Add(new MadDash());
        Hp = MaxHealth;
        Defence = 8;
        DamageDices = 2;
    }

}