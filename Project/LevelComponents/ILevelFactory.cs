using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace Project1.LevelComponents 
{
    public interface ILevelFactory
    {
        static ILevelFactory Instance { get; }
        IRoom CurrentRoom { get; set; }
        //Vector2 CurrentRoomPosition { get; set; }
        ILevelMap LevelMap { get; set; }
        int RoomBlockSize { get; set; }
        Dictionary<String, Texture2D> HUDTextures { get; set; }
        void LoadAllTextures(ContentManager content);
        void Draw(SpriteBatch spriteBatch);
        void Update(); 
        void Reset();
        void MoveUp(Vector2 position);
        void MoveDown(Vector2 position);
        void MoveLeft(Vector2 position);
        void MoveRight(Vector2 position);
        Rectangle GetPlayableRoomBounds();
        bool IsWithinRoomBounds(Vector2 location);

    }
}
