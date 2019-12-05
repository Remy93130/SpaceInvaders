using System.Drawing;
using SpaceInvaders.GameObjects;

namespace SpaceInvaders.Utils
{
    abstract class SimpleObject : GameObject
    {
        public int Lives { get; set; }

        public Bitmap Image { get; }

        protected SimpleObject(Vecteur2d position, int lives, Bitmap image, Side side) : base(position, side)
        {
            Lives = lives;
            Image = new Bitmap(image);
        }

        public override void Draw(Game gameInstance, Graphics graphics) => graphics.DrawImage(Image, (float)Position.X, (float)Position.Y, Image.Width, Image.Height);

        public override bool IsAlive() => Lives > 0;

        protected bool NotInSameArea(Missile m) => 
            m.Position.X > Position.X + Image.Width || 
            m.Position.Y > Position.Y + Image.Height || 
            Position.X > m.Position.X + m.Image.Width || 
            Position.Y > m.Position.Y + m.Image.Height;

        protected abstract void OnCollision(Missile m, Vecteur2d collisionPoint);

        public override void Collision(Missile m)
        {
            if (NotInSameArea(m) || m.Side.Equals(Side)) return;
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
