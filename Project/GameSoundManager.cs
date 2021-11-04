using System;
using System.Collections.Generic;
using System.Xml;
using Project1.BlockComponents;
using Project1.ItemComponents;
using Project1.EnemyComponents;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Project1.LevelComponents;

namespace Project1
{
    public class GameSoundManager
    {
        private static GameSoundManager instance = new GameSoundManager();
        public static GameSoundManager Instance
        {
            get
            {
                return instance;
            }
        }
        private GameSoundManager() { }

        private static Dictionary<string, SoundEffectInstance> SoundDict;
        private Song song;
        public Game1 Game; 

        public void Initialize(Game1 game)
        {/*
            Game = game;
            CreateDict(game);
            PlaySong(game);*/
        }
        private void PlaySong(Game1 game)
        {
            song = game.Content.Load<Song>("Sounds/Song");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;       // 0.0f is silent, 1.0f is full volume
        }
        private void CreateDict(Game1 game)
        {
            SoundDict = new Dictionary<string, SoundEffectInstance>();
            XmlDocument XMLData = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLData/XMLSounds.xml";
            XMLData.Load(path);
            XmlNodeList Sounds = XMLData.SelectNodes("Sounds/Sound");

            foreach (XmlNode node in Sounds)
            {
                String name = node.SelectSingleNode("Name").InnerText;
                bool loop = node.SelectSingleNode("Loop").InnerText.Equals("1");

                SoundEffect soundEffect = game.Content.Load<SoundEffect>("Sounds/" + name);     // Create the sound effect
                SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
                soundEffectInstance.IsLooped = loop;
                SoundDict.Add(name, soundEffectInstance);                                               // Add instance to dictionary
                System.Diagnostics.Debug.WriteLine("\n name: " + name);
                System.Diagnostics.Debug.WriteLine("\n bool: " + loop);
            }
        }

        public void PlaySound(string key)
        {/*
            if (SoundDict.ContainsKey(key))
            {
                SoundDict[key].Play();
            }
            else
            {
                throw new NotImplementedException();
            }*/
        }

        public void Reset()
        {
           
        }

    }
}
