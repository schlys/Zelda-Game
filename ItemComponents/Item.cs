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

        private string[] ItemTypes = { "WoodenSword", "WhiteSword", "MagicalSword", "MagicalRod",
            "SmallSheild", "MagicalSheild", "Boomerang", "MagicalBoomerang", "Bomb",
            "Bow", "Arrow", "SilverArrow", "BlueCandle", "RedCandle", "Recorder", "Food",
            "LifePotion", "SecondLifePotion", "MagicalRod", "Raft", "BookOfMagic", "BlueRing",
            "RedRing", "Stepladder", "MagicalKey", "PowerBracelet", "HeartContainer" };

        public Item1 item1 = new Item1();
        public Item2 item2 = new Item2();
        public Item3 item3 = new Item3();

        public Item(Game1 game)
        {
            Game = game;
            ItemState = new ItemWoodenSwordState(this);        // Wooden Sword by default 
            //Texture = SpriteFactory.Instance.ItemSpriteSheet();
        }

        public void PreviousItem()
        {
            switch (ItemState.ID)
            {
                case "Arrow":
                    ItemState = new ItemWoodenSwordState(this);
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
                default:
                    ItemState = new ItemArrowState(this);
                    break;
            }
        }

        public void NextItem()
        {
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
                    ItemState = new ItemWoodenSwordState(this);
                    break;
                case "WoodenSword":
                    ItemState = new ItemArrowState(this);
                    break;
                default:
                    ItemState = new ItemArrowState(this);
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            /*switch ((int)counter)
            {
                case 1:
                    item1.Draw(spriteBatch, Texture);
                    item1.Update();
                    break;
                case 2:
                    item2.Draw(spriteBatch, Texture);
                    item2.Update();
                    break;
                case 3:
                    item3.Draw(spriteBatch, Texture);
                    item3.Update();
                    break;
            }*/
            Rectangle sourceRectangle = new Rectangle(0, 40, 40, 40);
            Rectangle destinationRectangle = new Rectangle(600, 100, 80, 80);
            spriteBatch.Draw(ItemState.Texture, destinationRectangle, ItemState.SourceRectangle, Color.White);
        }

        public void Update()
        {
            ItemState.Update();
        }
    }
}
