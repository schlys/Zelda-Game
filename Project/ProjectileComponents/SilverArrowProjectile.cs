using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.ProjectileComponents;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.ProjectileComponents
{
    class SilverArrowProjectile : IProjectile
    {
        public bool InMotion {get;set;}
        public Sprite Sprite { get; set; }
        public Sprite Poof { get; set; }
        public Vector2 Position;
     
        public string Direction { get; set; }
        private int speed = 4;
        int counter;
        public SilverArrowProjectile(string direction, Vector2 position)
        {
            InMotion = true;
            Position = position;
            Direction = direction;
            Sprite = SpriteFactory.Instance.GetSpriteData("SilverArrow" + Direction);
            Poof = SpriteFactory.Instance.GetSpriteData("SilverArrowPoof");
            counter = 0;
         

            switch (Direction)
            {
                case "Up":
                    Position.X += 20;
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
                if (counter < 50)
                {
                    counter++;
                    Sprite.Draw(spriteBatch, Position, 80);
                }
                else if (counter < 60)
                {
                    Poof.Draw(spriteBatch, Position, 80);
                    counter++;
                }
                else
                {
                    InMotion = false;
                }
        }

        public void Update()
        {
           
                if (counter < 50)
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
