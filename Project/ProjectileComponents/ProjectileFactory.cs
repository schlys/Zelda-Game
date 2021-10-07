using Microsoft.Xna.Framework;
using Project1.ProjectileComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.ProjectileComponents
{
    class ProjectileFactory
    {
        private static ProjectileFactory instance = new ProjectileFactory();
        public static ProjectileFactory Instance
        {
            get
            {
                return instance;
            }
        }
        private ProjectileFactory() { }

        public IProjectile GetProjectile(string name, Vector2 position, string direction)
        {
            switch (name)
            {
                case "Fire":
                    return new FireProjectile(position, direction);
                case "Bomb":
                    return new BombProjectile(position, direction);
                case "Boomerang":
                    return new BoomerangProjectile(position, direction);
                case "MagicalBoomerang":
                    return new MagicalBoomerangProjectile(position, direction);
                case "SilverArrow":
                    return new SilverArrowProjectile(position, direction);
                default:
                    return new ArrowProjectile(position, direction);
            }
        }
    }
}
