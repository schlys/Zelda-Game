using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.ItemComponents;
using Project1.BlockComponents;
using Project1.EnemyComponents;
using Project1.LevelComponents;
using Project1.SpriteComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml;
using System.IO;
using Project1.GameState;
using Project1.DirectionState; 
using System.Reflection;

namespace Project1.LevelComponents
{
	public class Level: ILevel
	{
        public IRoom CurrentRoom { get; set; }
        private IRoom NextRoom;
        public Vector2 CurrentRoomPosition = new Vector2(0, 55 * GameObjectManager.Instance.ScalingFactor);
        private Vector2 CurrentRoomInitialPosition = new Vector2(0, 55 * GameObjectManager.Instance.ScalingFactor);

        public ILevelMap LevelMap { get; set; }
        public Vector2 LinkStartingPosition { get; set; }

        private static Dictionary<string, IRoom> LevelDict;

        // TODO: Load in XML
        // ****** What is the purpose of adjust? 
        
        private static int adjust = 2 * GameObjectManager.Instance.ScalingFactor;

        private static Vector2 ScrollAdjust = new Vector2(0, 0);
        private static float ScrollStep = (3 * GameObjectManager.Instance.ScalingFactor);

        private static Vector2 RoomPosition = new Vector2(0, 55 * GameObjectManager.Instance.ScalingFactor);
        private static int RoomBorderSize = 32 * GameObjectManager.Instance.ScalingFactor;// + adjust;
        private static int RoomBlockSize = SpriteFactory.Instance.BlockSize * GameObjectManager.Instance.ScalingFactor;
        private static int RoomRows = 7;
        private static int RoomColumns = 12;

        public int PlayableWidth;
        public int PlayableHeight;

        private static List<Vector2> NewLinkPosition;
        private static IDirectionState NewLinkDirection;

        private static string StartRoom = "room2";
        
        public Level()
        {
            RoomBlockSize = SpriteFactory.Instance.BlockSize * GameObjectManager.Instance.ScalingFactor;
            PlayableHeight = (RoomBlockSize * RoomRows) + adjust;
            PlayableWidth = (RoomBlockSize * RoomColumns) + adjust;

            CreateDict();   // Initializes and loads <LevelDict> 

            if (LevelDict.ContainsKey(StartRoom))
            {
                CurrentRoom = LevelDict[StartRoom];
            }
            else
            {
                throw new IndexOutOfRangeException("Given room key for LevelDict is not found");
            }

            LevelMap = new LevelMap(LevelFactory.Instance.GetHUDTexture("HUDLevelMap"));
        }

