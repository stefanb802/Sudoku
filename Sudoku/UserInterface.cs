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
        public const int EASY = 1;
        public const int MEDIUM = 2;
        public const int HARD = 3;
        public static void PrintBoard(int[,] userBoard)
        {
            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    if (userBoard[i, j] != 0)
                        Console.Write(userBoard[i, j] + " ");
                    else
                        Console.Write("  ");
                Console.WriteLine();
            }
        }

        public static int[] GetPosition()
        {
            int[] position = new int[2];
            do
            {
                try
                {
                    Console.WriteLine("Insert row!");
                    position[0] = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Insert column!");
                    position[1] = Convert.ToInt32(Console.ReadLine());
                    if (position[0] < 0 || position[0] > 8 || position[1] < 0 || position[1] > 8)
                        throw new ArgumentOutOfRangeException();
                    return position;
                }
                catch(ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Invalid input! Insert values between 1 and 8!");
                }
                catch(FormatException)
                {
                    Console.WriteLine("Invalid input!");
                }

            } while(true);
        }

        public static int GetValue()
        {
            do
            {
                try
                {
                    Console.WriteLine("Insert the value!");
                    int val = Convert.ToInt32(Console.ReadLine());
                    if (val < 1 || val > 9)
                        throw new ArgumentOutOfRangeException();
                    return val;
                }
                catch (FormatException) 
                {
                    Console.WriteLine("Invalid input!");
                }
                catch(ArgumentOutOfRangeException) 
                {
                    Console.WriteLine("Invalid value! Insert a number between 1 and 9!");
                }
            } while (true);
        }

        public static int GetDifficulty()
        {
            do
            {
                try
                {
                    Console.WriteLine("Choose the difficulty!(1 for Easy | 2 for Medium | 3 for Hard)");
                    int difficulty = Convert.ToInt32(Console.ReadLine());
                    if (difficulty < EASY || difficulty > HARD)
                        throw new ArgumentOutOfRangeException();
                    return difficulty;
                }
                catch(FormatException) 
                {
                    Console.WriteLine("Invalid input!");
                }
                catch(ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Invalid value! Insert a value between 1 and 4!");
                }
            } while(true);
        }

        public static void PrintFinalMessage(bool forceQuit)
        {
            if (forceQuit)
                Console.WriteLine("Game over! User ended the game!");
            else
                Console.WriteLine("Congratulations! You Won!");
        }

        public static bool ForceQuit()
        {
            Console.WriteLine("Do you want to quit?[press y for yes | press any key for no]");
            string ans=Console.ReadLine();
            return (ans == "y");
        }

    }
}
