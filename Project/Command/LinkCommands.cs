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

    public class LinkUseArrowCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkUseArrowCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.UseItem("Arrow");
        }
    }

    public class LinkUseSilverArrowCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkUseSilverArrowCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.UseItem("SilverArrow");
        }
    }

    public class LinkUseFireCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkUseFireCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.UseItem("Fire");
        }
    }

    public class LinkUseBombCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkUseBombCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.UseItem("Bomb");
        }
    }

    public class LinkUseBoomerangCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkUseBoomerangCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.UseItem("BoomerangSolid");
        }
    }

    public class LinkUseMagicalBoomerangCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkUseMagicalBoomerangCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.UseItem("MagicalBoomerangSolid");
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
    }
}


