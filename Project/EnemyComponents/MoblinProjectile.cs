using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.EnemyComponents
{
    class MoblinProjectile : IAquamentusProjectile
    {
        Sprite Sprite;
        public string ID;
        private Vector2 position;
        private string direction;
        private int counter;
        public MoblinProjectile(Vector2 position, string direction)
        {
            this.direction = direction;
            this.position = position;
            counter = 0;
            ID = "MoblinProjectile";
            Sprite = SpriteFactory.Instance.GetSpriteData(ID+ direction);
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
                    position += new Vector2(0, (float) -2);
                else if (direction.Equals("Down"))
                    position += new Vector2(0, (float)2);
                else if (direction.Equals("Right"))
                    position += new Vector2((float)2, 0);
                else if (direction.Equals("Left"))
                    position += new Vector2((float)-2, 0);
            }
        }
    }
}
