using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.LevelComponents;
using Project1.SpriteComponents;
using Project1.GameState;
using System.Reflection;
using System.Xml;

namespace Project1.HeadsUpDisplay
{
    class HUD : IHUD
    {
        public ILink Link { get; set; }
        public Texture2D HUDMain { get; set; }
        public Texture2D HUDMap { get; set; }
        public Texture2D HUDInventory { get; set; }
        public Texture2D HUDLevelMap { get; set; }

        private Game1 Game;
        private int Step;
        private int ScrollDeltaY;
        private SpriteFont Font;
        private Dictionary<String, Vector2> Positions;
        private Vector2 Position;
        private Vector2 InitialPosition;
        private Vector2 MapPosition;
        private Vector2 MapItemSelectPosition;
        private Vector2 MapItemPosition;
        private Vector2 CompassItemPosition;
        private Vector2 HeartPosition;
        private Vector2 RupeeCountPosition;
        private Vector2 BombCountPosition;
        private Vector2 KeyCountPosition;
        private Vector2 InventoryItemPosition;
        private Vector2 InventoryItem1Position;
        private Vector2 InventoryItem2Position;
        public HUD(ILink link, Game1 game)
        {
            Game = game;
            Link = link;

            // TODO: data drive 
            Font = Game.Content.Load<SpriteFont>("Fonts/TitleFont");
            Positions = new Dictionary<string, Vector2>();
            Position = new Vector2(0, 0);
            InitialPosition = Position; 

            XmlDocument XMLData = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLData/XMLPositions.xml";
            XMLData.Load(path);
            XmlNodeList Pos = XMLData.DocumentElement.SelectNodes("/Positions/Position");
            foreach (XmlNode node in Pos)
            {
                string name = node.SelectSingleNode("Name").InnerText;
                int x = Int16.Parse(node.SelectSingleNode("x").InnerText);
                int y = Int16.Parse(node.SelectSingleNode("y").InnerText);

                Positions.Add(name, (new Vector2(x, y) * GameObjectManager.Instance.ScalingFactor) + Position);

            }

            //add these elements to XMLPositiions and change code so it uses the dict and not the vectors
            // **TO MULAN - i agree a dict would be more efficent, but im concerned about the dict since it will need keys which are
            // then hard coded strings. could we load each variable in the XML instead? 
            MapPosition = (new Vector2(16, 16) * GameObjectManager.Instance.ScalingFactor) + Position;
            MapItemSelectPosition = (new Vector2(128, 8) * GameObjectManager.Instance.ScalingFactor) + Position;
            MapItemPosition = (new Vector2(47, 22) * GameObjectManager.Instance.ScalingFactor) + Position;
            CompassItemPosition = (new Vector2(47, 64) * GameObjectManager.Instance.ScalingFactor) + Position;
            HeartPosition = (new Vector2(162, 20) * GameObjectManager.Instance.ScalingFactor) + Position;
            RupeeCountPosition = (new Vector2(104, 16) * GameObjectManager.Instance.ScalingFactor) + Position;
            BombCountPosition = (new Vector2(104, 40) * GameObjectManager.Instance.ScalingFactor) + Position;
            KeyCountPosition = (new Vector2(104, 30) * GameObjectManager.Instance.ScalingFactor) + Position;
            InventoryItemPosition = (new Vector2(125, 45) * GameObjectManager.Instance.ScalingFactor) + Position;
            InventoryItem1Position = (new Vector2(128, 24) * GameObjectManager.Instance.ScalingFactor) + Position;
            InventoryItem2Position = (new Vector2(152, 24) * GameObjectManager.Instance.ScalingFactor) + Position;

            HUDMain = LevelFactory.Instance.HUDTextures["HUDMain"];
            HUDMap = LevelFactory.Instance.HUDTextures["HUDMap"];
            HUDLevelMap = LevelFactory.Instance.HUDTextures["HUDLevelMap"];
            HUDInventory = LevelFactory.Instance.HUDTextures["Inventory"];

            Step = 2;
            ScrollDeltaY = (HUDMap.Height * GameObjectManager.Instance.ScalingFactor) + (HUDInventory.Height * GameObjectManager.Instance.ScalingFactor); 
        }

        public void Update()
        {
            // TODO: needed? 
            // Check if scroll 
            if (GameStateManager.Instance.CanItemScroll())
            {
                Scroll(Step);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameStateManager.Instance.CanDrawHUD())
            {
                DrawHUD(spriteBatch, Position);
            }else if (GameStateManager.Instance.CanItemScroll())
            {
                //Scroll(Step);
                DrawItemSelect(spriteBatch, Position);
            }
            else
            {
                DrawItemSelect(spriteBatch, Position);
            }
        }

        private void DrawHUD(SpriteBatch spriteBatch, Vector2 position)
        {
            // Draw a black background 
            Texture2D dummyTexture = new Texture2D(GameObjectManager.Instance.Game.GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { Color.White });

            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y,
                HUDMain.Width * GameObjectManager.Instance.ScalingFactor,
                HUDMain.Height * GameObjectManager.Instance.ScalingFactor);

            spriteBatch.Draw(dummyTexture, destinationRectangle, Color.Black);

            // Draw the <HUDMain> background
            spriteBatch.Draw(HUDMain, destinationRectangle, Color.White);

            // Draw the <LevelMap> found in <LevelFactory> and draw the TriforceFragment location if able to
            Vector2 newMapPosition = MapPosition;
            newMapPosition += position;
            LevelFactory.Instance.LevelMap.Draw(spriteBatch, newMapPosition, Link.Inventory.CanHighlightTreasureMap());

