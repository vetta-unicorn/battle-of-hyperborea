
namespace BoH.Models;

using BoH.Interfaces;

public class LizardArcher : BaseUnit
{
    protected override int MaxHealth { get; } = 12;
    public int AttackRange { get; set; } = 4;

    public LizardArcher() : base("Lizard-Archer", '2', "Lizard", UnitType.Range)
    {
        Abilities.Add(new SelfCare());
        Hp = MaxHealth;
        Defence = 2;
        DamageDices = 3;
    }

}