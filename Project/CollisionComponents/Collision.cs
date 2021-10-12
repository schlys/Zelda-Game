using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Project1.LinkComponents;
using Project1.EnemyComponents;
using Project1.Command;

namespace Project1.CollisionComponents
{
    class Collision : ICollision
    {
        public ICollidable Item1 { get; set; }
        public ICollidable Item2 { get; set; }
        //public string Item1ID { get; set; }
        //public string Item2ID { get; set; }
        public string Key { get; set; }
        public string Direction { get; set; }   // direction item1 collides with item 2

        public Collision(ICollidable i1, ICollidable i2, string d)
        {
            Item1 = i1;
            Item2 = i2;
            Direction = d;
            //Item1ID = i1.GetType().Name.ToString();
            //Item2ID = i2.GetType().Name.ToString();
            Key = Item1.TypeID + Item2.TypeID + Direction; 
        }

        public void Execute()
        {
            // Get the type of commands to execute from command mappings
            Tuple<ConstructorInfo, ConstructorInfo> commands = CollisionManager.Instance.GetCommands(this);
            ConstructorInfo constructor1 = commands.Item1;
            ConstructorInfo constructor2 = commands.Item2;
          
            // NOTE: All of the parameters in the commands will have to be changed to ICollidable types
            // Create the commands
            object command1 = constructor1.Invoke(new object[] {Item1});
            object command2 = constructor2.Invoke(new object[] {Item2});

            // Execute the commands
            ICommand cmd1 = (ICommand)command1;
            ICommand cmd2 = (ICommand)command2;

            cmd1.Execute();
            cmd2.Execute();
        }

    }
}
