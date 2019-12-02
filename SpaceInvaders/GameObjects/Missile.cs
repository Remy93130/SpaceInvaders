using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.GameObjects
{
    class Missile : SimpleObject
    {
        private double speed;

        public Missile(Vecteur2d position, int lives, Bitmap image) : base(position, lives, image)
        {
            speed = .99;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            Position -= new Vecteur2d(0, speed);
            Lives = (Position.Y < 0 || Position.Y > gameInstance.gameSize.Height) ? 0 : Lives;
            foreach (var stupidObject in gameInstance.gameObjects)
            {
                if (stupidObject.Equals(this)) continue;
                stupidObject.Collision(this);
            }
        }
    }
}
