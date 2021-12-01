using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;
using Project1.LinkComponents; 

namespace Project1.Command
{
    public class LinkMoveUpCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }
        public LinkMoveUpCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link; 
        }

        public void Execute()
        {
            Link.MoveUp();
        }
    }

    public class LinkMoveDownCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkMoveDownCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }

        public void Execute()
        {
            Link.MoveDown();
        }
    }

    public class LinkMoveRightCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkMoveRightCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }

        public void Execute()
        {
            Link.MoveRight();
        }
    }

    public class LinkMoveLeftCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkMoveLeftCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }

        public void Execute()
        {
            Link.MoveLeft();
        }
    }

    public class LinkStopMotionCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkStopMotionCmd(ICollidable link)
        {
            Link = (ILink)link;
        }

        public void Execute()
        {
            Link.StopMotion();
        }
    }

    // TODO: remove no longer use? 
    
    public class LinkUseItem1Cmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkUseItem1Cmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.UseItem(1);
        }
    }
    public class LinkUseItem2Cmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkUseItem2Cmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.UseItem(2);
        }
    }

    public class LinkResetCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkResetCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.Reset();
        }
    }


    public class LinkItemSelectUpCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkItemSelectUpCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.Inventory.ItemUp();
        }
    }

    public class LinkItemSelectDownCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkItemSelectDownCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.Inventory.ItemDown();
        }
    }
    public class LinkItemSelectLeftCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkItemSelectLeftCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.Inventory.ItemLeft();
        }
    }
    public class LinkItemSelectRightCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkItemSelectRightCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.Inventory.ItemRight();
        } 
    }
    public class LinkDropItem1Cmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkDropItem1Cmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.Inventory.DropItem1();
        }
    }

    public class LinkSelectItemCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkSelectItemCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.Inventory.SelectItem();
        }
    }

    public class LinkSelectItem1Cmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkSelectItem1Cmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.Inventory.SelectItem(1);
        }
    }

    public class LinkSelectItem2Cmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkSelectItem2Cmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.Inventory.SelectItem(2);
        }
    }

    public class LinkPurchaseItem1Cmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkPurchaseItem1Cmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.Store.PurchaseItem1();
        }
    }

    public class LinkPurchaseItem2Cmd : ICommand
    {
        public ILink Link { get; set; }

        public LinkPurchaseItem2Cmd(Game1 game, ILink link)
        {
            Link = link;
        }
        public void Execute()
        {
            Link.Store.PurchaseItem2();
        }
    }

    public class LinkPurchaseItem3Cmd : ICommand
    {
        public ILink Link { get; set; }

        public LinkPurchaseItem3Cmd(Game1 game, ILink link)
        {
            Link = link;
        }
        public void Execute()
        {
            Link.Store.PurchaseItem3();
        }
    }

}


