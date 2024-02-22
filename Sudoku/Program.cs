using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class Program
    {
        //Game generation-related methods
        //Populates the grid with random values for the given section. The current time is used as seed to ensure the game's uniqueness
        static void PopulateGrid(int[,] grid, int imin, int imax, int jmin, int jmax)
        {
            List<int> numbers = new List<int>();
            for (int i = 1; i <= 9; i++)
                numbers.Add(i);
            int seed = (int)DateTime.Now.Ticks;
            Random rnd = new Random(seed+imin*jmin+imax*jmax);
            for (int i = imin; i <= imax; i++)
                for (int j = jmin; j <=jmax; j++)
                {
                    int index = rnd.Next(0, numbers.Count());
                    grid[i,j] = numbers[index];
                    numbers.RemoveAt(index);
                }
        }

        //Checks if a given value is encountered throughout its row
        static bool CheckRow(int[,] grid, int i, int j)
        {
            for(int n=0; n<9; n++)
                if (grid[i, n] == grid[i,j] && n!=j)
                    return false;
            return true;

        }

        //Checks if a given value is encountered throughout its column
        static bool CheckColumn(int[,] grid, int i, int j)
        {
            for (int m = 0; m < 9; m++)
                if (grid[m, j] == grid[i, j] && m != i)
                    return false;
            return true;
        }

        //Checks if a given value is encountered throughout its square
        static bool CheckSquare(int[,] grid, int i, int j)
        {
            //Gets the coordinates of the upper left corner
            int row = (i / 3) * 3;
            int col = (j / 3) * 3;
            for(int m=row; m<row+3; m++)
                for(int n=col; n<col+3; n++)
                    if (grid[m, n] == grid[i, j] && (m!=i || n!=j))
                        return false;
            return true;
        }
        
        //Checks if a value applied to a certain square is correct
        static bool IsCorrect(int[,] grid, int i, int j)
        {
            return (CheckRow(grid, i, j) && CheckColumn(grid, i, j) && CheckSquare(grid, i, j));
        }

        //Completes the grid using backtracking
        static bool CompleteGrid(int[,] grid, List<int> positions, int index)
        {
            if(index>=positions.Count) 
                return true;
            int i = positions[index] / 9;
            int j = positions[index] % 9;
            for(int num=1; num<=9; num++)
            {
                grid[i, j] = num;
                if(IsCorrect(grid, i, j) && CompleteGrid(grid, positions, index+1))
                    return true;
            }
            grid[i, j] = 0;
            return false;
        }

        //Creates the grid
        static int[,] GenerateGrid()
        {
            int[,] grid = new int[9,9];
            //Populates the three diagonal squares(they are not related to eachother, so there are no restrictions)
            PopulateGrid(grid, 0, 2, 0, 2);
            PopulateGrid(grid, 3, 5, 3, 5);
            PopulateGrid(grid, 6, 8, 6, 8);
            List<int> positions = new List<int>();
            for (int i = 0; i < 81; i++)
                positions.Add(i);
            //Removes the squares that are already completed
            positions.Remove(0);
            positions.Remove(1);
            positions.Remove(2);
            positions.Remove(9);
            positions.Remove(10);
            positions.Remove(11);
            positions.Remove(18);
            positions.Remove(19);
            positions.Remove(20);
            positions.Remove(30);
            positions.Remove(31);
            positions.Remove(32);
            positions.Remove(39);
            positions.Remove(40);
            positions.Remove(41);
            positions.Remove(48);
            positions.Remove(49);
            positions.Remove(50);
            positions.Remove(60);
            positions.Remove(61);
            positions.Remove(62);
            positions.Remove(69);
            positions.Remove(70);
            positions.Remove(71);
            positions.Remove(78);
            positions.Remove(79);
            positions.Remove(80);
            int index = 0;
            CompleteGrid(grid, positions, index);
            return grid;
        }

        //User-related methods
        static int GetDifficulty()
        {
            Console.WriteLine("Choose the difficulty!(1 for Easy | 2 for Medium | 3 for Hard | 4 for Expert)");
            int difficulty=Convert.ToInt32(Console.ReadLine());
            return difficulty;
        }

        //(not so)Randomly removes squares from the completed grid based on the difficulty.
        static int[,] GenerateUserGrid(int difficulty, int[,] grid)
        {
            int toRemove=0;
            switch (difficulty) 
            {
                case 1:
                    toRemove=40;
                    break;
                case 2:
                    toRemove = 45;
                    break;
                case 3:
                    toRemove = 50;
                    break;
                case 4:
                    toRemove = 55;
                    break;
                default:
                    Console.WriteLine("Invalid input!");
                    return grid;
            }
            int[,] userGrid = new int[9, 9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    userGrid[i, j] = grid[i, j];
            int toRemovePerLine = toRemove / 9;
            int seed = (int)DateTime.Now.Ticks;
            Random rand = new Random(seed);
            //Erases squares from every row in order to obtain a balanced grid that can be completed
            for (int i=0; i<9; i++)
            {
                HashSet<int> set = new HashSet<int>();
                while(set.Count<toRemovePerLine)
                    set.Add(rand.Next()%9);
                foreach (int j in set)
                    userGrid[i,j] = 0;
            }
            toRemove -= (9 * toRemovePerLine);
            //Randomly removes additional squares if needed
            while(toRemove > 0)
            {
                int index=rand.Next()%81;
                int i = index / 9;
                int j= index % 9;
                if (userGrid[i,j]!=0)
                {
                    userGrid[i,j] = 0;
                    toRemove--;
                }
            }
            return userGrid;
        }

        //Displays the user's board
        static void PrintBoard(int[,] userBoard)
        {
            for(int i=0; i<9; i++)
            {
                for (int j = 0; j < 9; j++)
                    if (userBoard[i, j] == 0)
                        Console.Write("  ");
                    else
                        Console.Write(userBoard[i, j] + " ");
                Console.WriteLine();
            }
        }

        //Checks if the user's input is the correct answer
        static bool CheckInput(int[,] grid, int i, int j, int val)
        {
            return (grid[i,j] == val);
        }

        //Handles the user's input
        static bool GetInput(int[,] grid, int[,] userBoard)
        {
            //Checks if the user wants to end the game before its completion
            Console.WriteLine("Insert 0 to end the game!(press any key to continue)");
            string s=Console.ReadLine();
            if (s == "0")
                return true;
            bool validRow = false;
            int i=0, j=0;
            //Ensures that the given row is correct
            while (!validRow)
            {
                Console.WriteLine("Insert row!");
                i = Convert.ToInt32(Console.ReadLine());
                if (i < 0 || i > 8)
                    Console.WriteLine("Invalid input!");
                else
                    validRow = true;
            }
            bool validCol = false;
            //Ensures that the given column is correct
            while (!validCol)
            {
                Console.WriteLine("Insert column!");
                j = Convert.ToInt32(Console.ReadLine());
                if(j<0 || j > 8)
                    Console.WriteLine("Invalid input!");
                else
                    validCol = true;
            }
            bool validVal = false;
            int val = 0;
            if (userBoard[i, j] == 0)
                //Ensures that the given value is between 1 and 9. If the value is wrong, it displays a message and takes no action
                while (!validVal)
                {
                    Console.WriteLine("Insert value!");
                    val = Convert.ToInt32(Console.ReadLine());
                    if (val <= 0 || val > 9)
                        Console.WriteLine("Invalid input!");
                    else if (!CheckInput(grid, i, j, val))
                    {
                        Console.WriteLine("Wrong number!");
                        return false;
                    }
                    else
                    {
                        validVal = true;
                        userBoard[i, j] = val;
                    }
                }
             return false;
        }

        //Checks if the user has completed the board
        static bool GameOver(int[,] userGrid)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (userGrid[i, j] == 0)
                        return false;
            return true;
        }

        //Displays the final message based on the cause of its ending
        static void FinalMessage(bool userEnded)
        {
            if (userEnded)
                Console.WriteLine("You ended the game! Game over!");
            else
                Console.WriteLine("Congratulations! You won!");
        }

        static void Main(string[] args)
        {
            Board board = new Board();
            //printBoard(board.hiddenBoard);
            board.PopulateSquare(1);
            board.PopulateSquare(5);
            board.PopulateSquare(9);
            board.CompleteBoard(0);
            board.GenerateUserBoard(0);
            UserInterface.PrintBoard(board.userBoard);
            UserInterface.PrintFinalMessage(false);
            /*
            int difficulty = GetDifficulty();
            //Ensures that the given difficulty is correct
            while(difficulty<1 || difficulty > 4)
            {
                Console.WriteLine("Invalid input!");
                difficulty=GetDifficulty();
            }
            bool userEnded=false;
            int[,] grid = GenerateGrid();
            int[,] userGrid=GenerateUserGrid(difficulty, grid);
            while(!GameOver(userGrid) && !userEnded)
            {
                PrintBoard(userGrid);
                //Ends the game if the user asks for that
                userEnded=GetInput(grid, userGrid);
            }
            FinalMessage(userEnded);
            */
        }
    }
}
