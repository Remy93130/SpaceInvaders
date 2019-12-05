using SpaceInvaders.GameObjects;
using SpaceInvaders.Utils;
using System.Drawing;

namespace SpaceInvaders
{
    /// <summary>
    /// This is the generic abstact base class for any entity in the game
    /// </summary>
    abstract class GameObject
    {
        #region Properties

        /// <summary>
        /// The position of the object (X, Y)
        /// </summary>
        public Vecteur2d Position { get; set; }

        /// <summary>
        /// The side of the object (neutral, enemy or ally)
        /// </summary>
        public Side Side { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor of the object
        /// </summary>
        /// <param name="position">The position</param>
        /// <param name="side">The side</param>
        public GameObject(Vecteur2d position, Side side)
        {
            Side = side;
            Position = position;
        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Update the state of a game objet
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public abstract void Update(Game gameInstance, double deltaT);

        /// <summary>
        /// Render the game object
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="graphics">graphic object where to perform rendering</param>
        public abstract void Draw(Game gameInstance, Graphics graphics);

        /// <summary>
        /// Determines if object is alive. If false, the object will be removed automatically.
        /// </summary>
        /// <returns>Am I alive ?</returns>
        public abstract bool IsAlive();

        /// <summary>
        /// Determines how to manage collision with a missile
        /// </summary>
        /// <param name="m">The missile which touched the object</param>
        public abstract void Collision(Missile m);

        #endregion
    }
}
