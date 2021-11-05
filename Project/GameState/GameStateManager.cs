﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;

namespace Project1.GameState
{
    /* GameStateManager is a singleton that manages the current state  for the game
     * using <CurrentState>. The states include the start, game play, item selection screen, 
     * game over win, game over lose, pause, game over, and restart state. 
     */ 
    public class GameStateManager: IGameStateManager
    {
        private static GameStateManager instance = new GameStateManager();
        public static GameStateManager Instance
        {
            get
            {
                return instance;
            }
        }
        public IGameState CurrentState { get; set; }
        public Game1 Game { get; set; }
        private Sprite Link = SpriteFactory.Instance.GetSpriteData("PickUpItem");
        private Sprite TriForceFragment = SpriteFactory.Instance.GetSpriteData("TriforceFragment");
        private int Height = 176 * GameObjectManager.Instance.ScalingFactor;
        private int Width = 256 * GameObjectManager.Instance.ScalingFactor;
        private GameStateManager() 
        {
            // TODO: set default room 
            CurrentState = new GameStateStart();
        }
        public void Initialize(Game1 game)
        {
            Game = game;
        }
        public void Update()
        {
            /*if (IsWin)
            {
                TriForceFragment = SpriteFactory.Instance.GetSpriteData("TriForceFragment");
                Link = SpriteFactory.Instance.GetSpriteData("PickUpItem");
            }*/
        }
        public void Draw(SpriteBatch spriteBatch) 
        {
            SpriteFont font = Game.Content.Load<SpriteFont>("Fonts/TitleFont");
            String text = "Game state: " + CurrentState.ID; 
            spriteBatch.DrawString(font, text, new Vector2(600, 10), Color.Black);

            String text2 = "Commands: \nX - start\nSpace - pause \nI - Item select\nR - Restart\nQ - Quit\nZ/N - Attack";
            spriteBatch.DrawString(font, text2, new Vector2(600, 30), Color.White);

            // TODO: Make black screen for lose/win
            // TODO: Remove magic numbers
            Texture2D blackRectangle = new Texture2D(Game.GraphicsDevice, 1, 1);
            blackRectangle.SetData(new[] { Color.Black });

            CurrentState.Draw(spriteBatch);
            
        }
        public void Reset() 
        {
            // Restart the game from the beginning 
            CurrentState = CurrentState.Reset();
        }
        public void Pause() 
        {
            CurrentState = CurrentState.Pause();
        }
        public void Start() 
        {
            // TODO: is this neccesary? how it is different from PlayGame() ??
            CurrentState = CurrentState.StartGame(); 
        }
        public void PlayGame() 
        {
            // Begin game play
            CurrentState = CurrentState.StartGame();
        }
        public void ItemSelection() 
        {
            // Display the item selection screen
            CurrentState = CurrentState.ItemSelectMenu();
        }
        public void GameOverLose() 
        {
            // Game is lost, can restart the game or exit 
            CurrentState = CurrentState.LoseGame();
        }
        public void GameOverWin() 
        {
            // Game is won, can restart the game or exit 
            CurrentState = CurrentState.WinGame();
        }
        public bool CanPlayGame()
        {
            // True if <CurrentGame> is of type GameStateGamePlay 
            // TODO: test type of object not ID
            return (CurrentState.ID.Equals("GamePlay")); 
        }
        public bool CanDrawHUD()
        {
            // True if not drawing item select screen 
            return !CanItemSelect();
        }
        public bool CanItemSelect()
        {
            // True if <CurrentGame> is of type GameItemSelect
            // TODO: test type of object not ID
            return (CurrentState.ID.Equals("ItemSelect"));
        }
    }
}
