using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.LinkComponents
{
    class CurrentItem
    {
        public Sprite Sprite { get; set; }
        public Vector2 Position;
        public string direction;
        private int speed = 4;
        public CurrentItem()
        {
        }
        public void Draw(SpriteBatch spriteBatch, int size)
        {
            if (Sprite != null)
                Sprite.Draw(spriteBatch, Position, size);
        }

        public void Update()
        {
            if (Sprite != null)
            {
                switch (direction)
                {
                    case "Up":
                        Position.Y-=speed;
                        break;
                    case "Down":
                        Position.Y+=speed;
                        break;
                    case "Right":
                        Position.X+=speed;
                        break;
                    default:
                        Position.X-=speed;
                        break;
                }
            }
        }
    }
}
