using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SpaceInvaders.GameObjects
{
    /// <summary>
    /// Group of ennemy, they expected try to kill
    /// the player
    /// </summary>
    class EnemyBlock : GameObject
    {
        #region Properties

        /// <summary>
        /// Set of the different ennemy ship
        /// </summary>
        public HashSet<SpaceShip> EnemyShips { get; }

        /// <summary>
        /// The base width of the enemyblock
        /// </summary>
        private readonly int baseWidth;

        /// <summary>
        /// The probability for each enemy to shoot
        /// </summary>
        private double randomShootProbability;

        /// <summary>
        /// The rng to check if the enemy should shoot
        /// </summary>
        private static readonly Random rng = new Random();

        /// <summary>
        /// The horizontal displacement
        /// </summary>
        private Vecteur2d vectorX = new Vecteur2d(.25, 0);

        /// <summary>
        /// The vertical displacement
        /// </summary>
        private readonly Vecteur2d vectorY = new Vecteur2d(0, 7.5);

        /// <summary>
        /// The size of the ennemy block
        /// </summary>
        private Size size;

        #endregion

        #region Constructors

        /// <summary>
        /// The main constructor
        /// </summary>
        /// <param name="position">Starting position of the block</param>
        /// <param name="baseWidth">The original width</param>
        /// <param name="side">The side of the block</param>
        public EnemyBlock(Vecteur2d position, int baseWidth, Side side) : base(position, side)
        {
            EnemyShips = new HashSet<SpaceShip>();
            this.baseWidth = baseWidth;
            this.randomShootProbability = .05;
        }

        #endregion

        #region Methods

        public override void Collision(Missile m)
        {
            foreach (var ship in EnemyShips) ship.Collision(m);
            int oldLen = EnemyShips.Count;
            EnemyShips.RemoveWhere(ship => ship.Lives <= 0);
            if (oldLen != EnemyShips.Count) UpdateSize();
        }

        public override void Draw(Game gameInstance, Graphics graphics) => EnemyShips.ToList().ForEach(enemy => enemy.Draw(gameInstance, graphics));

        public override bool IsAlive() => (EnemyShips.Count(_ship => _ship.IsAlive()) > 0)? true : false;

        public override void Update(Game gameInstance, double deltaT)
        {
            // If the enemy block need to go down & reverse vectorX
            if (Position.X + size.Width + vectorX.X > gameInstance.gameSize.Width || Position.X + vectorX.X < 0)
            {
                Position += vectorY;
                vectorX -= vectorX * 2.03;
                randomShootProbability += .015;
                EnemyShips.ToList().ForEach(enemy => enemy.Position += vectorY);
            }
            Position += vectorX;
            EnemyShips.ToList().ForEach(enemy => enemy.Position += vectorX);
            
            // Fire in the hole
            foreach (var ship in EnemyShips)
            {
                if (rng.NextDouble() <= randomShootProbability * deltaT) ship.Shoot(gameInstance);
                ship.Lives = (ship.Position.Y + ship.Image.Height > gameInstance.gameSize.Height) ? 0 : ship.Lives;
            }
        }

        /// <summary>
        /// Add a line of ennemy in the enemy block
        /// Create a beautifull offset for a cool display
        /// </summary>
        /// <param name="nbShips">Number of ships to add</param>
        /// <param name="nbLives">Ship's lives</param>
        /// <param name="shipImage">Ship's image</param>
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

        /// <summary>
        /// Update the size of the enemy block for a correct mouvement
        /// when they are a column of ship destroyed
        /// </summary>
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

        #endregion
    }
}
