using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.LevelComponents;

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
        }
        public void Reset()
        {
            // TODO: needed? 
        }
    }
}
