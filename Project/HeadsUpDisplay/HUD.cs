using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.LevelComponents;
using Project1.SpriteComponents; 

namespace Project1.HeadsUpDisplay
{
    class HUD : IHUD
    {
        public ILink Link { get; set; }

        private Game1 Game;

        public HUD(ILink link, Game1 game)
        {
            Game = game;
            Link = link;
        }
       
        public void Update()
        {
           // TODO: needed? 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Display <Item1> and <Item2> of Link's <Inventory>
            SpriteFont font = Game.Content.Load<SpriteFont>("Fonts/TitleFont");
            string item1 = "Item1: " + Link.Inventory.Item1;
            string item2 = "Item2: " + Link.Inventory.Item2;
            spriteBatch.DrawString(font, item1, new Vector2(400, 30), Color.Black);
            spriteBatch.DrawString(font, item2, new Vector2(400, 45), Color.Black);

            // Draw the <LevelMap> found in <LevelFactory>
            LevelFactory.Instance.LevelMap.Draw(spriteBatch);

            // Draw Link's Health Hearts 
            DrawLinkHealth(spriteBatch); 
        }

        private void DrawLinkHealth(SpriteBatch spriteBatch)
        {
            Sprite HeartFull = SpriteFactory.Instance.GetSpriteData("HeartFull");
            Sprite HeartHalf = SpriteFactory.Instance.GetSpriteData("HeartHalf");
            Sprite HeartEmpty = SpriteFactory.Instance.GetSpriteData("HeartEmpty");

            Vector2 position = new Vector2(200, 10);
            int spaceX = (int)(HeartFull.HitBox.X * GameObjectManager.Instance.ScalingFactor * 1.5); 
            LinkHealth Health = Link.Health;
            for(int i = 1; i <= Health.TotalNumHearts; i++)
            {
                if(Health.CurrNumHearts >= i)
                {
                    HeartFull.Draw(spriteBatch, position); 
                } else if (i - Health.CurrNumHearts < 1)
                {
                    HeartHalf.Draw(spriteBatch, position);
                } else
                {
                    HeartEmpty.Draw(spriteBatch, position);
                }
                position.X += spaceX;
            }
        }
        public void Reset()
        {
            // TODO: needed? 
        }
    }
}
