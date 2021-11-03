using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
using Project1.LevelComponents;

namespace Project1.GameState
{
    public class GameStateWin: IGameState
    {
        public Vector2 RoomPosition { get; set; }
        public string ID { get; set; }
        private Sprite Link;
        private Sprite TriForceFragment;
        //private int Height = 176 * GameObjectManager.Instance.ScalingFactor;
        //private int Width = 256 * GameObjectManager.Instance.ScalingFactor;
        public GameStateWin()
        {
            ID = "Win";
            TriForceFragment = SpriteFactory.Instance.GetSpriteData("TriforceFragment");
            Link = SpriteFactory.Instance.GetSpriteData("PickUpItem");
        }
        public void Update()
        {
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 RoomSize = LevelFactory.Instance.CurrentRoom.Size;

            Rectangle destinationRectangle = new Rectangle(50, 50, (int)RoomSize.X, (int)RoomSize.Y);
            //spriteBatch.Draw(, destinationRectangle, Color.White);
            TriForceFragment.Draw(spriteBatch, new Vector2(RoomSize.X / 2, RoomSize.Y / 4));
            Link.Draw(spriteBatch, new Vector2(RoomSize.X/2, RoomSize.Y / 2));
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
