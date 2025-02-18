using Avalonia.Controls;
using Avalonia.Media;
using BoH.GameLogic;
using BoH.Interfaces;
using BoH.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battle_GUI.ViewModels;

public class RoundViewModel : ViewModelBase
{

    public void TheFirstChoice(Button button, TurnManager turnManager, ICell cell, TextBlock Errors, List<object> RadioButtons)
    {


        RB_ViewModel RB = new RB_ViewModel();

        try
        {
            turnManager.SelectUnit(cell);
            if(turnManager._selectedUnit!=null)
            { 
            button.Background = new SolidColorBrush(Colors.Blue);
            RB.RadioVisible(RadioButtons, true, turnManager._selectedUnit);}
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

    public void TheSecondChoice(List<object> RadioButtons, TextBlock Errors, TurnManager turnManager, GameController gameController, Player[] players, int playnow, 
        ICell cell, Grid MainGrid)
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
            case "Move":
                {
                    try
                    {
                        List<ICell> targetcell = turnManager.ProcessScanner(ActionType.Move);
                        turnManager.ProcessPlayerAction(ActionType.Move, targetcell, cell);
                    }
                    catch (ArgumentNullException)
                    {
                        Errors.Text = "!";
                    }
                    catch (InvalidOperationException errors)
                    {
                        Errors.Text = errors.Message;
                    }
                    break;
                }
            case "Attack":
                {
                    try
                    {
                        List<ICell> targetcell = turnManager.ProcessScanner(ActionType.Attack);
                        turnManager.ProcessPlayerAction(ActionType.Attack, targetcell, cell);
                    }
                    catch (ArgumentNullException)
                    {
                        Errors.Text = "!";
                    }
                    catch (InvalidDataException qq)
                    {
                        Errors.Text = qq.Message;
                    }
                    catch(InvalidOperationException qq)
                    { 
                        Errors.Text = qq.Message;
                    }
                    
                    break;
                }
            case "Ability":
                {
                    string Ability = RB.ActionFlag(RadioButtons, "radioButtonGroup");
                    switch (Ability)
                    {
                        case "0":
                            {
                                Errors.Text = "You didn't choose an ability.";
                                break;
                            }
                        case "MadDash":
                            {
                                try
                                {
                                    List<ICell> targetcell = turnManager.ProcessScanner(ActionType.Ability);
                                    turnManager.ProcessPlayerAction(ActionType.Ability, targetcell, cell, new MadDash());
                                }
                                catch (ArgumentNullException)
                                {
                                    Errors.Text = "!";
                                }
                                catch (InvalidDataException qq )
                                {
                                    Errors.Text = qq.Message;
                                }
                                catch (InvalidOperationException qq)
                                {
                                    Errors.Text = qq.Message;
                                }
                                break;

                            }
                        case "StunningBlow":
                            {
                                try
                                {
                                    List<ICell> targetcell = turnManager.ProcessScanner(ActionType.Ability);
                                    turnManager.ProcessPlayerAction(ActionType.Ability, targetcell, cell, new StunningBlow());
                                }
                                catch (ArgumentNullException)
                                {
                                    Errors.Text = "!";
                                }
                                catch (InvalidDataException qq)
                                {
                                    Errors.Text = qq.Message;
                                }
                                catch (InvalidOperationException qq)
                                {
                                    Errors.Text = qq.Message;
                                }
                                break;

                            }
                    }


                    break;
                }
            case "End":
                {
                    if (gameController.CheckVictoryCondition(players))
                    {
                        Errors.Text = "Victory!!!";
                        MainGrid.IsEnabled = false;
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

