using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.CollisionComponents;

namespace Project1.LevelComponents
{
	public class Door : IDoor, ICollidable
	{
		// from IDoor
		public Vector2 Position { get; set; }
		public Vector2 PositionDelta { get; set; }
		public Sprite Sprite { get; set; }
		// from ICollidable
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public string TypeID { get; set; }
		public string Direction { get; set; }

		private bool locked;


		public Door(Vector2 position, string direction, bool locked, Vector2 positionDelta)
        {
			//PositionDelta = new Vector2(LevelFactory.Instance.RoomBlockSize*positionDelta.X, 
			//					LevelFactory.Instance.RoomBlockSize * positionDelta.Y);
			PositionDelta = positionDelta;

			Direction = direction;
			this.locked = locked;
			/*if (this.locked)*/ Sprite = SpriteFactory.Instance.GetSpriteData("Door" + direction);
			Position = position;
			Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox);
			/* Correct the position to account for empty space around the hitbox */
			int RoomBlockSize = SpriteFactory.Instance.UniversalSize * GameObjectManager.Instance.ScalingFactor;
			Position -= new Vector2((RoomBlockSize - Hitbox.Width) / 2, (RoomBlockSize - Hitbox.Height) / 2);
			/* Get correct hibox for updated position */
			Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox);
			IsMoving = false;
			TypeID = "Door";
        }

		public void Draw(SpriteBatch spriteBatch)
		{
			if (locked) Sprite.Draw(spriteBatch, Position);
		}
		public bool IsLocked()
        {
			return locked;
        }
		public void Unlock()
        {
			locked = false;
        }
		public void ChangeRoom()
        {
			switch (Direction)
			{
				case "Up":
					LevelFactory.Instance.MoveUp(PositionDelta);
					break;
				case "Down":
					LevelFactory.Instance.MoveDown(PositionDelta);
					break;
				case "Right":
					LevelFactory.Instance.MoveRight(PositionDelta);
					break;
				case "Left":
					LevelFactory.Instance.MoveLeft(PositionDelta);
					break;
			}
		}
	}
}
