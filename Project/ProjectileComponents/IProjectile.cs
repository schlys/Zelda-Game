using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteFactoryComponents;
using Microsoft.Xna.Framework;

namespace Project1.ProjectileComponents
{
    public interface IProjectile
    {
        bool InMotion { get; }
        Sprite Sprite { get; set; }
        Vector2 Position { get; set; }
        Vector2 OriginalPosition { get; set; }
        int Size { get; set; }
        String Direction { get; set; }
        String ID { get; set; }
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
