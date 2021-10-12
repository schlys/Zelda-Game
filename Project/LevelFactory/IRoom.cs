using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Project1.LevelFactory
{
    public interface IRoom
    {
        String ID { get; set; } // OR name? 
        IRoom UpRoom { get; set; }
        IRoom DownRoom { get; set; }
        IRoom LeftRoom { get; set; }
        IRoom RightRoom { get; set; }
        Color Color { get; set; }
        Texture2D Texture { get; set; }
    }
}
