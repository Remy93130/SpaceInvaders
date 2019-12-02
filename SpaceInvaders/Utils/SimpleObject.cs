using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SpaceInvaders.GameObjects;

namespace SpaceInvaders.Utils
{
    abstract class SimpleObject : GameObject
    {
        private int lives;

        private Bitmap image;

        public int Lives
        {
            get => lives;
            set => lives = value;
        }

        public Bitmap Image => image;

        protected SimpleObject(Vecteur2d position, int lives, Bitmap image) : base(position)
        {
            this.lives = lives;
            this.image = new Bitmap(image);
        }

        // public override void Draw(Game gameInstance, Graphics graphics) => graphics.DrawImage(Image, (float)Position.X, (float)Position.Y, Image.Width, Image.Height);

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(image, (float)Position.X, (float)Position.Y, image.Width, image.Height);
            // graphics.DrawRectangle(new Pen(Game.blackBrush), (float)Position.X, (float)Position.Y, Image.Width, Image.Height);
        }



        public override bool IsAlive() => Lives > 0;

        public override void Collision(Missile m)
        {
            return;
        }
        protected bool NotInSameArea(Missile m) => 
            m.Position.X > Position.X + image.Width || 
            m.Position.Y > Position.Y + image.Height || 
            Position.X > m.Position.X + m.image.Width || 
            Position.Y > m.Position.Y + m.image.Height;

        protected bool TouchPixelByPixel(Missile m)
        {
            
            return true;
        }
    }
}
