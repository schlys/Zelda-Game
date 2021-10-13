using Project1.CollisionComponents;
using Project1.LinkComponents;
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
            Projectile.InMotion = false;
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

    class NoCmd : ICommand
    {
        public NoCmd(ICollidable holder, string direction)
        {

        }
        public void Execute() { }
    }
}
