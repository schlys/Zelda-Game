using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.EnemyComponents
{
    class GoriyaProjectile : IProjectile
    {
        Sprite Sprite;
        public string ID;
        private Vector2 position;
        private Vector2 originalPosition;
        private string direction;
        private int counter;
        private int speed = 2;

        public GoriyaProjectile(Vector2 position, string direction)
        {
            this.direction = direction;
            this.position = position;
            originalPosition = position;
            counter = 0;
            ID = "Boomerang";
            Sprite = SpriteFactory.Instance.GetSpriteData(ID);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (counter < 100)
                Sprite.Draw(spriteBatch, position, 80);
        }
        public void Update()
        {
            Sprite.Update();
            counter++;
            if (counter < 100)
            {
                if (direction.Equals("Up"))
                    position += new Vector2(0, (float) -speed);
                else if (direction.Equals("Down"))
                    position += new Vector2(0, (float)speed);
                else if (direction.Equals("Right"))
                    position += new Vector2((float)speed, 0);
                else if (direction.Equals("Left"))
                    position += new Vector2((float)-speed, 0);

                if (position.Y < originalPosition.Y - 100 || position.Y > originalPosition.Y + 100 || position.X < originalPosition.X - 100 || position.X > originalPosition.X + 100)
                {
                    speed = -2;
                }

                
            }
        }
    }
}
