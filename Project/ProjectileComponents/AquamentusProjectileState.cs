/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using Project1.DirectionState; 

namespace Project1.ProjectileComponents
{ 
    class AquamentusProjectileState : IProjectileState
    {
        // Properties from IProjectileState
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }
        public double Damage { get; set; }

        // Other Properties
        private int Counter;
        private int MaxCounter = 200; 
        private int SpeedX = 2;
        private int SpeedY = 1;
        public AquamentusProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction; 
            TypeID = GameVar.AquamentusProjectileKey;       // used for the sprite key
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID);
            TypeID = GameVar.AquamentusSpriteKey;              // used for the collisions key
            Counter = 0;
        }
        public void StopMotion()
        {
            Projectile.RemoveProjectile(); 
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Projectile.Position);
        }
        public void Update()
        {
            Sprite.Update();
            Counter++;
            if (Counter < MaxCounter)
            {
                if (Direction.ID.Equals(GameVar.DirectionUp))  // up and left 
                    Projectile.Position += new Vector2((float)-SpeedX, -SpeedY);
                else if (Direction.ID.Equals(GameVar.DirectionLeft))
                    Projectile.Position += new Vector2((float)-SpeedX, 0);
                else if (Direction.ID.Equals(GameVar.DirectionDown))   // down and left 
                    Projectile.Position += new Vector2((float)-SpeedX, SpeedY);
            }
            else
            {
                Projectile.RemoveProjectile(); 
            }
        }
    }
}
