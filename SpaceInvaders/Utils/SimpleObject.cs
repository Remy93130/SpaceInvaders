using System.Drawing;
using SpaceInvaders.GameObjects;

namespace SpaceInvaders.Utils
{
    /// <summary>
    /// A game object but more complex
    /// </summary>
    abstract class SimpleObject : GameObject
    {
        #region Properties

        /// <summary>
        /// Number of lives available
        /// </summary>
        public int Lives { get; set; }

        /// <summary>
        /// The image to render
        /// </summary>
        public Bitmap Image { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// A Lovely abstract constructor to generate a simple object
        /// </summary>
        /// <param name="position">The position</param>
        /// <param name="lives">The number of lives</param>
        /// <param name="image">The image</param>
        /// <param name="side">The side</param>
        protected SimpleObject(Vecteur2d position, int lives, Bitmap image, Side side) : base(position, side)
        {
            Lives = lives;
            Image = new Bitmap(image);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Draw a simple object with the Image and the Position
        /// properties
        /// </summary>
        /// <param name="gameInstance">Singleton of game</param>
        /// <param name="graphics">Graphic object to draw</param>
        public override void Draw(Game gameInstance, Graphics graphics) => graphics.DrawImage(Image, (float)Position.X, (float)Position.Y, Image.Width, Image.Height);

        /// <summary>
        /// Check if the simple object still alive
        /// </summary>
        /// <returns>True of alive else false</returns>
        public override bool IsAlive() => Lives > 0;

        /// <summary>
        /// Check if the missile given in argument is in the same
        /// area with the current object
        /// </summary>
        /// <param name="m">The missile</param>
        /// <returns>True if not in the same area else false</returns>
        protected bool NotInSameArea(Missile m) => 
            m.Position.X > Position.X + Image.Width || 
            m.Position.Y > Position.Y + Image.Height || 
            Position.X > m.Position.X + m.Image.Width || 
            Position.Y > m.Position.Y + m.Image.Height;

        /// <summary>
        /// Check if the current object is in collision with a missile
        /// </summary>
        /// <param name="m">The missile</param>
        public override void Collision(Missile m)
        {
            if (NotInSameArea(m) || m.Side.Equals(Side)) return;
            PixelByPixel(m);
        }

        /// <summary>
        /// Method to detect if pixel from the missile touch an other pixel
        /// from the current object. If it's true so OnCollision method is called
        /// </summary>
        /// <param name="m">The missile with test collision</param>
        private void PixelByPixel(Missile m)
        {
            for (int i = 0; i < m.Image.Height; i++)
            {
                for (int j = 0; j < m.Image.Width; j++)
                {
                    var inter = new Vecteur2d(j - Position.X + m.Position.X, i - Position.Y + m.Position.Y);
                    if (inter.X < Image.Width && inter.Y < Image.Height && inter.X > 0 && inter.Y > 0)
                        if (Image.GetPixel((int)inter.X, (int)inter.Y) == Color.FromArgb(255, 0, 0, 0)) OnCollision(m, inter);
                }
            }
        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Method to manage the collision if this one appens.
        /// </summary>
        /// <param name="m">The missile in collision</param>
        /// <param name="collisionPoint">A vector with the position of the collision point</param>
        protected abstract void OnCollision(Missile m, Vecteur2d collisionPoint);

        #endregion
    }
}
