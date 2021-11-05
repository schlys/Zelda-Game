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

    // TODO - is this used, delete ? 
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

    public class LinkSwordAttackCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }


        public LinkSwordAttackCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.Attack();
        }

    }
    public class LinkUseWoodenSwordCmd : ICommand
        {
            public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkUseWoodenSwordCmd(Game1 game, ILink link)
            {
                Game = game;
            Link = link;
        }
            public void Execute()
            {
                Link.UseWoodenSword();
            }
        }

    public class LinkUseMagicalRodCmd : ICommand
        {
            public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkUseMagicalRodCmd(Game1 game, ILink link)
            {
                Game = game;
            Link = link;
        }
            public void Execute()
            {
                Link.UseMagicalRod();
            }
        }

    public class LinkUseMagicalSheildCmd : ICommand
        {
            public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkUseMagicalSheildCmd(Game1 game, ILink link)
            {
                Game = game;
            Link = link;
        }
            public void Execute()
            {
                Link.UseMagicalSheild();
            }
        }

    public class LinkUseMagicalSwordCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkUseMagicalSwordCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.UseMagicalSword(); 
        }
    }

    public class LinkUseWhiteSwordCmd : ICommand
        {
            public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkUseWhiteSwordCmd(Game1 game, ILink link)
            {
                Game = game;
            Link = link;
        }
            public void Execute()
            {
                Link.UseWhiteSword();
            }
        }
    /*
    public class LinkUseItemCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkUseItemCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.UseItem();
        }
    }
    */
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
            Link.UseItem1();
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
            Link.UseItem2();
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

    public class LinkPickUpArrowCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkPickUpArrowCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.PickUpItem("Arrow");
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
            Link.ItemUp();
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
            Link.ItemDown();
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
            Link.ItemLeft();
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
            Link.ItemRight();
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
            Link.SelectItem();
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
            Link.SelectItem1();
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
            Link.SelectItem2();
        }
    }
    /*
    public class LinkNextItemCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkNextItemCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.HUD.NextItem();
        }
    }
    public class LinkPreviousItemCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkPreviousItemCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.HUD.PreviousItem();
        }
    }*/
}


