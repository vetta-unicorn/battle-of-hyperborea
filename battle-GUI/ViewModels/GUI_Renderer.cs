using Avalonia.Controls;
using BoH.Interfaces;
using BoH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;

namespace battle_GUI.ViewModels;

public class Renderer_ViewModels: ViewModelBase
{
    public void Renderer(GameBoard _gameBoard, Grid MainGrid, Dictionary<string, Color>? colorMapping)
    {
        int size = _gameBoard.Width;
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if (_gameBoard is not null && _gameBoard[x, y] is Cell cell && cell is not null)
                {
                    var button = MainGrid.Children[y * 8 + x] as Button;

                    if (button != null)
                    {
                        button.Content = _gameBoard?.Cells[x, y]?.Content?.Icon;

                        if (cell.Content is null || colorMapping is null)
                        {
                            button.Background = new SolidColorBrush(Colors.LightGray);
                        }

                        else
                        {
                            // проверка на нахождение в словаре IIconHolder
                            if (colorMapping.ContainsKey(cell.Content.Icon))
                            {
                                button.Background = new SolidColorBrush(colorMapping[cell.Content.Icon]);
                            }
                        }
                    }

                }
            }
        }
    }

    public void ScanRenderer(GameBoard gameBoard, Grid MainGrid, List<ICell> scannedCells, Dictionary<string, Color>? scannerMapping)
    {
        int size = gameBoard.Width;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < gameBoard.Height; x++)
            {
                if (gameBoard is not null && gameBoard[x, y] is Cell cell && cell is not null)
                {
                    // будем перекрашивать только те ячейки, которые есть в радиусе сканнера
                    if (scannedCells.Contains(cell))
                    {
                        var button = MainGrid.Children[y * 8 + x] as Button;

                        if (button != null)
                        {
                            button.Content = gameBoard?.Cells[x, y]?.Content?.Icon;

                            if (cell.Content is null || scannerMapping is null)
                            {
                                button.Background = new SolidColorBrush(Colors.Yellow);
                            }

                            else
                            {
                                // проверка на нахождение в словаре IIconHolder
                                if (scannerMapping.ContainsKey(cell.Content.Icon))
                                {
                                    button.Background = new SolidColorBrush(scannerMapping[cell.Content.Icon]);
                                }
                            }
                        }
                    }

                }
            }
        }
    }

}