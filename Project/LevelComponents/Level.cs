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
using System.Reflection;

namespace Project1.LevelComponents
{
	public class level
	{
        public IRoom CurrentRoom { get; set; }
        private IRoom NextRoom;
        public Vector2 CurrentRoomPosition = new Vector2(0, 55 * GameObjectManager.Instance.ScalingFactor);
        private Vector2 CurrentRoomInitialPosition = new Vector2(0, 55 * GameObjectManager.Instance.ScalingFactor);

        public ILevelMap LevelMap { get; set; }
        public Dictionary<String, Texture2D> HUDTextures { get; set; }
        public Vector2 LinkStartingPosition { get; set; }

        private Vector2 LinkLeftRoomPosition;
        private Vector2 LinkRightRoomPosition;
        private Vector2 LinkUpRoomPosition;
        private Vector2 LinkDownRoomPosition;
        private Vector2 LinkNewScrollPosition;

        private static Dictionary<string, IRoom> LevelDict;

        // TODO: Load in XML
        // ****** What is the purpose of adjust? 
        private static int adjust = 2 * GameObjectManager.Instance.ScalingFactor;

        private static Vector2 ScrollAdjust = new Vector2(0, 0);
        private static float ScrollStep = (float)(1 * GameObjectManager.Instance.ScalingFactor);

        private static Vector2 RoomPosition = new Vector2(0, 55 * GameObjectManager.Instance.ScalingFactor);
        //private static Vector2 RoomPosition = new Vector2(514, 885);
        private static int RoomBorderSize = 32 * GameObjectManager.Instance.ScalingFactor;// + adjust;
        private static int RoomBlockSize = SpriteFactory.Instance.BlockSize * GameObjectManager.Instance.ScalingFactor;
        private static int RoomRows = 7;
        private static int RoomColumns = 12;
        private static Vector2 PositionChanger = new Vector2(0, 0);

        private static string StartRoom = "room2";

        public void Draw(SpriteBatch spriteBatch)
        {
            // TODO: Remove before submission 
            // For testing collision hitbox 
            Texture2D dummyTexture = new Texture2D(GameObjectManager.Instance.Game.GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { Color.White });
            Rectangle roomBorder = new Rectangle((int)RoomPosition.X, (int)RoomPosition.Y, (RoomBorderSize * 2) + (RoomBlockSize * RoomColumns), (RoomBorderSize * 2) + (RoomBlockSize * RoomRows));
            Rectangle roomFloor = GetPlayableRoomBounds(); //new Rectangle((int)RoomPosition.X + RoomBorderSize, (int)RoomPosition.Y + RoomBorderSize, (RoomBlockSize * RoomColumns), (RoomBlockSize * RoomRows));
            Rectangle roomTile = new Rectangle((int)LevelFactory.Instance.GetItemPosition(4, 1).X, (int)LevelFactory.Instance.GetItemPosition(4, 1).Y, RoomBlockSize, RoomBlockSize);

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

        /*
         clear - move - show up all items
         */

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
                }*/
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
                LevelMap.MoveRight();*/
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
                }*/
                LevelMap.MoveRight();
            }
        }

        private void Scroll()
        {
            /* Scroll the room in the direction <step>
             */

            if (Math.Abs(NextRoom.Position.X - CurrentRoomInitialPosition.X) <= ScrollStep &&
                Math.Abs(NextRoom.Position.Y - CurrentRoomInitialPosition.Y) <= ScrollStep)
            {
                CurrentRoom.Position = CurrentRoomInitialPosition;  // reset room position 
                NextRoom.Position = CurrentRoomInitialPosition;     // reset room position 

                GameStateManager.Instance.StopScroll();    // trigger stop room scroll
                CurrentRoom = NextRoom;
                GameObjectManager.Instance.UpdateRoomItems();   // readd room items 

                //GameObjectManager.Instance.SetLinkPosition(LinkNewScrollPosition);  // update link position
            }
            else
            {
                CurrentRoom.Position += ScrollAdjust;
                NextRoom.Position += ScrollAdjust;
            }
            /*float distance = CurrentRoom.XPos;
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
