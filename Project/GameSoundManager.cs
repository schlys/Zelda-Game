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
        {
            Game = game;
            CreateDict(game);
            song = game.Content.Load<Song>("Sounds/Song");
        }

        private void CreateDict(Game1 game)
        {
            SoundDict = new Dictionary<string, SoundEffectInstance>();      // Key is name of song, Value is the SoundEffectInstance
            XmlDocument XMLData = new XmlDocument();                        // Sound effect list stored in XMLSounds.xml
            var path = AppDomain.CurrentDomain.BaseDirectory + "XMLData/XMLSounds.xml";
            XMLData.Load(path);
            XmlNodeList Sounds = XMLData.SelectNodes("Sounds/Sound");       // Select each sound node in XMLSounds.xml

            foreach (XmlNode node in Sounds)    // Iterate through each sound
            {
                String name = node.SelectSingleNode("Name").InnerText;                          // Read sound name from XML
                bool loop = node.SelectSingleNode("Loop").InnerText.Equals("1");                // Read if sound is looped or not
                SoundEffect soundEffect = game.Content.Load<SoundEffect>("Sounds/" + name);     // Create the sound effect
                SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();         // Create an instance of the sound effect
                soundEffectInstance.IsLooped = loop;                                            // Set the value of IsLooped
                SoundDict.Add(name, soundEffectInstance);                                       // Add instance to dictionary
            }
        }

        public void PlaySong()
        {
            MediaPlayer.Play(song);             // Play background song
            MediaPlayer.IsRepeating = true;     // Loop the song
            MediaPlayer.Volume = 0.5f;          // 0.0f is silent, 1.0f is full volume
        }

        // Methods for specific sound effects
        public void PlayArrowBoomerang()
        {
            SoundDict["ArrowBoomerang"].Play();
        }
        public void PlayBombBlow()
        {
            SoundDict["BombBlow"].Play();
        }
        public void PlayBombDrop()
        {
            SoundDict["BombDrop"].Play();
        }
        public void PlayBossScream1()
        {
            SoundDict["BossScream1"].Play();
        }
        public void PlayCandle()
        {
            SoundDict["Candle"].Play();
        }
        public void PlayDoorUnlock()
        {
            SoundDict["DoorUnlock"].Play();
        }
        public void PlayEnemyDie()
        {
            SoundDict["EnemyDie"].Play();
        }
        public void PlayEnemyHit()
        {
            SoundDict["EnemyHit"].Play();
        }
        public void PlayFanfare()
        {
            SoundDict["Fanfare"].Play();
        }
        public void PlayGetHeart()
        {
            SoundDict["GetHeart"].Play();
        }
        public void PlayGetItem()
        {
            SoundDict["GetItem"].Play();
        }
        public void PlayGetRupee()
        {
            SoundDict["GetRupee"].Play();
        }
        public void PlayKeyAppear()
        {
            SoundDict["KeyAppear"].Play();
        }
        public void PlayLinkDie()
        {
            SoundDict["LinkDie"].Play();
        }
        public void PlayLinkHurt()
        {
            SoundDict["LinkHurt"].Play();
        }
        public void PlayLowHealth()
        {
            SoundDict["LowHealth"].Play();
        }
        public void PlayMagicalRod()
        {
            SoundDict["MagicalRod"].Play();
        }
        public void PlayRecorder()
        {
            SoundDict["Recorder"].Play();
        }
        public void PlayRefillLoop()
        {
            SoundDict["RefillLoop"].Play();
        }
        public void PlaySecret()
        {
            SoundDict["Secret"].Play();
        }
        public void PlayShield()
        {
            SoundDict["Shield"].Play();
        }
        public void PlayStairs()
        {
            SoundDict["Stairs"].Play();
        }
        public void PlaySwordCombined()
        {
            SoundDict["Sword"].Play();
        }
        public void PlaySwordShoot()
        {
            SoundDict["SwordShoot"].Play();
        }
        public void PlaySwordSlash()
        {
            SoundDict["SwordSlash"].Play();
        }
        public void PlayText()
        {
            SoundDict["Text"].Play();
        }
        public void PlayTextSlow()
        {
            SoundDict["TextSlow"].Play();
        }
    }
}
