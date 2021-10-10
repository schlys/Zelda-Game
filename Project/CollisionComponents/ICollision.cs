﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.CollisionComponents
{
    public interface ICollision
    {
        public ICollidable Item1 { get; set; }
        public ICollidable Item2 { get; set; }
        string First { get; set; }
        string Second { get; set; }
        public string Direction { get; set; }   // direction item1 collides with item 2
        void Execute();
    }
}
