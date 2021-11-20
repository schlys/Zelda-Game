using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Project1.SpriteComponents;
using System.Reflection;

namespace Project1.DirectionState
{
    public sealed class DirectionManager
    {
        private static DirectionManager instance = new DirectionManager();
        public static DirectionManager Instance
        {
            get
            {
                return instance;
            }
        }
        
        public IDirectionState GetDirectionState(string direction) 
        {
            IDirectionState state; 
            switch (direction)
            {
                case GameVar.DirectionDown:
                    state = new DirectionStateDown();
                    break; 
                case GameVar.DirectionUp:
                    state = new DirectionStateUp();
                    break;
                case GameVar.DirectionRight:
                    state = new DirectionStateRight();
                    break;
                case GameVar.DirectionLeft:
                    state = new DirectionStateLeft();
                    break;
                default:
                    throw new IndexOutOfRangeException();   // invalid entry
            }
            return state; 
        }
    }
}
