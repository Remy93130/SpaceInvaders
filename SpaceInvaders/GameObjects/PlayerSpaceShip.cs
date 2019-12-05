using SpaceInvaders.Utils;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders.GameObjects
{
    /// <summary>
    /// The player spaceship
    /// </summary>
    class PlayerSpaceShip : SpaceShip
    {
        #region Constructors

        /// <summary>
        /// Wow another empty constructor
        /// </summary>
        /// <param name="position">Initial position</param>
        /// <param name="lives">Number of lives</param>
        /// <param name="image">The image</param>
        /// <param name="side">Side</param>
        public PlayerSpaceShip(Vecteur2d position, int lives, Bitmap image, Side side) : base(position, lives, image, side)
        { }

        #endregion

        #region Methods

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

        #endregion
    }
}
