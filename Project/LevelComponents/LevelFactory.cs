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
        public int RoomBlockSize { get; set; }
        private IRoom NextRoom;
        public Vector2 CurrentRoomPosition = new Vector2(0, 55 * GameObjectManager.Instance.ScalingFactor);
        private Vector2 CurrentRoomInitialPosition = new Vector2(0, 55 * GameObjectManager.Instance.ScalingFactor);

        public ILevelMap LevelMap { get; set; }
        public Dictionary<String, Texture2D> HUDTextures { get; set; }
        public List<Vector2> LinkStartingPosition { get; set; }

        private Vector2 LinkLeftRoomPosition; 
        private Vector2 LinkRightRoomPosition;
        private Vector2 LinkUpRoomPosition;
        private Vector2 LinkDownRoomPosition;
        private Vector2 LinkPositionUpdate;

        private static Dictionary<string, IRoom> LevelDict;
        private static Dictionary<string, Texture2D> TextureDict;

        // TODO: Load in XML
        // ****** What is the purpose of adjust? 
        private static int adjust = 2 * GameObjectManager.Instance.ScalingFactor;

        private static Vector2 ScrollAdjust = new Vector2(0, 0);
        private static float ScrollStep = (float)(3 * GameObjectManager.Instance.ScalingFactor);

        private static Vector2 RoomPosition = new Vector2(0, 55 * GameObjectManager.Instance.ScalingFactor);
        private static int RoomBorderSize = 32 * GameObjectManager.Instance.ScalingFactor;// + adjust;
       
        private static int RoomRows = 7;
        private static int RoomColumns = 12;
        public int PlayableWidth;
        public int PlayableHeight;

        private static string StartRoom = "room2";

        private LevelFactory() { }

        public void LoadAllTextures(ContentManager content)
        {
            /* NOTE: Load the textures used for rooms, call CreateDict to create the level dictionary, set the
             * CurrentRoom to the starting room, and set the LinkStartingPosition. 
             */
            RoomBlockSize = SpriteFactory.Instance.BlockSize * GameObjectManager.Instance.ScalingFactor;

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

            // TODO: data drive
            LinkStartingPosition = new List<Vector2>(); 
            LinkStartingPosition.Add(GetItemPosition(4, 1));    // Link 1
            LinkStartingPosition.Add(GetItemPosition(5, 1));    // Link 2

            LinkLeftRoomPosition = GetItemPosition(3, (float)11.5);
            LinkRightRoomPosition = GetItemPosition(3, (float)-.5);
            LinkUpRoomPosition = GetItemPosition((float)6.5, (float)5.5);
            LinkDownRoomPosition = GetItemPosition((float)-.5, (float)5.5);

            PlayableHeight = (RoomBlockSize * RoomRows) + adjust;
            PlayableWidth = (RoomBlockSize * RoomColumns) + adjust;

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
        private void CreateDict()
        {
            // NOTE: Load the room data from XMLLevel.XML to the level dictionary 

            //textureMatrix = new int[RoomRows, RoomColumns];
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
                    float row = float.Parse(itemNode.SelectSingleNode("row").InnerText);
                    float column = float.Parse(itemNode.SelectSingleNode("column").InnerText);

                    //TODO: replace with reflection 
 
                    switch (type)
                    {
                        case "Item":
                            IItem item = new Item(GetItemPosition(row, column), type2);
                            Room.AddItem(item);
                            break;
                        case "Block":
                            bool breakable = false;
                            if (itemNode.Attributes["special"] != null) breakable = XmlConvert.ToBoolean(itemNode.Attributes["special"].Value);
                            IBlock block = new Block(GetItemPosition(row, column), type2, breakable);
                            Room.AddBlock(block);
                            break;
                        case "Enemy":
                            IEnemy enemy = new Enemy(GetItemPosition(row, column), type2);
                            Room.AddEnemy(enemy);
                            break;
                        case "Door":
                            bool locked = XmlConvert.ToBoolean(itemNode.Attributes["locked"].Value);
                            Vector2 positionDelta = Vector2.Zero;

                            if (itemNode.Attributes["col"] != null && itemNode.Attributes["row"] != null) 
                                positionDelta = new Vector2((float)XmlConvert.ToDouble(itemNode.Attributes["row"].Value), 
                                                    (float)XmlConvert.ToDouble(itemNode.Attributes["col"].Value));

                            IDoor door = new Door(GetItemPosition(row, column), type2, locked, positionDelta);
                            Room.AddDoor(door);
                            break;
                        default:
                            break;
                    }

                }
                LevelDict.Add(name.ToLower(), Room);
            }
        }

        public Vector2 GetItemPosition(float row, float column)
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

            float x = RoomPosition.X + RoomBorderSize + (Instance.RoomBlockSize * column);// - adjust;
            float y = RoomPosition.Y + RoomBorderSize + (Instance.RoomBlockSize * row); //+ adjust;
            return new Vector2(x, y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // TODO: Remove before submission 
            // For testing collision hitbox 
            Texture2D dummyTexture = new Texture2D(GameObjectManager.Instance.Game.GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { Color.Black });
            Rectangle roomBorder = new Rectangle((int)RoomPosition.X, (int)RoomPosition.Y, (RoomBorderSize * 2) + (RoomBlockSize * RoomColumns), (RoomBorderSize * 2) + (RoomBlockSize * RoomRows));
            Rectangle roomFloor = GetPlayableRoomBounds(); //new Rectangle((int)RoomPosition.X + RoomBorderSize, (int)RoomPosition.Y + RoomBorderSize, (RoomBlockSize * RoomColumns), (RoomBlockSize * RoomRows));
            Rectangle roomTile = new Rectangle((int)GetItemPosition(4,1).X, (int)GetItemPosition(4, 1).Y, RoomBlockSize, RoomBlockSize);
            

            CurrentRoom.Draw(spriteBatch);
            
            if (GameStateManager.Instance.CanRoomScroll())
            {
                NextRoom.Draw(spriteBatch); 
            }

                //NextRoom.Draw(spriteBatch);
                //spriteBatch.Draw(dummyTexture, roomTile, Color.Red); 
                //spriteBatch.Draw(dummyTexture, roomFloor, Color.White);

        }

        public void Update()
        {
            if (GameStateManager.Instance.CanRoomScroll())
            {
                Scroll(); 
            }
        }

        public void Reset()
        {
            /* Update <CurrentRoom> to be the <StartRoom> and reset the room.
             */
            CurrentRoomPosition = new Vector2(0, 55 * GameObjectManager.Instance.ScalingFactor);
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

        public void MoveUp(Vector2 position)
        {
            if (GameStateManager.Instance.CanPlayGame() && !CurrentRoom.UpRoom.Equals("") && LevelDict.ContainsKey(CurrentRoom.UpRoom))
            {
                GameStateManager.Instance.StartScroll();        // trigger room scroll
                GameObjectManager.Instance.ClearRoomItems();    // remove objects from room 

                ScrollAdjust = new Vector2(0, ScrollStep);

                if (!position.Equals(Vector2.Zero)) LinkPositionUpdate = GetItemPosition(position.X, position.Y);
                else LinkPositionUpdate = LinkUpRoomPosition;

                NextRoom = LevelDict[CurrentRoom.UpRoom];
                NextRoom.Position += new Vector2(0, -NextRoom.Size.Y);
                NextRoom.OpenDoor("Down");

                LevelMap.MoveUp();
            }
        }
        public void MoveDown(Vector2 position)
        {
            if (GameStateManager.Instance.CanPlayGame() && !CurrentRoom.DownRoom.Equals("") && LevelDict.ContainsKey(CurrentRoom.DownRoom))
            {
                GameStateManager.Instance.StartScroll();        // trigger room scroll
                GameObjectManager.Instance.ClearRoomItems();    // remove objects from room 
                
                ScrollAdjust = new Vector2(0, -ScrollStep);

                if (!position.Equals(Vector2.Zero)) LinkPositionUpdate = GetItemPosition(position.X, position.Y);
                else LinkPositionUpdate = LinkDownRoomPosition;

                NextRoom = LevelDict[CurrentRoom.DownRoom];
                NextRoom.Position += new Vector2(0, NextRoom.Size.Y);
                NextRoom.OpenDoor("Up");

                LevelMap.MoveDown();
            }
        }
        public void MoveLeft(Vector2 position)
        {
            if (GameStateManager.Instance.CanPlayGame() && !CurrentRoom.LeftRoom.Equals("") && LevelDict.ContainsKey(CurrentRoom.LeftRoom))
            {
                GameStateManager.Instance.StartScroll();    // trigger room scroll
                GameObjectManager.Instance.ClearRoomItems();    // remove objects from room 

                ScrollAdjust = new Vector2(ScrollStep, 0);

                if (!position.Equals(Vector2.Zero)) LinkPositionUpdate = GetItemPosition(position.X, position.Y);
                else LinkPositionUpdate = LinkLeftRoomPosition;

                NextRoom = LevelDict[CurrentRoom.LeftRoom];
                NextRoom.Position += new Vector2(-NextRoom.Size.X, 0);
                NextRoom.OpenDoor("Right");

                LevelMap.MoveLeft();
            }
        }
        public void MoveRight(Vector2 position)
        {
            if (GameStateManager.Instance.CanPlayGame() && !CurrentRoom.RightRoom.Equals("") && LevelDict.ContainsKey(CurrentRoom.RightRoom))
            {
                GameStateManager.Instance.StartScroll();    // trigger room scroll
                GameObjectManager.Instance.ClearRoomItems();

                ScrollAdjust = new Vector2(-ScrollStep, 0);

                if (!position.Equals(Vector2.Zero)) LinkPositionUpdate = GetItemPosition(position.X, position.Y);
                else LinkPositionUpdate = LinkRightRoomPosition;

                NextRoom = LevelDict[CurrentRoom.RightRoom];
                NextRoom.Position += new Vector2(NextRoom.Size.X, 0);
                NextRoom.OpenDoor("Left");

                LevelMap.MoveRight();
            }
        }

        private void Scroll()
        {
            /* Scroll the room in the direction <step>
             */

            if(Math.Abs(NextRoom.Position.X - CurrentRoomInitialPosition.X) <= ScrollStep &&
                Math.Abs(NextRoom.Position.Y - CurrentRoomInitialPosition.Y) <= ScrollStep)
            {
                CurrentRoom.Position = CurrentRoomInitialPosition;  // reset room position 
                NextRoom.Position = CurrentRoomInitialPosition;     // reset room position 

                GameStateManager.Instance.StopScroll();    // trigger stop room scroll
                CurrentRoom = NextRoom; 
                GameObjectManager.Instance.UpdateRoomItems();   // readd room items 

                GameObjectManager.Instance.SetLinkPosition(LinkPositionUpdate);  // update link position
            } else
            {
                CurrentRoom.Position += ScrollAdjust;
                NextRoom.Position += ScrollAdjust;
            }
        }

        public Rectangle GetPlayableRoomBounds()
        {
            // NOTE: Return the playable space within the room 
            return new Rectangle((int)RoomPosition.X + RoomBorderSize, (int)RoomPosition.Y + RoomBorderSize,
                PlayableWidth, PlayableHeight);

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
