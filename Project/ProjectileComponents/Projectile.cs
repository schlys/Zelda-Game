using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;
using Project1.DirectionState;
using System.Reflection;

namespace Project1.ProjectileComponents
{
    public class Projectile : IProjectile, ICollidable
    {
        // Properties from IProjectile 
        public Vector2 Position { get; set; }
        public Vector2 OriginalPosition { get; set; }
        public IProjectileState State { get; set; }
        public bool InMotion { get; set; }

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public string TypeID { get; set; }

        // Other Properties
       

        public Projectile(Vector2 position, string direction, string state, string beam = "")
        {
            Position = position;
            OriginalPosition = Position;
            State = GetProjectileState(state, direction, beam);
            InMotion = true;

            Hitbox = CollisionManager.Instance.GetHitBox(Position, State.Sprite.HitBox); 
            IsMoving = true;
            TypeID = this.GetType().Name.ToString() + State.TypeID;
        }

        private IProjectileState GetProjectileState(string state, string direction, string beam)
        {
            Assembly assem = typeof(IProjectileState).Assembly;
            Type projectileType = assem.GetType("Project1.ProjectileComponents." + state + "ProjectileState");

            assem = typeof(IDirectionState).Assembly;
            Type directionType = assem.GetType("Project1.DirectionState.DirectionState" +  direction);

            ConstructorInfo directionConstructor = directionType.GetConstructor(Type.EmptyTypes);
            object directionState = directionConstructor.Invoke(Type.EmptyTypes);

            ConstructorInfo constructor;
            object projectile;
            if (beam.Length > 0)
            {
                constructor = projectileType.GetConstructor(new[] { typeof(IProjectile), typeof(IDirectionState), typeof(string) });
                projectile = constructor.Invoke(new object[] { this, (IDirectionState)directionState, beam });
            }
            else
            {
                constructor = projectileType.GetConstructor(new[] { typeof(IProjectile), typeof(IDirectionState) });
                projectile = constructor.Invoke(new object[] { this, (IDirectionState)directionState });
            }

            return (IProjectileState)projectile;
        }

        public void OffsetOriginalPosition(IDirectionState direction)
        {
            // Adjust start location to be beside the sprite based on the direction
            // TODO: not hardcode offset values 
            
           
                switch (direction.ID)
                {
                    case "Up":
                        Position += new Vector2(5, -8);
                        break;
                    case "Down":
                        Position += new Vector2(5, 15);
                        break;
                    case "Right":
                        Position += new Vector2(13, 8);
                        break;
                    default: // Left
                        Position += new Vector2(-8, 8);
                        break;
                }
            
            OriginalPosition = Position; 
        }

        public void StopMotion()
        {
            //InMotion = false; 
            State.StopMotion(); 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            State.Draw(spriteBatch); 
        }
        public void Update()
        {
            State.Update();

            if (Position.Equals(OriginalPosition)) InMotion = false;

            // Update Hitbox for collisions 
            if (InMotion)
                Hitbox = CollisionManager.Instance.GetHitBox(Position, State.Sprite.HitBox);
            
        }
    }
}
