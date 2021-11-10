using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.ItemComponents;
using Project1.BlockComponents;
using Project1.EnemyComponents;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml;
using System.IO;
using Project1.GameState;
using System.Reflection;

namespace Project1.LevelComponents
{
    public sealed class LevelFactory : ILevelFactory
    {
        private static LevelFactory instance = new LevelFactory();
        public static LevelFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public IRoom CurrentRoom { get; set; }
        public ILevelMap LevelMap { get; set; }
        public Dictionary<String, Texture2D> HUDTextures { get; set; }
        public Vector2 LinkStartingPosition { get; set; }

        private static Dictionary<string, IRoom> LevelDict;
        private static Dictionary<string, Texture2D> TextureDict;
        public static int[,] textureMatrix;

        // TODO: Load in XML
        private static int adjust = 2 * GameObjectManager.Instance.ScalingFactor;
        private static Vector2 RoomPosition = new Vector2(0, 55 * GameObjectManager.Instance.ScalingFactor);
        private static int RoomBorderSize = 32 * GameObjectManager.Instance.ScalingFactor;// + adjust;
        private static int RoomBlockSize = SpriteFactory.Instance.BlockSize * GameObjectManager.Instance.ScalingFactor;
        private static int RoomRows = 7;
        private static int RoomColumns = 12;
        

        private static string StartRoom = "room2";

        private LevelFactory() { }

        public void LoadAllTextures(ContentManager content)
        {
            /* NOTE: Load the textures used for rooms, call CreateDict to create the level dictionary, set the
             * CurrentRoom to the starting room, and set the LinkStartingPosition. 
             */ 

            TextureDict = new Dictionary<string, Texture2D>();

            TextureDict.Add("room", content.Load<Texture2D>("Rooms/RoomMap")); // All rooms are in one png file.

            CreateDict();

            if (LevelDict.ContainsKey(StartRoom))
            {
                CurrentRoom = LevelDict[StartRoom];
            } else
            {
                throw new IndexOutOfRangeException("Index StartRoom given to LevelDict is not found"); 
            }

            LinkStartingPosition = GetItemPosition(4, 1);

            // Load Textures for HUD
            HUDTextures = new Dictionary<String, Texture2D>();

            XmlDocument XMLData = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLData/HUD.xml";
            XMLData.Load(path);
            XmlNodeList Sheets = XMLData.DocumentElement.SelectNodes("/Items/Item");

            foreach (XmlNode node in Sheets)
            {
                HUDTextures.Add(node.SelectSingleNode("name").InnerText, content.Load<Texture2D>(node.SelectSingleNode("sheet").InnerText));
            }


            LevelMap = new LevelMap(HUDTextures["HUDLevelMap"]);
        }

        private static Texture2D GetTexture(String key)
        {
            // TODO: return null texture object 
            if (TextureDict.ContainsKey(key))
            {
                return TextureDict[key];
            }
            return null;
        }
        private static void CreateDict()
        {
            // NOTE: Load the room data from XMLLevel.XML to the level dictionary 

            textureMatrix = new int[RoomRows, RoomColumns];
            LevelDict = new Dictionary<string, IRoom>();

            XmlDocument XMLData = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLData/XMLLevel.xml";
            XMLData.Load(path);
            XmlNodeList Sprites = XMLData.DocumentElement.SelectNodes("/Levels/Level/Room");

            foreach (XmlNode node in Sprites)
            {
                string name = node.SelectSingleNode("name").InnerText;
                string sheet = node.SelectSingleNode("sheet").InnerText;
                int xPos = Int32.Parse(node.SelectSingleNode("xPos").InnerText);
                int yPos = Int32.Parse(node.SelectSingleNode("yPos").InnerText);
                string up = node.SelectSingleNode("up").InnerText.ToLower();
                string down = node.SelectSingleNode("down").InnerText.ToLower();
                string left = node.SelectSingleNode("left").InnerText.ToLower();
                string right = node.SelectSingleNode("right").InnerText.ToLower();

                Texture2D Texture = GetTexture(sheet);
                IRoom Room = new Room(name, RoomPosition, xPos, yPos, up, down, left, right, TextureDict[sheet]);

                // Load the objects within each room 
                XmlNodeList objectsData = node.SelectNodes("object");
                // XMLData.DocumentElement.SelectNodes("/Levels/Level/Room/objects/data");
                foreach (XmlNode itemNode in objectsData)
                {
                    string type = itemNode.SelectSingleNode("type").InnerText;
                    string type2 = itemNode.SelectSingleNode("type2").InnerText;
                    int row = Int16.Parse(itemNode.SelectSingleNode("row").InnerText);
                    float column = float.Parse(itemNode.SelectSingleNode("column").InnerText);

                    //TODO: replace with reflection 
 
                    switch (type)
                    {
                        case "MovingItem":
                            IItem movingItem = new MovingItem(GetItemPosition(row, column), type2);
                            Room.AddItem(movingItem);
                            break;
                        case "Item":
                            IItem item = new NonMovingItem(GetItemPosition(row, column), type2);
                            Room.AddItem(item);
                            break;
                        case "Block":
                            IBlock block = new Block(GetItemPosition(row, column), type2);
                            Room.AddBlock(block);
                            break;
                        case "Enemy":
                            IEnemy enemy = new Enemy(GetItemPosition(row, column), type2);
                            Room.AddEnemy(enemy);
                            break;
                        case "Door":
                            bool locked = XmlConvert.ToBoolean(itemNode.Attributes["locked"].Value);
                            IDoor door = new Door(GetItemPosition(row, column), type2, locked);
                            Room.AddDoor(door);
                            break;
                        default:
                            break;
                    }

                }
                LevelDict.Add(name.ToLower(), Room);
            }
        }

