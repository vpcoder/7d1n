
using System;

namespace Engine.Data
{

    public class Game
    {

        #region Singleton

        private static readonly Lazy<Game> instance = new Lazy<Game>(() => new Game());
        public static Game Instance { get { return instance.Value; } }
        private Game() { }

        #endregion

        /// <summary>
        ///     Хранит информацию о сборке
        ///     ---
        ///     Stores information about the assembly
        /// </summary>
        public Buildtime Buildtime = new Buildtime();

        /// <summary>
        ///     Хранит информацию о персонаже
        ///     ---
        ///     Stores information about the character
        /// </summary>
        public Character Character = new Character();

        /// <summary>
        ///     Контекст рантайма (хранит информацию исключительно для работы приложения)
        ///     ---
        ///     Runtime context (stores information solely for the operation of the application)
        /// </summary>
        public Runtime Runtime = new Runtime();

    }

}
