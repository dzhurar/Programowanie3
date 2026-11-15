using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Programowanie3
{
    //state of each cell in the game board
    public enum CellState
    {
        Empty,
        Ship,
        Hit,
        Miss
    }

    public partial class Battleship : Window
    {
        public ObservableCollection<BattleCell> MyBoard { get; set; }
        public ObservableCollection<BattleCell> EnemyBoard { get; set; }

        private readonly bool _isPlayer1;
        private readonly GameSession _session;

        public Battleship(string playerTitle, bool isPlayer1, GameSession session)
        {
            InitializeComponent();

            Title = playerTitle;
            _isPlayer1 = isPlayer1;
            _session = session;

            MyBoard = isPlayer1 ? session.BoardPlayer1 : session.BoardPlayer2;
            EnemyBoard = isPlayer1 ? session.BoardPlayer2 : session.BoardPlayer1;

            DataContext = this;

            GenerateBoard(MyGrid, MyBoard, "Player");
            GenerateBoard(EnemyGrid, EnemyBoard, "Enemy");
        }

        private void GenerateBoard(Grid grid, ObservableCollection<BattleCell> board, string mode)
        {
            StateToColorConverter converter = new StateToColorConverter();

            for (int i = 0; i < 100; i++)
            {
                Button btn = new Button { Margin = new Thickness(1) };

                if (mode == "Enemy")
                {
                    btn.Tag = i.ToString(); // save click index in Tag
                    btn.Click += EnemyCell_Click;
                }
                else
                {
                    btn.IsHitTestVisible = false; // Your field cannot be clicked
                }

                // Create data binding (Binding) from C#
                Binding binding = new Binding($"[{i}].State")
                {
                    Source = board,
                    Converter = converter,
                    ConverterParameter = mode
                };
                btn.SetBinding(Button.BackgroundProperty, binding);

                // Set the button in the correct cell of the grid (Y = row, X = column)
                Grid.SetRow(btn, i / 10);
                Grid.SetColumn(btn, i % 10);

                grid.Children.Add(btn);
            }
        }

        private void EnemyCell_Click(object sender, RoutedEventArgs e)
        {
            // turn handling logic
            if (_isPlayer1 && !_session.IsPlayer1Turn) return;
            if (!_isPlayer1 && _session.IsPlayer1Turn) return;

            Button button = (Button)sender;
            int index = int.Parse(button.Tag.ToString());
            BattleCell clickedCell = EnemyBoard[index];

            // do not allow clicking on already hit or missed cells
            if (clickedCell.State == CellState.Hit || clickedCell.State == CellState.Miss)
            {
                return;
            }

            CellState newState;

            if (clickedCell.State == CellState.Ship)
            {
                newState = CellState.Hit;
                if (_isPlayer1) _session.Player1Hits++;
                else _session.Player2Hits++;
            }
            else
            {
                newState = CellState.Miss;
                _session.IsPlayer1Turn = !_session.IsPlayer1Turn;
            }

            // update the clicked cell state
            EnemyBoard[index] = new BattleCell { X = clickedCell.X, Y = clickedCell.Y, State = newState };

            // win condition check
            if (_session.Player1Hits >= 20 || _session.Player2Hits >= 20)
            {
                string winner = _session.Player1Hits >= 20 ? "Player 1" : "Player 2";
                MessageBox.Show($"{winner} wins!");
            }
        }
    }
}
