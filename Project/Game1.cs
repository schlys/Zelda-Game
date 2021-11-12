using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Command;
using Project1.Controller;
using Project1.LinkComponents;
using Project1.BlockComponents;
using Project1.ItemComponents;
using Project1.SpriteComponents;
using Project1.LevelComponents;
using Project1.EnemyComponents;
using Project1.ProjectileComponents;
using Project1.CollisionComponents;
using Project1.GameState;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 256*2;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 500;   // set this value to the desired height of your window
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            SpriteFactory.Instance.LoadAllTextures(Content);
            LevelFactory.Instance.LoadAllTextures(Content); 

            GameObjectManager.Instance.Initialize(this);
            GameStateManager.Instance.Initialize(this);
            GameSoundManager.Instance.Initialize(this);

        }


        protected override void Update(GameTime gameTime)
        {
            // NOTE: should we remove this? it's here by default 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GameObjectManager.Instance.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            GameObjectManager.Instance.Draw(spriteBatch);
            GameStateManager.Instance.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void Restart()
        {
            GameStateManager.Instance.Reset();
            GameObjectManager.Instance.Reset();
        }

        public void Pause()
        {
            GameStateManager.Instance.Pause(); 
        }

        public void StartGame()
        {
            // Must reset before starting for cases when won / lost 
            GameObjectManager.Instance.Reset();

            GameStateManager.Instance.Start();
        }

        public void ItemSelection()
        {
            GameStateManager.Instance.ItemSelection();
        }

        public void Win()
        {
            GameStateManager.Instance.GameOverWin();
        }
        public void Lose()
        {
            GameStateManager.Instance.GameOverLose();
        }
    }
}
