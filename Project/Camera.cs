using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.LinkComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1
{
    public class Camera
    {
        private Matrix transform;
        public Matrix Transform
        {
            get { return transform; }
        }

        private Vector2 center;
        private Viewport Viewport;

        private float zoom = 1;

        public float X
        {
            get { return center.X; }
            set { center.X = value; }
        }

        public float Y
        {
            get { return center.Y; }
            set { center.Y = value; }
        }

        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; }
        }

        private bool IsCollision = true;
        
        public Camera(Viewport viewport)
        {
            Viewport = viewport;
        }
        
        public void Update(Vector2 position)
        {
            center = new Vector2(position.X, position.Y);
            if (IsCollision) // TODO: Make camera move when it occurs collision (Connect to collision)
            {
                transform = Matrix.CreateTranslation(new Vector3(-center.X - 300, -center.Y + 40, 0)) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 0)) *
                Matrix.CreateTranslation(new Vector3(Viewport.Width / 2, Viewport.Height / 2, 0));
                IsCollision = false; // You can check camermovement when remove it.
            }
            else
            {
                
            }
            
             /*
            transform = Matrix.CreateScale(new Vector3(1, 1, 0))*
                Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0)) *
                Matrix.CreateTranslation(new Vector3(Viewport.Width / 2, Viewport.Height / 2, 0));
            */
            }

        public void GetPosition(List<ILink> Links)
        {
            Vector2 pos = new Vector2(0, 0);

            foreach (ILink link in Links)
            {
                pos = link.Position;
                Update(pos);            
            }
                
            //Transform = Matrix.CreateTranslation(-link.Position.X-20, -link.Position.Y-20, 0)* 
            //Matrix.CreateTranslation(256, 176, 0);

           
        }
    }
}
