using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.ProjectileComponents
{
    class ProjectileManager : IProjectileManager
    {
        private static ProjectileManager instance = new ProjectileManager();
        public static ProjectileManager Instance
        {
            get
            {
                return instance;
            }
        }
        private ProjectileManager() { }

        private List<IProjectile> projectileList = new List<IProjectile>();
        public void Add(IProjectile projectile)
        {
            projectileList.Add(projectile);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IProjectile Projectile in projectileList)
            {
                Projectile.Draw(spriteBatch);
            }
        }

        public void Update()
        {
           for (int i = 0; i < projectileList.Count; i++)
           {
                IProjectile Projectile = projectileList[i];
                if (Projectile.InMotion)
                    Projectile.Update();
                else
                    projectileList.Remove(Projectile);
           }
        }
    }
}
