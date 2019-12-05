using System;

namespace SpaceInvaders.Utils
{
    class Vecteur2d
    {
        public double X { get; }

        public double Y { get; }

        public Vecteur2d(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        public double Norm() => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));

        public override bool Equals(object obj) => obj is Vecteur2d d && X == d.X &&  Y == d.Y;

        public override int GetHashCode()
        {
            var hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public static Vecteur2d operator +(Vecteur2d v1, Vecteur2d v2) => new Vecteur2d(v1.X + v2.X, v1.Y + v2.Y);

        public static Vecteur2d operator -(Vecteur2d v1, Vecteur2d v2) => new Vecteur2d(v1.X - v2.X, v1.Y - v2.Y);

        public static Vecteur2d operator *(Vecteur2d v, double k) => new Vecteur2d(v.X * k, v.Y * k);

        public static Vecteur2d operator *(double k, Vecteur2d v) => new Vecteur2d(v.X * k, v.Y * k);

        public static Vecteur2d operator -(Vecteur2d v) => new Vecteur2d(-v.X, -v.Y);

        public static Vecteur2d operator /(Vecteur2d v, double k) => new Vecteur2d(v.X / k, v.Y / k);
    }
}
