using Microsoft.Xna.Framework;
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

        public static CollisionManager Instance
        {
            get
            {
                return instance;
            }
        } 
        public CollisionManager() 
        {
            MovingObjects = new List<ICollidable>();
            NonMovingObjects = new List<ICollidable>();
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
        public void DetectCollisions()
        {
            // TODO: try to make iteration more efficent 
            // NOTE: Compare all moving objects to all other non-moving and moving objects 
            foreach(ICollidable item1 in MovingObjects)
            {
                foreach(ICollidable item2 in NonMovingObjects)
                {
                    ICollision collision = DetectCollision(item1, item2); 
                    if (!collision.GetType().Name.ToString().Equals("NullCollision"))
                    {
                        item1.Collide(item2);
                        item2.Collide(item1); 
                    }
                }

                foreach (ICollidable item2 in MovingObjects)
                {
                    if (item1 != item2){
                        ICollision collision = DetectCollision(item1, item2);
                        if (!collision.GetType().Name.ToString().Equals("NullCollision"))
                        {
                            item1.Collide(item2);
                            item2.Collide(item1);
                        }
                    }
                }
            }
        }
        public ICollision DetectCollision(ICollidable item1, ICollidable item2)
        {
            
            String direction = ""; 
            if(item1.Hitbox.Intersects(item2.Hitbox))   
            {
                Rectangle Intersection = Rectangle.Intersect(item1.Hitbox, item2.Hitbox); 
                if(Intersection.Right == item1.Hitbox.Right)
                {
                    // collide on item1's right and item2's left 
                    direction = "Right"; 
                } else if(Intersection.Left == item1.Hitbox.Left)
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
                return new Collision(item1, item2, direction);  
            } 
            return new NullCollision(); 
        }

        public void Update()
        {
            DetectCollisions(); 
        }
    }
}
