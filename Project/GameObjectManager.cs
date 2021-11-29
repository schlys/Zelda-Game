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
using System.Windows.Forms;
using Project1.DirectionState;
using Project1.GameState;
using Project1.StoreComponents; 

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
        public List<ILink> Links_copy;  // ??? when is this used? 
        public int LinkCount;          // Accesed in GameStateManager.cs (change its setting - at the start window, player can press 'x' without press 2 because 2 seems to be selected)
        public List<IHUD> HUDs;
        //public List<IStore> Stores;
        public List<IBlock> Blocks;
        public List<IItem> Items;
        public List<IEnemy> Enemies;
        public List<IDoor> Doors;
        private List<IProjectile> Projectiles;
        private List<IController> Controllers;

        //private IRoom Room;
        public ILevel Level { get; set; }
         
        private Tuple<bool, ILink> FreezeEnemies;   // when true, stores the link who is freezing enemies 

        public Game1 Game;

        public void Initialize(Game1 game)
        {
            Game = game;

            Links = new List<ILink>();
            Links_copy = new List<ILink>();
            HUDs = new List<IHUD>();
            //Stores = new List<IStore>();
            Blocks = new List<IBlock>();
            Items = new List<IItem>();
            Enemies = new List<IEnemy>();
            Doors = new List<IDoor>();
            Controllers = new List<IController>();
            Projectiles = new List<IProjectile>();

            FreezeEnemies = new Tuple<bool, ILink>(false, null);

            IController KeyboardController = new KeyboardController(Game);
            Controllers.Add(KeyboardController);

            IController MouseController = new MouseController(Game);
            Controllers.Add(MouseController);

            LinkCount = 1;
            SetLinkCount(LinkCount);

            Level = new Level();

            // Register Keyboard commands 
            KeyboardController.InitializeGameCommands();
           
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

            //LevelFactory.Instance.Update();
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

            IRoom Room = Level.CurrentRoom;
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
             * Updates <Room> to be the <CurrentRoom> in LevelFactory. Remove all past <Projectiles>. 
             * Removes all objects from the CollisionManager and adds the newly added <Room> objects. 
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

            foreach (IProjectile projectile in Projectiles)
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

        public void Draw(SpriteBatch spriteBatch, int i)
        {
            //LevelFactory.Instance.Draw(spriteBatch);
            Level.Draw(spriteBatch); 

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
            if (i < HUDs.Count) HUDs[i].Draw(spriteBatch);

            //if (i < Stores.Count) Stores[i].Draw(spriteBatch);
        }

        public void Reset()
        {
            //LevelFactory.Instance.Reset();
            Level.Reset(); 

            UpdateRoomItems();
            /*(foreach (ILink link in Links)
            {
                link.Reset();
            }
            foreach (IHUD HUD in HUDs)
            {
                HUD.Reset();
            }*/
            foreach (IItem item in Items)
            {
                item.Reset();
            }
            foreach (IEnemy enemy in Enemies)
            {
                enemy.Reset();
            }

            foreach (IProjectile projectile in Projectiles)
            {
                CollisionManager.Instance.RemoveObject((ICollidable)projectile);
            }
            Projectiles = new List<IProjectile>();

            LinkCount = 1;
            Links.Clear();
            Links_copy.Clear();
            HUDs.Clear();
            //Stores.Clear(); 

            SetLinkCount(LinkCount);
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
            // Create <LinkCount> Links and a HUD for each Link. 
            // Parallel Construction between Link and HUDs
            for (int i = 0; i < LinkCount; i++)
            {
                Tuple<Vector2, Color> linkInfo = GameVar.GetLinkInfo(i);
                ILink Link = new Link(linkInfo.Item1, linkInfo.Item2, i, Game);
                Links.Add(Link);

                Links_copy = new List<ILink>(Links);

                IHUD HUD = new HUD(Link, Game);
                HUDs.Add(HUD);

                //IStore Store = new Store(Link, Game);
                //Stores.Add(Store);

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

        /* Methods that connect to <Level>, better to have them go through here since other 
         * places do not know about <Level> 
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
        public Vector2 GetRoomSize()
        {
            return Level.CurrentRoom.Size; 
        }
        public Rectangle GetPlayableRoomBounds()
        {
            return Level.GetPlayableRoomBounds(); 
        }
    }
}
