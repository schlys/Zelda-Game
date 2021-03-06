/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project1.LevelComponents
{
	public interface ILevelMap
	{
		Texture2D Texture { get; set; }
		void Draw(SpriteBatch spriteBatch, Vector2 position, bool CanDrawTriforceFragment);
		void Reset();
		void MoveUp();
		void MoveDown();
		void MoveRight();
		void MoveLeft();
	}
}
