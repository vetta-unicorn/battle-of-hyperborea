using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace battle_GUI.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private void OnButtonClick(object sender, RoutedEventArgs e)
    {
        // Изменяем цвет фона кнопки
        var button = sender as Button;
        var act = sender as RadioButton() ;
        if (button != null)
        {
            if (act != null) {
                act.Go




            button.Background = Brushes.Pink; // Установите нужный цвет   // Находим родительский Grid
                var parentGrid = (Grid)button.Parent;
                // Получаем строку и столбец кнопки
                int row = Grid.GetRow(button);
                int column = Grid.GetColumn(button);
                // Теперь у Вас есть координаты





            }
        }
    }

  
   
}