        private static Vector2 GetItemPosition(int row, float column)
        {
            /* NOTE: return the location of the item in the room given the row and column. 
             * Throw an exception if the row or column is out of the room range. 
             */
            if (row > RoomRows)
            {
                throw new ArgumentException("Index is out of range");
            }
            if (column > RoomColumns)
            {
                throw new ArgumentException("Index is out of range");
            }

            float x = RoomPosition.X + RoomBorderSize + (RoomBlockSize * column);// - adjust;
            float y = RoomPosition.Y + RoomBorderSize + (RoomBlockSize * row); //+ adjust;
            return new Vector2(x, y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // TODO: Remove before submission 
            // For testing collision hitbox 
            Texture2D dummyTexture = new Texture2D(GameObjectManager.Instance.Game.GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { Color.White });
            Rectangle roomBorder = new Rectangle((int)RoomPosition.X, (int)RoomPosition.Y, (RoomBorderSize * 2) + (RoomBlockSize * RoomColumns), (RoomBorderSize * 2) + (RoomBlockSize * RoomRows));
            Rectangle roomFloor = GetPlayableRoomBounds(); //new Rectangle((int)RoomPosition.X + RoomBorderSize, (int)RoomPosition.Y + RoomBorderSize, (RoomBlockSize * RoomColumns), (RoomBlockSize * RoomRows));
            Rectangle roomTile = new Rectangle((int)GetItemPosition(4,1).X, (int)GetItemPosition(4, 1).Y, RoomBlockSize, RoomBlockSize);

            CurrentRoom.Draw(spriteBatch);

            //spriteBatch.Draw(dummyTexture, roomTile, Color.Red); 
            //spriteBatch.Draw(dummyTexture, roomFloor, Color.White);

        }

        public void Reset()
        {
            /* Update <CurrentRoom> to be the <StartRoom> and reset the room.
             */
            if (LevelDict.ContainsKey(StartRoom))
            {
                CurrentRoom = LevelDict[StartRoom];
            }
            else
            {
                throw new IndexOutOfRangeException("Index StartRoom given to LevelDict is not found");
            }
            CurrentRoom.Reset();
            LevelMap.Reset();
        }

        public void MoveUp()
        {
            if (GameStateManager.Instance.CanPlayGame() && !CurrentRoom.UpRoom.Equals("") && LevelDict.ContainsKey(CurrentRoom.UpRoom))
            {
                Room previousRoom = (Room)CurrentRoom;
                CurrentRoom = LevelDict[CurrentRoom.UpRoom];
                CurrentRoom.Up(previousRoom);
                GameObjectManager.Instance.UpdateRoomItems();
                LevelMap.MoveUp();
            }
        }
        public void MoveDown()
        {
            if (GameStateManager.Instance.CanPlayGame() && !CurrentRoom.DownRoom.Equals("") && LevelDict.ContainsKey(CurrentRoom.DownRoom))
            {
                Room previousRoom = (Room)CurrentRoom;
                CurrentRoom = LevelDict[CurrentRoom.DownRoom];
                CurrentRoom.Down(previousRoom);
                GameObjectManager.Instance.UpdateRoomItems();
                LevelMap.MoveDown();
            }
        }
        public void MoveLeft()
        {
            if (GameStateManager.Instance.CanPlayGame() && !CurrentRoom.LeftRoom.Equals("") && LevelDict.ContainsKey(CurrentRoom.LeftRoom))
            {
                //Camera.Instance.CheckCollision(true);
                Room previousRoom = (Room)CurrentRoom;
                CurrentRoom = LevelDict[CurrentRoom.LeftRoom];
                CurrentRoom.Left(previousRoom);
                GameObjectManager.Instance.UpdateRoomItems();
                LevelMap.MoveLeft();
            }
        }
        public void MoveRight()
        {
            if (GameStateManager.Instance.CanPlayGame() && !CurrentRoom.RightRoom.Equals("") && LevelDict.ContainsKey(CurrentRoom.RightRoom))
            {
                Room previousRoom = (Room)CurrentRoom;
                CurrentRoom = LevelDict[CurrentRoom.RightRoom];
                CurrentRoom.Right(previousRoom);
                GameObjectManager.Instance.UpdateRoomItems();
                LevelMap.MoveRight();
            }
        }
        public Rectangle GetPlayableRoomBounds()
        {
            // NOTE: Return the playable space within the room 
            return new Rectangle((int)RoomPosition.X + RoomBorderSize, (int)RoomPosition.Y + RoomBorderSize, 
                (RoomBlockSize * RoomColumns) + adjust, (RoomBlockSize * RoomRows) + adjust);

        }

        public bool IsWithinRoomBounds(Vector2 location)
        {
            // NOTE: Return true if the given location is within the playable space within the room 
            Rectangle bounds = GetPlayableRoomBounds(); 
            if (bounds.Contains(location.X, location.Y))
            {
                return true;
            }
            return false;
        }
    }
}
