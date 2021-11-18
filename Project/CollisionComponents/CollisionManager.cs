using Microsoft.Xna.Framework;
using Project1.Command;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
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

        public List<ICollidable> MovingObjects;

        public List<ICollidable> NonMovingObjects;

        private static Dictionary<string, Tuple<ConstructorInfo, ConstructorInfo>> CollisionMappings;

        private CollisionManager() 
        {
            MovingObjects = new List<ICollidable>();
            NonMovingObjects = new List<ICollidable>();
            CreateDict();
        }

        private void CreateDict()
        {
            CollisionMappings = new Dictionary<string, Tuple<ConstructorInfo, ConstructorInfo>>();

            // NOTE: Load the commands for each collision from an XML document into the CollisionMappings dictionary 
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
            string key = collision.SpecificKey;
            if (CollisionMappings.ContainsKey(key)) return CollisionMappings[key];
            else if (CollisionMappings.ContainsKey(collision.Key)) return CollisionMappings[collision.Key];
            //TODO: create Null object instead of returning null 
            return null; 
        }

        public void ExecuteCommands(ICollision collision, Tuple<ConstructorInfo, ConstructorInfo> commands)
        {
            ICollidable item1 = collision.Item1;
            ICollidable item2 = collision.Item2;
            // Get the type of commands to execute from command mappings
            ConstructorInfo constructor1 = commands.Item1;
            ConstructorInfo constructor2 = commands.Item2;

            // NOTE: All of the parameters in the commands will have to be changed to ICollidable types
            // Create the commands
            object command1 = constructor1.Invoke(new object[] { item1, item2, collision.Direction });
            object command2 = constructor2.Invoke(new object[] { item2, item1, collision.Direction });

            // Execute the commands if they are not "NoCmd" 
            ICommand cmd1 = (ICommand)command1;
            ICommand cmd2 = (ICommand)command2;

            if (!cmd1.GetType().Name.Equals("NoCmd"))cmd1.Execute();          
            if (!cmd2.GetType().Name.Equals("NoCmd"))cmd2.Execute();
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

        // NOTE: Returns the proper hitbox given the position and hitbox dimensions
        public Rectangle GetHitBox(Vector2 position, Vector2 dimensions, int size)
        {
            int xPos = (int)(position.X + (size / 2) - (dimensions.X / 2));
            int yPos = (int)(position.Y + (size / 2) - (dimensions.Y / 2));
            return new Rectangle(xPos, yPos, (int)dimensions.X, (int)dimensions.Y); 
        }

        // NOTE: Returns the proper hitbox given the position and hitbox dimensions
        public Rectangle GetHitBox(Vector2 position, Vector2 dimensions)
        {
            int size = SpriteFactory.Instance.UniversalSize * GameObjectManager.Instance.ScalingFactor;
            dimensions.X *= GameObjectManager.Instance.ScalingFactor;
            dimensions.Y *= GameObjectManager.Instance.ScalingFactor;
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

                for (int j = i+1; j < MovingObjects.Count; j++)
                {
                    ICollidable item2 = MovingObjects[j];
                    ICollision collision = DetectCollision(item1, item2); 
                    if (!collision.GetType().Name.ToString().Equals("NullCollision"))
                    {
                        Tuple<ConstructorInfo, ConstructorInfo> commands = GetCommands(collision); 
                        if(commands != null)
                        {
                            ExecuteCommands(collision, commands); 
                        }
                    }
                }

                foreach (ICollidable item2 in NonMovingObjects)
                {
                   
                    ICollision collision = DetectCollision(item1, item2);
                    if (!collision.GetType().Name.ToString().Equals("NullCollision"))
                    {
                        Tuple<ConstructorInfo, ConstructorInfo> commands = GetCommands(collision);
                        if (commands != null)
                        {
                            ExecuteCommands(collision, commands);
                        }
                        // break here so movers don't get knocked back by two different blocks
                        //break;
                    }
  
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
                        direction = "Top";
                    }
                    else
                    {
                        // collide on item1's bottom and item2's top
                        direction = "Bottom";
                    }
    
                } else // Collision in horizontal direction 
                {
                    if (Intersection.Right == item1.Hitbox.Right)
                    {
                        // collide on item1's right and item2's left 
                        direction = "Right";
                    }
                    else
                    {
                        // collide on item1's left and item2's right 
                        direction = "Left";
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
            MovingObjects = new List<ICollidable>();
            NonMovingObjects = new List<ICollidable>();
        }
    }
}
