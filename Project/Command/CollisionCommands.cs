using Project1.CollisionComponents;
using Project1.LinkComponents;
using Project1.EnemyComponents;
using Project1.ItemComponents;
using Project1.ProjectileComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.Command
{
    public class ProjectileHitCmd : ICommand 
    {
        IProjectile Projectile;
        public ProjectileHitCmd(ICollidable projectile, string direction)
        {
            Projectile = (IProjectile)projectile;
        }

        public void Execute()
        {
            //Projectile.InMotion = false;
            Projectile.StopMotion();
        }
    }
    
    public class LinkMagicalSword : ICommand
    {
        private ILink Link;
        public LinkMagicalSword(ICollidable link, string direction = "")
        {
            Link = (ILink)link;
        }
        public void Execute()
        {
            Link.Weapon = "MagicalSword";
        }
    }

    public class LinkTakeDamageCmd : ICommand
    {

        public ILink Link { get; set; }
        string Direction;
        public LinkTakeDamageCmd(ICollidable link, string direction = "")
        {
            Direction = direction;
            Link = (ILink)link;
        }
        public void Execute()
        {
            Link.TakeDamage(Direction, 20);
        }
    }

    public class LinkBlockToGoCmd : ICommand
    {

        public ILink Link { get; set; }
        string Direction;
        public LinkBlockToGoCmd(ICollidable link, string direction = "")
        {
            Direction = direction;
            Link = (ILink)link;
        }
        public void Execute()
        {
            Link.BlockToGo(Direction);
        }
    }

    public class EnemyTakeDamageCmd : ICommand
    {
        public IEnemy Enemy { get; set; }
        private string direction;
        public EnemyTakeDamageCmd(ICollidable enemy, string direction)
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
        public EnemyAvoidOtherCmd(ICollidable enemy, string direction)
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
        Dictionary<string,string> dir = new Dictionary<string, string> {
            { "Top", "Bottom"},
            { "Bottom", "Top"},
            {"Right","Left" },
            {"Left","Right" }
        };

        public EnemyHitPlayerCmd(ICollidable enemy, string direction)
        {
            Direction = direction;
            Enemy = (IEnemy)enemy;
        }
        public void Execute()
        {
            Enemy.AvoidEnemy(dir[Direction], 20);
        }
    }

    class ItemPickedUpCmd : ICommand
    {
        public IItem Item { get; set; }
        public ItemPickedUpCmd(ICollidable item, string direction)
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
        public WeaponsBlockedCmd(ICollidable projectile, string direction = "")
        {
            Projectile = (IProjectile)projectile;
        }
        public void Execute()
        {
            //Projectile.End(); 
            Projectile.StopMotion();
        }
    }

    class NoCmd : ICommand
    {
        public NoCmd(ICollidable holder, string direction)
        {

        }
        public void Execute() { }
    }
}
