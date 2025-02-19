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



    public void RadioVisible(List<object> RadioButtons, bool isVisible, IUnit user = null)
    {
        foreach (var child in RadioButtons)
        {
            if (child is TextBlock tt)
            {
                tt.IsVisible = isVisible;

            }

            else if (child is RadioButton radioButton)
            {
                if (radioButton.GroupName == "radioButtonAbilityGroup")
                {
                    if (isVisible)
                    {
                        List<IAbility> Abilities = user.Abilities;
                        foreach (var ability in Abilities)
                        {
                            if (radioButton.Name == ability.Name) radioButton.IsVisible = isVisible;

                        }
                    }
                    else
                    {
                        radioButton.IsVisible = isVisible;
                        radioButton.IsChecked = false;
                    }

                }
                else
                {
                    radioButton.IsVisible = isVisible;
                    if (RadioButtons[1] is RadioButton rb)
                    {
                        rb.IsChecked = true;
                    }
                }
            }

        }
    }

    public string ActionFlag(List<object> RadioButtons, string Group)
    {
        foreach (var child in RadioButtons)
        {
            if (child is RadioButton radioButton && radioButton != null && radioButton.Name != null &&  radioButton.GroupName == Group && radioButton.IsChecked == true) return radioButton.Name;

        }

        return "0";

    }

}
