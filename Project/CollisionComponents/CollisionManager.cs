using Microsoft.Xna.Framework;
using Project1.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.CollisionComponents
{
    class CollisionManager : ICollisionManager
    {
        private static CollisionManager instance = new CollisionManager();
        public List<ICollidable> MovingObjects;
        public List<ICollidable> NonMovingObjects;
        private static Dictionary<string, Tuple<Type, Type>> CollisionMappings;

        public static CollisionManager Instance
        {
            get
            {
                return instance;
            }
        } 
        private CollisionManager() 
        {
            MovingObjects = new List<ICollidable>();
            NonMovingObjects = new List<ICollidable>();
            CreateDict();
        }

        private void CreateDict()
        {
            CollisionMappings = new Dictionary<string, Tuple<Type, Type>>();

            CollisionMappings.Add("LinkBlockTop",Tuple.Create(typeof(LinkStopMovingCmd), typeof(NoCmd)));
            CollisionMappings.Add("LinkBlockBottom", Tuple.Create(typeof(LinkStopMovingCmd), typeof(NoCmd)));
            CollisionMappings.Add("LinkBlockRight", Tuple.Create(typeof(LinkStopMovingCmd), typeof(NoCmd)));
            CollisionMappings.Add("LinkBlockLeft", Tuple.Create(typeof(LinkStopMovingCmd), typeof(NoCmd)));

            CollisionMappings.Add("LinkItemTop", Tuple.Create(typeof(LinkStopMovingCmd), typeof(NoCmd)));
            CollisionMappings.Add("LinkItemBottom", Tuple.Create(typeof(LinkStopMovingCmd), typeof(NoCmd)));
            CollisionMappings.Add("LinkItemRight", Tuple.Create(typeof(LinkStopMovingCmd), typeof(NoCmd)));
            CollisionMappings.Add("LinkItemLeft", Tuple.Create(typeof(LinkStopMovingCmd), typeof(NoCmd)));

            CollisionMappings.Add("EnemyMoblinProjectileTop", Tuple.Create(typeof(NoCmd), typeof(NoCmd)));
            CollisionMappings.Add("EnemyMoblinProjectileBottom", Tuple.Create(typeof(NoCmd), typeof(NoCmd)));
            CollisionMappings.Add("EnemyMoblinProjectileRight", Tuple.Create(typeof(NoCmd), typeof(NoCmd)));
            CollisionMappings.Add("EnemyMoblinProjectileLeft", Tuple.Create(typeof(NoCmd), typeof(NoCmd)));

            CollisionMappings.Add("LinkMoblinProjectileTop", Tuple.Create(typeof(LinkTakeDamageCmd), typeof(NoCmd)));
            CollisionMappings.Add("LinkMoblinProjectileBottom", Tuple.Create(typeof(LinkTakeDamageCmd), typeof(NoCmd)));
            CollisionMappings.Add("LinkMoblinProjectileRight", Tuple.Create(typeof(LinkTakeDamageCmd), typeof(NoCmd)));
            CollisionMappings.Add("LinkMoblinProjectileLeft", Tuple.Create(typeof(LinkTakeDamageCmd), typeof(NoCmd)));

            CollisionMappings.Add("MoblinProjectileItemRight", Tuple.Create(typeof(NoCmd), typeof(NoCmd)));
        }
        public Tuple<Type, Type> GetCommands(ICollision collision)
        {
            // TODO: need to test if found in dictionary, because not all found. else error 
            return CollisionMappings[collision.First + collision.Second + collision.Direction];
        }

        public void AddObject(ICollidable item)
        {
            if (item.IsMoving && !MovingObjects.Contains(item))   // Not allow duplicate objects 
            {
                MovingObjects.Add(item);
                
            } else if(!item.IsMoving && !NonMovingObjects.Contains(item))   // Not allow duplicate objects 
            {
                NonMovingObjects.Add(item);
            }
        }
        public void RemoveObject(ICollidable item)
        {
            if (item.IsMoving)
            {
                MovingObjects.Remove(item); 
            }
            else 
            {
                NonMovingObjects.Remove(item);
            }
        }

        // NOTE: belong somewhere else? 
        public Rectangle GetHitBox(Vector2 position, Vector2 dimensions, int size)
        {
            int xPos = (int)(position.X + (size / 2) - (dimensions.X / 2));
            int yPos = (int)(position.Y + (size / 2) - (dimensions.Y / 2));
            return new Rectangle(xPos, yPos, (int)dimensions.X, (int)dimensions.Y); 
        }
        public void DetectCollisions()
        {
            // TODO: try to make iteration more efficent 
            // NOTE: Compare all moving objects to all other non-moving and moving objects 
            for (int i = 0; i < MovingObjects.Count; i++)
            {
                ICollidable item1 = MovingObjects[i];

                //foreach(ICollidable item2 in MovingObjects)
                for (int j = i+1; j < MovingObjects.Count; j++)
                {
                    ICollidable item2 = MovingObjects[j];
                    ICollision collision = DetectCollision(item1, item2); 
                    if (!collision.GetType().Name.ToString().Equals("NullCollision"))
                    {
                        
                        //collision.Execute();
                    }
                }

                foreach (ICollidable item2 in NonMovingObjects)
                {
                   
                    ICollision collision = DetectCollision(item1, item2);
                    if (!collision.GetType().Name.ToString().Equals("NullCollision"))
                    {
                        //collision.Execute();
                    }
  
                }
            }
        }
        public ICollision DetectCollision(ICollidable item1, ICollidable item2)
        {

            string direction;
            if(item1.Hitbox.Intersects(item2.Hitbox))   
            {
                // TODO: how handle collision if both moving? 
                if(item1.IsMoving && item2.IsMoving)
                {
                    Rectangle Intersection = Rectangle.Intersect(item1.Hitbox, item2.Hitbox);
                    if (Intersection.Right == item1.Hitbox.Right)
                    {
                        // collide on item1's right and item2's left 
                        direction = "Right";
                    }
                    else if (Intersection.Left == item1.Hitbox.Left)
                    {
                        // collide on item1's left and item2's right 
                        direction = "Left";
                    }
                    else if (Intersection.Top == item1.Hitbox.Top)
                    {
                        // collide on item1's top and item2's bottom 
                        direction = "Top";
                    }
                    else
                    {
                        // collide on item1's bottom and item2's top
                        direction = "Bottom";
                    }
                //return new Collision(item1, item2, direction);
                }
                else if(item1.IsMoving)
                {
                    direction = item1.DirectionMoving.ID; 
                } else if (item2.IsMoving)
                {
                    direction = item2.DirectionMoving.ID;
                } else // NOTE: Is it possible to have a collision between two non moving objects? 
                {
                    direction = "Right"; 
                }
                return new Collision(item1, item2, direction);  
            } 
            return new NullCollision(); 
        }

        public void Update()
        {
            DetectCollisions();

            /* NOTE: moving objects can become nonmoving objects while we use next / prev items
             *  so to account for the same object switching between lists, we must remove and readd 
             *  all items. 
             */
            
            List<ICollidable> copy_MovingObjects = MovingObjects;
            List<ICollidable> copy_NonMovingObjects = NonMovingObjects;
            
            MovingObjects = new List<ICollidable>();
            NonMovingObjects = new List<ICollidable>(); 

            foreach(ICollidable c in copy_MovingObjects)
            {
                AddObject(c); 
            }
            foreach (ICollidable c in copy_NonMovingObjects)
            {
                AddObject(c);
            }
        }
    }
}
