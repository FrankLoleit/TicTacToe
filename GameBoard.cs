using System.Collections.Generic;
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
