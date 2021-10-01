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
        
        private int counter;
        public LinkStateBomb(string direction, Vector2 position)
        {
            Position = position;
            Direction = direction;
            Sprite = SpriteFactory.Instance.GetSpriteData("Bomb");
            counter = 0;
            isUsing = true;


            switch (Direction)
            {
                case "Up":
                    Position.X+=20;
                    Position.Y -= 20;
                    break;
                case "Down":
                    Position.X += 20;
                    Position.Y += 60;
                    break;
                case "Right":
                    Position.X += 60;
                    Position.Y += 20;
                    break;
                default:
                    Position.X -= 20;
                    Position.Y += 20;
                    break;
            }

        }
        public void Draw(SpriteBatch spriteBatch, int size)
        {
            if (Sprite != null)
                if (counter < 100)
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

                Sprite.DelayRate = 0.1;
                Sprite.MaxDelay = 1;
                if (counter < 70)
                {
                    counter++;
                }
                else
                {
                    Sprite.Update();
                }
                
               
                
            }

        }
    }
}
