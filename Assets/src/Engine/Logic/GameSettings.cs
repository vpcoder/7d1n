using Engine.DB;
using System;

namespace Engine
{

    public class GameSettings
    {

        #region Singleton

        private static readonly Lazy<GameSettings> instance = new Lazy<GameSettings>(() => new GameSettings());
        public static GameSettings Instance { get { return instance.Value; } }

        #endregion

        private Settings settings;
        public Settings Settings { get { return settings; } }

        public GameSettings()
        {
            LoadSettings();
        }

        public void LoadSettings()
        {
            Db.Instance.Do(connection =>
            {
                settings = connection.QueryFirst<Settings>(0);
            });
        }

        public void SaveSettings()
        {
            Db.Instance.Do(connection =>
            {
                connection.Update(settings);
            });
        }

    }

}
