using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Command;
using Project1.Controller;
using Project1.LinkComponents;
using Project1.BlockComponents;
using Project1.ItemComponents;
using Project1.SpriteFactoryComponents;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public ILink Link;
        public IBlock Block;
        public IItem Item;
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
            //Link = new Link(this);
            //Block = new Block(this);
            //Item = new Item(this); 
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteFactory.Instance.LoadAllTextures(Content);
            Link = new Link(this);
            Block = new Block(this);
            Item = new Item(this); 
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

            KeyboardController.RegisterCommand(new LinkUseWoodenSwordCmd(this), Keys.NumPad1);
            KeyboardController.RegisterCommand(new LinkUseWoodenSwordCmd(this), Keys.D1);

            KeyboardController.RegisterCommand(new LinkUseMagicalRodCmd(this), Keys.NumPad2);
            KeyboardController.RegisterCommand(new LinkUseMagicalRodCmd(this), Keys.D2);

            KeyboardController.RegisterCommand(new LinkUseMagicalSheildCmd(this), Keys.NumPad3);
            KeyboardController.RegisterCommand(new LinkUseMagicalSheildCmd(this), Keys.D3);

            KeyboardController.RegisterCommand(new LinkUseMagicalSwordCmd(this), Keys.NumPad4);
            KeyboardController.RegisterCommand(new LinkUseMagicalSwordCmd(this), Keys.D4);

            KeyboardController.RegisterCommand(new LinkUseWhiteSwordCmd(this), Keys.NumPad5);
            KeyboardController.RegisterCommand(new LinkUseWhiteSwordCmd(this), Keys.D5);

            /* Requirement - Use 'q' to quit 
             * and 'r' to reset the program back to its initial state.
             */
            KeyboardController.RegisterCommand(new GameEndCmd(this), Keys.Q);
            KeyboardController.RegisterCommand(new GameRestartCmd(this), Keys.R);

            /* Requirement - Use keys "t" and "y" to cycle between which block is currently being 
             * shown (i.e. think of the obstacles as being in a list where the game's current 
             * obstacle is being drawn, "t" switches to the previous item and "y" switches to the next)
             */
            KeyboardController.RegisterCommand(new PreviousBlockCmd(this), Keys.T);
            KeyboardController.RegisterCommand(new NextBlockCmd(this), Keys.Y);

            /* Requirement - Use keys "u" and "i" to cycle between which item is currently being shown 
             * (i.e. think of the items as being in a list where the game's current item is being drawn, 
             * "u" switches to the previous item and "i" switches to the next)
             * Items should move and animate as they do in the final game, but should not interact with any other objects
             */
            KeyboardController.RegisterCommand(new PreviousItemCmd(this), Keys.U);
            KeyboardController.RegisterCommand(new NextItemCmd(this), Keys.I);

            /* Requirement - Use keys "o" and "p" to cycle between which enemy or npc is currently being shown 
             * (i.e. think of these characters as being in a list where the game's current character is being drawn, 
             * "o" switches to the previous item and "p" switches to the next)
             * characters should move, animate, fire projectiles, etc. as they do in the final game, but should not 
             * interact with any other objects
             */



            // Load sprite images 
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardController.Update(this);
            Link.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            
            Link.Draw(_spriteBatch);
            Block.Draw(_spriteBatch);
            Item.Draw(_spriteBatch); 

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void Restart()
        {

        }
    }
}
