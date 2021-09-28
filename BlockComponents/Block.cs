using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;
using Project1.SpriteComponents;


namespace Project1.BlockComponents
{
    class Block : IBlock
    {
        public IBlockState BlockState { get; set; }
        private Game1 Game;
        private double counter = 1.0;
        
        public Block(Game1 game)
        {
            Game = game;
            BlockState = new BlockBaseState(this);
        }

        public void PreviousBlock()
        {
            switch ((int)counter)
            {
                case 1:
                    BlockState = new BlockBaseState(this);
                    break;
                case 2:
                    BlockState = new BlockStripeState(this);
                    break;
                case 3:
                    BlockState = new BlockBrickState(this);
                    break;
                case 4:
                    BlockState = new BlockStairState(this);
                    break;
                case 5:
                    BlockState = new BlockBlueState(this);
                    break;
                case 6:
                    BlockState = new BlockDotsState(this);
                    break;
                case 7:
                    BlockState = new BlockBlackState(this);
                    break;
                case 8:
                    BlockState = new BlockDragonState(this);
                    break;
                case 9:
                    BlockState = new BlockFishState(this);
                    break;
                case 10:
                    BlockState = new BlockLastState(this);
                    break;
            }

            if (counter >= 1)
            {
                counter-=0.1;
            }
            else
            {
                counter = 11;
            }
        }

        public void NextBlock()
        {
            switch ((int)counter)
            {
                case 1:
                    BlockState = new BlockBaseState(this);
                    break;
                case 2:
                    BlockState = new BlockStripeState(this);
                    break;
                case 3:
                    BlockState = new BlockBrickState(this);
                    break;
                case 4:
                    BlockState = new BlockStairState(this);
                    break;
                case 5:
                    BlockState = new BlockBlueState(this);
                    break;
                case 6:
                    BlockState = new BlockDotsState(this);
                    break;
                case 7:
                    BlockState = new BlockBlackState(this);
                    break;
                case 8:
                    BlockState = new BlockDragonState(this);
                    break;
                case 9:
                    BlockState = new BlockFishState(this);
                    break;
                case 10:
                    BlockState = new BlockLastState(this);
                    break;
            }
            if (counter <= 11)
            {
                counter+=0.1;
            }
            else
            {
                counter = 1;
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
