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
        public CollisionManager() { }

        public void AddMovingObject(ICollidable item)
        {
            if(!MovingObjects.Contains(item))   // Not allow duplicate objects 
            {
                MovingObjects.Add(item); 
            }
        }
        public void RemoveMovingObject(ICollidable item)
        {
            MovingObjects.Remove(item); 
        }
        public void AddNonMovingObject(ICollidable item)
        {
            if (!NonMovingObjects.Contains(item))   // Not allow duplicate objects 
            {
                NonMovingObjects.Add(item);
            }
        }
        public void RemoveNonMovingObject(ICollidable item)
        {
            NonMovingObjects.Remove(item);
        }

        public void DetectCollisions()
        {

        }
        public bool DetectCollision(ICollidable item1, ICollidable item2)
        {
            return false; 
        }
        public void Update()
        {
            
        }
    }
}
