namespace BoH.Models;

using BoH.Interfaces;

public class RusArcher : BaseUnit
{
    protected override int MaxHealth { get; } = 10;

    public RusArcher() : base("Rus-Archer", 'Я', "Rus", UnitType.Range)
    {
        Abilities.Add(new SelfCare());
        Range = 5;
        Hp = MaxHealth;
        Defence = 2;
        DamageDices = 3;
    }

}