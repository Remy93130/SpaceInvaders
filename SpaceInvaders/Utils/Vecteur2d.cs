using System;

namespace SpaceInvaders.Utils
{
    /// <summary>
    /// A simple vector with 2 point X and Y
    /// </summary>
    class Vecteur2d
    {
        #region Properties

        /// <summary>
        /// The X coord
        /// </summary>
        public double X { get; }

        /// <summary>
        /// The Y coord
        /// </summary>
        public double Y { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor to initialize the vector
        /// without arguments the constructor return
        /// a vector with origin as point
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vecteur2d(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Return the norm of the vector
        /// </summary>
        /// <returns>The norm of the vector (double)</returns>
        public double Norm() => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));

        public override bool Equals(object obj) => obj is Vecteur2d d && X == d.X &&  Y == d.Y;

        public override int GetHashCode()
        {
            var hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Addition between two vector
        /// </summary>
        /// <param name="v1">The first vector</param>
        /// <param name="v2">The second vector</param>
        /// <returns>The new vector</returns>
        public static Vecteur2d operator +(Vecteur2d v1, Vecteur2d v2) => new Vecteur2d(v1.X + v2.X, v1.Y + v2.Y);

        /// <summary>
        /// Subtraction between two vector
        /// </summary>
        /// <param name="v1">The first vector</param>
        /// <param name="v2">The second vector</param>
        /// <returns>The new vector</returns>
        public static Vecteur2d operator -(Vecteur2d v1, Vecteur2d v2) => new Vecteur2d(v1.X - v2.X, v1.Y - v2.Y);

        /// <summary>
        /// A factor between a vector and a scalar
        /// </summary>
        /// <param name="v">The vector</param>
        /// <param name="k">The scalar</param>
        /// <returns>The new vector</returns>
        public static Vecteur2d operator *(Vecteur2d v, double k) => new Vecteur2d(v.X * k, v.Y * k);

        /// <summary>
        /// A factor between a scalar and a vector
        /// </summary>
        /// <param name="k">The scalar</param>
        /// <param name="v">The vector</param>
        /// <returns>The new vector</returns>
        public static Vecteur2d operator *(double k, Vecteur2d v) => new Vecteur2d(v.X * k, v.Y * k);

        /// <summary>
        /// Unary subtraction
        /// </summary>
        /// <param name="v">The vector</param>
        /// <returns>The new vector</returns>
        public static Vecteur2d operator -(Vecteur2d v) => new Vecteur2d(-v.X, -v.Y);

        /// <summary>
        /// A division between a vector and a scalar
        /// </summary>
        /// <param name="v">The vector</param>
        /// <param name="k">The scalar</param>
        /// <returns>The new vector</returns>
        public static Vecteur2d operator /(Vecteur2d v, double k) => new Vecteur2d(v.X / k, v.Y / k);

        #endregion
    }
}
