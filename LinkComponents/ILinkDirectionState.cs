using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.LinkComponents
{
    public interface ILinkDirectionState
    {
        ILink Link { get; set; }
        void Update();
        void MoveUp();
        void MoveDown();
        void MoveRight();
        void MoveLeft();
    }
}
