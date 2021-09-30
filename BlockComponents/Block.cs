using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;

namespace Project1.BlockComponents
{
    class Block : IBlock
    {
        public IBlockState BlockState { get; set; }
        private Game1 Game;
        private double counter = 0.0;

        private string[] ID = { "Base", "Stripe", "Brick", "Stair", "Blue", "Dots", "Black", "Dragon", "Fish", "Last", "Empty" };

        public Block(Game1 game)
        {
            Game = game;
            BlockState = new BlockBaseState(this);
        }

        public void PreviousBlock()
        {
            switch (ID[(int)counter])
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

            if (counter >= 0)
            {
                counter -= 0.25;
            }
            else
            {
                counter = 10;
            }
        }

        public void NextBlock()
        {
            switch (ID[(int)counter])
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
            if (counter <= 9.9)
            {
                counter += 0.25;
            }
            else
            {
                counter = 0;
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
    }
}
