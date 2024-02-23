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
            Board board=new Board();
            //Populating the 3 diagonal squares because they are not interdependent
            board.PopulateSquare(1);
            board.PopulateSquare(5);
            board.PopulateSquare(9);
            board.CompleteBoard(0);
            board.GenerateUserBoard(35);
            //UserInterface.PrintBoard(board.hiddenBoard);
            bool forceQuit=true;
            while(!GameWon(board.userBoard))
            {
                UserInterface.PrintBoard(board.userBoard);
                forceQuit = UserInterface.ForceQuit();
                if (forceQuit)
                    break;
                int[] position = new int[2];
                position = UserInterface.GetPosition();
                int value = UserInterface.GetValue();
                if (IsCorrect(board.hiddenBoard, position, value))
                    board.userBoard[position[0], position[1]] = value;
                else
                    Console.WriteLine("Wrong value!");
            }
            UserInterface.PrintBoard(board.hiddenBoard);
            UserInterface.PrintFinalMessage(forceQuit);
        }
    }
}
