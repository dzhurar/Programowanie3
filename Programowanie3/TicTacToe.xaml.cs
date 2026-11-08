using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel; // for ObservableCollection

namespace Programowanie3
{

    public class CellData
    {
        public int Index { get; set; }
        public string Mark { get; set; } // "", "X", "O"
    }

    public class TicTacToeGame
    {
        public ObservableCollection<CellData> Cells { get; set; }
        public string CurrentPlayer { get; private set; }
        public bool IsGameOver { get; private set; }

        public TicTacToeGame()
        {
            Cells = new ObservableCollection<CellData>();
            StartNewGame();
        }

        public void StartNewGame()
        {
            Cells.Clear();
            IsGameOver = false;
            CurrentPlayer = "X";

            for (int i = 0; i < 9; i++)
            {
                Cells.Add(new CellData { Index = i, Mark = "" });
            }
        }

        public void MakeMove(int index)
        {
            if (IsGameOver || Cells[index].Mark != "") return;

            Cells[index] = new CellData { Index = index, Mark = CurrentPlayer };

            if (CheckWinner()) return;

            CurrentPlayer = CurrentPlayer == "X" ? "O" : "X";
        }

        private bool CheckWinner()
        {
            int[,] winningCombinations = new int[,]
            {
                {0, 1, 2}, {3, 4, 5}, {6, 7, 8},
                {0, 3, 6}, {1, 4, 7}, {2, 5, 8},
                {0, 4, 8}, {2, 4, 6}
            };

            for (int i = 0; i < 8; i++)
            {
                int a = winningCombinations[i, 0];
                int b = winningCombinations[i, 1];
                int c = winningCombinations[i, 2];

                if (Cells[a].Mark != "" && Cells[a].Mark == Cells[b].Mark && Cells[a].Mark == Cells[c].Mark)
                {
                    IsGameOver = true;
                    MessageBox.Show($"Player {Cells[a].Mark} wins!");
                    StartNewGame();
                    return true;
                }
            }

            bool isDraw = true;
            foreach (var cell in Cells)
            {
                if (cell.Mark == "")
                {
                    isDraw = false;
                    break;
                }
            }

            if (isDraw)
            {
                IsGameOver = true;
                MessageBox.Show("The game is a draw!");
                StartNewGame();
                return true;
            }

            return false;
        }
    }

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
