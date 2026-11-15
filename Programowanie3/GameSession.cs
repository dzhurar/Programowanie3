using System.Collections.ObjectModel;

namespace Programowanie3
{
    public class GameSession
    {
        public bool IsPlayer1Turn { get; set; } = true;
        public int Player1Hits { get; set; } = 0;
        public int Player2Hits { get; set; } = 0;

        public ObservableCollection<BattleCell> BoardPlayer1 { get; set; }
        public ObservableCollection<BattleCell> BoardPlayer2 { get; set; }

        private readonly Random _rand = new Random();

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
                while (!isPlaced)
                {
                    int startX = _rand.Next(0, 10);
                    int startY = _rand.Next(0, 10);
                    bool isHorizontal = _rand.Next(0, 2) == 0;

                    if (CanPlaceShip(board, startX, startY, size, isHorizontal))
                    {
                        for (int i = 0; i < size; i++)
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
            if (isHorizontal && startX + size > 10) return false;
            if (!isHorizontal && startY + size > 10) return false;

            for (int i = 0; i < size; i++)
            {
                int x = isHorizontal ? startX + i : startX;
                int y = isHorizontal ? startY : startY + i;

                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        int checkX = x + dx;
                        int checkY = y + dy;

                        if (checkX >= 0 && checkX < 10 && checkY >= 0 && checkY < 10)
                        {
                            int index = checkY * 10 + checkX;
                            if (board[index].State != CellState.Empty)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
    }

}