            // Draw <Item1> and <Item2> of Link's <Inventory>
            Vector2 newItem1Position = InventoryItem1Position;
            newItem1Position += position;
            Link.Inventory.DrawItem(spriteBatch, Link.Inventory.Item1, newItem1Position);

            Vector2 newItem2Position = InventoryItem2Position;
            newItem2Position += position;
            Link.Inventory.DrawItem(spriteBatch, Link.Inventory.Item2, newItem2Position);

            // Draw Link's Health Hearts 
            Vector2 newHeartPosition = HeartPosition;
            newHeartPosition += position;
            DrawLinkHealth(spriteBatch, newHeartPosition);

            // Draw Rupee Count
            string RupeeCount = Link.Inventory.RupeeCount.ToString();
            Vector2 newRupeeCountPosition = RupeeCountPosition;
            newRupeeCountPosition += position;
            spriteBatch.DrawString(Font, RupeeCount, newRupeeCountPosition, Color.White);

            // Draw Bomb Count
            string BombCount = Link.Inventory.BombCount.ToString();
            Vector2 newBombCountPosition = BombCountPosition;
            newBombCountPosition += position;
            spriteBatch.DrawString(Font, BombCount, newBombCountPosition, Color.White);

            // Draw Key Count
            string KeyCount = Link.Inventory.KeyCount.ToString();
            Vector2 newKeyCountPosition = KeyCountPosition;
            newKeyCountPosition += position;
            spriteBatch.DrawString(Font, KeyCount, newKeyCountPosition, Color.White);
        }

        private void DrawLinkHealth(SpriteBatch spriteBatch, Vector2 position)
        {
            Sprite HeartFull = SpriteFactory.Instance.GetSpriteData("HeartFull");
            Sprite HeartHalf = SpriteFactory.Instance.GetSpriteData("HeartHalf");
            Sprite HeartEmpty = SpriteFactory.Instance.GetSpriteData("HeartEmpty");

            int spaceX = (int)(HeartFull.HitBox.X * GameObjectManager.Instance.ScalingFactor * 1.5);
            LinkHealth Health = Link.Health;
            for (int i = 1; i <= Health.TotalNumHearts; i++)
            {
                if (Health.CurrNumHearts >= i)
                {
                    HeartFull.Draw(spriteBatch, position);
                }
                else if (i - Health.CurrNumHearts < 1)
                {
                    HeartHalf.Draw(spriteBatch, position);
                }
                else
                {
                    HeartEmpty.Draw(spriteBatch, position);
                }
                position.X += spaceX;
            }
        }

        private void DrawItemSelect(SpriteBatch spriteBatch, Vector2 position)
        {
            // Draw HUD
            DrawHUD(spriteBatch, position);

            // Draw Large Map
            position.Y -= HUDMap.Height * GameObjectManager.Instance.ScalingFactor;
            DrawMap(spriteBatch, position);

            // Draw Inventory 
            position.Y -= HUDInventory.Height * GameObjectManager.Instance.ScalingFactor;
            DrawInventory(spriteBatch, position);
        }

        private void DrawInventory(SpriteBatch spriteBatch, Vector2 position)
        {
            // Draw the <HUDInventory> background
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y,
                HUDInventory.Width * GameObjectManager.Instance.ScalingFactor,
                HUDInventory.Height * GameObjectManager.Instance.ScalingFactor);
            spriteBatch.Draw(HUDInventory, destinationRectangle, Color.White);

            // Draw all the items in <Link>'s <Inventory>
            Link.Inventory.Draw(spriteBatch, position);
        }
        private void DrawMap(SpriteBatch spriteBatch, Vector2 position)
        {
            // Draw the <HUDMap>
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y,
                HUDMap.Width * GameObjectManager.Instance.ScalingFactor,
                HUDMap.Height * GameObjectManager.Instance.ScalingFactor);
            spriteBatch.Draw(HUDMap, destinationRectangle, Color.White);

            // Draw the compass and map items 
            Vector2 newMapItemPosition = MapItemPosition;
            newMapItemPosition += position;
            Link.Inventory.DrawItemMap(spriteBatch, newMapItemPosition);

            Vector2 newCompassItemPosition = CompassItemPosition;
            newCompassItemPosition += position;
            Link.Inventory.DrawItemCompass(spriteBatch, newCompassItemPosition);

            // Draw the <LevelMap> found in <LevelFactory> and draw the TriforceFragment location if able to
            Vector2 newMapPosition = MapItemSelectPosition;
            newMapPosition += position;
            LevelFactory.Instance.LevelMap.Draw(spriteBatch, newMapPosition, Link.Inventory.CanHighlightTreasureMap());
        }
        private void Scroll(int step)
        {
           /* Increases Position.Y by Step to create the animation of the item selection screen scrolling in. 
            * Once the screen is finished scrolling, it stops the animation by triggering a state change in 
            * GameStateManager and reverses step so it will scroll out in the opposite direction. 
            */ 
            if((Step > 0) && (Position.Y > InitialPosition.Y + ScrollDeltaY))   // Scroll down into item selection screen
            {
                GameStateManager.Instance.StopScroll();
                Step = -Step; 
            }
            else if((Step < 0) && (Position.Y <= InitialPosition.Y))  // Scrol up and out of item selection screen 
            {
                GameStateManager.Instance.StopScroll();
                Step = -Step;
            } 
            else
            {
                Position.Y += step;
            }
            
            // TODO: test bounds 
        }
        public void Reset()
        {
            // TODO: needed? 
        }
    }
}