        private void CreateDict()
        {
            // Initalize and load <LevelDict> with the room data from XMLLevel.XML

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

                Texture2D texture = LevelFactory.Instance.GetTexture(sheet);
                IRoom Room = new Room(name, RoomPosition, new Vector2(xPos, yPos), up, down, left, right, texture);

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

        public void Draw(SpriteBatch spriteBatch)
        {            
            CurrentRoom.Draw(spriteBatch);

            if (GameStateManager.Instance.CanRoomScroll())
            {
                NextRoom.Draw(spriteBatch);
            }
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
            CurrentRoomPosition = CurrentRoomInitialPosition;
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
                GameObjectManager.Instance.ClearRoomItems();    // remove objects from room - including Link(s)

                ScrollAdjust = new Vector2(0, ScrollStep);
                
                // Update Link Position
                NewLinkDirection = new DirectionStateUp();

                if (!position.Equals(Vector2.Zero)) // door not located in typical location
                {
                    NewLinkPosition = new List<Vector2>();
                    NewLinkPosition.Add(GetItemPosition(position.X, position.Y));
                    NewLinkPosition.Add(GetItemPosition(position.X, position.Y));
                }
                else // door located in typial location
                {
                    NewLinkPosition = GameVar.GetLinkNewRoomPosition(NewLinkDirection); 
                }

                // Update <NextRoom> 
                NextRoom = LevelDict[CurrentRoom.UpRoom];
                NextRoom.Position += new Vector2(0, -NextRoom.Size.Y);
                NextRoom.OpenDoor(new DirectionStateDown());

                LevelMap.MoveUp();
            }
        }
        public void MoveDown(Vector2 position)
        {
            if (GameStateManager.Instance.CanPlayGame() && !CurrentRoom.DownRoom.Equals("") && LevelDict.ContainsKey(CurrentRoom.DownRoom))
            {
                GameStateManager.Instance.StartScroll();        // trigger room scroll
                GameObjectManager.Instance.ClearRoomItems();    // remove objects from room - including Link(s)

                ScrollAdjust = new Vector2(0, -ScrollStep);
                
                // Update Link Position
                NewLinkDirection = new DirectionStateDown();

                if (!position.Equals(Vector2.Zero)) // door not located in typical location
                {
                    NewLinkPosition = new List<Vector2>();
                    NewLinkPosition.Add(GetItemPosition(position.X, position.Y));
                    NewLinkPosition.Add(GetItemPosition(position.X, position.Y));
                }
                else // door located in typial location
                {
                    NewLinkPosition = GameVar.GetLinkNewRoomPosition(NewLinkDirection);
                }

                // Update <NextRoom> 
                NextRoom = LevelDict[CurrentRoom.DownRoom];
                NextRoom.Position += new Vector2(0, NextRoom.Size.Y);
                NextRoom.OpenDoor(new DirectionStateUp());

                LevelMap.MoveDown();
            }
        }
        public void MoveLeft(Vector2 position)
        {
            if (GameStateManager.Instance.CanPlayGame() && !CurrentRoom.LeftRoom.Equals("") && LevelDict.ContainsKey(CurrentRoom.LeftRoom))
            {
                GameStateManager.Instance.StartScroll();    // trigger room scroll
                GameObjectManager.Instance.ClearRoomItems();    // remove objects from room - including Link(s) 

                ScrollAdjust = new Vector2(ScrollStep, 0);

                // Update Link Position
                NewLinkDirection = new DirectionStateLeft();

                if (!position.Equals(Vector2.Zero)) // door not located in typical location
                {
                    NewLinkPosition = new List<Vector2>();
                    NewLinkPosition.Add(GetItemPosition(position.X, position.Y));
                    NewLinkPosition.Add(GetItemPosition(position.X, position.Y));
                }
                else // door located in typial location
                {
                    NewLinkPosition = GameVar.GetLinkNewRoomPosition(NewLinkDirection);
                }

                // Update <NextRoom> 
                NextRoom = LevelDict[CurrentRoom.LeftRoom];
                NextRoom.Position += new Vector2(-NextRoom.Size.X, 0);
                NextRoom.OpenDoor(new DirectionStateRight());

                LevelMap.MoveLeft();
            }
        }
        public void MoveRight(Vector2 position)
        {
            if (GameStateManager.Instance.CanPlayGame() && !CurrentRoom.RightRoom.Equals("") && LevelDict.ContainsKey(CurrentRoom.RightRoom))
            {
                GameStateManager.Instance.StartScroll();    // trigger room scroll
                GameObjectManager.Instance.ClearRoomItems();   // remove objects from room - including Link(s) 

                ScrollAdjust = new Vector2(-ScrollStep, 0);

                // Update Link Position
                NewLinkDirection = new DirectionStateRight();

                if (!position.Equals(Vector2.Zero)) // door not located in typical location
                {
                    NewLinkPosition = new List<Vector2>();
                    NewLinkPosition.Add(GetItemPosition(position.X, position.Y));
                    NewLinkPosition.Add(GetItemPosition(position.X, position.Y));
                }
                else // door located in typial location
                {
                    NewLinkPosition = GameVar.GetLinkNewRoomPosition(NewLinkDirection);
                }

                // Update <NextRoom> 
                NextRoom = LevelDict[CurrentRoom.RightRoom];
                NextRoom.Position += new Vector2(NextRoom.Size.X, 0);
                
                NextRoom.OpenDoor(new DirectionStateLeft());

                LevelMap.MoveRight();
            }
        }

