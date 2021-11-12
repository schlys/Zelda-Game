using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;
using Project1.DirectionState;

namespace Project1.ProjectileComponents
{
    class BoomerangSolidProjectileState : IProjectileState
    {
        // Properties from IProjectileState
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }

        // Other Properties 
        public bool isUsing { get; set; }
        private int Speed = 6;
        int Counter;
        int CounterMax = 50;
        private Vector2 InitialPosition; 
        public BoomerangSolidProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction; 
            TypeID = "Boomerang"; 
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID);
            Counter = 0;
            Projectile.OffsetOriginalPosition(Direction);
            InitialPosition = Projectile.Position; 
        }

        public void StopMotion()
        {
            Speed *= -1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Projectile.Position);
        }

        public void Update()
        {
            Sprite.Update();

            if (Counter == 0)
            {
                GameSoundManager.Instance.PlayArrowBoomerang();
            }

            switch (Direction.ID)
            {
                case "Up":
                    Projectile.Position = new Vector2(Projectile.Position.X, Projectile.Position.Y - Speed);
                    break;
                case "Down":
                    Projectile.Position = new Vector2(Projectile.Position.X, Projectile.Position.Y + Speed);
                    break;
                case "Right":
                    Projectile.Position = new Vector2(Projectile.Position.X + Speed, Projectile.Position.Y);
                    break;
                default:
                    Projectile.Position = new Vector2(Projectile.Position.X - Speed, Projectile.Position.Y);
                    break;
            }

            Counter++;

            if (Counter > (CounterMax / 2) && Speed > 0)  // reverse direction for first time 
            {
                Speed *= -1;
            }
            else if (Counter > CounterMax || (Speed < 0 && IsCollide(InitialPosition, Projectile.Position, Speed)))    // returned to sender 
            {
                Projectile.RemoveProjectile();
            }

        }

        private bool IsCollide(Vector2 pos1, Vector2 pos2, int buffer)
        {
            /* Return true if <pos1> and <pos2> are within <buffer> of eachother in both the x and y
             * Parallel Construction: in MagicalBoomerangSolidProjectileState.cs and GoriyaProjectileState.cs 
             */
            return Math.Abs(pos1.X - pos2.X) < Math.Abs(buffer) && Math.Abs(pos1.Y - pos2.Y) < Math.Abs(buffer);
        }
    }
}
