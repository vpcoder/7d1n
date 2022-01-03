using System;

namespace Engine.Config {

	/// <summary>
	/// Абстрактный класс чтения/записи конфигураций из файла
	/// </summary>
	public abstract class DefaultAbstractConfigReader : IConfigReader {

		/// <summary>
		/// Устанавливает конфигурации по умолчанию и сохраняет их в файл
		/// </summary>
		/// <param name="configFile">Выходной файл с конфигурациями</param>
		/// <param name="settings">Выходные конфигурации</param>
		public virtual void DefaultConfig(string configFile, ref SettingsData settings) {

			settings.SetValue(FltSettingsProprety.SoundsVolume, 80.0f);
			settings.SetValue(FltSettingsProprety.MusicsVolume, 70.0f);
			settings.SetValue(BoolSettingsProprety.Mute, false);
			settings.SetValue(StrSettingsProprety.Localization, "ru");

			SaveConfig(configFile, settings);
		}

		/// <summary>
		/// Читает конфигурации из файла в settings
		/// </summary>
		/// <param name="configFile">Входной файл с конфигурациями</param>
		/// <param name="settings">Выходной класс с конфигурациями (не должен быть null)</param>
		public abstract void ReadConfig(string configFile, ref SettingsData settings);

		/// <summary>
		/// Сохраняет конфигурации settings в файл
		/// </summary>
		/// <param name="configFile">Выходной файл с конфигурациями</param>
		/// <param name="settings">Входные конфигурации</param>
		public abstract void SaveConfig(string configFile, SettingsData settings);

	}

}
