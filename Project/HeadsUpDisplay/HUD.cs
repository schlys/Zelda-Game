﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.LevelComponents;
using Project1.SpriteComponents;
using Project1.GameState;

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
        private Vector2 Position;
        private Vector2 MapPosition;
        private Vector2 HeartPosition;
        private Vector2 RupeeCountPosition;
        private Vector2 InventoryItemPosition;
        private Vector2 InventoryItem1Position;
        private Vector2 InventoryItem2Position;
        public HUD(ILink link, Game1 game)
        {
            Game = game;
            Link = link;

            // TODO: data drive 
            Position = new Vector2(0, 0);
            MapPosition = (new Vector2(16, 16) * GameObjectManager.Instance.ScalingFactor) + Position;
            HeartPosition = (new Vector2(162, 20) * GameObjectManager.Instance.ScalingFactor) + Position;
            RupeeCountPosition = (new Vector2(104, 16) * GameObjectManager.Instance.ScalingFactor) + Position;
            InventoryItemPosition = (new Vector2(125, 45) * GameObjectManager.Instance.ScalingFactor) + Position;
            InventoryItem1Position = (new Vector2(128, 24) * GameObjectManager.Instance.ScalingFactor) + Position;
            InventoryItem2Position = (new Vector2(152, 24) * GameObjectManager.Instance.ScalingFactor) + Position;

            HUDMain = LevelFactory.Instance.HUDTextures["HUDMain"];
            HUDMap = LevelFactory.Instance.HUDTextures["HUDMap"];
            HUDLevelMap = LevelFactory.Instance.HUDTextures["HUDLevelMap"];
            HUDInventory = LevelFactory.Instance.HUDTextures["Inventory"];
        }

        public void Update()
        {
            // TODO: needed? 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameStateManager.Instance.CanDrawHUD())
            {
                DrawHUD(spriteBatch, Position);
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
            SpriteFont font = Game.Content.Load<SpriteFont>("Fonts/TitleFont");
            string RupeeCount = Link.Inventory.RupeeCount.ToString();
            Vector2 newRupeeCountPosition = RupeeCountPosition;
            newRupeeCountPosition.Y += position.Y;
            spriteBatch.DrawString(font, RupeeCount, newRupeeCountPosition, Color.White);
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
            // Draw Inventory 
            DrawInventory(spriteBatch, position);
            position.Y += HUDInventory.Height * GameObjectManager.Instance.ScalingFactor;

            // Draw Large Map
            DrawMap(spriteBatch, position);
            position.Y += HUDMap.Height * GameObjectManager.Instance.ScalingFactor;

            // Draw HUD
            DrawHUD(spriteBatch, position);
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

            // TODO: draw the map
        }
        public void Reset()
        {
            // TODO: needed? 
        }
    }
}
