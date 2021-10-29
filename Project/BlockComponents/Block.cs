using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteComponents;
using Project1.CollisionComponents;
using Project1.LevelComponents; 
using Project1.DirectionState;
using System;
using System.Xml;
using System.Reflection;
using System.Linq;

namespace Project1.BlockComponents
{
    class Block : IBlock, ICollidable
    {
        // Properties from IBlock 
        public IBlockState BlockState { get; set; }
        public Vector2 Position { get; set; }

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }
        public String TypeID { get; set; }

        // Other Properies 

        private double Counter = 0.0;
        private double Step = 0.2; 
        private Dictionary<String, String> BlockConstructors = new Dictionary<string, string>();
        private string[] BlockTypeKeys = {  };

        public Block(Vector2 position, String type)
        {

            Assembly assem = typeof(IBlockState).Assembly;
            XmlDocument XMLData = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLData/XMLBlock.xml";
            XMLData.Load(path);
            XmlNodeList Blocks = XMLData.DocumentElement.SelectNodes("/Blocks/Block");
            foreach (XmlNode node in Blocks)
            {
                string name = node.SelectSingleNode("Name").InnerText;
                string Type = node.SelectSingleNode("Type").InnerText;
                BlockConstructors.Add(name, Type);
                BlockTypeKeys.Append(name);

            }
            if (BlockConstructors.ContainsKey(type)) {
                Type command1Type = assem.GetType("Project1.BlockComponents." + BlockConstructors[type]);
                ConstructorInfo constructor1 = command1Type.GetConstructor(new[] { typeof(IBlock) });
                object command1 = constructor1.Invoke(new object[] { this });
                BlockState = (IBlockState)command1;
            }

 


            /* Get accurate dimensions for the hitbox, but position is off */
            Position = position; 
            Hitbox = CollisionManager.Instance.GetHitBox(Position, BlockState.BlockSprite.HitBox);
            /* Correct the position to account for empty space around the hitbox */
            int RoomBlockSize = SpriteFactory.Instance.UniversalSize * GameObjectManager.Instance.ScalingFactor;
            Position -= new Vector2((RoomBlockSize-Hitbox.Width)/2, (RoomBlockSize -Hitbox.Height) / 2);
            /* Get correct hibox for updated position */
            Hitbox = CollisionManager.Instance.GetHitBox(Position, BlockState.BlockSprite.HitBox);

            Hitbox = CollisionManager.Instance.GetHitBox(Position, BlockState.BlockSprite.HitBox);
            IsMoving = false;
            TypeID = this.GetType().Name.ToString();

        }
        

        private void SetBlockState(int i)
        {
            Assembly assem = typeof(IBlockState).Assembly;

            if (BlockConstructors.ContainsKey(BlockTypeKeys[i]))
            {
                Type command1Type = assem.GetType("Project1.BlockComponents." + BlockConstructors[BlockTypeKeys[i]]);
                ConstructorInfo constructor1 = command1Type.GetConstructor(new[] { typeof(IBlock) });
                object command1 = constructor1.Invoke(new object[] { this });
                BlockState = (IBlockState)command1;
            }
        }

        public void PreviousBlock()
        {
            SetBlockState((int)Counter);
            IncrementCounter(-Step); 
        }

        public void NextBlock()
        {
            SetBlockState((int)Counter);
            IncrementCounter(Step); 
        }

        // Increment the field Counter by i and ensure counter stays within the bounds [0, ItemTypeKeys.Length] 
        private void IncrementCounter(double i)
        {
            Counter += i;
            if (Counter > (BlockTypeKeys.Length - Step / 2))
            {
                Counter = 0;
            }
            else if (Counter < -Step / 2)
            {
                Counter = BlockTypeKeys.Length - 1;
            }
        }

        public void Reset()
        {
            BlockState = new BlockBaseState(this);
            Hitbox = CollisionManager.Instance.GetHitBox(Position, BlockState.BlockSprite.HitBox);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            BlockState.Draw(spriteBatch);
        }
    }
}
