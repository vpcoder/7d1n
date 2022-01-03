using System;

namespace Engine.Config {

	/// <summary>
	/// Интерфейс читателя основных конфигураций
	/// </summary>
	public interface IConfigReader {

		/// <summary>
		/// Читает конфигурации из файла в settings
		/// </summary>
		/// <param name="configFile">Входной файл с конфигурациями</param>
		/// <param name="settings">Выходной класс с конфигурациями (не должен быть null)</param>
		void ReadConfig(string configFile, ref SettingsData settings);

		/// <summary>
		/// Сохраняет конфигурации settings в файл
		/// </summary>
		/// <param name="configFile">Выходной файл с конфигурациями</param>
		/// <param name="settings">Входные конфигурации</param>
		void SaveConfig(string configFile, SettingsData settings);

		/// <summary>
		/// Устанавливает конфигурации по умолчанию и сохраняет их в файл
		/// </summary>
		/// <param name="configFile">Выходной файл с конфигурациями</param>
		/// <param name="settings">Выходные конфигурации</param>
		void DefaultConfig(string configFile, ref SettingsData settings);

	}

}
