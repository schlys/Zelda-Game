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
     * using <CurrentState>. It also makes calls to GameObjectManager like drawing and 
     * resetting at the appropriate time. The states include start, game play, item 
     * selection, store, game over win, game over lose, pause, restart, and the 
     * scrolling states. 
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
        public SpriteFont TitleFont { get; set; }
        public SpriteFont BodyFont { get; set; }

        private List<Tuple<SwapChainRenderTarget, Form>> SwapChain;
        
        private GameStateManager() 
        {
            // Set default room 
            CurrentState = new GameStateStart();
        }
        public void Initialize(Game1 game)
        {
            Game = game;
            
            TitleFont = GameStateManager.Instance.Game.Content.Load<SpriteFont>(GameVar.TitleFont);
            BodyFont = GameStateManager.Instance.Game.Content.Load<SpriteFont>(GameVar.BodyFont);

            // Add the current window to <SwapChain> 
            SwapChain = new List<Tuple<SwapChainRenderTarget, Form>>();
            Game.Window.Title = "Project1 - player 1";
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            /* Draw <CurrentState> in each window of <SwapChain> and afterwards
             * redraw it on the initial window to prevent flickering. 
             */ 
            
            for (int i = 0; i < SwapChain.Count; i++)
            {
                Game.GraphicsDevice.SetRenderTarget(SwapChain[i].Item1);
                Game.GraphicsDevice.Clear(GameVar.GetLinkColor(i+1));
                CurrentState.Draw(spriteBatch, i+1);
                SwapChain[i].Item1.Present();
            }

            Game.GraphicsDevice.SetRenderTarget(null);
            Game.GraphicsDevice.Clear(GameVar.GetLinkColor(GameVar.Player1));
            CurrentState.Draw(spriteBatch, GameVar.Player1);
           
        }
        public void Reset() 
        {
            /* Restart the game, reset <GameObjectManager, and clear <SwapChain>. 
             */
            
            CurrentState = CurrentState.Reset();
            GameObjectManager.Instance.Reset();

            if (SwapChain.Count > 0)
            {
                for (int i = 0; i < SwapChain.Count; i++)
                {
                    SwapChain[i].Item2.Visible = false;
                }
                SwapChain.RemoveRange(0, SwapChain.Count);
            }
        }
        public void Pause() 
        {
            /* Pause the game - stop all motion
             */ 
            
            CurrentState = CurrentState.Pause();
        }
        public void Start() 
        {
            /* Start the game, create a new window for [number of players - 1] since 
             * one window already exists. Create the palyers in <GameObjectManager>. 
             */ 
            
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

                    SwapChain.Add(Tuple.Create(new SwapChainRenderTarget(Game.GraphicsDevice,
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
            /* Display the item selection screen
             */  
            CurrentState = CurrentState.ItemSelectMenu();
        }
        public void GameOverLose() 
        {
            /* Game is lost, can restart the game or exit 
             */  
            GameSoundManager.Instance.PlayLinkDie();
            GameSoundManager.Instance.StopSong();
            CurrentState = CurrentState.LoseGame();
        }
        public void GameOverWin() 
        {
            /* Game is won, can restart the game or exit 
             */  
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
            /* In GamePlay, trigger scroll animation
             */  
            CurrentState = CurrentState.StartScroll();
        }
        public void StopScroll()
        {
            CurrentState = CurrentState.StopScroll();
        }

        public void EnterStoreMenu()
        {
            CurrentState = CurrentState.EnterStoreMenu(); 
        }
        public void ExitStoreMenu()
        {
            CurrentState = CurrentState.ExitStoreMenu();
        }
        public bool CanPlayGame()
        {
            return (CurrentState is GameStateGamePlay); 
        }
        public bool CanDrawHUD()
        {
            // True if not drawing item select screen or item scrolling 
            return !CanItemSelect() && !CanItemScroll();
        }
        public bool CanItemSelect()
        {
            return (CurrentState is GameStateItemSelect);
        }
        public bool CanItemScroll()
        {
            return (CurrentState is GameStateItemScroll); 
        }
        public bool CanRoomScroll()
        {
            return (CurrentState is GameStateRoomScroll);
        }
        public bool CanStoreMenu()
        {
            return (CurrentState is GameStateStore);
        }
    }
}
