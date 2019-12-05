using SpaceInvaders.Utils;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    /// <summary>
    /// Common classe between player spaceship and enemy spaceship
    /// </summary>
    class SpaceShip : SimpleObject
    {
        #region Properties

        /// <summary>
        /// The spaceship's speed
        /// </summary>
        protected double Speed { get; }

        /// <summary>
        /// The missile assign to the spaceship
        /// </summary>
        protected Missile Missile { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position">Base position</param>
        /// <param name="lives">Number of lives</param>
        /// <param name="image">Image to draw</param>
        /// <param name="side">Side</param>
        public SpaceShip(Vecteur2d position, int lives, Bitmap image, Side side) : base(position, lives, image, side)
        {
            Speed = 1;
            Missile = null;
        }

        #endregion

        #region Methods

        public override void Update(Game gameInstance, double deltaT)
        {
        }

        /// <summary>
        /// Check if the last missile still alive else shoot a new one
        /// </summary>
        /// <param name="gameInstance"></param>
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

        #endregion
    }
}
