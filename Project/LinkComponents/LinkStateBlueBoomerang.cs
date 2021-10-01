using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.LinkComponents
{
    class LinkStateBlueBoomerang : ILinkItemState
    {
        public Sprite Sprite { get; set; }
        public Vector2 Position;
        public bool isUsing { get; set; }
        public string Direction { get; set; }
        private int speed = 6;
        private Vector2 originalPosition;
        int counter;
        public LinkStateBlueBoomerang(string direction, Vector2 position)
        {
            Position = position;
            Direction = direction;
            Sprite = SpriteFactory.Instance.GetSpriteData("BlueBoomerang");
            counter = 0;
            isUsing = true;

            switch (Direction)
            {
                case "Up":
                    Position.X+=20;
                    Position.Y -= 5;
                    break;
                case "Down":
                    Position.X += 20;
                    Position.Y += 50;
                    break;
                case "Right":
                    Position.X += 50;
                    Position.Y += 20;
                    break;
                default:
                    Position.X -= 10;
                    Position.Y += 20;
                    break;
            }

            originalPosition.X = Position.X;
            originalPosition.Y = Position.Y;

        }
        public void Draw(SpriteBatch spriteBatch, int size)
        {
            if (Sprite != null)
                if (counter < 90)
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
                Sprite.Update();

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

                if(Position.Y<originalPosition.Y-200 || Position.Y > originalPosition.Y+200 || Position.X < originalPosition.X - 200 || Position.X > originalPosition.X + 200)
                {
                    speed = -4;
                }
            }
        }
    }
}
