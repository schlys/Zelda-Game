using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;

namespace Project1.LevelComponents
{
	public class LevelMap : ILevelMap
	{
		public Vector2 Position { get; set; }
		public Texture2D Texture { get; set; }
		private Vector2 Size;
		private Vector2 BlockSize;
		private Vector2 DestinationBlockSize;
		private Vector2 StartBlock;
		private Vector2 CurrentBlock;
		private int BufferSize; 

		public LevelMap(Texture2D texture)
        {
			Texture = texture;

			// TODO: load from XML
			Position = new Vector2(10, 10);
			Size = new Vector2(63 * GameObjectManager.Instance.ScalingFactor, 31 * GameObjectManager.Instance.ScalingFactor);
			BlockSize = new Vector2(7 * GameObjectManager.Instance.ScalingFactor, 3 * GameObjectManager.Instance.ScalingFactor);
			CurrentBlock = new Vector2(24 * GameObjectManager.Instance.ScalingFactor, 27 * GameObjectManager.Instance.ScalingFactor) + Position;
			StartBlock = CurrentBlock;
			BufferSize = 1 * GameObjectManager.Instance.ScalingFactor;
		}

		public void Draw(SpriteBatch spriteBatch)
        {
			// draw map
			Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
			spriteBatch.Draw(Texture, destinationRectangle, Color.White);

			// draw block highlighting current room
			destinationRectangle = new Rectangle((int)CurrentBlock.X, (int)CurrentBlock.Y, (int)BlockSize.X, (int)BlockSize.Y);
			Texture2D CurrentBlockTexture = new Texture2D(GameObjectManager.Instance.Game.GraphicsDevice, 1, 1);
			CurrentBlockTexture.SetData(new Color[] { Color.White });
			spriteBatch.Draw(CurrentBlockTexture, destinationRectangle, Color.White);
		}
		public void Reset()
        {
			CurrentBlock = StartBlock;
        }

		public void MoveUp()
        {
			CurrentBlock.Y -= (BlockSize.Y + BufferSize);
        }
		public void MoveDown()
        {
			CurrentBlock.Y += (BlockSize.Y + BufferSize);
		}
		public void MoveRight()
        {
			CurrentBlock.X += (BlockSize.X + BufferSize);
		}
		public void MoveLeft()
        {
			CurrentBlock.X -= (BlockSize.X + BufferSize);
		}
	}
}
