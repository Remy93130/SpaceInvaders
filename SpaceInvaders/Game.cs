using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using SpaceInvaders.GameObjects;
using SpaceInvaders.Utils;

namespace SpaceInvaders
{
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
        private HashSet<GameObject> pendingNewGameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Schedule a new object for addition in the game.
        /// The new object will be added at the beginning of the next update loop
        /// </summary>
        /// <param name="gameObject">object to add</param>
        public void AddNewGameObject(GameObject gameObject)
        {
            pendingNewGameObjects.Add(gameObject);
        }
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

        private EnemyBlock enemies;

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
        private static Font defaultFont = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);

        public GameState state;
        #endregion


        #region constructors
        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// 
        /// <returns></returns>
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
        private Game(Size gameSize)
        {
            state = GameState.Play;
            this.gameSize = gameSize;
            Bitmap image = Properties.Resources.ship3;
            int centerX = (gameSize.Width / 2) - (image.Width / 2);
            int downY = (gameSize.Height - 10) - (image.Height);
            CreateBunkers();
            CreateEnemies();
            playerShip = new PlayerSpaceShip(new Vecteur2d(centerX, downY), 3, image);
            pendingNewGameObjects.Add(playerShip);
        }

        private void CreateEnemies()
        {
            enemies = new EnemyBlock(new Vecteur2d(10, 10), gameSize.Width - 50);
            enemies.AddLine(3, 1, Properties.Resources.ship4);
            enemies.AddLine(6, 1, Properties.Resources.ship2);
            enemies.AddLine(6, 1, Properties.Resources.ship9);
            enemies.AddLine(6, 1, Properties.Resources.ship8);
            enemies.UpdateSize();
            AddNewGameObject(enemies);
        }

        private void CreateBunkers()
        {
            Bitmap bunker = Properties.Resources.bunker;
            float offset = (gameSize.Width - 3 * bunker.Width) / 4;
            for (int i = 1; i < 4; i++)
                gameObjects.Add(new Bunker(new Vecteur2d(offset * i + bunker.Width * (i - 1), gameSize.Width - 75 - bunker.Height)));
        }

        #endregion

        #region methods

        /// <summary>
        /// Force a given key to be ignored in following updates until the user
        /// explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        public void ReleaseKey(Keys key)
        {
            keyPressed.Remove(key);
        }


        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {
            if (state == GameState.Pause)
            {
                SizeF stringSize = g.MeasureString("Pause", defaultFont);
                g.DrawString("Pause", defaultFont, blackBrush, (float)gameSize.Width / 2 - stringSize.Width / 2, (float)gameSize.Height / 2);
            }
            foreach (GameObject gameObject in gameObjects)
                gameObject.Draw(this, g);       
        }

        /// <summary>
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {
            if (keyPressed.Contains(Keys.P)) ManageState();
            if (state == GameState.Pause) return;
            // add new game objects
            gameObjects.UnionWith(pendingNewGameObjects);
            pendingNewGameObjects.Clear();

            // update each game object
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(this, deltaT);
            }

            // remove dead objects
            gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());
        }

        private void ManageState()
        {
            state = (state == GameState.Pause) ? GameState.Play : GameState.Pause;                
            ReleaseKey(Keys.P);
        }
        #endregion
    }
}
