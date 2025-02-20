namespace BoH.Models;

public class RusWarrior : BaseUnit
{
    protected override int MaxHealth { get; } = 15;
    public RusWarrior() : base("Rus-Warrior", 'R', "Rus")
    {
        Abilities.Add(new StunningBlow());
        Hp = MaxHealth;
        Defence = 8;
        DamageDices = 2;
    }

}