using Project1.CollisionComponents;
using Project1.LinkComponents;
using Project1.EnemyComponents;
using Project1.ItemComponents;
using Project1.ProjectileComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.LevelComponents;
using Microsoft.Xna.Framework;
using Project1.BlockComponents;

namespace Project1.Command
{
    public class ProjectileHitCmd : ICommand
    {
        IProjectile Projectile;
        public ProjectileHitCmd(ICollidable projectile, ICollidable holder, string direction)
        {
            Projectile = (IProjectile)projectile;
        }

        public void Execute()
        {
            //Projectile.InMotion = false;
            Projectile.StopMotion();
        }
    }


    public class LinkTakeDamageCmd : ICommand
    {
        public ILink Link { get; set; }
        string Direction;
        public LinkTakeDamageCmd(ICollidable link, ICollidable holder, string direction = "")
        {
            Direction = direction;
            Link = (ILink)link;
        }
        public void Execute()
        {
            Link.TakeDamage(Direction, 20);
        }
    }

    public class LinkHitBlockCmd : ICommand
    {
        public ILink Link { get; set; }
        string Direction;
        public LinkHitBlockCmd(ICollidable link, ICollidable holder, string direction)
        {
            Direction = direction;
            Link = (ILink)link;
        }
        public void Execute()
        {
            Link.HitBlock(Direction);
        }
    }
   
    public class LinkAddItemToInventoryCmd : ICommand
    {
        public ILink Link { get; set; }
        public IItem Item { get; set; }
       

        public LinkAddItemToInventoryCmd(ICollidable link, ICollidable item, string direction)
        {
            Link = (ILink)link;
            Item = (IItem)item;
        }
        public void Execute()
        {
            Link.PickUpItem(Item);
        }
    }

    public class LinkUseKeyCmd : ICommand
    {

       

        public ILink Link { get; set; }
        public IDoor Door { get; set; }
        private List<Delegate> rooms = new List<Delegate>();
        
        
        public LinkUseKeyCmd(ICollidable link, ICollidable door, string direction)
        {
            
            Link = (ILink)link;
            Door = (IDoor)door;
           
            
        }
        public void Execute()
        {
            // Unlock the door if link has a key
           
            if (Link.DirectionState.ID.Equals(Door.Direction))
            {
                //if (Door.IsLocked() && Link.CanUseKey()) Door.Unlock();

                //if (!Door.IsLocked())
                //{
                //    switch (Door.Direction)
                //    {
                //        case "Up":
                //            LevelFactory.Instance.MoveUp();
                //            break;
                //        case "Down":
                //            LevelFactory.Instance.MoveDown();
                //            break;
                //        case "Right":
                //            LevelFactory.Instance.MoveRight();
                //            break;
                //        case "Left":
                //            LevelFactory.Instance.MoveLeft();
                //            break;
                //    }
                //}

                switch (Door.Direction)
                {
                    case "Up":
                        LevelFactory.Instance.MoveUp();
                        break;
                    case "Down":
                        LevelFactory.Instance.MoveDown();
                        break;
                    case "Right":
                        LevelFactory.Instance.MoveRight();
                        break;
                    case "Left":
                        LevelFactory.Instance.MoveLeft();
                        break;
                }

            }
        }
    }


    public class EnemyTakeDamageCmd : ICommand
    {
        public IEnemy Enemy { get; set; }
        private string direction;
        public EnemyTakeDamageCmd(ICollidable enemy, ICollidable holder, string direction)
        {
            Enemy = (IEnemy)enemy;
            this.direction = direction;
        }

        public void Execute()
        {
            Enemy.TakeDamage(0.5, direction);
        }
    }

    public class EnemyAvoidOtherCmd : ICommand
    {

        public IEnemy Enemy { get; set; }
        string Direction;
        public EnemyAvoidOtherCmd(ICollidable enemy, ICollidable holder, string direction)
        {
            Direction = direction;
            Enemy = (IEnemy)enemy;
        }
        public void Execute()
        {
            Enemy.AvoidEnemy(Direction);
        }
    }

    public class EnemyHitPlayerCmd: ICommand
    {
        public IEnemy Enemy { get; set; }
        string Direction;
        // Change Direction to opposite
        Dictionary<string,string> dir = new Dictionary<string, string> {
            { "Top", "Bottom"},
            { "Bottom", "Top"},
            {"Right","Left" },
            {"Left","Right" }
        };

        public EnemyHitPlayerCmd(ICollidable enemy, ICollidable holder, string direction)
        {
            Direction = direction;
            Enemy = (IEnemy)enemy;
        }
        public void Execute()
        {
            Enemy.AvoidEnemy(dir[Direction]);
        }
    }

    public class ItemPickedUpCmd : ICommand
    {
        public IItem Item { get; set; }
        public ItemPickedUpCmd(ICollidable item, ICollidable holder, string direction)
        {
            Item = (IItem)item;
        }
        public void Execute()
        {
            Item.RemoveItem();
        }
    }

    public class WeaponsBlockedCmd : ICommand
    {

        public IProjectile Projectile { get; set; }
        public WeaponsBlockedCmd(ICollidable projectile, ICollidable holder, string direction = "")
        {
            Projectile = (IProjectile)projectile;
        }
        public void Execute()
        {
            //Projectile.End(); 
            Projectile.StopMotion();
        }
    }

    public class BreakBlockCmd : ICommand
    {
        IBlock Block { get; set; }
        public BreakBlockCmd(ICollidable block, ICollidable holder, string direction = "")
        {
            Block = (IBlock)block;
        }
        public void Execute()
        {
            Block.Change("Base");
        }
    }
    public class DownStairsCmd : ICommand
    {
        public DownStairsCmd(ICollidable block, ICollidable holder, string direction = "") { }
        public void Execute()
        {
            LevelFactory.Instance.MoveDown(new Vector2((float)-4.5 * LevelFactory.Instance.RoomBlockSize, -LevelFactory.Instance.PlayableHeight/2));
        }
    }

    class NoCmd : ICommand
    {
        public NoCmd(ICollidable holder, ICollidable holder2, string direction)
        {

        }
        public void Execute() { }
    }
}
