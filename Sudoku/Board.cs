//Creates both the hidden board and the board that is diplayed to the user. Can randomly populate any square on the board(square=3x3). Keeps track of the empty positions in order to complete the solution using backtracking.
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class Board
    { 
        public int[,] hiddenBoard;
        public int[,] userBoard;
        public List<int> emptyPositions;
        public Board()
        {
            hiddenBoard = new int[9, 9];
            userBoard = new int[9, 9];
            emptyPositions = new List<int>();
            for(int i = 0; i < 81; i++) 
            {
                emptyPositions.Add(i);
            }
        }

        //Randomly populate a square(I have chosen the time and the n*sqrt(n) as seed to make it more random). 
        public void PopulateSquare(int n)
        {
            int seed = (int)DateTime.Now.Ticks;
            Random rnd = new Random(seed + n*Convert.ToInt32(Math.Sqrt(n)));
            List<int> numbers = new List<int>();
            for (int i = 1; i <= 9; i++)
                numbers.Add(i);
            int rowStart = (n - 1) / 3 * 3;
            int colStart = (n - 1) % 3 * 3;
            for (int i = rowStart; i<rowStart+3; i++)
                for (int j = colStart; j<colStart+3; j++)
                {
                    int index = rnd.Next(0, numbers.Count());
                    hiddenBoard[i, j] = numbers[index];
                    numbers.RemoveAt(index);
                    emptyPositions.Remove(9*i+j);
                }
        }

        //Checks if the value of a cell is correct
        public bool IsCorrect(int m, int n)
        {
            for (int x = 0; x < 9; x++)
                if (hiddenBoard[m, x] == hiddenBoard[m, n] && x != n)
                    return false;
                else if (hiddenBoard[x, n] == hiddenBoard[m,n] && x !=m)
                    return false;
            int row=(m/3)*3;
            int col=(n/3)*3;
            for(int i=row; i< row+3; i++)
                for(int j=col; j< col+3; j++)
                    if (hiddenBoard[m, n] == hiddenBoard[i,j] && (m!=i || n!=j))
                        return false;
            return true;
        }

        //Completes the hidden board using backtracking
        public bool CompleteBoard(int index)
        {
            if (index >= emptyPositions.Count)
                return true;
            int i = emptyPositions[index] / 9;
            int j = emptyPositions[index] % 9;
            for (int num = 1; num <= 9; num++)
            {
                hiddenBoard[i, j] = num;
                if (IsCorrect(i, j) && CompleteBoard(index + 1))
                    return true;
            }
            hiddenBoard[i, j] = 0;
            return false;
        }

        //Randomly erases n values on the user's board based on the difficulty
        public void GenerateUserBoard(int toRemove)
        {
            for(int i=0; i<9; i++)
                for(int j=0; j < 9; j++)
                    userBoard[i, j] = hiddenBoard[i,j];
            int toRemovePerLine = toRemove / 9;
            int seed = (int)DateTime.Now.Ticks;
            Random rand = new Random(seed);
            //Erases squares from every row in order to obtain a balanced grid that can be completed
            for (int i = 0; i < 9; i++)
            {
                HashSet<int> set = new HashSet<int>();
                while (set.Count < toRemovePerLine)
                    set.Add(rand.Next() % 9);
                foreach (int j in set)
                    userBoard[i, j] = 0;
            }
            toRemove -= (9 * toRemovePerLine);
            //Randomly removes additional squares if needed
            while (toRemove > 0)
            {
                int index = rand.Next() % 81;
                int i = index / 9;
                int j = index % 9;
                if (userBoard[i, j] != 0)
                {
                    userBoard[i, j] = 0;
                    toRemove--;
                }
            }
        }
    }
}
