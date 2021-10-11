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
        private string[] Directions = { "Left", "Right", "Up", "Down" };    // possible directions 

        private Dictionary<Tuple<ICollidable, ICollidable, string>, Type> commandsMap; //make a map to connect objects and command
       
        private HashSet<Tuple<ICollidable, ICollidable, string>> typesSet;

        private ILink Link;

        public Collision(ICollidable i1, ICollidable i2, String d)
        {
            Item1 = i1;
            Item2 = i2;
            Direction = d;
            First = i1.GetType().Name.ToString();
            Second = i2.GetType().Name.ToString();

            commandsMap = new Dictionary<Tuple<ICollidable, ICollidable, string>, Type>();
            typesSet = new HashSet<Tuple<ICollidable, ICollidable, string>>();
        }

        public void CreateDictionary()
        {
            //commnadsMap.Add(new Tuple<ICollidable, ICollidable, string>(typeof(Link), typeof(Enemy), Direction), typeof(LinkTakeDamageCmd));

        }

        public ICommand TypeCommandConverter(ICollidable item1, ICollidable item2, string direction) // connect commandsMap's command type to command
        {
            ConstructorInfo commandConstructor = null;
            return null;
        }

        public void ResponseCollision(ICollidable item1, ICollidable item2, string direction)
        {
            Tuple<ICollidable, ICollidable, string> type = new Tuple<ICollidable, ICollidable, string>(item1, item2, direction);
            if (typesSet.Contains(type))
            {

                ICommand command = TypeCommandConverter(item1, item2, direction);

                if (command != null)
                    command.Execute();
            }
        }

        public void Execute()
        {
            // Get the type of commands to execute from command mappings
            Tuple<Type, Type> commands = CollisionHandler.Instance.GetCommands(this);
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
