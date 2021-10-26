using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
using Microsoft.Xna.Framework;
using Project1.DirectionState;

namespace Project1.ProjectileComponents
{
    public interface IProjectile
    {
        Vector2 Position { get; set; }
        Vector2 OriginalPosition { get; set; }
        IProjectileState State { get; set; }
        bool InMotion { get; set; }
        void OffsetOriginalPosition(IDirectionState direction);
        void StopMotion(); 
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
