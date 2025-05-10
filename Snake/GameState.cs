using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class GameState
    {
        public int Rows { get; }
        public int Cols { get; }

        // the comma inside [,] means it's a multi-dimensional array, specifically a 2D array
        public GridValue[,] Grid { get; }
        public Direction Dir { get; private set; }
        public int Score { get; private set; }
        public bool GameOver { get; private set; }

        private readonly LinkedList<Position> snakePositions = new LinkedList<Position>();

        // select a random position where the food is genereated
        private readonly Random random = new Random();

        
        public GameState(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Grid = new GridValue[rows, cols];
            Dir = Direction.Right;

            AddSnake();
            AddFood();
        }

        private void AddSnake()
        {
            int r = Rows / 2;

            for (int c = 1; c <= 3; c++ )
            {
                Grid[r, c] = GridValue.Snake;
                snakePositions.AddFirst(new Position(r, c));
            }
        }

        private IEnumerable<Position> EmptyPositions()
        {
            for (int r= 0; r< Rows; r++)
            {

                for (int c = 0; c < Cols; c++ )
                {
                    if (Grid[r,c ] == GridValue.Empty)
                    {
                        //yield keyword is used in iterator blocks to simplify the creation of enumerators. It allows a method to return elements one at a time without creating and managing an intermediate collection or implementing IEnumerator manually.
                        yield return new Position(r, c);
                    }
                }
            }
        }

        private void AddFood()
        {
            List<Position> empty = new List<Position>(EmptyPositions());

            if (empty.Count == 0)
            {
                return;
            }

            Position pos = empty[random.Next(empty.Count)];
            Grid[pos.Row, pos.Col] = GridValue.Food;
        }

        // Get Snake head helper method
        public Position HeadPosition()
        {
            return snakePositions.First.Value;
        }

        // Get Sanke tail hepler method
        public Position TailPosition()
        {
            return snakePositions.Last.Value;
        }

        // return all snake positons
        public IEnumerable<Position> SnakePositions()
        {
            return snakePositions;
        }

        private void AddHead(Position pos)
        {
            snakePositions.AddFirst(pos);
            Grid[pos.Row, pos.Col] = GridValue.Snake;
        }

        private void RemoveTail()
        {
            Position tail = snakePositions.Last.Value;
            Grid[tail.Row, tail.Col] = GridValue.Empty;
            snakePositions.RemoveLast();
        }

        public void ChangeDirection (Direction dir)
        {
            Dir = dir;
        }

        // check if the snake hits the boudaries of the grid
        private bool OutsideGrid( Position pos)
        {
            return pos.Row < 0 || pos.Row >= Rows || pos.Col < 0 || pos.Col >= Cols;
        }

        private GridValue WillHit(Position newHeadPos)
        {
            // check if the snake hits the boundaries of the grid
            if (OutsideGrid(newHeadPos))
            {
                return GridValue.Outside;
            }

            // if the head position is the same as the tail position, then it means the snake is not hitting itself
            if (newHeadPos == TailPosition())
            {
                return GridValue.Empty;
            }
            return Grid[newHeadPos.Row, newHeadPos.Col];
        }

        public void Move()
        {
            Position newHeadPos = HeadPosition().Translate(Dir);
            GridValue hit = WillHit(newHeadPos);

            if (hit == GridValue.Outside  || hit == GridValue.Snake)
            {
                GameOver = true;
            } 
            else if ( hit == GridValue.Empty)
            {
                RemoveTail();
                AddHead(newHeadPos);
            }
            else if (hit == GridValue.Food)
            {
                AddHead(newHeadPos);
                Score++;
                AddFood();
            }
        }
    }
}
