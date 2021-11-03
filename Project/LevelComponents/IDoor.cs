using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Project1.LinkComponents;
using Project1.SpriteComponents;

namespace Project1.LevelComponents
{
	public interface IDoor
	{
		Vector2 Position { get; set; }
		Sprite Sprite { get; set; }
		/* I don't think the door object should handle the room scroll, rather a collision between Link and a door object
		 * will call a command to LevelFactory. Therefore, we only need to draw the door
		 */
		void Draw(SpriteBatch spriteBatch);
		bool IsLocked();
		void Unlock();
	}
}
