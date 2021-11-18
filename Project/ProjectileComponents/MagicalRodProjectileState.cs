﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.DirectionState;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.ProjectileComponents
{
    class MagicalRodProjectileState : IProjectileState
    {
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }

        // Other Properties
        private int Speed = 4;
        private int Counter = 0;
        private int CounterMax = 100;
        
        public MagicalRodProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction;
            TypeID = "MagicalRodProjectile";
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID + direction.ID);
            TypeID = "MagicalRod";
            Projectile.OffsetOriginalPosition(Direction);
        }
        public void StopMotion() { }
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Projectile.Position);
        }

        public void Update() 
        {
            Counter++;
            Sprite.Update();
            switch (Direction.ID)
            {
                case "Up":
                    Projectile.Position = new Vector2(Projectile.Position.X, Projectile.Position.Y - Speed);
                    break;
                case "Down":
                    Projectile.Position = new Vector2(Projectile.Position.X, Projectile.Position.Y + Speed);
                    break;
                case "Right":
                    Projectile.Position = new Vector2(Projectile.Position.X + Speed, Projectile.Position.Y);
                    break;
                default:
                    Projectile.Position = new Vector2(Projectile.Position.X - Speed, Projectile.Position.Y);
                    break;
            }
            if (Counter > CounterMax) Projectile.RemoveProjectile();
        }
    }
}
