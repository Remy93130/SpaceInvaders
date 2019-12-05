using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SpaceInvaders.GameObjects
{
    class EnemyBlock : GameObject
    {
        public HashSet<SpaceShip> EnemyShips { get; }

        private readonly int baseWidth;

        private double randomShootProbability;

        private static readonly Random rng = new Random();

        private Vecteur2d vectorX = new Vecteur2d(.25, 0);

        private readonly Vecteur2d vectorY = new Vecteur2d(0, 7.5);

        Size size;

        public EnemyBlock(Vecteur2d position, int baseWidth, Side side) : base(position, side)
        {
            EnemyShips = new HashSet<SpaceShip>();
            this.baseWidth = baseWidth;
            this.randomShootProbability = .05;
        }

        public override void Collision(Missile m)
        {
            foreach (var ship in EnemyShips)
            {
                ship.Collision(m);
            }
            int oldLen = EnemyShips.Count;
            EnemyShips.RemoveWhere(ship => ship.Lives <= 0);
            if (oldLen != EnemyShips.Count) UpdateSize();
        }

        // public override void Draw(Game gameInstance, Graphics graphics) => enemyShips.ToList().ForEach(enemy => enemy.Draw(gameInstance, graphics));

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            EnemyShips.ToList().ForEach(enemy => enemy.Draw(gameInstance, graphics));
            // graphics.DrawRectangle(new Pen(Game.blackBrush), (float)Position.X, (float)Position.Y, size.Width, 100);
        }

        public override bool IsAlive() => (EnemyShips.Count(_ship => _ship.IsAlive()) > 0)? true : false;

        public override void Update(Game gameInstance, double deltaT)
        {
            if (Position.X + size.Width + vectorX.X > gameInstance.gameSize.Width || Position.X + vectorX.X < 0)
            {
                Position += vectorY;
                vectorX -= vectorX * 2.03;
                randomShootProbability += .015;
                EnemyShips.ToList().ForEach(enemy => enemy.Position += vectorY);
                EnemyShips.ToList().ForEach(enemy => enemy.Position -= vectorX);
            }
            else
            {
                Position += vectorX;
                EnemyShips.ToList().ForEach(enemy => enemy.Position += vectorX);
            }
            foreach (var ship in EnemyShips)
            {
                double r = rng.NextDouble();
                if (r <= randomShootProbability * deltaT) ship.Shoot(gameInstance);
                ship.Lives = (ship.Position.Y + ship.Image.Height > gameInstance.gameSize.Height) ? 0 : ship.Lives;
            }
        }

        public void AddLine(int nbShips, int nbLives, Bitmap shipImage)
        {
            float offsetY = shipImage.Height + (size.Height * 40);
            float offsetX = (baseWidth - nbShips * shipImage.Width) / (nbShips + 1);
            for (int i = 0; i < nbShips; i++)
            {
                var vector = new Vecteur2d((i * shipImage.Width) + offsetX * (i + 1), offsetY);
                EnemyShips.Add(new SpaceShip(vector, nbLives, shipImage, Side.Enemy));
            }
            size.Height++;
        }

        public void UpdateSize()
        {
            double minLenX = Double.MaxValue;
            double maxLenX = Double.MinValue;
            double image_len = 0;
            foreach(var ship in EnemyShips)
            {
                if (ship.Position.X > maxLenX)
                {
                    maxLenX = ship.Position.X;
                    image_len = ship.Image.Width;
                }
                if (ship.Position.X < minLenX) minLenX = ship.Position.X;
            }
            Position = new Vecteur2d(minLenX, Position.Y);
            size.Width = (int)maxLenX + (int)image_len - (int)Position.X;
        }
    }
}
