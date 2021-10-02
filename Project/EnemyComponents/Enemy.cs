using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactoryComponents;
using System;

namespace Project1.EnemyComponents
{
    class Enemy : IEnemy
    {
        // TODO: create a class that extends IEnemyState for each type of enemy - use EnemyStateMoblin as a model 
        // TODO: update the prevEnemy and nextEnemy methods to use a switch case that changes the EnemyState property in a cycle 
        // TODO: remove sprite logic from this class, should only rely on sprite class. sprite object for enemy is now in EnemyState property 
        // TODO OPTIONAL: in stopmoving, make stop moving when it stops - this does the opposite 
        
        public IEnemyState EnemyState { get; set; }
        public EnemyHealth Health { get; set; }
        
        public Vector2 Position { get; set; }
        public Vector2 InitialPosition { get; set; }
        
        private double Step = .1;
        private double counter = 0.0;

        // NOTE: for personal reference, remove before submission 
        private string[] EnemyTypes = { "Moblin" , "Keese", "Stalfos", "Aquamentus", "Gel", "Goriya", "OldMan"};

        public Enemy(Game1 game)
        {
            EnemyState = new EnemyStateMoblin(this);            // default type state is Moblin
            Health = new EnemyHealth(3, 3);                     // default health is 3 of 3 hearts 
            Position = new Vector2(400, 200);
            InitialPosition = Position;
        }
       
    

        // NOTE: not need to have enemies take damage 
        public void TakeDamage()
        {
            Health.DecreaseHealth(0.5);             // need determine value to decrease by  
        }

        public void PreviousEnemy()
        {
            ResetPosition();
            SetEnemyState((int)counter);
            IncrementCounter(Step);
        }

        public void NextEnemy()
        {
            ResetPosition();
            SetEnemyState((int)counter);
            IncrementCounter(Step);
        }
        public void SetEnemyState(int i)
        {
            switch (EnemyTypes[i])
            {
                case "Moblin":
                    EnemyState = new EnemyStateMoblin(this);
                    break;
                case "Stalfos":
                    EnemyState = new EnemyStateStalfos(this);
                    break;
                case "Keese":
                    EnemyState = new EnemyStateKeese(this);
                    break;
                case "Aquamentus":
                    EnemyState = new EnemyStateAquamentus(this);
                    break;
                case "Gel":
                    EnemyState = new EnemyStateGel(this);
                    break;
                case "Goriya":
                    EnemyState = new EnemyStateGoriya(this);
                    break;
                case "OldMan":
                    EnemyState = new EnemyStateOldMan(this);
                    break;
            }
        }
        public void IncrementCounter(double i)
        {
            counter += i;
            if (counter > (EnemyTypes.Length - Step / 2))
            {
                counter = 0;
            }
            else if (counter < -Step / 2)
            {
                counter = EnemyTypes.Length - 1;
            }
        }

        public void Reset()
        {
            ResetPosition();
            EnemyState = new EnemyStateMoblin(this);            // default type state is Moblin 
            Health = new EnemyHealth(3, 3);                  // default health is 3 of 3 hearts 

        }
        public void ResetPosition()
        {
            Position = InitialPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            EnemyState.Draw(spriteBatch, Position);
        }

        public void Update()
        {
            EnemyState.Update();
        }
    }
}