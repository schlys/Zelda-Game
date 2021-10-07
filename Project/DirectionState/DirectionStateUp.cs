﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.DirectionState
{
    class DirectionStateUp : IDirectionState
    {
        public string ID { get; set; }

        public DirectionStateUp()
        {
            ID = "Up";

        }


        public IDirectionState MoveDown()
        {
            return new DirectionStateDown();
        }

        public IDirectionState MoveLeft()
        {
            return new DirectionStateLeft();
        }

        public IDirectionState MoveRight()
        {
            return new DirectionStateRight();
        }

        public IDirectionState MoveUp()
        {
            return new DirectionStateUp();
        }
    }
}