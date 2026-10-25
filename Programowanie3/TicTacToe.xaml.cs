using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel; // for ObservableCollection
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Programowanie3
{
    /// <summary>
    /// Interaction logic for TicTacToe.xaml
    /// </summary>
    public class CellData
    {
        public int Index { get; set; }
        public string Mark { get; set; } // "", "X", "O"
    }
    public partial class TicTacToe : Window
    {
        public ObservableCollection<CellData> Cells { get; set; }
        private string _currentPlayer = "X";
        private bool _isGameOver = false;
        public TicTacToe()
        {
            Cells = new ObservableCollection<CellData>();
            DataContext = this;
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            Cells.Clear();
            _isGameOver = false;
            _currentPlayer = "X";
            for (int i = 0; i < 9; i++)
            {
                Cells.Add(new CellData { Index = i, Mark = "" });
            }
        }
        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            if (_isGameOver) return;

            Button button = (Button)sender;
            int index = (int)button.Tag;
            if (Cells[index].Mark != "") return; // Cell already marked
            Cells[index] = new CellData { Index = index, Mark = _currentPlayer };
            if (CheckWinner()) return;
            if (_currentPlayer == "X")
            {
                _currentPlayer = "O";
            }
            else
            {
                _currentPlayer = "X";
            }
        }
        private bool CheckWinner()
        {
            int[,] winningCombinations = new int[,]
            {
                {0, 1, 2},
                {3, 4, 5},
                {6, 7, 8},
                {0, 3, 6},
                {1, 4, 7},
                {2, 5, 8},
                {0, 4, 8},
                {2, 4, 6}
            };
            for (int i = 0; i < 8; i++)
            {
                int a = winningCombinations[i, 0];
                int b = winningCombinations[i, 1];
                int c = winningCombinations[i, 2];
                if(Cells[a].Mark != "" && Cells[a].Mark == Cells[b].Mark && Cells[a].Mark == Cells[c].Mark)
                {
                    _isGameOver = true;
                    MessageBox.Show($"Player {Cells[a].Mark} wins!");
                    StartNewGame();
                    return true;

                }
            }
            // Check for draw
            bool isDraw = true;
            for (int i = 0; i < Cells.Count; i++)
            {
                if (Cells[i].Mark == "")
                {
                    isDraw = false;
                    break;
                }
            }
            if (isDraw)
            {
                _isGameOver = true;
                MessageBox.Show("The game is a draw!");
                StartNewGame();
                return true;
            }
            return false;
        }
    }
}