using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TicTacToe
{
    public class GameBoard
    {
        int amountFields = 9;
        public List<Field> fields = new List<Field>();

        public GameBoard()
        {
            for (int i = 0; i < amountFields; i++)
            {
                fields.Add(new Field(("Button" + (i + 1).ToString()), false, ""));
            }
        }
    }
}
