//Handles all the user-related actions, such as displaying the board, getting the input etc.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal static class UserInterface
    {
        public static void PrintBoard(int[,] userBoard)
        {
            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    if (userBoard[i, j] != 0)
                        Console.Write(userBoard[i, j] + " ");
                    else
                        Console.WriteLine("  ");
                Console.WriteLine();
            }
        }

        public static int[] GetPosition()
        {
            int[] position = new int[2];
            Console.WriteLine("Insert row!");
            position[0] = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Insert column!");
            position[1] = Convert.ToInt32(Console.ReadLine());
            return position;
        }

        public static int GetValue()
        {
            Console.WriteLine("Insert the value!");
            int val=Convert.ToInt32(Console.ReadLine());
            return val;
        }

        public static int GetDifficulty()
        {
            Console.WriteLine("Choose the difficulty!(1 for Easy | 2 for Medium | 3 for Hard | 4 for Expert)");
            int difficulty = Convert.ToInt32(Console.ReadLine());
            return difficulty;
        }

        public static void PrintFinalMessage(bool forceQuit)
        {
            if (forceQuit)
                Console.WriteLine("Game over! User ended the game!");
            else
                Console.WriteLine("Congratulations! You Won!");
        }

    }
}
