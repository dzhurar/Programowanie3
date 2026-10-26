using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
    // cells for the game board
    public class BattleCell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public CellState State { get; set; }
    }
    public class GameSession
    {
        public bool IsPlayer1Turn { get; set; } = true;
        public int Player1Hits { get; set; } = 0;
        public int Player2Hits { get; set; } = 0;
        public ObservableCollection<BattleCell> BoardPlayer1 { get; set; }
        public ObservableCollection<BattleCell> BoardPlayer2 { get; set; }

        private Random _rand = new Random();

        public GameSession()
        {
            BoardPlayer1 = new ObservableCollection<BattleCell>();
            BoardPlayer2 = new ObservableCollection<BattleCell>();

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    BoardPlayer1.Add(new BattleCell { X = x, Y = y, State = CellState.Empty });
                    BoardPlayer2.Add(new BattleCell { X = x, Y = y, State = CellState.Empty });
                }
            }
            PlaceShipsRandomly(BoardPlayer1);
            PlaceShipsRandomly(BoardPlayer2);
        }
        private void PlaceShipsRandomly(ObservableCollection<BattleCell> board)
        {
            int[] shipSizes = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };
            foreach (int size in shipSizes)
            {
                bool isPlaced = false;
                while(!isPlaced)
                {
                    int startX = _rand.Next(0, 10);
                    int startY = _rand.Next(0, 10);
                    bool isHorizontal = _rand.Next(0, 2) == 0;
                    if (CanPlaceShip(board, startX, startY, size, isHorizontal))
                    {
                        for(int i = 0; i < size; i++)
                        {
                            int x = isHorizontal ? startX + i : startX;
                            int y = isHorizontal ? startY : startY + i;
                            int index = y * 10 + x;
                            board[index] = new BattleCell { X = x, Y = y, State = CellState.Ship };
                        }
                        isPlaced = true;
                    }
                }
            }
        }
        private bool CanPlaceShip(ObservableCollection<BattleCell> board, int startX, int startY, int size, bool isHorizontal)
        {
            // Out of bounds check
            if (isHorizontal && startX + size > 10) return false;
            if (!isHorizontal && startY + size > 10) return false;

            // check surrounding cells to ensure no adjacent ships
            for (int i = 0; i < size; i++)
            {
                int x = isHorizontal ? startX + i : startX;
                int y = isHorizontal ? startY : startY + i;

                // check surrounding cells
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        int checkX = x + dx;
                        int checkY = y + dy;

                        // check if the cell is within bounds
                        if (checkX >= 0 && checkX < 10 && checkY >= 0 && checkY < 10)
                        {
                            int index = checkY * 10 + checkX;
                            if (board[index].State != CellState.Empty)
                            {
                                return false; // Place is occupied
                            }
                        }
                    }
                }
            }
            return true; // Place is valid
        }
    }

    public partial class Battleship : Window
    {
        public ObservableCollection<BattleCell> MyBoard { get; set; }
        public ObservableCollection<BattleCell> EnemyBoard { get; set; }
        private bool _isPlayer1;
        private GameSession _session;
        public Battleship(string playerTitle, bool isPlayer1, GameSession session)
        {
            InitializeComponent();

            this.Title = playerTitle;
            _isPlayer1 = isPlayer1;
            _session = session;

            MyBoard = isPlayer1 ? session.BoardPlayer1 : session.BoardPlayer2;
            EnemyBoard = isPlayer1 ? session.BoardPlayer2 : session.BoardPlayer1;

            DataContext = this;
        }
        private void EnemyCell_Click(object sender, RoutedEventArgs e)
        {
            if (_isPlayer1 && !_session.IsPlayer1Turn) return;
            if (!_isPlayer1 && _session.IsPlayer1Turn) return;

            Button button = (Button)sender;
            BattleCell clickedCell = (BattleCell)button.Tag;

            if(clickedCell.State == CellState.Hit || clickedCell.State == CellState.Miss)
            {
                return;
            }

            int index = clickedCell.Y * 10 + clickedCell.X;
            CellState newState;

            if(clickedCell.State == CellState.Ship)
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

            EnemyBoard[index] = new BattleCell { X = clickedCell.X, Y = clickedCell.Y, State = newState };
            if(_session.Player1Hits >= 20 || _session.Player2Hits >= 20)
            {
                string winner = _session.Player1Hits >= 20 ? "Player 1" : "Player 2";
                MessageBox.Show($"{winner} wins!");
            }
        }
    }
}
