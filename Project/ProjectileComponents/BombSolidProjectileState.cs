using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using Project1.CollisionComponents;
using Project1.DirectionState;

namespace Project1.ProjectileComponents
{
    class BombSolidProjectileState : IProjectileState
    {
        // Properties from IProjectileState
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }
        public double Damage { get; set; }

        // Other Properties
        private int Speed = 0;
        private int Counter = 0;
        private int CounterExplode = 30;
        private int CounterMax;
        private ICollidable proj;
        public BombSolidProjectileState(IProjectile projectile, IDirectionState direction)
        {
            TypeID = "Bomb";
            Damage = 30;
            Projectile = projectile;
            proj = (ICollidable)projectile;
            Direction = direction; 
            Sprite = SpriteFactory.Instance.GetSpriteData("Bomb");
            CounterMax = CounterExplode + (int)((Sprite.TotalFrames)*(Sprite.MaxDelay * Sprite.DelayRate));
            Projectile.OffsetOriginalPosition(Direction);
        }
        public void StopMotion()
        {
            // trigger explosion
            if (Counter < CounterExplode)
            {
                Counter = CounterExplode;  
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Projectile.Position);
        }

        public void Update()
        {

            if (Counter == 0)
            {
                GameSoundManager.Instance.PlayBombDrop();
            }
            if (Counter == CounterExplode)
            {
                GameSoundManager.Instance.PlayBombBlow();
            }

            Counter++;
            if (Counter < CounterExplode)
            {
                switch (Direction.ID)
                {
                    case GameVar.DirectionUp:
                        Projectile.Position += new Vector2(0, -Speed);
                        break;
                    case GameVar.DirectionDown:
                        Projectile.Position += new Vector2(0, +Speed);
                        break;
                    case GameVar.DirectionRight:
                        Projectile.Position += new Vector2(Speed, 0);
                        break;
                    default:
                        Projectile.Position += new Vector2(-Speed, 0);
                        break;
                }
            }
            else if (Counter < CounterMax) // Explosion animation 
            {
                Sprite.Update();
                TypeID = "Explosion";
                proj.TypeID = proj.TypeID + TypeID;
            }
            else
            {
                Projectile.RemoveProjectile(); 
            }

        }
    }
}
