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
using Project1.CollisionComponents;
using Microsoft.Xna.Framework;
using Project1.LevelComponents; 

namespace Project1
{
    public class GameObjectManager
    {
        private static GameObjectManager instance = new GameObjectManager();
        public static GameObjectManager Instance
        {
            get
            {
                return instance;
            }
        }
        private GameObjectManager() { }

        public int ScalingFactor = 2; 
        public List<ILink> Links;
        public List<IBlock> Blocks;
        public List<IItem> Items;
        public List<IEnemy> Enemies;
        private List<IProjectile> Projectiles;
        private List<IController> Controllers;
        private IRoom Room; 

        public Game1 Game; 

        public void Initialize(Game1 game)
        {
            Links = new List<ILink>();
            Blocks = new List<IBlock>();
            Items = new List<IItem>();
            Enemies = new List<IEnemy>();
            Controllers = new List<IController>();
            Projectiles = new List<IProjectile>();
            
            Game = game;

            IController KeyboardController = new KeyboardController(Game);
            Controllers.Add(KeyboardController);

            IController MouseController = new MouseController(Game);
            Controllers.Add(MouseController);

            // Add Link 
            // TODO: Data drive Link Position 
            Vector2 LinkPosition = LevelFactory.Instance.LinkStartingPosition;
            ILink Link = new Link(LinkPosition); 
            Links.Add(Link);

            UpdateRoomItems();

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

            // Register Mouse commands 
            MouseController.InitializeGameCommands();

        }

        public void Update()
        {
            //UpdateRoomItems(); 

            foreach (IController controller in Controllers)
            {
                controller.Update();
            }

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
            for (int i = 0; i < Projectiles.Count; i++)
            {
                IProjectile Projectile = Projectiles[i];
                if (Projectile.InMotion)
                {
                    Projectile.Update();
                }
                else
                {
                    CollisionManager.Instance.RemoveObject((ICollidable)Projectile);
                    Projectiles.Remove(Projectile);
                }
            }

            CollisionManager.Instance.Update();
        }

        public void UpdateRoomItems()
        {
            Room = LevelFactory.Instance.CurrentRoom;
            Items = Room.Items;
            Blocks = Room.Blocks;
            Enemies = Room.Enemies;
            Projectiles = new List<IProjectile>();

            CollisionManager.Instance.Reset();

            foreach (ILink link in Links)
            {
                CollisionManager.Instance.AddObject((ICollidable)link);
            }
            foreach (IBlock block in Blocks)
            {
                CollisionManager.Instance.AddObject((ICollidable)block);
            }
            foreach (IItem item in Items)
            {
                CollisionManager.Instance.AddObject((ICollidable)item);
            }
            foreach (IEnemy enemy in Enemies)
            {
                CollisionManager.Instance.AddObject((ICollidable)enemy);
            }
            /*
            foreach (IProjectile projectile in Projectiles)
            {
                CollisionManager.Instance.AddObject((ICollidable)projectile);
            }*/
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LevelFactory.Instance.Draw(spriteBatch); 

            // TODO: Remove before submission 
            // For testing collision hitbox 
            /*Texture2D dummyTexture = new Texture2D(Game.GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { Color.White });
            
            foreach (ICollidable c in CollisionManager.Instance.MovingObjects)
            {
                spriteBatch.Draw(dummyTexture, c.Hitbox, Color.Black);
            }
            foreach (ICollidable c in CollisionManager.Instance.NonMovingObjects)
            {
                spriteBatch.Draw(dummyTexture, c.Hitbox, Color.White);
            }*/

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
            foreach(IProjectile Projectile in Projectiles)
            {
                Projectile.Draw(spriteBatch);
            }
        }

        public void Reset()
        {
            Room.Reset(); 
            foreach (ILink link in Links)
            {
                link.Reset();
            }
            foreach (IItem item in Items)
            {
                item.Reset();
            }
            foreach (IEnemy enemy in Enemies)
            {
                enemy.Reset();
            }

            foreach(IProjectile projectile in Projectiles)
            {
                CollisionManager.Instance.RemoveObject((ICollidable)projectile); 
            }
            Projectiles = new List<IProjectile>();
        }

        public void AddProjectile(IProjectile projectile)
        {
            Projectiles.Add(projectile);
            CollisionManager.Instance.AddObject((ICollidable)projectile);
        }
    }
}
