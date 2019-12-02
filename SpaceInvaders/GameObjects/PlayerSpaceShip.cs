using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders.GameObjects
{
    class PlayerSpaceShip : SpaceShip
    {

        public PlayerSpaceShip(Vecteur2d position, int lives, Bitmap image) : base(position, lives, image)
        { }

        public override void Update(Game gameInstance, double deltaT)
        {
            if (gameInstance.keyPressed.Contains(Keys.Right))
            {
                var oldPosition = Position;
                Position += new Vecteur2d(Speed);
                if (Position.X > gameInstance.gameSize.Width - Image.Width) Position = oldPosition;
            }
            else if (gameInstance.keyPressed.Contains(Keys.Left))
            {
                var oldPosition = Position;
                Position -= new Vecteur2d(Speed);
                if (Position.X < 0) Position = oldPosition;
            }
            if (gameInstance.keyPressed.Contains(Keys.Space) && Missile == null || gameInstance.keyPressed.Contains(Keys.Space) && !Missile.IsAlive())
                Shoot(gameInstance);
        }
    }
}
