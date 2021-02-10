using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public class Field 
    {
        public string btnName;
        public bool occupied;
        public string occupiedBy;

        public Field(string btnName, bool isOccupied, string isOccupiedBy)
        {
            this.btnName = btnName;
            this.occupied = isOccupied;
            this.occupiedBy = isOccupiedBy;
        }
        
    }
}
