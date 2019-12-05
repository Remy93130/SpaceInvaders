using SpaceInvaders.Utils;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    /// <summary>
    /// Just a missile go all right until touch something or leave window
    /// </summary>
    class Missile : SimpleObject
    {
        #region Properties

        /// <summary>
        /// The missile's speed
        /// </summary>
        private readonly double speed = 1.5;

        #endregion

        #region Constructors

        /// <summary>
        /// Another constructor calling just the abstract constructor
        /// </summary>
        /// <param name="position">Missile base position</param>
        /// <param name="lives">Number of lives</param>
        /// <param name="image">Missile's image</param>
        /// <param name="side">Side of the missile</param>
        public Missile(Vecteur2d position, int lives, Bitmap image, Side side) : base(position, lives, image, side)
        {
        }

        #endregion

        #region Methods

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

        #endregion
    }
}
