using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Command;
using Project1.Controller;
using Project1.LinkComponents;
using Project1.BlockComponents;
using Project1.ItemComponents;
using Project1.SpriteFactoryComponents;
using Project1.EnemyComponents;
using Project1.ProjectileComponents;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        // TODO: create a game object to house all items like link, item, block... 
        // TODO: create a list for all items so can easily add multiple 
        public ILink Link;
        public IBlock Block;
        public IItem Item;
        public IEnemy Enemy;
        private IController KeyboardController;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            KeyboardController = new KeyboardController(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            SpriteFactory.Instance.LoadAllTextures(Content);
            Link = new Link();
            Block = new Block();
            Item = new Item();
            Enemy = new Enemy(this);

            // Register Keyboard commands 
            KeyboardController.InitializeGameCommands();
            KeyboardController.InitializeLinkCommands(Link);
            KeyboardController.InitializeBlockCommands(Block);
            KeyboardController.InitializeItemCommands(Item);
            KeyboardController.InitializeEnemyCommands(Enemy);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardController.Update(this);
            Link.Update();
            Item.Update();
            Enemy.Update();
            ProjectileManager.Instance.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            _spriteBatch.Begin();
            
            Link.Draw(_spriteBatch);
            Block.Draw(_spriteBatch);
            Item.Draw(_spriteBatch);
            Enemy.Draw(_spriteBatch);
            ProjectileManager.Instance.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void Restart()
        {
            Link.Reset();
            Block.Reset();
            Item.Reset();
            Enemy.Reset();
        }
    }
}
