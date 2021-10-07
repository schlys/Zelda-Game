using System;
using System.Collections.Generic;
using System.Text;
using Project1.Controller;
using Project1.LinkComponents;
using Project1.BlockComponents;
using Project1.ItemComponents;
using Project1.EnemyComponents;
using Project1.ProjectileComponents;
using Microsoft.Xna.Framework.Graphics;

namespace Project1
{
    public class GameObjectManager
    {
        public List<ILink> Links;
        public List<IBlock> Blocks;
        public List<IItem> Items;
        public List<IEnemy> Enemies;
        private List<IController> Controllers;
        Game1 Game; 

        public GameObjectManager(Game1 game)
        {
            Links = new List<ILink>();
            Blocks = new List<IBlock>();
            Items = new List<IItem>();
            Enemies = new List<IEnemy>();
            Controllers = new List<IController>();

            Game = game;

            IController KeyboardController = new KeyboardController(Game);
            Controllers.Add(KeyboardController);
            Links.Add(new Link());
            Blocks.Add(new Block());
            Items.Add(new Item());
            Enemies.Add(new Enemy());

            // Register Keyboard commands 
            KeyboardController.InitializeGameCommands();
            foreach(ILink link in Links) 
            {
                KeyboardController.InitializeLinkCommands(link);
            }
            foreach (IBlock block in Blocks)
            {
                KeyboardController.InitializeBlockCommands(block);
            }
            foreach (IItem item in Items)
            {
                KeyboardController.InitializeItemCommands(item);
            }
            foreach (IEnemy enemy in Enemies)
            {
                KeyboardController.InitializeEnemyCommands(enemy);
            }
        }

        public void Update()
        {
            foreach (ILink link in Links)
            {
                link.Update();             
            }

            // NOTE: Blocks do not update 

            foreach (IItem item in Items)
            {
                item.Update();
            }
            foreach (IEnemy enemy in Enemies)
            {
                enemy.Update();
            }
            foreach (IController controller in Controllers)
            {
                controller.Update();
            }
            ProjectileManager.Instance.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ILink link in Links)
            {
                link.Draw(spriteBatch);
            }
            foreach (IBlock block in Blocks)
            {
                block.Draw(spriteBatch);
            }
            foreach (IItem item in Items)
            {
                item.Draw(spriteBatch);
            }
            foreach (IEnemy enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }
            ProjectileManager.Instance.Draw(spriteBatch);
        }

        public void Reset()
        {
            foreach (ILink link in Links)
            {
                link.Reset();
            }
            foreach (IBlock block in Blocks)
            {
                block.Reset();
            }
            foreach (IItem item in Items)
            {
                item.Reset();
            }
            foreach (IEnemy enemy in Enemies)
            {
                enemy.Reset();
            }
            ProjectileManager.Instance.Reset();
        }
    }
}
