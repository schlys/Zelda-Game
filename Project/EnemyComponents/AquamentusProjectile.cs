using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.EnemyComponents
{
    class AquamentusProjectile : IAquamentusProjectile
    {
        Sprite Sprite;
        public string ID;
        private Vector2 position;
        private string direction;
        private int counter;
        public AquamentusProjectile(Vector2 position, string direction)
        {
            this.direction = direction;
            this.position = position;
            counter = 0;
            ID = "AquamentusProjectile";
            Sprite = SpriteFactory.Instance.GetSpriteData(ID);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (counter < 200)
                Sprite.Draw(spriteBatch, position, 80);
        }
        public void Update()
        {
            Sprite.Update();
            counter++;
            if (counter < 200)
            {
                if (direction.Equals("Up"))
                    position += new Vector2((float)-2, -1);
                else if (direction.Equals("Straight"))
                    position += new Vector2((float)-2, 0);
                else
                    position += new Vector2((float)-2, 1);
            }
        }
    }
}
