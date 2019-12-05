using SpaceInvaders.Utils;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    class SpaceShip : SimpleObject
    {

        protected double Speed { get; }

        protected Missile Missile { get; set; }


        public SpaceShip(Vecteur2d position, int lives, Bitmap image, Side side) : base(position, lives, image, side)
        {
            Speed = 1;
            Missile = null;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
        }

        public void Shoot(Game gameInstance)
        {
            if (Missile != null && Missile.IsAlive()) return;
            double positionY = (Side == Side.Ally) ? Position.Y - Image.Height / 2 : Position.Y + Image.Height;
            Missile = new Missile(new Vecteur2d(Position.X + Image.Width / 2, positionY), 10, Properties.Resources.shoot1, Side);
            gameInstance.AddNewGameObject(Missile);
        }

        protected override void OnCollision(Missile m, Vecteur2d collisionPoint)
        {
            if (m.IsAlive())
            {
                Lives--;
                m.Lives = 0;
            }
        }
    }
}
