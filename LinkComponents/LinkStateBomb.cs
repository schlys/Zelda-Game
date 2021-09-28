using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.LinkComponents
{
    class LinkStateBomb : ILinkItemState
    {
        public Sprite Sprite { get; set; }
        public Vector2 Position;
        public bool isUsing { get; set; }
        public string Direction { get; set; }
        private int speed = 4;
        int counter;
        public LinkStateBomb(string direction, Vector2 position)
        {
            Position = position;
            Direction = direction;
            Sprite = SpriteFactory.Instance.GetSpriteData("Bomb" + Direction);
            counter = 0;
            isUsing = true;
        }
        public void Draw(SpriteBatch spriteBatch, int size)
        {
            if (Sprite != null)
                if (counter < 50)
                {
                    counter++;
                    Sprite.Draw(spriteBatch, Position, size);
                }else
                {
                    isUsing = false;
                }
        }

        public void Update()
        {
            if (Sprite != null)
            {
                    switch (Direction)
                    {
                        case "Up":
                            Position.Y -= speed;
                            break;
                        case "Down":
                            Position.Y += speed;
                            break;
                        case "Right":
                            Position.X += speed;
                            break;
                        default:
                            Position.X -= speed;
                            break;
                    }
                
            }
        }
    }
}
