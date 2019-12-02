using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders.GameObjects
{
    class SpaceShip : SimpleObject
    {
        private double speed;

        private Missile missile;

        protected double Speed => speed;

        protected Missile Missile => missile;


        public SpaceShip(Vecteur2d position, int lives, Bitmap image) : base(position, lives, image)
        {
            speed = 1;
            missile = null;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
        }

        protected void Shoot(Game gameInstance)
        {
            missile = new Missile(new Vecteur2d(Position.X + Image.Width / 2, Position.Y - Image.Height / 2), 10, Properties.Resources.shoot1);
            gameInstance.AddNewGameObject(missile);
        }
    }
}
