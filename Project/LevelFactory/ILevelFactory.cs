﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace Project1.LevelFactory
{
    public interface ILevelFactory
    {
        static ILevelFactory Instance { get; }
        IRoom CurrentRoom { get; set; }
        void LoadAllTextures(ContentManager content);

        void Draw(SpriteBatch spriteBatch);
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();
    }
}
