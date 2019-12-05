using SpaceInvaders.Utils;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    class Bunker : SimpleObject
    {
        public Bunker(Vecteur2d position) : base(position, 1, Properties.Resources.bunker, Side.Neutral)
        {
        }

        public override void Update(Game gameInstance, double deltaT)
        {
        }

        protected override void OnCollision(Missile m, Vecteur2d collisionPoint)
        {
            Image.SetPixel((int)collisionPoint.X, (int)collisionPoint.Y, Color.FromArgb(0, 255, 255, 255));
            m.Lives--;
        }
    }
}
