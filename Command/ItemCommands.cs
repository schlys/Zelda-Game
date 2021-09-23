using System;
using System.Collections.Generic;
using System.Text;
using Project1.ItemComponents; 

namespace Project1.Command
{
    // Change so operate on a single Item given when initialized, not 'Item' of Game 
    class PreviousItemCmd : ICommand
    {
        public Game1 Game { get; set; }
        public IItem Item { get; set; }
        public PreviousItemCmd(IItem item)
        {
            Item = item; 
        }

        public void Execute()
        {
            Item.PreviousItem();
        }
    }

    class NextItemCmd : ICommand
    {
        public Game1 Game { get; set; }
        public IItem Item { get; set; }


        public NextItemCmd(IItem item)
        {
            Item = item; 
        }

        public void Execute()
        {
            Item.NextItem();
        }
    }
}
