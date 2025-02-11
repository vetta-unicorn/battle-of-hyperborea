using Avalonia.Controls;
using Avalonia.Dialogs.Internal;
using BoH.Interfaces;
using DynamicData.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battle_GUI.ViewModels;

public class RB_ViewModel : ViewModelBase
{



    public void RadioVisible(List<object> RadioButtons, bool isVisible, IUnit user)
    {
        foreach (var child in RadioButtons)
        {
            if (child is TextBlock tt)
            {
                tt.IsVisible = isVisible;

            }

            else if (child is RadioButton radioButton)
            {
                if (radioButton.GroupName == "radioButtonAbilityGroup" && isVisible)
                { 
                        List<IAbility> Abilities = user.Abilities;
                        foreach (var ability in Abilities)
                        {
                            if (radioButton.Name == ability.Name) radioButton.IsVisible = isVisible;

                        }
                    
                }
                else { radioButton.IsVisible = isVisible; }
            }

        }
    }

    public string ActionFlag (List<object> RadioButtons, string Group)
    {
        foreach (var child in RadioButtons)
        {
            if (child is RadioButton radioButton && radioButton.GroupName == Group) return radioButton.Name;
            
        }

        return "0";

    }

}
