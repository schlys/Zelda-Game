using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.LevelComponents
{
	public class LevelMap : ILevelMap
	{
		// Properties from ILevelMap 
		public Texture2D Texture { get; set; }
		
		// Other properties 
		private Vector2 BlockSize;
		private Vector2 StartBlock;
		private Vector2 TriforceFragmentBlock;      // the TriforceFragment treasure is in room 15 
		private Vector2 CurrentBlock;
		private int BufferSize; 

		public LevelMap(Texture2D texture)
        {
			Texture = texture;

			TriforceFragmentBlock = GameVar.GetLevelMapTriforceFragmentPosition() * GameVar.ScalingFactor;
			BlockSize = GameVar.GetLevelMapBlockSize() * GameVar.ScalingFactor;
			CurrentBlock = GameVar.GetLevelMapStartPosition() * GameVar.ScalingFactor;
			StartBlock = CurrentBlock;
			BufferSize = 1 * GameVar.ScalingFactor;
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 position, bool CanDrawTriforceFragment)
        {
			// Draw the map
			Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 
				(int)Texture.Width * GameVar.ScalingFactor, 
				(int)Texture.Height * GameVar.ScalingFactor);
			spriteBatch.Draw(Texture, destinationRectangle, Color.White);

			// Draw the block highlighting the current room
			destinationRectangle = new Rectangle((int)(CurrentBlock.X + position.X), (int)(CurrentBlock.Y+ position.Y), 
				(int)BlockSize.X, (int)BlockSize.Y);
			Texture2D CurrentBlockTexture = new Texture2D(GameObjectManager.Instance.Game.GraphicsDevice, 1, 1);
			CurrentBlockTexture.SetData(new Color[] { Color.White });
			spriteBatch.Draw(CurrentBlockTexture, destinationRectangle, Color.White);
			
			// Draw the block highlighting the triforce fragment room if possible
			if (CanDrawTriforceFragment)
			{
				destinationRectangle = new Rectangle((int)(TriforceFragmentBlock.X + position.X), (int)(TriforceFragmentBlock.Y + position.Y),
					(int)BlockSize.X, (int)BlockSize.Y);
				Texture2D TriforceFragmentTexture = new Texture2D(GameObjectManager.Instance.Game.GraphicsDevice, 1, 1);
				TriforceFragmentTexture.SetData(new Color[] { Color.Yellow });
				spriteBatch.Draw(CurrentBlockTexture, destinationRectangle, Color.Yellow);
			}
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