        /*
         * OLD MOVE METHODS 
         * need to confirm i can delete these
         clear - move - show up all items
         */
        /*
        public void MoveUp()
        {
            if (GameStateManager.Instance.CanPlayGame() && !CurrentRoom.UpRoom.Equals("") && LevelDict.ContainsKey(CurrentRoom.UpRoom))
            {
                GameStateManager.Instance.StartScroll();        // trigger room scroll
                GameObjectManager.Instance.ClearRoomItems();    // remove objects from room 

                ScrollAdjust = new Vector2(0, ScrollStep);

                LinkNewScrollPosition = LinkUpRoomPosition;

                // previousRoom = (Room)CurrentRoom;
                NextRoom = LevelDict[CurrentRoom.UpRoom];
                NextRoom.Position += new Vector2(0, -NextRoom.Size.Y);
                //CurrentRoom.Up(previousRoom);

                //GameObjectManager.Instance.UpdateRoomItems();
                LevelMap.MoveUp();
            }
        }
        public void MoveDown()
        {
            if (GameStateManager.Instance.CanPlayGame() && !CurrentRoom.DownRoom.Equals("") && LevelDict.ContainsKey(CurrentRoom.DownRoom))
            {
                GameStateManager.Instance.StartScroll();        // trigger room scroll
                GameObjectManager.Instance.ClearRoomItems();    // remove objects from room 

                ScrollAdjust = new Vector2(0, -ScrollStep);

                LinkNewScrollPosition = LinkDownRoomPosition;

                //Room previousRoom = (Room)CurrentRoom;
                NextRoom = LevelDict[CurrentRoom.DownRoom];
                NextRoom.Position += new Vector2(0, NextRoom.Size.Y);
                //CurrentRoom.Down(previousRoom);

                //GameObjectManager.Instance.UpdateRoomItems();
                LevelMap.MoveDown();
            }
        }
        public void MoveLeft()
        {
            if (GameStateManager.Instance.CanPlayGame() && !CurrentRoom.LeftRoom.Equals("") && LevelDict.ContainsKey(CurrentRoom.LeftRoom))
            {
                GameStateManager.Instance.StartScroll();    // trigger room scroll
                GameObjectManager.Instance.ClearRoomItems();    // remove objects from room 

                ScrollAdjust = new Vector2(ScrollStep, 0);

                LinkNewScrollPosition = LinkLeftRoomPosition;

                //Camera.Instance.CheckCollision(true);
                //GameObjectManager.Instance.ClearRoomItems();

                //Room previousRoom = (Room)CurrentRoom;
                NextRoom = LevelDict[CurrentRoom.LeftRoom];
                NextRoom.Position += new Vector2(-NextRoom.Size.X, 0);
                /*float distance = CurrentRoom.XPos;


                while (distance!=NextRoom.XPos)
                {
                    CurrentRoomPosition.X-=(float)0.25;
                    distance -= (float)0.25;
                    if (distance == NextRoom.XPos)
                    {
                        //CurrentRoom.Left((Room)NextRoom);
                        //CurrentRoom = NextRoom;
                        GameObjectManager.Instance.UpdateRoomItems();
                        LevelMap.MoveLeft();
                    }
                }* /
                LevelMap.MoveLeft();
            }
        }
        public void MoveRight()
        {
            if (GameStateManager.Instance.CanPlayGame() && !CurrentRoom.RightRoom.Equals("") && LevelDict.ContainsKey(CurrentRoom.RightRoom))
            {
                GameStateManager.Instance.StartScroll();    // trigger room scroll
                GameObjectManager.Instance.ClearRoomItems();

                ScrollAdjust = new Vector2(-ScrollStep, 0);

                LinkNewScrollPosition = LinkRightRoomPosition;
                /*
                Room previousRoom = (Room)CurrentRoom;
                CurrentRoom = LevelDict[CurrentRoom.RightRoom];
                CurrentRoom.Right(previousRoom);
                GameObjectManager.Instance.UpdateRoomItems();
                LevelMap.MoveRight();* /
                // TODO: be slow down and update room items
                //float distance = CurrentRoom.XPos;
                NextRoom = LevelDict[CurrentRoom.RightRoom];
                NextRoom.Position += new Vector2(NextRoom.Size.X, 0);

                /*
                while (distance != (NextRoom.XPos+NextRoom.Size.X/2))
                {
                    CurrentRoomPosition.X += (float)0.25;
                    distance += (float)0.25;
                    if (distance == (NextRoom.XPos + NextRoom.Size.X/2))
                    {
                        //CurrentRoom.Right((Room)NextRoom);
                        CurrentRoom = NextRoom;
                        GameObjectManager.Instance.UpdateRoomItems();
                        LevelMap.MoveRight();
                    }
                }* /
                LevelMap.MoveRight();
            }
        }
        */
        private void Scroll()
        {
            /* Scroll the room in the direction <step>
             */

            // TODO: need to make sure BOTH LINK's inventory's are done scrolling, otherwise have loop! 

            if (Math.Abs(NextRoom.Position.X - CurrentRoomInitialPosition.X) <= ScrollStep &&
                Math.Abs(NextRoom.Position.Y - CurrentRoomInitialPosition.Y) <= ScrollStep)
            {
                CurrentRoom.Position = CurrentRoomInitialPosition;  // reset room position 
                NextRoom.Position = CurrentRoomInitialPosition;     // reset room position 

                GameStateManager.Instance.StopScroll();    // trigger stop room scroll
                CurrentRoom = NextRoom;
                GameObjectManager.Instance.UpdateRoomItems();   // readd room items 

                GameObjectManager.Instance.SetLinkPosition(NewLinkPosition, NewLinkDirection);
            }
            else
            {
                CurrentRoom.Position += ScrollAdjust;
                NextRoom.Position += ScrollAdjust;
            }

            /* float distance = CurrentRoom.XPos;
            NextRoom = LevelDict[CurrentRoom.RightRoom];

            while (distance != NextRoom.XPos)
            {
                CurrentRoomPosition.X -= (float)0.25;
                distance -= (float)0.25;
                if (distance == NextRoom.XPos)
                {
                    //CurrentRoom.Left((Room)NextRoom);
                    //CurrentRoom = NextRoom;
                    GameObjectManager.Instance.UpdateRoomItems();
                    LevelMap.MoveLeft();
                }
            }*/
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

            float x = RoomPosition.X + RoomBorderSize + (RoomBlockSize * column);// - adjust;
            float y = RoomPosition.Y + RoomBorderSize + (RoomBlockSize * row); //+ adjust;
            return new Vector2(x, y);
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
