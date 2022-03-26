using System;
using System.Collections.Generic;

namespace LeoReview
{
    public class Model : IObservableModel
    {
        private static readonly Random _random;

        private readonly List<IObserver> _observers;
        private readonly int[][] _tiles;
        private readonly List<Cell> _emptyCells;

        static Model()
        {
            _random = new Random();
        }

        public Model(int fieldSize)
        {
            _emptyCells = new List<Cell>();
            _observers = new List<IObserver>();
            _tiles = new int[fieldSize][];

            for (int i = 0; i < fieldSize; i++)
                _tiles[i] = new int[fieldSize];

            InitializeTiles();
        }

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);

            NotifyObservers();
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            _observers.ForEach((IObserver observer) =>
            {
                observer.Update(_tiles);
            });
        }

        public bool MoveTiles(Direction direction)
        {
            switch (direction)
            {
                case Direction.DownToUp:
                    MoveTilesUp();
                    break;

                case Direction.UpToDown:
                    MoveTilesDown();
                    break;

                case Direction.LeftToRight:
                    MoveTilesRight();
                    break;

                case Direction.RightToLeft:
                    MoveTilesLeft();
                    break;

                default:
                    break;
            }

            AddToEmptyCell();
            NotifyObservers();

            return CheckGameContinue();
        }

        private bool CheckZeros(int start, Direction direction, int row, int column)
        {
            int x = direction == Direction.UpToDown || direction == Direction.LeftToRight ? start : 0;
            int y = direction == Direction.UpToDown || direction == Direction.LeftToRight ? _tiles.Length : start + 1;

            return CheckTilesForZeros(x, y, direction, row, column);
        }

        private bool CheckTilesForZeros(int start, int end, Direction direction, int row, int column)
        {
            int x;
            int y;

            for (int i = start; i < end; i++)
            {
                x = direction == Direction.UpToDown || direction == Direction.DownToUp ? i : row;
                y = direction == Direction.UpToDown || direction == Direction.DownToUp ? column : i;

                if (_tiles[x][y] != 0)
                    return false;
            }

            return true;
        }

        private void MoveTilesDown()
        {
            for (int j = 0; j < _tiles.Length; j++)
            {
                int i = _tiles.Length - 1;

                while (i > 0)
                {
                    if (CheckZeros(i-1, Direction.DownToUp, 0, j))///0
                        break;

                    if (_tiles[i][j] == 0)
                    {
                        MoveColumn(j, i, 0);

                        continue;
                    }

                    if (_tiles[i][j] == _tiles[i - 1][j])
                    {
                        _tiles[i][j] *= 2;
                        _tiles[i - 1][j] = 0;

                        i--;

                        continue;
                    }
                    else
                    {
                        if (_tiles[i - 1][j] == 0)
                            MoveColumn(j, i - 1, 0);
                        else
                            i--;
                    }
                }
            }
        }

        private void MoveTilesUp()
        {
            for (int j = 0; j < _tiles.Length; j++)
            {
                int i = 0;

                while (i < _tiles.Length)
                {
                    if (CheckZeros(i + 1, Direction.UpToDown, 0, j))/////0
                        break;

                    if (_tiles[i][j] == 0)
                    {
                        MoveColumn(j, i, _tiles.Length - 1);

                        continue;
                    }

                    if (_tiles[i][j] == _tiles[i + 1][j])
                    {
                        _tiles[i][j] *= 2;
                        _tiles[i + 1][j] = 0;

                        i++;

                        continue;
                    }
                    else
                    {
                        if (_tiles[i + 1][j] == 0)
                            MoveColumn(j, i + 1, _tiles.Length - 1);
                        else
                            i++;
                    }
                }
            }
        }

        private void MoveColumn(int column, int from, int to)
        {
            if (from > to)
            {
                for (int i = from; i > to; i--)
                    _tiles[i][column] = _tiles[i - 1][column];
            }
            else
            {
                for (int i = from; i < to; i++)
                    _tiles[i][column] = _tiles[i + 1][column];
            }

            _tiles[to][column] = 0;
        }

        private void MoveRow(int row, int from, int to)
        {
            if (from > to)
            {
                for (int j = from; j > to; j--)
                    _tiles[row][j] = _tiles[row][j - 1];
            }
            else
            {
                for (int j = from; j < to; j++)
                    _tiles[row][j] = _tiles[row][j + 1];
            }

            _tiles[row][to] = 0;
        }

        private void MoveTilesRight()
        {
            for (int i = 0; i < _tiles.Length; i++)
            {
                int j = _tiles.Length - 1;

                while (j > 0)
                {
                    if (CheckZeros(j - 1, Direction.RightToLeft, i, 0))//0
                        break;

                    if (_tiles[i][j] == 0)
                    {
                        MoveRow(i, j, 0);

                        continue;
                    }

                    if (_tiles[i][j] == _tiles[i][j - 1])
                    {
                        _tiles[i][j] *= 2;
                        _tiles[i][j - 1] = 0;

                        j--;

                        continue;
                    }
                    else
                    {
                        if (_tiles[i][j - 1] == 0)
                            MoveRow(i, j - 1, 0);
                        else
                            j--;
                    }
                }
            }
        }

        private void MoveTilesLeft()
        {
            for (int i = 0; i < _tiles.Length; i++)
            {
                int j = 0;

                while (j < _tiles.Length)
                {
                    if (CheckZeros(j + 1, Direction.LeftToRight, i, 0))///0
                        break;

                    if (_tiles[i][j] == 0)
                    {
                        MoveRow(i, j, _tiles.Length - 1);

                        continue;
                    }

                    if (_tiles[i][j] == _tiles[i][j + 1])
                    {
                        _tiles[i][j] *= 2;
                        _tiles[i][j + 1] = 0;

                        j++;

                        continue;
                    }
                    else
                    {
                        if (_tiles[i][j + 1] == 0)
                            MoveRow(i, j + 1, 0);
                        else
                            j++;
                    }
                }
            }
        }

        private bool CheckGameContinue()
        {
            return _emptyCells.Count != 0 || HasVerticalMoves() || HasHorizontalMoves();
        }

        private bool HasVerticalMoves()
        {
            for (int i = 0; i < _tiles.Length - 1; i++)
                for (int j = 0; j < _tiles.Length; j++)
                    if (_tiles[i][j] == _tiles[i + 1][j])
                        return true;

            return false;
        }

        private bool HasHorizontalMoves()
        {
            for (int i = 0; i < _tiles.Length; i++)
                for (int j = 0; j < _tiles.Length - 1; j++)
                    if (_tiles[i][j] == _tiles[i][j + 1])
                        return true;

            return false;
        }

        private void InitializeTiles()
        {
            for (int i = 0; i < _tiles.Length; i++)
                for (int j = 0; j < _tiles.Length; j++)
                    _tiles[i][j] = 0;

            AddToEmptyCell();
        }

        private void AddToEmptyCell()
        {
            GetEmptyCells();

            if (_emptyCells.Count > 0)
            {
                int index = _random.Next(_emptyCells.Count);
                Cell cell = _emptyCells[index];
                int chance = _random.Next(1, 11);
                int value = 1;

                if (chance == 10)
                    value = 2;

                _tiles[cell.X][cell.Y] = (int)Math.Pow(2.0, value);
            }
        }

        private void GetEmptyCells()
        {
            _emptyCells.Clear();

            int outerIndex = 0;

            foreach (int[] row in _tiles)
            {
                int innerIndex = 0;

                foreach (int cell in row)
                {
                    if (cell == 0)
                        _emptyCells.Add(new Cell(outerIndex, innerIndex));

                    innerIndex++;
                }

                outerIndex++;
            }
        }
    }
}
