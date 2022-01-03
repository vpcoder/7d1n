using UnityEngine;
using System.Collections.Generic;

namespace Engine.Config {

	/// <summary>
	/// Класс конфигураций приложения/игры
	/// </summary>
	public class GameConfig {

		#region Hidden Fields

		/// <summary>
		/// Хранит текущий режим
		/// </summary>
		private static int          gameMode = GameModes.MODE_GAME;

		/// <summary>
		/// Хранит текущие конфигурации
		/// </summary>
		private static SettingsData settings = new SettingsData();

		#endregion

		/// <summary>
		/// Статичный конструктор. Инициализирует конфигурации.
		/// </summary>
		static GameConfig(){
			INIConfigReader reader = new INIConfigReader();
			reader.ReadConfig(Dictionary.CONFIG_FILE_PATH, ref settings);
		}

		#region Properties

		/// <summary>
		/// Устанавливает/Возвращает конфигурации
		/// </summary>
		public static SettingsData Settings {
			get{
				return settings;
			}
			set {
				settings = value;
			}
		}

		/// <summary>
		/// Устанавливает/Возвращает текущий режим
		/// </summary>
		public static int GameMode {
			get {
				return gameMode;
			}
			set {

				gameMode = value;

				switch (gameMode) {
					case GameModes.MODE_GAME:
						Cursor.visible = false;
						break;
					case GameModes.MODE_GUI:
						Cursor.visible = true;
						break;
				}

			}

		}

		#endregion

	}

}
