using System.Collections.Generic;

namespace TicTacToe
{
    public class GameLogic
    {
        public string sign;
        //public static int X_wins = 0;
        //public static int O_wins = 0;
        public static bool gameOver = false;

        public int[] winningRow = { -1, -1, -1 };
        public static int[,] rows =
        {
            {0, 1, 2 }, //Horizontal
            {3, 4, 5 },
            {6, 7, 8 },

            {0, 3, 6 }, //Vertical
            {1, 4, 7 },
            {2, 5, 8 },

            {0, 4, 8 }, //Diagonal
            {2, 4, 6 },
        };

        public bool MoveAllowed(string btnName, List<Field> fields, string sign)
        {
            Field field = fields.Find(item => item.btnName == btnName);
            if (field.occupied)
                return false;
            else
                return true;
        }

        public bool Draw(List<Field> fields)
        {
            foreach (var field in fields)
            {
                if (!field.occupied)
                    return false;
            }
            gameOver = true;
            return true;
        }

        public bool FullRowFound(List<Field> fields)
        {
            for(int i = 0; i<rows.GetLength(0); i++)
            {
                int amountX_inRow = 0;
                int amountO_inRow = 0;

                for(int j = 0; j<rows.GetLength(1); j++)
                {
                    if (!fields[rows[i,j]].occupied)
                        break;
                    string btnContent = fields[rows[i, j]].occupiedBy;
                    switch (btnContent)
                    {
                        case "X":
                            amountX_inRow++;
                            break;
                        case "O":
                            amountO_inRow++;
                            break;
                        default:
                            break;
                    }
                    winningRow[j] = rows[i, j];
                    if (amountX_inRow == 3 || amountO_inRow == 3)
                    {
                        gameOver = true;
                        return true;
                    }
                }
            }
            for (int i = 0; i < 3; i++)
                winningRow[i] = -1;
            return false;
        }
    }
}
    
        
    

