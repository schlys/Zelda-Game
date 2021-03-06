/*
 * Created by Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh.
 * Class: CSE 3902 AU21
 */

using Microsoft.Xna.Framework;

namespace Project1.CollisionComponents
{
    public interface ICollidable
    {
        Rectangle Hitbox { get; set; }  
        bool IsMoving { get; set; }     // True if the hitbox or location of the hitbox ever changes  
        string TypeID { get; set; }
    }
}
