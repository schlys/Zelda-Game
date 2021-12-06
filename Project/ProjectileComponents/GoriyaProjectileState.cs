using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using Project1.DirectionState;

namespace Project1.ProjectileComponents
{
    class GoriyaProjectileState : IProjectileState
    {
        // Properties from IProjectileState
        public IProjectile Projectile { get; set; }
        public Sprite Sprite { get; set; }
        public String TypeID { get; set; }
        public IDirectionState Direction { get; set; }
        public double Damage { get; set; }

        // Other Properties 
        private int Counter = 0;
        private int CounterMax = 100;
        private int Speed = 2;
        private Vector2 InitialPosition; 
        public GoriyaProjectileState(IProjectile projectile, IDirectionState direction)
        {
            Projectile = projectile;
            Direction = direction;
            TypeID = "GoriyaProjectile";    // used for the sprite key 
            Sprite = SpriteFactory.Instance.GetSpriteData(TypeID);
            TypeID = "Goriya";              // used for the collisions key 
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

            Counter++;

            if (Counter < CounterMax)
            {
                switch (Direction.ID)
                { 
                    case GameVar.DirectionUp:
                        Projectile.Position += new Vector2(0, (float)-Speed);
                        break;
                    case GameVar.DirectionDown:
                        Projectile.Position += new Vector2(0, (float)Speed);
                        break;
                    case GameVar.DirectionRight:
                        Projectile.Position += new Vector2((float)Speed, 0);
                        break;
                    default: //Left
                        Projectile.Position += new Vector2((float)-Speed, 0);
                        break;
                }
            }        

            if (Counter > (CounterMax / 2) && Speed > 0)
            {
                    Speed *= -1;
            }

            if(Counter > CounterMax || (Speed < 0 && IsCollide(InitialPosition, Projectile.Position, Speed)))    // returned to sender 
            {
                Projectile.RemoveProjectile();
            }
        }

        private bool IsCollide(Vector2 pos1, Vector2 pos2, int buffer)
        {
            /* Return true if <pos1> and <pos2> are within <buffer> of eachother in both the x and y
             * Parallel Construction: in MagicalBoomerangSolidProjectileState.cs and BoomerandSolidProjectileState.cs 
             */
            return Math.Abs(pos1.X - pos2.X) < Math.Abs(buffer) && Math.Abs(pos1.Y - pos2.Y) < Math.Abs(buffer);
        }
    }
}
