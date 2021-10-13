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
        bool InMotion { get; set; }
        Sprite Sprite { get; set; }
        Vector2 Position { get; set; }
        Vector2 OriginalPosition { get; set; }
        int Size { get; set; }
        //String Direction { get; set; }
        IDirectionState Direction { get; set; }
        //String ID { get; set; }
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
