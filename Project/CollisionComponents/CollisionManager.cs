using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.CollisionComponents
{
    class CollisionManager : ICollisionManager
    {
        private static CollisionManager instance = new CollisionManager();
        private List<ICollidable> MovingObjects;
        private List<ICollidable> NonMovingObjects; 
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
                    if(DetectCollision(item1, item2))
                    {
                        item1.Collide(item2);
                        item2.Collide(item1); 
                    }
                }

                foreach (ICollidable item2 in MovingObjects)
                {
                    if (item1 != item2 && DetectCollision(item1, item2))
                    {
                        item1.Collide(item2);
                        item2.Collide(item1);
                    }
                }
            }
        }
        public bool DetectCollision(ICollidable item1, ICollidable item2)
        {
            if(item1.Hitbox.Intersects(item2.Hitbox))   
            {
                return true; 
            } 
            return false; 
        }
        public void Update()
        {
            DetectCollisions(); 
        }
    }
}
