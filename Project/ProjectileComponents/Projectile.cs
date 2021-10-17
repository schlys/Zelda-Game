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
        public int Size { get; set; }
        public bool InMotion { get; set; }

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public string TypeID { get; set; }

        // Other Properties
       

        public Projectile(Vector2 position, string direction, string state)
        {
            Position = position;
            OriginalPosition = Position;
            State = GetProjectileState(state, direction);
            Size = 80;
            InMotion = true;

          
            Hitbox = CollisionManager.Instance.GetHitBox(Position, State.Sprite.HitBox, Size);
            IsMoving = true;
            TypeID = this.GetType().Name.ToString() + State.TypeID;
        }

        private IProjectileState GetProjectileState(string state, string direction)
        {
           /* IDirectionState Direction = GetDirectionState(direction);
            IProjectileState State = new ArrowProjectileState(this, Direction);  //default
            switch(state)
            {
                case "Aquamentus":
                    State = new AquamentusProjectileState(this, Direction);
                    break;
                case "Arrow":
                    State = new ArrowProjectileState(this, Direction);
                    break;
                case "Bomb":
                    State = new BombProjectileState(this, Direction);
                    break;
                case "Boomerang":
                    State = new BoomerangProjectileState(this, Direction);
                    break;
                case "Fire":
                    State = new FireProjectileState(this, Direction);
                    break;
                case "Goriya":
                    State = new GoriyaProjectileState(this, Direction);
                    break;
                case "MagicalBoomerang":
                    State = new MagicalBoomerangProjectileState(this, Direction);
                    break;
                case "Moblin":
                    State = new MoblinProjectileState(this, Direction);
                    break;
                case "SilverArrow":
                    State = new SilverArrowProjectileState(this, Direction);
                    break;
                default:
                    throw new InvalidOperationException("Invalid Projectile State used");
            }
            return State;*/

            Assembly assem = typeof(IProjectileState).Assembly;
            Type projectileType = assem.GetType("Project1.ProjectileComponents." + state + "ProjectileState");

            assem = typeof(IDirectionState).Assembly;
            Type directionType = assem.GetType("Project1.DirectionState.DirectionState" +  direction);

            ConstructorInfo directionConstructor = directionType.GetConstructor(Type.EmptyTypes);
            ConstructorInfo constructor = projectileType.GetConstructor(new[] { typeof(IProjectile), typeof(IDirectionState) });

            object directionState = directionConstructor.Invoke(Type.EmptyTypes);
            object projectile = constructor.Invoke(new object[] { this, (IDirectionState)directionState });

            return (IProjectileState)projectile;
        }

        /*
        private IDirectionState GetDirectionState(string direction)
        {
            IDirectionState Direction; 
            switch (direction)
            {
                case "Up":
                    Direction = new DirectionStateUp();
                    break;
                case "Down":
                    Direction = new DirectionStateDown();
                    break;
                case "Left":
                    Direction = new DirectionStateLeft();
                    break;
                case "Right":
                    Direction = new DirectionStateRight();
                    break;
                default:
                    Direction = new DirectionStateRight();
                    break;
            }
            return Direction; 
        }*/

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
            InMotion = false; 
            State.StopMotion(); 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            State.Draw(spriteBatch); 
        }
        public void Update()
        {
            State.Update();
            
            // Update Hitbox for collisions 
            Hitbox = CollisionManager.Instance.GetHitBox(Position, State.Sprite.HitBox, Size);
        }
    }
}
