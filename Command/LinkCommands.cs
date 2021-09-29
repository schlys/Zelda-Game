using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Command
{
    public class LinkMoveUpCmd : ICommand
    {
        public Game1 Game { get; set; }

        public LinkMoveUpCmd(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Link.MoveUp();
        }
    }

    public class LinkMoveDownCmd : ICommand
    {
        public Game1 Game { get; set; }

        public LinkMoveDownCmd(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Link.MoveDown();
        }
    }

    public class LinkMoveRightCmd : ICommand
    {
        public Game1 Game { get; set; }

        public LinkMoveRightCmd(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Link.MoveRight();
        }
    }

    public class LinkMoveLeftCmd : ICommand
    {
        public Game1 Game { get; set; }
        public LinkMoveLeftCmd(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Link.MoveLeft();
        }
    }

    public class LinkStopMovingCmd : ICommand
    {
        public Game1 Game { get; set; }
        public LinkStopMovingCmd(Game1 game)
        {
            Game = game;
        }

        public void Execute()
        {
            Game.Link.StopMoving();
        }
    }

    public class LinkSwordAttackCmd : ICommand
    {
        public Game1 Game { get; set; }


        public LinkSwordAttackCmd(Game1 game)
        {
            Game = game;
        }
        public void Execute()
        {
            //Game.Link.LinkItemState = new LinkStateWoodenSword();
            Game.Link.Attack();
        }

    }

    public class LinkUseNoItemCmd : ICommand
        {
            public Game1 Game { get; set; }

            public LinkUseNoItemCmd(Game1 game)
            {
                Game = game;
            }
            public void Execute()
            {
                Game.Link.UseNoItem();
            }
        }
    public class LinkUseWoodenSwordCmd : ICommand
        {
            public Game1 Game { get; set; }

            public LinkUseWoodenSwordCmd(Game1 game)
            {
                Game = game;
            }
            public void Execute()
            {
                Game.Link.UseWoodenSword();
            }
        }

    public class LinkUseMagicalRodCmd : ICommand
        {
            public Game1 Game { get; set; }

            public LinkUseMagicalRodCmd(Game1 game)
            {
                Game = game;
            }
            public void Execute()
            {
                Game.Link.UseMagicalRod();
            }
        }

    public class LinkUseMagicalSheildCmd : ICommand
        {
            public Game1 Game { get; set; }

            public LinkUseMagicalSheildCmd(Game1 game)
            {
                Game = game;
            }
            public void Execute()
            {
                Game.Link.UseMagicalSheild();
            }
        }

    public class LinkUseMagicalSwordCmd : ICommand
    {
        public Game1 Game { get; set; }

        public LinkUseMagicalSwordCmd(Game1 game)
        {
            Game = game;
        }
        public void Execute()
        {
            //Game.Link.Weapon = "MagicalSword";
            //Game.Link.Attack();
            Game.Link.UseMagicalSword(); 
        }
    }

    public class LinkUseWhiteSwordCmd : ICommand
        {
            public Game1 Game { get; set; }

            public LinkUseWhiteSwordCmd(Game1 game)
            {
                Game = game;
            }
            public void Execute()
            {
                Game.Link.UseWhiteSword();
            }
        }

    public class LinkUseArrowCmd : ICommand
    {
        public Game1 Game { get; set; }

        public LinkUseArrowCmd(Game1 game)
        {
            Game = game;
        }
        public void Execute()
        {
            Game.Link.UseArrow();
        }
    }

    public class LinkUseFireCmd : ICommand
    {
        public Game1 Game { get; set; }

        public LinkUseFireCmd(Game1 game)
        {
            Game = game;
        }
        public void Execute()
        {
            Game.Link.UseFire();
        }
    }

    public class LinkUseBombCmd : ICommand
    {
        public Game1 Game { get; set; }

        public LinkUseBombCmd(Game1 game)
        {
            Game = game;
        }
        public void Execute()
        {
            Game.Link.UseBomb();
        }
    }

    public class LinkUseBoomerangCmd : ICommand
    {
        public Game1 Game { get; set; }

        public LinkUseBoomerangCmd(Game1 game)
        {
            Game = game;
        }
        public void Execute()
        {
            Game.Link.UseBoomerang();
        }
    }

    public class LinkTakeDamageCmd : ICommand
    {
        public Game1 Game { get; set; }

        public LinkTakeDamageCmd(Game1 game)
        {
            Game = game;
        }
        public void Execute()
        {
            Game.Link.TakeDamage();
        }
    }

    public class LinkResetCmd : ICommand
    {
        public Game1 Game { get; set; }

        public LinkResetCmd(Game1 game)
        {
            Game = game;
        }
        public void Execute()
        {
            Game.Link.Reset();
        }
    }
}


