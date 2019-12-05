using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SpaceInvaders.GameObjects;
using SpaceInvaders.Utils;

namespace SpaceInvaders
{
    /// <summary>
    /// The main class with the game logic
    /// </summary>
    class Game
    {

        #region GameObjects management

        /// <summary>
        /// Set of all game objects currently in the game
        /// </summary>
        public HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Set of new game objects scheduled for addition to the game
        /// </summary>
        private readonly HashSet<GameObject> pendingNewGameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Schedule a new object for addition in the game.
        /// The new object will be added at the beginning of the next update loop
        /// </summary>
        /// <param name="gameObject">object to add</param>
        public void AddNewGameObject(GameObject gameObject) => pendingNewGameObjects.Add(gameObject);

        #endregion

        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;

        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        public SpaceShip playerShip;

        /// <summary>
        /// 
        /// </summary>
        private double bunkerPosition;

        /// <summary>
        /// The enemy block
        /// </summary>
        private EnemyBlock enemies;

        /// <summary>
        /// The state of the game
        /// </summary>
        private GameState state;

        #endregion

        #region static fields (helpers)

        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

        /// <summary>
        /// A shared black brush
        /// </summary>
        public static Brush blackBrush = new SolidBrush(Color.Black);

        /// <summary>
        /// A shared simple font
        /// </summary>
        public static Font defaultFont = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);

        #endregion

        #region Constructors

        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// <returns>Game instance</returns>
        public static Game CreateGame(Size gameSize)
        {
            if (game == null)
                game = new Game(gameSize);
            return game;
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private Game(Size gameSize) => InitGame(gameSize);

        /// <summary>
        /// Method to create a new game
        /// Used when the program is started or if the player
        /// want do one more party
        /// </summary>
        /// <param name="gameSize"></param>
        private void InitGame(Size gameSize)
        {
            state = GameState.Play;
            this.gameSize = gameSize;
            Bitmap image = Properties.Resources.ship3;
            int centerX = (gameSize.Width / 2) - (image.Width / 2);
            int downY = (gameSize.Height - 10) - (image.Height);
            CreateBunkers();
            CreateEnemies();
            playerShip = new PlayerSpaceShip(new Vecteur2d(centerX, downY), 3, image, Side.Ally);
            pendingNewGameObjects.Add(playerShip);
        }

        /// <summary>
        /// Method to create ennemies
        /// Add more if you dare
        /// </summary>
        private void CreateEnemies()
        {
            enemies = new EnemyBlock(new Vecteur2d(10, 10), gameSize.Width - 50, Side.Enemy);
            enemies.AddLine(6, 1, Properties.Resources.ship2);
            enemies.AddLine(6, 1, Properties.Resources.ship9);
            enemies.AddLine(6, 1, Properties.Resources.ship8);
            enemies.AddLine(3, 1, Properties.Resources.ship4);
            enemies.UpdateSize();
            AddNewGameObject(enemies);
        }

        /// <summary>
        /// Create bunkers with a beautifull offset
        /// </summary>
        private void CreateBunkers()
        {
            Bitmap bunker = Properties.Resources.bunker;
            float offset = (gameSize.Width - 3 * bunker.Width) / 4;
            bunkerPosition = gameSize.Width - 75 - bunker.Height;
            for (int i = 1; i < 4; i++)
                gameObjects.Add(new Bunker(new Vecteur2d(offset * i + bunker.Width * (i - 1), bunkerPosition)));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Force a given key to be ignored in following updates until the user
        /// explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        public void ReleaseKey(Keys key) => keyPressed.Remove(key);


        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {
            switch (state)
            {
                case GameState.Pause:
                    g.DrawString("Pause", defaultFont, blackBrush, (float)gameSize.Width / 2 - g.MeasureString("Pause", defaultFont).Width / 2, (float)gameSize.Height / 2);
                    break;
                case GameState.Lost:
                    g.DrawString("You lost", defaultFont, blackBrush, (float)gameSize.Width / 2 - g.MeasureString("You lost", defaultFont).Width / 2, (float)gameSize.Height / 2);
                    return;
                case GameState.Win:
                    g.DrawString("You win", defaultFont, blackBrush, (float)gameSize.Width / 2 - g.MeasureString("You win", defaultFont).Width / 2, (float)gameSize.Height / 2);
                    return;
            }
            foreach (GameObject gameObject in gameObjects) gameObject.Draw(this, g);      
        }

        /// <summary>
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {
            CheckEndGame();
            if (keyPressed.Contains(Keys.P)) SwitchState();
            if (state == GameState.Pause || state == GameState.Win || state == GameState.Lost) return;

            gameObjects.UnionWith(pendingNewGameObjects);
            pendingNewGameObjects.Clear();

            foreach (GameObject gameObject in gameObjects) gameObject.Update(this, deltaT);

            gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());
        }

        /// <summary>
        /// Put the game in pause and resume it
        /// </summary>
        private void SwitchState()
        {
            state = (state == GameState.Pause) ? GameState.Play : GameState.Pause;                
            ReleaseKey(Keys.P);
        }

        /// <summary>
        /// Check if the game is over and update the state
        /// Look if the ennemy block is in the bunkers line
        /// Look if still ennemy alive
        /// Look if player still alive
        /// </summary>
        private void CheckEndGame()
        {
            foreach (var ship in enemies.EnemyShips) if (ship.Position.Y >= bunkerPosition - 20) playerShip.Lives = 0;
            if (!enemies.IsAlive()) state = GameState.Win;
            if (!playerShip.IsAlive()) state = GameState.Lost;
            if (state != GameState.Pause && state != GameState.Play && keyPressed.Contains(Keys.Space))
            {
                gameObjects.Clear();
                InitGame(gameSize);
                ReleaseKey(Keys.Space);
            }
        }

        #endregion
    }
}
