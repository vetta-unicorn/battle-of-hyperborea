using Avalonia.Controls;

namespace YourNamespace
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnButtonClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MessageBox.Show("Button clicked!");
        }
    }
}

