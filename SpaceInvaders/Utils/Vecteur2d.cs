using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Utils
{
    class Vecteur2d
    {
        private readonly double x;

        private readonly double y;

        public double X => x;

        public double Y => y;

        public Vecteur2d(double x = 0, double y = 0)
        {
            this.x = x;
            this.y = y;
        }

        public double Norm() => Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

        public override bool Equals(object obj)
        {
            return obj is Vecteur2d d &&
                   x == d.x &&
                   y == d.y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }

        public static Vecteur2d operator +(Vecteur2d v1, Vecteur2d v2) => new Vecteur2d(v1.x + v2.x, v1.y + v2.y);

        public static Vecteur2d operator -(Vecteur2d v1, Vecteur2d v2) => new Vecteur2d(v1.x - v2.x, v1.y - v2.y);

        public static Vecteur2d operator *(Vecteur2d v, double k) => new Vecteur2d(v.x * k, v.y * k);

        public static Vecteur2d operator *(double k, Vecteur2d v) => new Vecteur2d(v.x * k, v.y * k);

        public static Vecteur2d operator -(Vecteur2d v) => new Vecteur2d(-v.x, -v.y);

        public static Vecteur2d operator /(Vecteur2d v, double k) => new Vecteur2d(v.x / k, v.y / k);
    }
}
