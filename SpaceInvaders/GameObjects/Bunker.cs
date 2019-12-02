using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects
{
    class Bunker : SimpleObject
    {
        public Bunker(Vecteur2d position) : base(position, 1, Properties.Resources.bunker)
        {
        }

        public override void Update(Game gameInstance, double deltaT)
        {
        }

        public override void Collision(Missile m)
        {
            if (NotInSameArea(m)) return;
            PixelByPixel(m);
        }

        private void PixelByPixel(Missile m)
        {
            int touched = 0;
            for (int i = 0; i < m.Image.Height; i++)
            {
                if (!m.IsAlive()) return;
                for (int j = 0; j < m.Image.Width; j++)
                {
                    var missile = new Vecteur2d(j, i);
                    var inter = new Vecteur2d((m.Position.X - m.Image.Width / 2) - (Position.X - Image.Width / 2) + missile.X, (m.Position.Y - m.Image.Width / 2) - (Position.Y - Image.Width / 2) + missile.Y);
                    if (inter.X < Image.Width && inter.Y < Image.Height && inter.X > 0 && inter.Y > 0)
                        if (Image.GetPixel((int)inter.X, (int)inter.Y) == Color.FromArgb(255, 0, 0, 0))
                            touched++;
                }
            }
        }
    }
}
