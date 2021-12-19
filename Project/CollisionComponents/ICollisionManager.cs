/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

namespace Project1.CollisionComponents
{
    interface ICollisionManager
    {
        static CollisionManager Instance { get; } 
        void DetectCollisions(); 
        void Update();
        void Reset(); 
    }
}
