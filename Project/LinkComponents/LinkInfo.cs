using Microsoft.Xna.Framework;
using Project1.LevelComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.LinkComponents
{
    class LinkInfo : ILinkInfo
    {
        private static LinkInfo instance = new LinkInfo();
        public static LinkInfo Instance
        {
            get
            {
                return instance;
            }
        }
        private LinkInfo() 
        {
            Initialize();
        }
        public List<Tuple<Vector2, Color>> Info { get; set; }

        private void Initialize()
        {
            Info = new List<Tuple<Vector2, Color>>
            {
                {Tuple.Create(LevelFactory.Instance.GetItemPosition(4,1), Color.White)},
                {Tuple.Create(LevelFactory.Instance.GetItemPosition(5,1), Color.CornflowerBlue)}
            };
        }
        public Tuple<Vector2, Color> GetInfo(int player)
        {
            return Info[player];
        }
    }
}
