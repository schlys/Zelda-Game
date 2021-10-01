using System;
using System.Collections.Generic;
using System.Text;
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

    public class LinkStopMovingCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkStopMovingCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }

        public void Execute()
        {
            Link.StopMoving();
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

    public class LinkUseNoItemCmd : ICommand
        {
            public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkUseNoItemCmd(Game1 game, ILink link)
            {
                Game = game;
            Link = link;
        }
            public void Execute()
            {
                Link.UseNoItem();
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
            Link.UseArrow();
        }
    }

    public class LinkUseBlueArrowCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkUseBlueArrowCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.UseBlueArrow();
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
            Link.UseFire();
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
            Link.UseBomb();
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
            Link.UseBoomerang();
        }
    }

    public class LinkUseBlueBoomerangCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkUseBlueBoomerangCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.UseBlueBoomerang();
        }
    }

    public class LinkTakeDamageCmd : ICommand
    {
        public Game1 Game { get; set; }
        public ILink Link { get; set; }

        public LinkTakeDamageCmd(Game1 game, ILink link)
        {
            Game = game;
            Link = link;
        }
        public void Execute()
        {
            Link.TakeDamage();
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
}


