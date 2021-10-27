using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;

namespace Project1.LevelComponents
{
	public class Door : IDoor
	{
		public Vector2 Position { get; set; }
		public Texture2D Texture { get; set; }

		private Color Color = Color.White;
		private int Height = 16 * GameObjectManager.Instance.ScalingFactor;
		private int Width = 16 * GameObjectManager.Instance.ScalingFactor;

		public Door(Vector2 position, Texture2D texture)
        {
			Position = position;
			Texture = texture;
        }

		public void Draw(SpriteBatch spriteBatch)
		{
			Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
			spriteBatch.Draw(Texture, destinationRectangle, Color);
		}
	}
}
