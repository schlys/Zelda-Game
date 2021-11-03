using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.HeadsUpDisplay
{
    public interface IHUD
    {
        ILink Link { get; set; }
        Texture2D HUDMain { get; set; }

        Texture2D HUDMap { get; set; }
        Texture2D HUDInventory { get; set; }
        Texture2D HUDLevelMap { get; set; }
        void Update();
        void Draw(SpriteBatch spriteBatch);
        void Reset();
    }
}
