using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.ProjectileComponents
{
    class BombProjectile : IProjectile
    {
        public bool InMotion { get; set; }
        public Sprite Sprite { get; set; }
        public Vector2 Position;
        
        public string Direction { get; set; }
        
        private int counter;
        public BombProjectile(string direction, Vector2 position)
        {
            Position = position;
            Direction = direction;
            Sprite = SpriteFactory.Instance.GetSpriteData("Bomb");
            counter = 0;
            InMotion = true;


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
        public void Draw(SpriteBatch spriteBatch)
        {
            if (InMotion)
                if (counter < 100)
                {
                    counter++;
                    Sprite.Draw(spriteBatch, Position, 80);
                }else
                {
                    InMotion = false;
                }
        }

        public void Update()
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
