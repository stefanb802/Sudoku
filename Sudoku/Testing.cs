//Ensures that the generated grid is solvable. A correct sudoku grid should have an unique solution 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class Testing
    {
        public int difficulty;
        public Board solvable;

        //Completing the grid using backtracking
        public bool TestingIsCorrect(int[,] testingBoard,int m, int n)
        {
            for (int x = 0; x < 9; x++)
                if (testingBoard[m, x] == testingBoard[m, n] && x != n)
                    return false;
                else if (testingBoard[x, n] == testingBoard[m, n] && x != m)
                    return false;
            int row = (m / 3) * 3;
            int col = (n / 3) * 3;
            for (int i = row; i < row + 3; i++)
                for (int j = col; j < col + 3; j++)
                    if (testingBoard[m, n] == testingBoard[i, j] && (m != i || n != j))
                        return false;
            return true;
        }

        public bool TestingCompleteBoard(int[,] testingBoard, List<int> emptyPositions, int index)
        {
            if (index >= emptyPositions.Count)
                return true;
            int i = emptyPositions[index] / 9;
            int j = emptyPositions[index] % 9;
            for (int num = 1; num <= 9; num++)
            {
                testingBoard[i, j] = num;
                if (TestingIsCorrect(testingBoard,i, j) && TestingCompleteBoard(testingBoard, emptyPositions,index + 1))
                    return true;
            }
            testingBoard[i, j] = 0;
            return false;
        }

        public List<int> GetEmptyPositions(int[,] testingBoard, int[,] userBoard)
        {
            List<int> result=new List<int>();
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    testingBoard[i, j] = userBoard[i, j];
                    if (userBoard[i, j] == 0)
                        result.Add(9 * i + j);
                }
            return result;
        }

        //If the testing solution is not identical to the initial one, the board is flawed
        public bool CompareSolutions(int[,] testingBoard, int[,] hiddenBoard)
        {
            for(int i=0; i<9; i++)
                for(int j=0; j<9; j++)
                    if (testingBoard[i,j] != hiddenBoard[i,j])
                        return false;
            return true;
        }
        public Testing(int difficulty)
        {
            this.difficulty = difficulty;
            do
            {
                try
                {
                    solvable = new Board();
                    //Populating the three diagonal squares because they are not interdependent
                    solvable.PopulateSquare(1);
                    solvable.PopulateSquare(5);
                    solvable.PopulateSquare(9);
                    solvable.CompleteBoard(0);
                    solvable.GenerateUserBoard(35 + 5 * this.difficulty);
                    int[,] testingBoard = new int[9, 9];
                    List<int> emptyPositions = GetEmptyPositions(testingBoard, solvable.userBoard);
                    //Getting the tesing solution
                    TestingCompleteBoard(testingBoard, emptyPositions, 0);
                    //Regenerates the grid if flawed
                    if (!CompareSolutions(testingBoard, solvable.hiddenBoard))
                        throw new ArgumentException();
                    break;
                }
                catch (ArgumentException)
                {
                    //Console.WriteLine("X");
                }
            } while (true);
        }
    }
}
