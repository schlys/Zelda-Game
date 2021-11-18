using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.BlockComponents;
using Project1.ItemComponents;
using Project1.EnemyComponents;

namespace Project1.LevelComponents
{
    public class Room : IRoom 
    {
        public string ID { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public string UpRoom { get; set; }
        public string DownRoom { get; set; }
        public string LeftRoom { get; set; }
        public string RightRoom { get; set; }
        public List<IBlock> Blocks { get; set; }
        public List<IItem> Items { get; set; }
        public List<IEnemy> Enemies { get; set; }
        public List<IDoor> Doors { get; set; }
        public Texture2D Texture { get; set; }

        public Color Color = Color.White;

        Room PreviousRoom;

        Rectangle sourceRectangle2;
        Rectangle destinationRectangle2;
        Boolean scroll = false;
        private int timer = 0;

        private Vector2 TextureRoomSize; 
        //private int Height = 176 * GameObjectManager.Instance.ScalingFactor; 
        //private int Width = 256 * GameObjectManager.Instance.ScalingFactor; 

        public Room(string id, Vector2 position, int xPos, int yPos, string up, string down, string left, string right, Texture2D texture)
        {
            ID = id;
            Position = position;
            // TODO: how is xpos, ypos different than position? 
            XPos = xPos;
            YPos = yPos;
            UpRoom = up;
            DownRoom = down;
            LeftRoom = left;
            RightRoom = right;
            Blocks = new List<IBlock>();
            Items = new List<IItem>();
            Enemies = new List<IEnemy>();
            Doors = new List<IDoor>();
            Texture = texture;

            // TODO: data drive 
            Size = new Vector2(256, 176) * GameObjectManager.Instance.ScalingFactor;
            TextureRoomSize = new Vector2(256, 176); 
        }

        public void AddBlock(IBlock block)
        {
            Blocks.Add(block);
        }
        public void AddItem(IItem item)
        {
            Items.Add(item);
        }
        public void AddEnemy(IEnemy enemy)
        {
            Enemies.Add(enemy);
        }
        public void AddDoor(IDoor door)
        {
            Doors.Add(door);
        }
        public void OpenDoor(string direction)
        {
            foreach (IDoor door in Doors)
            {
                if (direction.Equals(door.Direction)) 
                { 
                    door.Unlock();
                    break;
                }
            }
        }
        
        public void Reset()
        {
            // TODO: should we handle object reset here and not in gameobject manager? 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // TODO: remove hardcode 
            Rectangle sourceRectangle = new Rectangle(XPos, YPos, (int)TextureRoomSize.X, (int)TextureRoomSize.Y);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color);
        }
    }
}
