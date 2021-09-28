using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.SpriteFactoryComponents;

namespace Project1.ItemComponents
{
    class Item : IItem
    {
        public IItemState ItemState { get; set; }
        //public Texture2D Texture { get; set; }
        //public Sprite ItemSprite { get; set; }
        public string ID { get; set; }
        private Game1 Game;
        private double counter = 1.0;

        private string[] ItemTypes = { "Angel", "Sword", "WhiteSword", "MagicalSword", "MagicalRod",
            "SmallSheild", "MagicalSheild", "Boomerang", "MagicalBoomerang", "Bomb",
            "Bow", "Arrow", "SilverArrow", "Candle", "RedCandle", "Recorder", "Food",
            "LifePotion", "SecondLifePotion", "MagicalRod", "Raft", "BookOfMagic", "BlueRing",
            "RedRing", "Stepladder", "MagicalKey", "PowerBracelet", "HeartContainer" };

        public Item(Game1 game)
        {
            Game = game;
            ItemState = new ItemAngelState(this);        // Wooden Sword by default 
            //Texture = SpriteFactory.Instance.ItemSpriteSheet();
        }

        public void PreviousItem()
        {
            switch ((int)counter)
            {
                case 1:
                    ItemState = new ItemAngelState(this);
                    break;
                case 2:
                    ItemState = new ItemHeartState(this);
                    break;
                case 3:
                    ItemState = new ItemJewelryState(this);
                    break;
                case 4:
                    ItemState = new ItemLifePotionState(this);
                    break;
                case 5:
                    ItemState = new ItemBookState(this);
                    break;
                case 6:
                    ItemState = new ItemFoodState(this);
                    break;
                case 7:
                    ItemState = new ItemTriangleState(this);
                    break;
                case 8:
                    ItemState = new ItemSwordState(this);
                    break;
                case 9:
                    ItemState = new ItemBoomerangState(this);
                    break;
                case 10:
                    ItemState = new ItemBombState(this);
                    break;
                case 11:
                    ItemState = new ItemArrowState(this);
                    break;
                case 12:
                    ItemState = new ItemCandleState(this);
                    break;
                case 13:
                    ItemState = new ItemRingState(this);
                    break;
                case 14:
                    ItemState = new ItemKeyState(this);
                    break;
                default:
                    ItemState = new ItemAngelState(this);
                    break;
            }

            counter -= 0.1;
            if (counter < 1)
            {
                counter = 15;
            }

            /*
            switch (ItemState.ID)
            {
                case "Arrow":
                    ItemState = new ItemSwordState(this);
                    break;
                case "BlueCandle":
                    ItemState = new ItemArrowState(this);
                    break;
                case "BlueRing":
                    ItemState = new ItemBlueCandleState(this);
                    break;
                case "Bomb":
                    ItemState = new ItemBlueRingState(this);
                    break;
                case "BookOfMagic":
                    ItemState = new ItemBombState(this);
                    break;
                case "Boomerang":
                    ItemState = new ItemBookOfMagicState(this);
                    break;
                case "Bow":
                    ItemState = new ItemBoomerangState(this);
                    break;
                case "Food":
                    ItemState = new ItemBowState(this);
                    break;
                case "HeartContainer":
                    ItemState = new ItemFoodState(this);
                    break;
                case "LifePotion":
                    ItemState = new ItemHeartContainerState(this);
                    break;
                case "MagicalBoomerang":
                    ItemState = new ItemLifePotionState(this);
                    break;
                case "MagicalKey":
                    ItemState = new ItemMagicalBoomerangState(this);
                    break;
                case "MagicalRod":
                    ItemState = new ItemMagicalKeyState(this);
                    break;
                case "MagicalSheild":
                    ItemState = new ItemMagicalRodState(this);
                    break;
                case "MagicalSword":
                    ItemState = new ItemMagicalSheildState(this);
                    break;
                case "PowerBracelet":
                    ItemState = new ItemMagicalSwordState(this);
                    break;
                case "Raft":
                    ItemState = new ItemPowerBraceletState(this);
                    break;
                case "Recorder":
                    ItemState = new ItemRaftState(this);
                    break;
                case "RedCandle":
                    ItemState = new ItemRecorderState(this);
                    break;
                case "RedRing":
                    ItemState = new ItemRedCandleState(this);
                    break;
                case "SecondLifePotion":
                    ItemState = new ItemRedRingState(this);
                    break;
                case "SilverArrow":
                    ItemState = new ItemSecondLifePotionState(this);
                    break;
                case "SmallSheild":
                    ItemState = new ItemSilverArrowState(this);
                    break;
                case "StepLadder":
                    ItemState = new ItemSmallSheildState(this);
                    break;
                case "WhiteSword":
                    ItemState = new ItemStepLadderState(this);
                    break;
                case "WoodenSword":
                    ItemState = new ItemWhiteSwordState(this);
                    break;
                case "Angel":
                    ItemState = new ItemAngel(this);
                    break;
                default:
                    ItemState = new ItemArrowState(this);
                    break;
            }
            */
        }

