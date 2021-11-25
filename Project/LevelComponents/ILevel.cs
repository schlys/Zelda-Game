using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace Project1.LevelComponents 
{
    public interface ILevel
    {
        IRoom CurrentRoom { get; set; }
        ILevelMap LevelMap { get; set; }
        void Draw(SpriteBatch spriteBatch);
        void Update(); 
        void Reset();
        void MoveUp(Vector2 position);
        void MoveDown(Vector2 position);
        void MoveLeft(Vector2 position);
        void MoveRight(Vector2 position);
        Vector2 GetItemPosition(float row, float column); 
        Rectangle GetPlayableRoomBounds();
        bool IsWithinRoomBounds(Vector2 location);
    }
}
