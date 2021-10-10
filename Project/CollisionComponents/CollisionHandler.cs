using Project1.Command;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.CollisionComponents
{
    class CollisionHandler
    {
        private static CollisionHandler instance = new CollisionHandler();
        public static CollisionHandler Instance
        {
            get
            {
                return instance;
            }
        }
        private CollisionHandler() 
        {
            CreateDict();
        }


        private static Dictionary<string, Tuple<string, string>> CollisionMappings;
        private void CreateDict()
        {
            CollisionMappings = new Dictionary<string, Tuple<string, string>>();

            
        }
        public Tuple<string, string> GetCommands(ICollision collision)
        {
            return CollisionMappings[collision.First + collision.Second + collision.Direction];
        }
    }
}
