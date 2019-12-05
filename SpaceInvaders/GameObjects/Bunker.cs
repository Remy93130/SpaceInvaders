using SpaceInvaders.Utils;
using System.Drawing;

namespace SpaceInvaders.GameObjects
{
    /// <summary>
    /// Neutral object where the player can hide
    /// </summary>
    class Bunker : SimpleObject
    {
        #region Constructors

        /// <summary>
        /// Public constructor calling base constructor
        /// </summary>
        /// <param name="position">The bunker's position</param>
        public Bunker(Vecteur2d position) : base(position, 1, Properties.Resources.bunker, Side.Neutral)
        {
        }

        #endregion

        #region Methods

        public override void Update(Game gameInstance, double deltaT)
        {
        }

        protected override void OnCollision(Missile m, Vecteur2d collisionPoint)
        {
            Image.SetPixel((int)collisionPoint.X, (int)collisionPoint.Y, Color.FromArgb(0, 255, 255, 255));
            m.Lives--;
        }

        #endregion
    }
}
