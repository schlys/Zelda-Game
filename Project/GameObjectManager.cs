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
using Project1.HeadsUpDisplay;

namespace Project1
{
    public sealed class GameObjectManager
    {
        /* GameObjectManager is a singleton that manages the objects used for the game. It also makes calls to
         * CollisionManager. 
         */ 

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
        public List<ILink> Links_copy;
        private int LinkCount;
        public List<IHUD> HUDs;
        public List<IBlock> Blocks;
        public List<IItem> Items;
        public List<IEnemy> Enemies;
        public List<IDoor> Doors;
        private List<IProjectile> Projectiles;
        private List<IController> Controllers;
        private IRoom Room;
        private Tuple<bool, ILink> FreezeEnemies;   // when true, stores the link who is freezing enemies 

        public Game1 Game; 

        public void Initialize(Game1 game)
        {
            Links = new List<ILink>();
            HUDs = new List<IHUD>();
            Blocks = new List<IBlock>();
            Items = new List<IItem>();
            Enemies = new List<IEnemy>();
            Doors = new List<IDoor>();
            Controllers = new List<IController>();
            Projectiles = new List<IProjectile>();

            FreezeEnemies = new Tuple<bool, ILink>(false, null);
            Game = game;

            IController KeyboardController = new KeyboardController(Game);
            Controllers.Add(KeyboardController);

            IController MouseController = new MouseController(Game);
            Controllers.Add(MouseController);

            /* Add Link and their HUD
             * Parallel Contruction: between Link and HUD 
             */
            Vector2 LinkPosition = LevelFactory.Instance.LinkStartingPosition;
            ILink Link = new Link(LinkPosition, Game); 
            Links.Add(Link);
            Links_copy = new List<ILink>(Links); 
            IHUD HUD = new HUD(Link, game);
            HUDs.Add(HUD); 

            UpdateRoomItems();

            // Register Keyboard commands 
            KeyboardController.InitializeGameCommands();
            int player = 1;
            foreach(ILink link in Links) 
            {
                KeyboardController.InitializeLinkCommands(link, player);
                player++;
            }

            // Register Mouse commands 
            MouseController.InitializeGameCommands();

        }

        public void Update()
        {
            foreach (IController controller in Controllers)
            {
                controller.Update();
            }
            foreach (IHUD HUD in HUDs)
            {
                HUD.Update();
            }

            // TODO: change to Level Object not the factory when separate class 
            LevelFactory.Instance.Update();

            if (GameState.GameStateManager.Instance.CanPlayGame())
            {
                foreach (ILink link in Links)
                {
                    link.Update();
                    Links_copy = new List<ILink>(Links);
                    if (link.Inventory.CanFreeze) // check if Link freeze enemies from moving
                    {
                        FreezeEnemies = new Tuple<bool, ILink>(true, link);
                    }
                }
                
                // NOTE: Blocks do not update 

                foreach (IItem item in Items)
                {
                    item.Update();
                }

                if (!FreezeEnemies.Item1)
                {
                    foreach (IEnemy enemy in Enemies)
                    {
                        enemy.Update();
                    }
                }

               for (int i = 0; i < Projectiles.Count; i++)
                {
                    IProjectile Projectile = Projectiles[i];
                    Projectile.Update();
                }

                CollisionManager.Instance.Update();
            }
        }

        public void UpdateRoomItems()
        {
            /* Called when room switch
             * Updates <Room> to be the <CurrentRoom> in LevelFactory. Updates <Items>, <Blocks> and
             * <Enemies> to be the items in <Room>. Remove all past <Projectiles>. Removes all objects 
             * from the CollisionManager and adds the newly added <Room> objects. 
             * Unfreeze the enemies 
             */

            Room = LevelFactory.Instance.CurrentRoom;
            Links = new List<ILink>(Links_copy);
            Items = Room.Items;
            Blocks = Room.Blocks;
            Enemies = Room.Enemies;
            Doors = Room.Doors;
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
                enemy.Spawn();
            }
            foreach (IDoor door in Doors)
            {
                CollisionManager.Instance.AddObject((ICollidable)door);
            }
            
            if (FreezeEnemies.Item1)
            {
                FreezeEnemies.Item2.Inventory.CanFreeze = false;
                FreezeEnemies = new Tuple<bool, ILink>(false, null);
            }
        }

        public void ClearRoomItems()
        {
            /* Called when room switch
             * Updates <Room> to be the <CurrentRoom> in LevelFactory. Updates <Items>, <Blocks> and
             * <Enemies> to be the items in <Room>. Remove all past <Projectiles>. Removes all objects 
             * from the CollisionManager and adds the newly added <Room> objects. 
             * Unfreeze the enemies 
             */

            CollisionManager.Instance.Reset();

            foreach (ILink link in Links)
            {
                CollisionManager.Instance.RemoveObject((ICollidable)link);
            }
            foreach (IBlock block in Blocks)
            {
                CollisionManager.Instance.RemoveObject((ICollidable)block);
            }
            foreach (IItem item in Items)
            {
                CollisionManager.Instance.RemoveObject((ICollidable)item);
            }
            foreach (IEnemy enemy in Enemies)
            {
                CollisionManager.Instance.RemoveObject((ICollidable)enemy);
            }
            foreach (IDoor door in Doors)
            {
                CollisionManager.Instance.RemoveObject((ICollidable)door);
            }
            
            foreach(IProjectile projectile in Projectiles)
            {
                CollisionManager.Instance.RemoveObject((ICollidable)projectile);
            }

            if (FreezeEnemies.Item1)
            {
                FreezeEnemies.Item2.Inventory.CanFreeze = false;
                FreezeEnemies = new Tuple<bool, ILink>(false, null);
            }

            Links = new List<ILink>(); 
            Blocks = new List<IBlock>();
            Items = new List<IItem>();
            Enemies = new List<IEnemy>();
            Doors = new List<IDoor>();
            Projectiles = new List<IProjectile>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LevelFactory.Instance.Draw(spriteBatch); 
            

            foreach (IBlock block in Blocks)
            {
                block.Draw(spriteBatch);
            }
            foreach (IItem item in Items)
            {
                item.Draw(spriteBatch);
            }
            foreach (ILink link in Links)
            {
                link.Draw(spriteBatch);
            }
            foreach (IEnemy enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (IDoor door in Doors)
            {
                door.Draw(spriteBatch);
            }
            foreach (IProjectile Projectile in Projectiles)
            {
                Projectile.Draw(spriteBatch);
            }

            
            // NOTE: Draw HUD last so covers all sprites on ItemSelect screen
            foreach (IHUD HUD in HUDs)
            {
                HUD.Draw(spriteBatch);
            }
        }

        public void Reset()
        {
            LevelFactory.Instance.Reset();
            UpdateRoomItems(); 

            foreach (ILink link in Links)
            {
                link.Reset();
            }
            foreach (IHUD HUD in HUDs)
            {
                HUD.Reset();
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

        public void RemoveProjectile(IProjectile projectile)
        {
            if (Projectiles.Contains(projectile))
            {
                Projectiles.Remove(projectile);
            }
            
            CollisionManager.Instance.RemoveObject((ICollidable)projectile);
        }
        public void SetLinkCount(int n)
        {
            /* Sets <LinkCount> to n 
             */
            LinkCount = n;
        }
        public void SetLinkPosition(Vector2 position)
        {
            foreach (ILink link in Links)
            {
                link.SetPosition(position); 
            }
        }
    }
}
