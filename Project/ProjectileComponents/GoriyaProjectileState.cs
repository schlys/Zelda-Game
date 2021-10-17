﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;
using Project1.DirectionState;

namespace Project1.ProjectileComponents
{
    class GoriyaProjectileState : IProjectileState
    {
        // Properties from IProjectileState
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }

        // Other Properties 
        private int counter;
        private int speed = 2;

        public GoriyaProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction; 
            TypeID = "Boomerang";
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID);
            counter = 0;
        }
        public void StopMotion()
        {
            // Draw Poof 
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Projectile.InMotion)
                Sprite.Draw(spriteBatch, Projectile.Position, Projectile.Size);
            else
                Projectile.InMotion = false;
        }
        public void Update()
        {
            Sprite.Update();
            counter++;
            if (counter < 100)
            {
                if (Direction.ID.Equals("Up"))
                    Projectile.Position += new Vector2(0, (float)-speed);
                else if (Direction.ID.Equals("Down"))
                    Projectile.Position += new Vector2(0, (float)speed);
                else if (Direction.ID.Equals("Right"))
                    Projectile.Position += new Vector2((float)speed, 0);
                else if (Direction.ID.Equals("Left"))
                    Projectile.Position += new Vector2((float)-speed, 0);

                if (Projectile.Position.Y < Projectile.OriginalPosition.Y - 100 || 
                    Projectile.Position.Y > Projectile.OriginalPosition.Y + 100 ||
                    Projectile.Position.X < Projectile.OriginalPosition.X - 100 ||
                    Projectile.Position.X > Projectile.OriginalPosition.X + 100)
                {
                    speed = -2;
                }
            }
            else
                Projectile.InMotion = false;

        }
    }
}
