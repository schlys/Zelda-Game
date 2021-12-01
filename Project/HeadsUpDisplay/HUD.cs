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
using Project1.CollisionComponents; 

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
        private int InitialStep;
        private int ScrollDeltaY;

        private Sprite TextNum1;
        private Sprite TextNum2;
        private Sprite HeartFull;
        private Sprite HeartHalf;
        private Sprite HeartEmpty; 

        private SpriteFont BodyFont;

        //private Dictionary<String, Vector2> Positions;

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
        private Vector2 InventoryItem1TextPosition;
        private Vector2 InventoryItem2TextPosition;

        public HUD(ILink link, Game1 game)
        {
            Game = game;
            Link = link;

            // TODO: data drive 
            BodyFont = Game.Content.Load<SpriteFont>(GameVar.BodyFont);

            //Positions = new Dictionary<string, Vector2>();

            Position = GameVar.GetHUDPosition(); 
            InitialPosition = Position; 

            /*
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
            */

            
            MapPosition = (GameVar.GetMapPosition() * GameVar.ScalingFactor) + Position;
            MapItemSelectPosition = (GameVar.GetItemSelectPosition() * GameVar.ScalingFactor) + Position;
            MapItemPosition = (GameVar.GetMapItemPosition() * GameVar.ScalingFactor) + Position;
            CompassItemPosition = (GameVar.GetCompassItemPosition() * GameVar.ScalingFactor) + Position;
            HeartPosition = (GameVar.GetHeartPosition() * GameVar.ScalingFactor) + Position;
            RupeeCountPosition = (GameVar.GetRupeeCountPosition() * GameVar.ScalingFactor) + Position;
            BombCountPosition = (GameVar.GetBombCountPosition() * GameVar.ScalingFactor) + Position;
            KeyCountPosition = (GameVar.GetKeyCountPosition() * GameVar.ScalingFactor) + Position;
            InventoryItemPosition = (GameVar.GetInventoryItemPosition() * GameVar.ScalingFactor) + Position;
            InventoryItem1Position = (GameVar.GetInventoryItem1Position() * GameVar.ScalingFactor) + Position;
            InventoryItem2Position = (GameVar.GetInventoryItem2Position() * GameVar.ScalingFactor) + Position;
            InventoryItem1TextPosition = (GameVar.GetInventoryItem1TextPosition() * GameVar.ScalingFactor) + Position;
            InventoryItem2TextPosition = (GameVar.GetInventoryItem2TextPosition() * GameVar.ScalingFactor) + Position;

            HUDMain = LevelFactory.Instance.GetHUDTexture(GameVar.HUDMainSpriteKey);
            HUDMap = LevelFactory.Instance.GetHUDTexture(GameVar.HUDMapSpriteKey);
            HUDLevelMap = LevelFactory.Instance.GetHUDTexture(GameVar.HUDLevelMapSpriteKey);
            HUDInventory = LevelFactory.Instance.GetHUDTexture(GameVar.HUDInventorySpriteKey);

            HeartFull = SpriteFactory.Instance.GetSpriteData(GameVar.HeartFullSpriteKey);
            HeartHalf = SpriteFactory.Instance.GetSpriteData(GameVar.HeartHalfSpriteKey);
            HeartEmpty = SpriteFactory.Instance.GetSpriteData(GameVar.HeartEmptySpriteKey);

            if (Link.PlayerNum == GameVar.Player1)
            {
                TextNum1 = SpriteFactory.Instance.GetSpriteData(GameVar.Player1Num1SpriteKey);
                TextNum2 = SpriteFactory.Instance.GetSpriteData(GameVar.Player1Num2SpriteKey);
            }
            else if (Link.PlayerNum == GameVar.Player2)
            {
                TextNum1 = SpriteFactory.Instance.GetSpriteData(GameVar.Player2Num1SpriteKey);
                TextNum2 = SpriteFactory.Instance.GetSpriteData(GameVar.Player2Num2SpriteKey);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }

            Step = GameVar.ScrollStep;
            InitialStep = Step;
            ScrollDeltaY = (HUDMap.Height * GameVar.ScalingFactor) + (HUDInventory.Height * GameVar.ScalingFactor) +
                (GameVar.buffer * GameVar.ScalingFactor); 
        }

        public void Update()
        {
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
                HUDMain.Width * GameVar.ScalingFactor,
                HUDMain.Height * GameVar.ScalingFactor);

            spriteBatch.Draw(dummyTexture, destinationRectangle, Color.Black);

            // Draw the <HUDMain> background
            spriteBatch.Draw(HUDMain, destinationRectangle, Color.White);

            // Draw the <LevelMap> found in <LevelFactory> and draw the TriforceFragment location if able to
            Vector2 newMapPosition = MapPosition;
            newMapPosition += position;
            GameObjectManager.Instance.Level.LevelMap.Draw(spriteBatch, newMapPosition, Link.Inventory.CanHighlightTreasureMap());

            // Draw <Item1> and <Item2> of Link's <Inventory>
            Vector2 newItem1Position = InventoryItem1Position;
            newItem1Position += position;
            Link.Inventory.DrawItem(spriteBatch, Link.Inventory.Item1, newItem1Position);

            Vector2 newItem2Position = InventoryItem2Position;
            newItem2Position += position;
            Link.Inventory.DrawItem(spriteBatch, Link.Inventory.Item2, newItem2Position);

            // Draw <NumText1> and <NumText1> to denote the keys for items
            Vector2 newItem1TextPosition = InventoryItem1TextPosition;
            newItem1TextPosition += position;
            newItem1TextPosition = GetItemPosition(TextNum1, newItem1TextPosition);
            TextNum1.Draw(spriteBatch, newItem1TextPosition);

            Vector2 newItem2TextPosition = InventoryItem2TextPosition;
            newItem2TextPosition += position;
            newItem2TextPosition = GetItemPosition(TextNum2, newItem2TextPosition);
            TextNum2.Draw(spriteBatch, newItem2TextPosition);

            // Draw Link's Health Hearts 
            Vector2 newHeartPosition = HeartPosition;
            newHeartPosition += position;
            DrawLinkHealth(spriteBatch, newHeartPosition);

            // Draw Rupee Count
            string RupeeCount = Link.Inventory.RupeeCount.ToString();
            Vector2 newRupeeCountPosition = RupeeCountPosition;
            newRupeeCountPosition += position;
            spriteBatch.DrawString(BodyFont, RupeeCount, newRupeeCountPosition, Color.White);

            // Draw Bomb Count
            string BombCount = Link.Inventory.BombCount.ToString();
            Vector2 newBombCountPosition = BombCountPosition;
            newBombCountPosition += position;
            spriteBatch.DrawString(BodyFont, BombCount, newBombCountPosition, Color.White);

            // Draw Key Count
            string KeyCount = Link.Inventory.KeyCount.ToString();
            Vector2 newKeyCountPosition = KeyCountPosition;
            newKeyCountPosition += position;
            spriteBatch.DrawString(BodyFont, KeyCount, newKeyCountPosition, Color.White);
        }

        private void DrawLinkHealth(SpriteBatch spriteBatch, Vector2 position)
        {

            int spaceX = (int)(HeartFull.HitBox.X * GameVar.ScalingFactor * GameVar.HeartSpaceX);
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
            position.Y -= HUDMap.Height * GameVar.ScalingFactor;
            DrawMap(spriteBatch, position);

            // Draw Inventory 
            position.Y -= HUDInventory.Height * GameVar.ScalingFactor;
            DrawInventory(spriteBatch, position);
        }

        private void DrawInventory(SpriteBatch spriteBatch, Vector2 position)
        {
            // Draw the <HUDInventory> background
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y,
                HUDInventory.Width * GameVar.ScalingFactor,
                HUDInventory.Height * GameVar.ScalingFactor);
            spriteBatch.Draw(HUDInventory, destinationRectangle, Color.White);

            // Draw all the items in <Link>'s <Inventory>
            Link.Inventory.Draw(spriteBatch, position);
        }
        private void DrawMap(SpriteBatch spriteBatch, Vector2 position)
        {
            // Draw the <HUDMap>
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y,
                HUDMap.Width * GameVar.ScalingFactor,
                HUDMap.Height * GameVar.ScalingFactor);
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

            // TODO: bad coupling!! 
            GameObjectManager.Instance.Level.LevelMap.Draw(spriteBatch, newMapPosition, Link.Inventory.CanHighlightTreasureMap());
        }
        private void Scroll(int step)
        {
           /* Increases Position.Y by Step to create the animation of the item selection screen scrolling in. 
            * Once the screen is finished scrolling, it stops the animation by triggering a state change in 
            * GameStateManager and reverses step so it will scroll out in the opposite direction. 
            */ 
           
            if((Step > 0) && (Position.Y > InitialPosition.Y + ScrollDeltaY))   // Scroll down into item selection screen
            {
                GameObjectManager.Instance.StopScroll(); 
            }
            else if((Step < 0) && (Position.Y <= InitialPosition.Y))  // Scrol up and out of item selection screen 
            {
                GameObjectManager.Instance.StopScroll();
            } 
            else
            {
                Position.Y += step;
            }
        }

        public void StopScroll()
        {
            /* Called in GameObjectManager 
             * reverse <Step> direction
             */ 
            Step = -Step;
        }

        public void Reset()
        {
            Position = InitialPosition;
        }

        private Vector2 GetItemPosition(Sprite sprite, Vector2 position)
        {
            /* Get accurate dimensions for the hitbox, but position is off */
            Rectangle Hitbox = CollisionManager.Instance.GetHitBox(position, sprite.HitBox);
            /* Correct the position to account for empty space around the hitbox */
            int BlockSize = SpriteFactory.Instance.UniversalSize * GameVar.ScalingFactor;
            position -= new Vector2((BlockSize - Hitbox.Width) / 2, (BlockSize - Hitbox.Height) / 2);
            /* Get correct hibox for updated position */
            return position;
        }
    }
}
