﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using Project1.SpriteFactoryComponents;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Project1.EnemyComponents;
using System.Xml;
using System.IO;

namespace Project1.LevelFactory
{
    class LevelFactory : ILevelFactory
    {
        private static LevelFactory instance = new LevelFactory();
        public static LevelFactory Instance
        {
            get
            {
                return instance;
            }
        }
        private LevelFactory() { }
        private static Dictionary<string, Level> LevelDict;
        private static Dictionary<string, Texture2D> TextureDict;
        private static int[,] textureMatrix;


        public void LoadAllTextures(ContentManager content)
            {
            TextureDict.Add("room1", content.Load<Texture2D>("Rooms/Room1"));
            TextureDict.Add("room2", content.Load<Texture2D>("Rooms/Room2"));
            TextureDict.Add("room3", content.Load<Texture2D>("Rooms/Room3"));

        }
        private static void CreateDict()
        {
            textureMatrix = new int[7, 7];
            LevelDict = new Dictionary<string, Level>();

            XmlDocument XMLData = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLLevel.xml";
            XMLData.Load(path);
            XmlNodeList Sprites = XMLData.DocumentElement.SelectNodes("/Levels/Level");

            foreach (XmlNode node in Sprites)
            {
                 
        string name = node.SelectSingleNode("name").InnerText;
                string sheet = node.SelectSingleNode("sheet").InnerText;
                string Up = node.SelectSingleNode("Up").InnerText;
                string Down = node.SelectSingleNode("Down").InnerText;
                string Left = node.SelectSingleNode("Left").InnerText;
                string Right = node.SelectSingleNode("Right").InnerText;

                //LevelDict.Add(name, new Level(TextureDict[sheet], Up, Down, Left, Right, textureMatrix));
            }

        }
        
    }
}
