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
            graphics.DrawRectangle(new Pen(Game.blackBrush), (float)Position.X, (float)Position.Y, Image.Width, Image.Height);
        }



        public override bool IsAlive() => Lives > 0;

        protected bool NotInSameArea(Missile m) => 
            m.Position.X > Position.X + image.Width || 
            m.Position.Y > Position.Y + image.Height || 
            Position.X > m.Position.X + m.image.Width || 
            Position.Y > m.Position.Y + m.image.Height;

        protected abstract void OnCollision(Missile m, Vecteur2d collisionPoint);

        public override void Collision(Missile m)
        {
            if (NotInSameArea(m)) return;
            PixelByPixel(m);
        }

        private void PixelByPixel(Missile m)
        {
            for (int i = 0; i < m.Image.Height; i++)
            {
                for (int j = 0; j < m.Image.Width; j++)
                {
                    var inter = new Vecteur2d(j - Position.X + m.Position.X, i - Position.Y + m.Position.Y);
                    if (inter.X < Image.Width && inter.Y < Image.Height && inter.X > 0 && inter.Y > 0)
                        if (Image.GetPixel((int)inter.X, (int)inter.Y) == Color.FromArgb(255, 0, 0, 0)) OnCollision(m, inter);
                }
            }
        }
    }
}
