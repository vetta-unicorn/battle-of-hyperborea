using BoH.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battle_GUI.ViewModels;

public class UnitInfoViewModel : ViewModelBase
{
    private IUnit Unit { get; set; }

    
    public UnitInfoViewModel(IUnit _Unit)
    {
        Unit = _Unit;
    }

    public string UnitInfoString()
    {
        string st = $"Name: {Unit.UnitName}\nTeam: {Unit.Team}\nHp: {Unit.Hp}\nDefense: {Unit.Defence}\n";

        if (Unit.IsDead)
        {
            st += "Dead!\n";
        }

        if (Unit.IsStunned)
        {
            st += "Stunned!\n";
        }

        return st;
    }
}
