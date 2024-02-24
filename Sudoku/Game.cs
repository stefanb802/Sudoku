//Handles the game logic
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class Game
    {
        public int difficulty;
        public bool GameWon(int[,] userBoard)
        {
            for(int i = 0; i<9; i++)
                for(int j = 0; j<9; j++)
                    if (userBoard[i,j]==0)
                        return false;
            return true;
        }

        public bool IsCorrect(int[,] hiddenBoard, int[] position, int value)
        {
            return (hiddenBoard[position[0], position[1]] == value); 
        }

        public Game() 
        {
            Console.WriteLine("Hello! You are playing Sudoku!");
            difficulty = UserInterface.GetDifficulty();
            Testing tested = new Testing(difficulty);
            bool forceQuit=true;
            while(!GameWon(tested.solvable.userBoard))
            {
                UserInterface.PrintBoard(tested.solvable.userBoard);
                forceQuit = UserInterface.ForceQuit();
                if (forceQuit)
                    break;
                int[] position = new int[2];
                position = UserInterface.GetPosition();
                int value = UserInterface.GetValue();
                if (IsCorrect(tested.solvable.hiddenBoard, position, value))
                    tested.solvable.userBoard[position[0], position[1]] = value;
                else
                    Console.WriteLine("Wrong value!");
            }

            UserInterface.PrintBoard(tested.solvable.hiddenBoard);
            UserInterface.PrintFinalMessage(forceQuit);
        }
    }
}
