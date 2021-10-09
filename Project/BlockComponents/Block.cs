using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;
using Project1.CollisionComponents;

namespace Project1.BlockComponents
{
    class Block : IBlock, ICollidable
    {
        // Properties from IBlock 
        public IBlockState BlockState { get; set; }
        public Vector2 Position { get; set; }
        public int Size { get; set; }

        // Properties from ICollidable 
        public Rectangle Hitbox { get; set; }
        public bool IsMoving { get; set; }

        private double Counter = 0.0;
        private double Step = 0.2; 

        private string[] BlockTypeKeys = { "Base", "Stripe", "Brick", "Stair", "Blue", "Dots", "Black", "Dragon", "Fish", "Last", "Empty" };

        public Block()
        {
            BlockState = new BlockBaseState(this);
            Position = new Vector2(100, 100);
            Size = 40; 
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, BlockState.BlockSprite.hitX, BlockState.BlockSprite.hitY);
            IsMoving = true;
        }

        private void SetBlockState(int i)
        {
            // TODO: change to jump table 
            switch (BlockTypeKeys[i])
            {
                case "Base":
                    BlockState = new BlockBaseState(this);
                    break;
                case "Stripe":
                    BlockState = new BlockStripeState(this);
                    break;
                case "Brick":
                    BlockState = new BlockBrickState(this);
                    break;
                case "Stair":
                    BlockState = new BlockStairState(this);
                    break;
                case "Blue":
                    BlockState = new BlockBlueState(this);
                    break;
                case "Dots":
                    BlockState = new BlockDotsState(this);
                    break;
                case "Black":
                    BlockState = new BlockBlackState(this);
                    break;
                case "Dragon":
                    BlockState = new BlockDragonState(this);
                    break;
                case "Fish":
                    BlockState = new BlockFishState(this);
                    break;
                case "Last":
                    BlockState = new BlockLastState(this);
                    break;
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            BlockState.Draw(spriteBatch);
        }

        public void Collide(ICollidable item)
        {
            
        }
    }
}
