using BoH.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using BoH.Interfaces;
using System.Collections.Generic;
using System;
using BoH.Services;
using System.Reactive;
using Avalonia.Controls;
using BoH.GameLogic;
using DynamicData;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace battle_GUI.ViewModels;

public class UnitColors: ViewModelBase
{
    public static Dictionary<string, Avalonia.Media.Color>? UnitColorMapping {  get; set; }

    public UnitColors()
    {
        UnitColorMapping = new Dictionary<string, Avalonia.Media.Color>()
        {
            { "2", Avalonia.Media.Color.FromRgb(0, 128, 0) }, // ящер-лучник
            { "S", Avalonia.Media.Color.FromRgb(124, 252, 0) }, // ящер-боец
            { "R", Avalonia.Media.Color.FromRgb(240, 128, 128) }, // рус-боец
            { "Я", Avalonia.Media.Color.FromRgb(255, 192, 203) }, // рус-лучник
            { "B", Avalonia.Media.Color.FromRgb(0, 0, 0) } // препятствие
        };
    }

}
