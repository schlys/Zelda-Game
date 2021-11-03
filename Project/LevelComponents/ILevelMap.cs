using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project1.LinkComponents;

namespace Project1.LevelComponents
{
	public interface ILevelMap
	{
		Vector2 Position { get; set; }
		Texture2D Texture { get; set; }
		void Draw(SpriteBatch spriteBatch, Vector2 position);
		void Reset();
		void MoveUp();
		void MoveDown();
		void MoveRight();
		void MoveLeft();
	}
}
