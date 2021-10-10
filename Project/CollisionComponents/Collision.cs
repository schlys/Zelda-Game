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
        public string First { get; set; }
        public string Second { get; set; }
        public string Direction { get; set; }   // direction item1 collides with item 2
        private string[] Directions = { "Left", "Right", "Up", "Down" };    // possible directions 

        private Game1 Game;
        private Dictionary<Tuple<ICollidable, ICollidable, string>, Type> commandsMap; //make a map to connect objects and command
       
        private HashSet<Tuple<ICollidable, ICollidable, string>> typesSet;

        private ILink Link;

        public Collision(Game1 game, ICollidable i1, ICollidable i2, String d)
        {
            Game = game;
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

        }

    }
}
