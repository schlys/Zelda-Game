using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using Project1.Command;
using Project1.SpriteComponents; 

namespace Project1.CollisionComponents
{
    public sealed class CollisionManager : ICollisionManager
    {
        private static CollisionManager instance = new CollisionManager();
        public static CollisionManager Instance
        {
            get
            {
                return instance;
            }
        }

        private List<ICollidable> MovingObjects;

        private List<ICollidable> NonMovingObjects;

        private static Dictionary<string, Tuple<ConstructorInfo, ConstructorInfo>> CollisionMappings;

        private CollisionManager() 
        {
            MovingObjects = new List<ICollidable>();
            NonMovingObjects = new List<ICollidable>();
            CreateDict();
        }

        private void CreateDict()
        {
            /* Initalizes and loads <CollisionMappings> form the file <XMLCollisions.xml> with collisions 
             * and their corresponding commands. 
             */
            
            CollisionMappings = new Dictionary<string, Tuple<ConstructorInfo, ConstructorInfo>>();

            XmlDocument XMLData = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLData/XMLCollisions.xml";
            XMLData.Load(path);
            XmlNodeList Commands = XMLData.DocumentElement.SelectNodes("/Collisions/Collision");

            Assembly assem = typeof(ICommand).Assembly;

            foreach (XmlNode node in Commands)
            {
                string name = node.SelectSingleNode("name").InnerText;
                string command1 = node.SelectSingleNode("command1").InnerText;
                string command2 = node.SelectSingleNode("command2").InnerText;

                // Get the constructor for the type 
                Type command1Type = assem.GetType("Project1.Command." + command1);
                Type command2Type = assem.GetType("Project1.Command." + command2);

                // Get the constructors for the commands
                ConstructorInfo constructor1 = command1Type.GetConstructor(new[] { typeof(ICollidable), typeof(ICollidable), typeof(string) });
                ConstructorInfo constructor2 = command2Type.GetConstructor(new[] { typeof(ICollidable), typeof(ICollidable), typeof(string) });

                CollisionMappings.Add(name, Tuple.Create(constructor1, constructor2));
            }   
        }

        public Tuple<ConstructorInfo, ConstructorInfo> GetCommands(ICollision collision)
        {
            /* Return the two commands for the corresponding <collision> found in <CollisionMappings>. 
             * The key may be the <DirectionKey> property for direction specific collisions or the <Key> 
             * property for non direction specific collisions. 
             */ 

            if (CollisionMappings.ContainsKey(collision.DirectionKey))
            {
                return CollisionMappings[collision.DirectionKey];
            }
            else if (CollisionMappings.ContainsKey(collision.Key))
            {
                return CollisionMappings[collision.Key];
            }

            return null; 
        }

        public void ExecuteCommands(ICollision collision, Tuple<ConstructorInfo, ConstructorInfo> commands)
        {
            /* Given the <collision> and two corresponding <commands>, execture the commands on the appropriate 
             * items of <collision> if they are not NullCollision. 
             */ 
            
            ICollidable item1 = collision.Item1;
            ICollidable item2 = collision.Item2;
            
            // Get the type of commands to execute from command mappings
            ConstructorInfo constructor1 = commands.Item1;
            ConstructorInfo constructor2 = commands.Item2;

            // Create the commands
            object command1 = constructor1.Invoke(new object[] { item1, item2, collision.Direction });
            object command2 = constructor2.Invoke(new object[] { item2, item1, collision.Direction });

            // Execute the commands if they are not NullCollision  
            ICommand cmd1 = (ICommand)command1;
            ICommand cmd2 = (ICommand)command2;

            if (!(cmd1 is NullCollision))cmd1.Execute();          
            if (!(cmd2 is NullCollision)) cmd2.Execute();
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

        public Rectangle GetHitBox(Vector2 position, Vector2 dimensions)
        {
            /* Returns the proper hitbox given the position and hitbox dimensions. 
             * The hitbox is centered within a square of <size>. 
             */

            int size = SpriteFactory.Instance.UniversalSize * GameVar.ScalingFactor;
            
            dimensions.X *= GameVar.ScalingFactor;
            dimensions.Y *= GameVar.ScalingFactor;
            
            int xPos = (int)(position.X + (size / 2) - (dimensions.X / 2));
            int yPos = (int)(position.Y + (size / 2) - (dimensions.Y / 2));
            
            return new Rectangle(xPos, yPos, (int)dimensions.X, (int)dimensions.Y);
        }

        public void DetectCollisions()
        {
            /* Compare all moving objects to all other non-moving and moving objects. 
             */ 
            
            for (int i = 0; i < MovingObjects.Count; i++)
            {
                ICollidable item1 = MovingObjects[i];

                for (int j = i+1; j < MovingObjects.Count; j++)
                {
                    ICollidable item2 = MovingObjects[j];
                    Collide(item1, item2); 
                }

                foreach (ICollidable item2 in NonMovingObjects)
                {
                    Collide(item1, item2);
                }
            }
        }
        private void Collide(ICollidable item1, ICollidable item2)
        {
            /* Detect if there is a collision between <Item1> and <Item2>, if so execute their 
             * collision. 
             */

            ICollision collision = DetectCollision(item1, item2);
            if (!(collision is NullCollision))
            {
                Tuple<ConstructorInfo, ConstructorInfo> commands = GetCommands(collision);
                if (commands != null)
                {
                    ExecuteCommands(collision, commands);
                }
            }
        }

        public ICollision DetectCollision(ICollidable item1, ICollidable item2)
        {
            /* NOTE: Finds the direction of the collision based on the minimum intersection dimension
             *  (width or height). It then finds whether it's a top/bottom intersection for vertical 
             *  collisions or a left/right intersection for horizontal collisions. 
             */ 
            string direction;
            if(item1.Hitbox.Intersects(item2.Hitbox))   
            {
                Rectangle Intersection = Rectangle.Intersect(item1.Hitbox, item2.Hitbox);
                // Get min dimension of intersections 
                if(Intersection.Height < Intersection.Width) // Collision in vertical direction 
                {
                    if (Intersection.Top == item1.Hitbox.Top)
                    {
                        // collide on item1's top and item2's bottom 
                        direction = GameVar.DirectionUp;
                    }
                    else
                    {
                        // collide on item1's bottom and item2's top
                        direction = GameVar.DirectionDown;
                    }
    
                } else // Collision in horizontal direction 
                {
                    if (Intersection.Right == item1.Hitbox.Right)
                    {
                        // collide on item1's right and item2's left 
                        direction = GameVar.DirectionRight;
                    }
                    else
                    {
                        // collide on item1's left and item2's right 
                        direction = GameVar.DirectionLeft;
                    }
                }
                return new Collision(item1, item2, direction);  
            } 
            return new NullCollision(); 
        }

        public void Update()
        {
            DetectCollisions();
        }

        public void Reset()
        {
            /* Removes all items from the two lists 
             */  
            MovingObjects = new List<ICollidable>();
            NonMovingObjects = new List<ICollidable>();
        }
    }
}
