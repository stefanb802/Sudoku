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

            
        }
    }
}
