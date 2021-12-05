using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.CollisionComponents;
using Project1.DirectionState; 

namespace Project1.LevelComponents
{
	public class Door : IDoor, ICollidable
	{
		// from IDoor
		public Vector2 Position { get; set; }
		public Vector2 PositionDelta { get; set; }		// if nonzero, the location of the door relative to the room
		public Sprite Sprite { get; set; }
		public IDirectionState DirectionState { get; set; }

		// from ICollidable
		public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public string TypeID { get; set; }

		private bool locked;

		public Door(Vector2 position, string direction, bool locked, Vector2 positionDelta)
		{ 
			PositionDelta = positionDelta;

			DirectionState = DirectionManager.Instance.GetDirectionState(direction);

			// TODO: readd locked doors before submission 
			this.locked = locked;
			/*if (this.locked)*/ Sprite = SpriteFactory.Instance.GetSpriteData(GameVar.DoorKey + direction);
			
			Position = position;
			Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox);
			/* Correct the position to account for empty space around the hitbox */
			int RoomBlockSize = SpriteFactory.Instance.UniversalSize * GameVar.ScalingFactor;
			Position -= new Vector2((RoomBlockSize - Hitbox.Width) / 2, (RoomBlockSize - Hitbox.Height) / 2);
			/* Get correct hibox for updated position */
			Hitbox = CollisionManager.Instance.GetHitBox(Position, Sprite.HitBox);
			
			IsMoving = false;
			TypeID = GameVar.DoorKey;
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
			GameSoundManager.Instance.PlayDoorUnlock();
		}
		public void ChangeRoom()
        {
			if(DirectionState is DirectionStateUp)
            {
				GameObjectManager.Instance.MoveUp(PositionDelta);
			}
			else if (DirectionState is DirectionStateDown)
			{
				GameObjectManager.Instance.MoveDown(PositionDelta);
			}
			else if (DirectionState is DirectionStateLeft)
			{
				GameObjectManager.Instance.MoveLeft(PositionDelta);
			}
			else if (DirectionState is DirectionStateRight)
			{
				GameObjectManager.Instance.MoveRight(PositionDelta);
			}
		}
	}
}
