using SpaceInvaders.Utils;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders.GameObjects
{
    class PlayerSpaceShip : SpaceShip
    {

        public PlayerSpaceShip(Vecteur2d position, int lives, Bitmap image, Side side) : base(position, lives, image, side)
        { }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            base.Draw(gameInstance, graphics);
            graphics.DrawString("Lives:" + Lives, Game.defaultFont, Game.blackBrush, 0, gameInstance.gameSize.Height - 30);
        }

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
            if (gameInstance.keyPressed.Contains(Keys.Space))
                Shoot(gameInstance);
        }
    }
}
