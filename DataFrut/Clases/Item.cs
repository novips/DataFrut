using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFrut.Clases
{
    public class Item
    {
        public string Name;
        public int Value;
        public Item(string name, int value)
        {
            Name = name; Value = value;
        }

        public Item()
        {

        }
        public override string ToString()
        {
            // Generates the text shown in the combo box
            return Name;
        }
        
    }
}