        public void NextItem()
        {
            switch ((int) counter)
            {
                case 1:
                    ItemState = new ItemAngelState(this);
                    break;
                case 2:
                    ItemState = new ItemHeartState(this);
                    break;
                case 3:
                    ItemState = new ItemJewelryState(this);
                    break;
                case 4:
                    ItemState = new ItemLifePotionState(this);
                    break;
                case 5:
                    ItemState = new ItemBookState(this);
                    break;
                case 6:
                    ItemState = new ItemFoodState(this);
                    break;
                case 7:
                    ItemState = new ItemTriangleState(this);
                    break;
                case 8:
                    ItemState = new ItemSwordState(this);
                    break;
                case 9:
                    ItemState = new ItemBoomerangState(this);
                    break;
                case 10:
                    ItemState = new ItemBombState(this);
                    break;
                case 11:
                    ItemState = new ItemArrowState(this);
                    break;
                case 12:
                    ItemState = new ItemCandleState(this);
                    break;
                case 13:
                    ItemState = new ItemRingState(this);
                    break;
                case 14:
                    ItemState = new ItemKeyState(this);
                    break;
                default:
                    ItemState = new ItemAngelState(this);
                    break;
            }

            counter += 0.1;
            if (counter > 15)
            {
                counter = 1;
            }
            /*
            switch(ItemState.ID)
            {
                case "Arrow":
                    ItemState = new ItemBlueCandleState(this);
                    break; 
                case "BlueCandle":
                    ItemState = new ItemBlueRingState(this);
                    break;
                case "BlueRing":
                    ItemState = new ItemBombState(this);
                    break;
                case "Bomb":
                    ItemState = new ItemBookOfMagicState(this);
                    break;
                case "BookOfMagic":
                    ItemState = new ItemBoomerangState(this);
                    break;
                case "Boomerang":
                    ItemState = new ItemBowState(this);
                    break;
                case "Bow":
                    ItemState = new ItemFoodState(this);
                    break;
                case "Food":
                    ItemState = new ItemHeartContainerState(this);
                    break;
                case "HeartContainer":
                    ItemState = new ItemLifePotionState(this);
                    break;
                case "LifePotion":
                    ItemState = new ItemMagicalBoomerangState(this);
                    break;
                case "MagicalBoomerang":
                    ItemState = new ItemMagicalKeyState(this);
                    break;
                case "MagicalKey":
                    ItemState = new ItemMagicalRodState(this);
                    break;
                case "MagicalRod":
                    ItemState = new ItemMagicalSheildState(this);
                    break;
                case "MagicalSheild":
                    ItemState = new ItemMagicalSwordState(this);
                    break;
                case "MagicalSword":
                    ItemState = new ItemPowerBraceletState(this);
                    break;
                case "PowerBracelet":
                    ItemState = new ItemRaftState(this);
                    break;
                case "Raft":
                    ItemState = new ItemRecorderState(this);
                    break;
                case "Recorder":
                    ItemState = new ItemRedCandleState(this);
                    break;
                case "RedCandle":
                    ItemState = new ItemRedRingState(this);
                    break;
                case "RedRing":
                    ItemState = new ItemSecondLifePotionState(this);
                    break;
                case "SecondLifePotion":
                    ItemState = new ItemSilverArrowState(this);
                    break;
                case "SilverArrow":
                    ItemState = new ItemSmallSheildState(this);
                    break;
                case "SmallSheild":
                    ItemState = new ItemStepLadderState(this);
                    break;
                case "StepLadder":
                    ItemState = new ItemWhiteSwordState(this);
                    break;
                case "WhiteSword":
                    ItemState = new ItemSwordState(this);
                    break;
                case "WoodenSword":
                    ItemState = new ItemArrowState(this);
                    break;
                case "Angel":
                    ItemState = new ItemAngel(this);
                    break;
                default:
                    ItemState = new ItemArrowState(this);
                    break;
            }
            */
        }

        public void Reset()
        {
            ItemState = new ItemAngelState(this);        // Wooden Sword by default 
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            Rectangle sourceRectangle = new Rectangle(0, 40, 40, 40);
            Rectangle destinationRectangle = new Rectangle(600, 100, 80, 80);
            //spriteBatch.Draw(ItemState.Texture, destinationRectangle, ItemState.SourceRectangle, Color.White);

            ItemState.Draw(spriteBatch);
            
        }

        public void Update()
        {
            ItemState.Update();
        }
    }
}
