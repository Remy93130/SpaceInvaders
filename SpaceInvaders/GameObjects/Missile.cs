using SpaceInvaders.Utils;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    class Missile : SimpleObject
    {
        private readonly double speed = 1.5;

        public Missile(Vecteur2d position, int lives, Bitmap image, Side side) : base(position, lives, image, side)
        {
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            Position = (Side == Side.Ally)? Position -= new Vecteur2d(0, speed) : Position += new Vecteur2d(0, speed);
            Lives = (Position.Y < 0 || Position.Y > gameInstance.gameSize.Height) ? 0 : Lives;
            foreach (var stupidObject in gameInstance.gameObjects)
            {
                if (stupidObject.Equals(this)) continue;
                stupidObject.Collision(this);
            }
        }

        protected override void OnCollision(Missile m, Vecteur2d collisionPoint)
        {
            Lives = 0;
            m.Lives = 0;
        }
    }
}
