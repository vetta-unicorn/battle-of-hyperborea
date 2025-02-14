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
    public static Dictionary<string, Avalonia.Media.Color>? ScannerColorMapping { get; set; }

    public UnitColors()
    {
        // создаем юнитов
        RusArcher RusAr = new RusArcher();
        RusWarrior RusWar = new RusWarrior();

        LizardArcher LizAr = new LizardArcher();
        LizardWarrior LizWar = new LizardWarrior();

        // препятствие
        Obstacle obst = new Obstacle();

        // словарь для окраски Iconholder при рендеринге поля
        UnitColorMapping = new Dictionary<string, Avalonia.Media.Color>()
        {
            { LizAr.Icon, Avalonia.Media.Color.FromRgb(0, 128, 0) }, // Green
            { LizWar.Icon, Avalonia.Media.Color.FromRgb(124, 252, 0) }, // Yellow-Green
            { RusWar.Icon, Avalonia.Media.Color.FromRgb(240, 128, 128) }, // Coral
            { RusAr.Icon, Avalonia.Media.Color.FromRgb(255, 192, 203) }, // Pink
            { obst.Icon, Avalonia.Media.Color.FromRgb(0, 0, 0) } // Black
        };

        // словарь для перекраски Inconholder при сканировании поля
        ScannerColorMapping = new Dictionary<string, Avalonia.Media.Color>()
        {
            { LizAr.Icon, Avalonia.Media.Color.FromRgb(0, 255, 127) }, // SpringGreen
            { LizWar.Icon, Avalonia.Media.Color.FromRgb(0, 255, 127) }, // SpringGreen
            { RusWar.Icon, Avalonia.Media.Color.FromRgb(255, 99, 71) }, // Tomato
            { RusAr.Icon, Avalonia.Media.Color.FromRgb(255, 99, 71) }, // Tomato
            { obst.Icon, Avalonia.Media.Color.FromRgb(0, 0, 0) } // Black
        };
    }

}
