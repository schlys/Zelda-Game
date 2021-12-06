﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project1.DirectionState;

namespace Project1.ProjectileComponents
{
    public interface IProjectile
    {
        Vector2 Position { get; set; }
        IProjectileState State { get; set; }
        double Damage { get; set; }
        void OffsetOriginalPosition(IDirectionState direction);
        void StopMotion();
        void RemoveProjectile();
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
