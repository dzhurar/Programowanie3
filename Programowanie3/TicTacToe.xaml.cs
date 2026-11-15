using System.Windows;
using System.Windows.Controls;

namespace Programowanie3
{
    public partial class TicTacToe : Window
    {
        private TicTacToeGame _game;

        public TicTacToe()
        {
            InitializeComponent();

            _game = new TicTacToeGame();
            DataContext = _game;
        }

        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int index = int.Parse(button.Tag.ToString());
            _game.MakeMove(index);
        }
    }
}
