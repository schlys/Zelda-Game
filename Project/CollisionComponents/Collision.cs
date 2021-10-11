﻿using System;
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
        public string First { get; set; }
        public string Second { get; set; }
        public string Direction { get; set; }   // direction item1 collides with item 2

        public Collision(ICollidable i1, ICollidable i2, string d)
        {
            Item1 = i1;
            Item2 = i2;
            Direction = d;
            First = i1.GetType().Name.ToString();
            Second = i2.GetType().Name.ToString();

        }

        public void Execute()
        {
            // Get the type of commands to execute from command mappings
            Tuple<Type, Type> commands = CollisionManager.Instance.GetCommands(this);
            Type type1 = commands.Item1;
            Type type2 = commands.Item2;
           
            // Get the constructor for the commands to execute
            // NOTE: All of the parameters in the commands will have to be changed to ICollidable types
            ConstructorInfo constructor1 = type1.GetConstructor(new[] { typeof(ICollidable)});
            ConstructorInfo constructor2 = type2.GetConstructor(new[] { typeof(ICollidable)});

            // Create the commands
            object command1 = constructor1.Invoke(new object[] {Item1});
            object command2 = constructor2.Invoke(new object[] {Item2});

            // Execute the commands
            MethodInfo execute1 = type1.GetMethod("Execute");
            MethodInfo execute2 = type2.GetMethod("Execute");
            
            execute1.Invoke(command1, new object[] { });
            execute2.Invoke(command2, new object[] { });
        }

    }
}
