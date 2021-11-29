using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
using System.Reflection;
using System.Windows.Forms;

namespace Project1.GameState
{
    /* GameStateManager is a singleton that manages the current state  for the game
     * using <CurrentState>. The states include the start, game play, item selection screen, 
     * game over win, game over lose, pause, game over, restart, and scrollscreen state. 
     */ 
    public sealed class GameStateManager: IGameStateManager
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
        public SpriteFont Font { get; set; }

        public List<Tuple<SwapChainRenderTarget, Form>> swapChain;
        
        private GameStateManager() 
        {
            // Set default room 
            CurrentState = new GameStateStart();
        }
        public void Initialize(Game1 game)
        {
            Game = game;
            Font = GameStateManager.Instance.Game.Content.Load<SpriteFont>("Fonts/TitleFont");

            swapChain = new List<Tuple<SwapChainRenderTarget, Form>>();

            Game.Window.Title = "Project1 - player 1";
        }

        
        public void Draw(SpriteBatch spriteBatch) 
        {
            for (int i = 0; i < swapChain.Count; i++)
            {
                Game.GraphicsDevice.SetRenderTarget(swapChain[i].Item1);
                Game.GraphicsDevice.Clear(Color.Black);
                CurrentState.Draw(spriteBatch, i+1);
                swapChain[i].Item1.Present();
            }

            Game.GraphicsDevice.SetRenderTarget(null);
            Game.GraphicsDevice.Clear(Color.Black);

            // Draw first player window separately to prevent flickering
            CurrentState.Draw(spriteBatch, 0);
           
        }
        public void Reset() 
        {
            // Restart the game from the beginning 
            CurrentState = CurrentState.Reset();
            GameObjectManager.Instance.Reset();

            if (swapChain.Count > 0)
            {
                for (int i = 0; i < swapChain.Count; i++)
                {
                    swapChain[i].Item2.Visible = false;
                }
                swapChain.RemoveRange(0, swapChain.Count);
            }
        }
        public void Pause() 
        {
            // Pause the game - stop all motion
            CurrentState = CurrentState.Pause();
        }
        public void Start() 
        {
            if (CurrentState is GameStateStart)
            {
                // create a new window for each new player - 1 since one window already exists
                for (int i = 1; i < GameObjectManager.Instance.LinkCount; i++)
                {
                    GameWindow newWindow = GameWindow.Create(Game, GameVar.ScreenWidth, GameVar.ScreenHeight);
                    newWindow.Title = "Project1 - player " + (i + 1);
                    newWindow.AllowUserResizing = true; // user can resize the window freely.

                    Form newForm = (Form)Form.FromHandle(newWindow.Handle);

                    newForm.Location = new System.Drawing.Point(0, GameVar.ScreenWidth / 8);
                    newForm.Visible = true;

                    swapChain.Add(Tuple.Create(new SwapChainRenderTarget(Game.GraphicsDevice,
                        newWindow.Handle,
                        newWindow.ClientBounds.Width,
                        newWindow.ClientBounds.Height,
                        false,
                        SurfaceFormat.Color,
                        DepthFormat.Depth24Stencil8,
                        1,
                        RenderTargetUsage.PlatformContents,
                        PresentInterval.Default), newForm)
                   );
                }
                GameObjectManager.Instance.CreatePlayers();

                GameSoundManager.Instance.PlaySong();
                CurrentState = CurrentState.StartGame();
            }
        }
        public void ItemSelection() 
        {
            // Display the item selection screen
            CurrentState = CurrentState.ItemSelectMenu();
        }
        public void GameOverLose() 
        {
            // Game is lost, can restart the game or exit 
            GameSoundManager.Instance.PlayLinkDie();
            GameSoundManager.Instance.StopSong();
            CurrentState = CurrentState.LoseGame();
        }
        public void GameOverWin() 
        {
            // Game is won, can restart the game or exit 
            CurrentState = CurrentState.WinGame();
        }

        public void SetLinkCount(int n)
        {
            /* Sets the Link Count in <GameObjectManager> to n iff the <CurrentState> is of type GameStartState. 
             */
            if(CurrentState is GameStateStart)
            {
                GameObjectManager.Instance.SetLinkCount(n);
            }
        }
        public void StartScroll()
        {
            // In GamePlay, trigger scroll animation
            CurrentState = CurrentState.StartScroll();
        }
        public void StopScroll()
        {
            CurrentState = CurrentState.StopScroll();
        }

        public void StoreMenu()
        {
            CurrentState = CurrentState.StoreMenu(); 
        }
        public bool CanPlayGame()
        {
            // True if <CurrentState> is of type GameStateGamePlay 
            return (CurrentState is GameStateGamePlay); 
        }
        public bool CanDrawHUD()
        {
            // True if not drawing item select screen 
            return !CanItemSelect() && !CanItemScroll();
        }
        public bool CanItemSelect()
        {
            // True if <CurrentState> is of type GameItemSelect
            return (CurrentState is GameStateItemSelect);
        }
        public bool CanItemScroll()
        {
            // True if <CurrentState> is of type GameItemScroll 
            return (CurrentState is GameStateItemScroll); 
        }
        public bool CanRoomScroll()
        {
            // True if <CurrentState> is of type GameRoomScroll
            return (CurrentState is GameStateRoomScroll);
        }
        public bool CanStoreMenu()
        {
            // True if <CurrentState> is of type GameRoomScroll
            return (CurrentState is GameStateStore);
        }
    }
}
