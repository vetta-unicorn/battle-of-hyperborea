using Avalonia.Controls;
using Avalonia.Media;
using BoH.GameLogic;
using BoH.Interfaces;
using BoH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battle_GUI.ViewModels;

public class RoundViewModel : ViewModelBase
{
    public void TheFirstChoice(Button button, TurnManager turnManager, ICell cell, TextBlock Errors, List<object> RadioButtons)
    {

        
        RB_ViewModel RB = new RB_ViewModel();

        try { turnManager.SelectUnit(cell);
            button.Background = new SolidColorBrush(Colors.Blue);
            RB.RadioVisible(RadioButtons, true, turnManager._selectedUnit);
        }
        catch (InvalidOperationException)
        {
            Errors.Text = "The unit is unavailable for selection.";
        }

        catch (ArgumentNullException)
        {
            Errors.Text = "There was no unit in the cage.";
        }
    }

    public void TheSecondChoice(List<object> RadioButtons, TextBlock Errors, TurnManager turnManager, GameController gameController, Player[] players, int playnow)
    {
        RB_ViewModel RB = new RB_ViewModel();
        string Action = RB.ActionFlag(RadioButtons, "radioButtonGroup");

        switch (Action)
        {
            case "0":
                {
                    Errors.Text = "You didn't choose an action.";
                    break;
                }
            case "None":
                {
                    break;
                }
            case "End":
                {
                    if (gameController.CheckVictoryCondition(players))
                    {
                        Errors.Text = "Victory!!!";
                        //мб удалить доску или заблокировать ее?
                    }
                    else
                    {
                        turnManager.EndTurn();
                        playnow = (playnow + 1) % 2;
                        turnManager.StartNewRound(players[playnow]);

                    }
                    
                    break;
                }
            case "Skip":
                {
                    turnManager.ProcessPlayerAction(ActionType.Skip);
                    break;
                }



        }



        RB.RadioVisible(RadioButtons, false);

    }


}

