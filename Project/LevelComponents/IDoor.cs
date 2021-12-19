/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project1.SpriteComponents;
using Project1.DirectionState; 

namespace Project1.LevelComponents
{
	public interface IDoor
	{
		Vector2 Position { get; set; }
		Vector2 PositionDelta { get; set; }
		Sprite Sprite { get; set; }
		IDirectionState DirectionState { get; set; }
		void Draw(SpriteBatch spriteBatch);
		bool IsLocked();
		void Unlock();
		void ChangeRoom();
	}
}
