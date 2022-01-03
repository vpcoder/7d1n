
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
        /// Хранит информацию о сборке
        /// </summary>
        public Buildtime Buildtime = new Buildtime();

        /// <summary>
        /// Хранит информацию о персонаже
        /// </summary>
        public Character Character = new Character();

        /// <summary>
        /// Контекст рантайма (хранит информацию исключительно для работы приложения)
        /// </summary>
        public Runtime Runtime = new Runtime();

    }

}
