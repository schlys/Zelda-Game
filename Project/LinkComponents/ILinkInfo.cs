using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.LinkComponents
{
    public interface ILinkInfo
    {
        List<Tuple<Vector2, Color>> Info { get; set; }
        Tuple<Vector2, Color> GetInfo(int player);
    }
}
