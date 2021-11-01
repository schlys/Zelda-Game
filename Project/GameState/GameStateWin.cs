using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;

namespace Project1.GameState
{
    public class GameStateWin: IGameState
    {
        public Vector2 RoomPosition { get; set; }
        public string ID { get; set; }
        private Sprite Link;
        private Sprite TriForceFragment;
        private int Height = 176 * GameObjectManager.Instance.ScalingFactor;
        private int Width = 256 * GameObjectManager.Instance.ScalingFactor;
        public GameStateWin()
        {
            ID = "Win"; 
        }
        public void Update()
        {
            TriForceFragment = SpriteFactory.Instance.GetSpriteData("triForceFragment");
            Link = SpriteFactory.Instance.GetSpriteData("PickUpItem");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destinationRectangle = new Rectangle(50, 50, Width, Height);
            //spriteBatch.Draw(, destinationRectangle, Color.White);
            TriForceFragment.Draw(spriteBatch, new Vector2(Width / 2, Height / 4));
            Link.Draw(spriteBatch, new Vector2(Width/2, Height / 2));
        }
        public IGameState Reset()
        {
            return new GameStateStart();
        }
        public IGameState Pause()
        {
            // TODO: cannot go win to pause ??
            return this; 
        }
        public IGameState StartGame()
        {
            return new GameStateStart();
        }
        public IGameState WinGame()
        {
            return this;
        }
        public IGameState LoseGame()
        {
            // TODO: cannot go win to pause ??
            return this;
        }
        public IGameState ItemSelectMenu()
        {
            // TODO: cannot go win to pause ??
            return this;
        }
    }
}
