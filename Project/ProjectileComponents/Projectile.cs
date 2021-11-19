using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.CollisionComponents;
using Project1.DirectionState;
using System.Reflection;
using Project1.LevelComponents;
using System.Xml;

namespace Project1.ProjectileComponents
{
    public class Projectile : IProjectile, ICollidable
    {
        // Properties from IProjectile 
        public Vector2 Position { get; set; }
        public IProjectileState State { get; set; }

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public string TypeID { get; set; }

        // Other Properties
       

        public Projectile(Vector2 position, string direction, string state, string beam = "")
        {
            Position = position;
            State = GetProjectileState(state, direction, beam);

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

            XmlDocument XMLData = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLData/XMLProjectile.xml";
            XMLData.Load(path);
            XmlNodeList Offset = XMLData.DocumentElement.SelectNodes("/Position/Offset");
             Dictionary<String, Vector2> Offsets = new Dictionary<string, Vector2>();
            foreach (XmlNode node in Offset)
            {
                string name = node.SelectSingleNode("Name").InnerText;
                int x = Int16.Parse(node.SelectSingleNode("x").InnerText) * GameObjectManager.Instance.ScalingFactor;
                int y = Int16.Parse(node.SelectSingleNode("y").InnerText) * GameObjectManager.Instance.ScalingFactor;

                Offsets.Add(name, new Vector2(x, y));
            }
            Position += Offsets[direction.ID];         
        }

        public void StopMotion()
        {
            State.StopMotion(); 
        }

        public void RemoveProjectile()
        {
            // Removes this from GameObjectManager 
            GameObjectManager.Instance.RemoveProjectile(this); 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            State.Draw(spriteBatch); 
        }
        public void Update()
        {
            State.Update();

            //if (Position.Equals(OriginalPosition)) InMotion = false;

            // Update Hitbox for collisions 
            Hitbox = CollisionManager.Instance.GetHitBox(Position, State.Sprite.HitBox);

            // stop projectile from going out of bounds
            Vector2 TopLeft = new Vector2(Hitbox.X, Hitbox.Y);
            Vector2 BottomRight = new Vector2(Hitbox.X + Hitbox.Width, Hitbox.Y + Hitbox.Height);
            if (!LevelFactory.Instance.IsWithinRoomBounds(TopLeft) || !LevelFactory.Instance.IsWithinRoomBounds(BottomRight))
            {
                State.StopMotion(); 
            }

        }
    }
}
