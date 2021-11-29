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
using Project1.DirectionState;
using Project1.GameState; 

namespace Project1.Command
{
    public class ProjectileStopCmd : ICommand
    {
        public IProjectile Projectile { get; set; }
        public ProjectileStopCmd(ICollidable projectile, ICollidable holder, string direction = "")
        {
            Projectile = (IProjectile)projectile;
        }
        public void Execute()
        {
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
            Link.TakeDamage(Direction, GameVar.LinkDamage);
        }
    }

    public class LinkKnockbackCmd : ICommand
    {
        public ILink Link { get; set; }
        string Direction;
        public LinkKnockbackCmd(ICollidable link, ICollidable holder, string direction)
        {
            Direction = direction;
            Link = (ILink)link;
        }
        public void Execute()
        {
            Link.HitBlock(DirectionManager.Instance.GetDirectionState(Direction));
        }
    }

    public class LinkKnockbackReverseCmd : ICommand
    {
        public ILink Link { get; set; }
        string Direction;
        public LinkKnockbackReverseCmd(ICollidable link, ICollidable holder, string direction)
        {
            Direction = direction;
            Link = (ILink)link;
        }
        public void Execute()
        {
            Link.HitBlock(DirectionManager.Instance.GetReverseDirectionState(Direction));
        }
    }

    public class LinkAddItemToInventoryCmd : ICommand
    {
        public ILink Link { get; set; }
        public IItem Item { get; set; }
        public LinkAddItemToInventoryCmd(ICollidable item, ICollidable link, string direction)
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
            /* If link collides with the door in the proper direction, enter the new room if 
             * the door is unlocked. If the door is locked, try to unlock the door by using 
             * one of Link's keys and then enter the new room. 
             */ 
            
            // TODO: uncomment key code before submission 

            IDirectionState DoorDirection = Door.DirectionState; 
            if (Link.DirectionState.GetType().Name.Equals(Door.DirectionState.GetType().Name))
            {

                //if (Door.IsLocked() && Link.CanUseKey()) Door.Unlock();

                //if (!Door.IsLocked()) Door.ChangeRoom();

               
                Door.ChangeRoom();

            }
        }
    }

    public class EnemyTakeDamageCmd : ICommand
    {
        public IEnemy Enemy { get; set; }
        public IProjectile Projectile { get; set; }
        private string direction;
        public EnemyTakeDamageCmd(ICollidable enemy, ICollidable projectile, string direction)
        {
            Projectile = (IProjectile)projectile;
            Enemy = (IEnemy)enemy;
            this.direction = direction;
        }

        public void Execute()
        {
            Enemy.TakeDamage(Projectile.Damage, direction);
        }
    }

    public class EnemyKnockbackCmd : ICommand
    {

        public IEnemy Enemy { get; set; }
        string Direction;
        public EnemyKnockbackCmd(ICollidable enemy, ICollidable holder, string direction)
        {
            Direction = direction;
            Enemy = (IEnemy)enemy;
        }
        public void Execute()
        {
            Enemy.AvoidEnemy(Direction);
        }
    }

    public class EnemyKnockbackReverseCmd: ICommand
    {
        public IEnemy Enemy { get; set; }
        string Direction;

        public EnemyKnockbackReverseCmd(ICollidable enemy, ICollidable holder, string direction)
        {
            Direction = direction;
            Enemy = (IEnemy)enemy;
        }
        public void Execute()
        {
            // Avoid in the opposite direction of the collision 
            Enemy.AvoidEnemy(DirectionManager.Instance.GetReverseDirectionState(Direction).ID);
        }
    }

    public class ItemPickedUpCmd : ICommand
    {
        public IItem Item { get; set; }
        public ILink Link { get; set; }
        public ItemPickedUpCmd(ICollidable link, ICollidable item, string direction)
        {
            Item = (IItem)item;
            Link = (ILink)link;
        }
        public void Execute()
        {
            if (!Link.HasItem(Item.Kind)) Item.RemoveItem();
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
            Block.Change(GameVar.BlockBase);
        }
    }

    public class EnterStoreMenuCmd : ICommand
    {
        public ILink Link { get; set; }
        string Direction;

        public EnterStoreMenuCmd(ICollidable link, ICollidable oldman, string direction)
        {
            Link = (ILink)link;
        }
        public void Execute()
        {
            GameStateManager.Instance.StoreMenu();
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
