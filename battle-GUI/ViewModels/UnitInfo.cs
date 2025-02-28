using Avalonia.Controls;
using BoH.Interfaces;
using BoH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battle_GUI.ViewModels;

public class UnitInfo : ViewModelBase
{
    public void DisplayInfo(string textInfo, TextBlock Info)
    {
        Info.Text = textInfo;
    }

    public string GetCellInfo(int x, int y, GameBoard _gameBoard, TextBlock Info)
    {
        string st = "";

        if (_gameBoard != null && _gameBoard[x, y] != null && _gameBoard[x, y] is Cell cell)
        {
            if (cell.Content is Obstacle)
            {
                st = "Obstacle!";
            }

            else if (cell.Content is IUnit unit)
            {
                st = $"Team: {unit.Team}\nName: {unit.UnitName}\n" +
                    $"Hp: {unit.Hp}\nDefense: {unit.Defence}\n";

                if (unit.IsDead)
                {
                    st += "Unit is dead!\n";
                }

                if (unit.IsStunned)
                {
                    st += "Unit is stunned!\n";
                }
            }
        }

        return st;
    }
}