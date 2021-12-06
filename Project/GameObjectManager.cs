using System;
using System.Collections.Generic;
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
using Project1.DirectionState;
using Project1.GameState;

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

        public List<ILink> Links;
        public List<ILink> Links_copy; 
        public int LinkCount;          
        public List<IHUD> HUDs;
        public List<IBlock> Blocks;
        public List<IItem> Items;
        public List<IItem> DroppedItems;
        public List<IEnemy> Enemies;
        public List<IDoor> Doors;
        private List<IProjectile> Projectiles;
        private List<IController> Controllers;

        public ILevel Level { get; set; }
         
        private Tuple<bool, ILink> FreezeEnemies;   // when true, stores the link who is freezing enemies 

        public Game1 Game;

        public void Initialize(Game1 game)
        {
            Game = game;

            Links = new List<ILink>();
            Links_copy = new List<ILink>();
            HUDs = new List<IHUD>();
            Blocks = new List<IBlock>();
            Items = new List<IItem>();
            DroppedItems = new List<IItem>(); 
            Enemies = new List<IEnemy>();
            Doors = new List<IDoor>();
            Controllers = new List<IController>();
            Projectiles = new List<IProjectile>();

            FreezeEnemies = new Tuple<bool, ILink>(false, null);

            IController KeyboardController = new KeyboardController(Game);
            Controllers.Add(KeyboardController);

            LinkCount = 1;
            SetLinkCount(LinkCount);

            Level = new Level();

            // Register Keyboard commands 
            KeyboardController.InitializeGameCommands();
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

            Level.Update(); 

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

                foreach (IItem item in DroppedItems)
                {
                    item.Update();
                }

                if (!FreezeEnemies.Item1)
                {
                    for(int i = 0; i < Enemies.Count; i++)
                    {
                        Enemies[i].Update();
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
             * <Enemies> to be the items in <Room>. Remove all past <Projectiles> and <DroppedItems>. 
             * Removes all objects from the CollisionManager and adds the newly added <Room> objects. 
             * Unfreeze the enemies 
             */

            IRoom Room = Level.CurrentRoom;
            Links = new List<ILink>(Links_copy);
            Items = Room.Items;
            DroppedItems = new List<IItem>(); 
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
                if (!enemy.Health.Dead())
                {
                    CollisionManager.Instance.AddObject((ICollidable)enemy);
                    enemy.Spawn();
                }
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
             * Updates <Room> to be the <CurrentRoom> in LevelFactory. Remove all past <Projectiles>. 
             * Removes all objects from the CollisionManager and adds the newly added <Room> objects. 
             * Unfreeze the enemies 
             */

            CollisionManager.Instance.Reset();

            if (FreezeEnemies.Item1)
            {
                FreezeEnemies.Item2.Inventory.CanFreeze = false;
                FreezeEnemies = new Tuple<bool, ILink>(false, null);
            }

            Links = new List<ILink>();
            Blocks = new List<IBlock>();
            Items = new List<IItem>();
            DroppedItems = new List<IItem>(); 
            Enemies = new List<IEnemy>();
            Doors = new List<IDoor>();
            Projectiles = new List<IProjectile>();
        }

        public void Draw(SpriteBatch spriteBatch, int i)
        {
            Level.Draw(spriteBatch);

            foreach (IBlock block in Blocks)
            {
                block.Draw(spriteBatch);
            }
            foreach (IItem item in Items)
            {
                item.Draw(spriteBatch);
            }
            foreach (IItem item in DroppedItems)
            {
                item.Draw(spriteBatch);
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
            foreach (ILink link in Links)
            {
                link.Draw(spriteBatch);
            }

            // NOTE: Draw HUD last so covers all sprites on ItemSelect screen
            if (i < HUDs.Count) HUDs[i].Draw(spriteBatch);
        }

        public void Reset()
        {
            Level.Reset();

            foreach (IController controller in Controllers)
            {
                controller.Reset();
                controller.InitializeGameCommands();
            }

            Projectiles.Clear();
            LinkCount = 1;
            Links.Clear();
            Links_copy.Clear();
            HUDs.Clear(); 
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

        public void DropItem(IItem item)
        {
            DroppedItems.Add(item);

            CollisionManager.Instance.AddObject((ICollidable)item); 
        }

        public void EnemyDie(IEnemy enemy)
        {
            CollisionManager.Instance.RemoveObject((ICollidable)enemy); 
        }

        public void SetLinkCount(int n)
        {
            LinkCount = n;
        }

        // Precondition: len(positions) >= len(Links)
        public void SetLinkPosition(List<Vector2> positions, IDirectionState direction)
        {
            for(int i = 0; i < Links.Count; i++)
            {
                Links[i].SetPosition(positions[i], direction);
            }
        }

        public void CreatePlayers()
        {
            /* Create <LinkCount> Links and a HUD for each Link. 
             * Parallel Construction between Link and HUDs
             */ 
            
            for (int i = 0; i < LinkCount; i++)
            {
                ILink Link = new Link(GameVar.GetLinkStartPosition(i), i, Game);
                Links.Add(Link);

                Links_copy = new List<ILink>(Links);

                IHUD HUD = new HUD(Link, Game);
                HUDs.Add(HUD);

                foreach (IController controller in Controllers)
                {
                    if (controller is KeyboardController)
                    {
                        controller.InitializeLinkCommands(Link, i + 1);
                    }
                }
            }

            UpdateRoomItems();
        }

        public void SetLinksHasMap()
        {
            foreach (ILink link in Links)
            {
                link.Inventory.HasMap = true;
            }
        }
        public void SetLinksHasCompass()
        {
            foreach (ILink link in Links)
            {
                link.Inventory.HasCompass = true;
            }
        }

        public void StopScroll()
        {
            /* Trigger stop scroll in GameStateManager, and stop scrolling in all Link <HUDs>
             */

            GameStateManager.Instance.StopScroll(); 

            foreach(IHUD hud in HUDs)
            {
                hud.StopScroll(); 
            }
        }

        /* Methods that connect to <Level> 
         */
        public bool IsWithinRoomBounds(Vector2 location)
        {
            return Level.IsWithinRoomBounds(location); 
        }

        public Vector2 GetItemPosition(float row, float column)
        {
            return Level.GetItemPosition(row, column); 
        }

        public void MoveUp(Vector2 position)
        {
            Level.MoveUp(position); 
        }
        public void MoveDown(Vector2 position)
        {
            Level.MoveDown(position);
        }
        public void MoveLeft(Vector2 position)
        {
            Level.MoveLeft(position);
        }
        public void MoveRight(Vector2 position)
        {
            Level.MoveRight(position);
        }
        public Rectangle GetPlayableRoomBounds()
        {
            return Level.GetPlayableRoomBounds(); 
        }
    }
}
