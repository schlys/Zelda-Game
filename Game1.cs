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

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
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
            KeyboardController = new KeyboardController();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            SpriteFactory.Instance.LoadAllTextures(Content);
            Link = new Link();
            Block = new Block(this);
            Item = new Item();
            Enemy = new Enemy(this);
            
            // Register keyboard commands 
            // Requirement - Arrow and "wasd" keys should move Link and change his facing direction.
            KeyboardController.RegisterCommand(new LinkMoveUpCmd(this), Keys.W);
            KeyboardController.RegisterCommand(new LinkMoveDownCmd(this), Keys.S);
            KeyboardController.RegisterCommand(new LinkMoveRightCmd(this), Keys.D);
            KeyboardController.RegisterCommand(new LinkMoveLeftCmd(this), Keys.A);

            KeyboardController.RegisterCommand(new LinkMoveUpCmd(this), Keys.Up);
            KeyboardController.RegisterCommand(new LinkMoveDownCmd(this), Keys.Down);
            KeyboardController.RegisterCommand(new LinkMoveRightCmd(this), Keys.Right);
            KeyboardController.RegisterCommand(new LinkMoveLeftCmd(this), Keys.Left);

            /* Requirement - The 'z' and 'n' key should cause Link to attack using his sword. 
             * Number keys(1, 2, 3, etc.) should be used to have Link use a different item(later 
             *  this will be replaced with a menu system and 'x' and 'm' for the secondary item. 
             * Use 'e' to cause Link to become damaged
            */
            KeyboardController.RegisterCommand(new LinkSwordAttackCmd(this), Keys.Z);
            KeyboardController.RegisterCommand(new LinkSwordAttackCmd(this), Keys.N);

            KeyboardController.RegisterCommand(new LinkTakeDamageCmd(this), Keys.E);

            KeyboardController.RegisterCommand(new LinkUseNoItemCmd(this), Keys.NumPad0);
            KeyboardController.RegisterCommand(new LinkUseNoItemCmd(this), Keys.D0);
            /*
            KeyboardController.RegisterCommand(new LinkUseMagicalRodCmd(this), Keys.NumPad1);
            KeyboardController.RegisterCommand(new LinkUseMagicalRodCmd(this), Keys.D1);

            KeyboardController.RegisterCommand(new LinkUseMagicalSheildCmd(this), Keys.NumPad2);
            KeyboardController.RegisterCommand(new LinkUseMagicalSheildCmd(this), Keys.D2);

            KeyboardController.RegisterCommand(new LinkUseMagicalSwordCmd(this), Keys.NumPad3);
            KeyboardController.RegisterCommand(new LinkUseMagicalSwordCmd(this), Keys.D3);

            KeyboardController.RegisterCommand(new LinkUseWhiteSwordCmd(this), Keys.NumPad4);
            KeyboardController.RegisterCommand(new LinkUseWhiteSwordCmd(this), Keys.D4);

            KeyboardController.RegisterCommand(new LinkUseWoodenSwordCmd(this), Keys.NumPad5);
            KeyboardController.RegisterCommand(new LinkUseWoodenSwordCmd(this), Keys.D5);
            */
            KeyboardController.RegisterCommand(new LinkUseArrowCmd(this), Keys.NumPad1);
            KeyboardController.RegisterCommand(new LinkUseArrowCmd(this), Keys.D1);

            KeyboardController.RegisterCommand(new LinkUseBlueArrowCmd(this), Keys.NumPad2);
            KeyboardController.RegisterCommand(new LinkUseBlueArrowCmd(this), Keys.D2);

            KeyboardController.RegisterCommand(new LinkUseFireCmd(this), Keys.NumPad3);
            KeyboardController.RegisterCommand(new LinkUseFireCmd(this), Keys.D3);

            KeyboardController.RegisterCommand(new LinkUseBombCmd(this), Keys.NumPad4);
            KeyboardController.RegisterCommand(new LinkUseBombCmd(this), Keys.D4);

            KeyboardController.RegisterCommand(new LinkUseBoomerangCmd(this), Keys.NumPad5);
            KeyboardController.RegisterCommand(new LinkUseBoomerangCmd(this), Keys.D5);

            KeyboardController.RegisterCommand(new LinkUseBlueBoomerangCmd(this), Keys.NumPad6);
            KeyboardController.RegisterCommand(new LinkUseBlueBoomerangCmd(this), Keys.D6);

            /* Requirement - Use 'q' to quit 
             * and 'r' to reset the program back to its initial state.
             */
            KeyboardController.RegisterCommand(new GameEndCmd(this), Keys.Q);
            KeyboardController.RegisterCommand(new GameRestartCmd(this), Keys.R);

            /* Requirement - Use keys "t" and "y" to cycle between which block is currently being 
             * shown (i.e. think of the obstacles as being in a list where the game's current 
             * obstacle is being drawn, "t" switches to the previous item and "y" switches to the next)
             */
            KeyboardController.RegisterCommand(new PreviousBlockCmd(this, Block), Keys.T);
            KeyboardController.RegisterCommand(new NextBlockCmd(this, Block), Keys.Y);

            /* Requirement - Use keys "u" and "i" to cycle between which item is currently being shown 
             * (i.e. think of the items as being in a list where the game's current item is being drawn, 
             * "u" switches to the previous item and "i" switches to the next)
             * Items should move and animate as they do in the final game, but should not interact with any other objects
             */
            KeyboardController.RegisterCommand(new PreviousItemCmd(this, Item), Keys.U);
            KeyboardController.RegisterCommand(new NextItemCmd(this, Item), Keys.I);

            /* Requirement - Use keys "o" and "p" to cycle between which enemy or npc is currently being shown 
             * (i.e. think of these characters as being in a list where the game's current character is being drawn, 
             * "o" switches to the previous item and "p" switches to the next)
             * characters should move, animate, fire projectiles, etc. as they do in the final game, but should not 
             * interact with any other objects
             */
            KeyboardController.RegisterCommand(new PreviousEnemyCmd(this, Enemy), Keys.O);
            KeyboardController.RegisterCommand(new NextEnemyCmd(this, Enemy), Keys.P);

            // Load sprite images 
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardController.Update(this);
            Link.Update();
            Item.Update();
            Enemy.Update();
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

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void Restart()
        {
            // IDEA - add restart methods to each item 
            Link.Reset();
            Block.Reset();
            Item.Reset();
            Enemy.Reset();
        }
    }
}
