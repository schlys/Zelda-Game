using Microsoft.Xna.Framework;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        public void Follow(List<ILink> Links)
        {
            /*
            var offset = Matrix.CreateTranslation(
                Game1.ScreenWidth / 2,
                Game1.ScreenHeight / 2,
                0);
            */

            foreach (ILink link in Links)
                Transform = Matrix.CreateTranslation(-link.Position.X, -link.Position.Y, 0)* 
                    Matrix.CreateTranslation(256, 176, 0);
        }
    }
}
