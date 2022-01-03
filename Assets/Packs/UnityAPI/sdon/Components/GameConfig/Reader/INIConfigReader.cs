using System;
using System.IO;
using UnityEngine;

namespace Engine.Config {

	/// <summary>
	/// Класс для чтения INI конфигураций из файловой системы
	/// </summary>
	public class INIConfigReader : DefaultAbstractConfigReader {

		/// <summary>
		/// Читает конфигурации из файла в settings
		/// </summary>
		/// <param name="configFile">Входной файл с конфигурациями</param>
		/// <param name="settings">Выходной класс с конфигурациями (не должен быть null)</param>
		/// <exception cref="System.ArgumentException">ArgumentException</exception>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.IO.IOException">IOException</exception>
		public override void ReadConfig(string configFile, ref SettingsData settings) {

			if (configFile == null) {
				throw new ArgumentNullException("constFile не может быть null!");
			}

			if (settings == null) {
				throw new ArgumentNullException("settings не может быть null!");
			}

			if(!File.Exists(configFile)){
				DefaultConfig(configFile, ref settings);
				return;
			}

			try {

				StreamReader reader = new StreamReader(Application.persistentDataPath + configFile);

					while (reader.EndOfStream) {

						string line = reader.ReadLine();

						if (line == null) {
							break;
						}

						if (line.IndexOf("=") == -1) {
							continue;
						}

						string[] parameter = line.Split('=');
						string   name      = parameter[0];

						try {

							if (Enums<FltSettingsProprety>.ContainsValue(name)) {
								settings.SetValue(Enums<FltSettingsProprety>.Parse(name), float.Parse(parameter[1]));
								continue;
							}

							if (Enums<FltSettingsProprety>.ContainsValue(name)) {
								settings.SetValue(Enums<StrSettingsProprety>.Parse(name), parameter[1]);
								continue;
							}

							if (Enums<FltSettingsProprety>.ContainsValue(name)) {
								settings.SetValue(Enums<BoolSettingsProprety>.Parse(name), bool.Parse(parameter[1]));
								continue;
							}

						} catch (FormatException) {
								
#if UNITY_EDITOR

									Debug.LogError("Чтение свойства '"+name+"' с ошибкой! Не удалось распознать значение '"+parameter[1]+"'!");

#endif
								
						}

					}

				reader.Close();
				reader = null;

			} catch (Exception e) {

				Debug.LogError("Не удалось прочитать конфигурации! ["+configFile+"]");
				foreach (object d in e.Data.Keys) {
					Debug.LogError(d.ToString() + ": " + e.Data[d].ToString());
				}
            }

		}

		/// <summary>
		/// Сохранение текущих конфигураций
		/// </summary>
		/// <param name="configFile">Выходной файл конфигураций</param>
		public override void SaveConfig(string configFile, SettingsData settings) {

			try {

				try {
					Directory.CreateDirectory(Application.persistentDataPath);
				} catch (Exception) { }

				StreamWriter writer = new StreamWriter(Application.persistentDataPath + configFile);

				writer.WriteLine("[Configurations]");

				foreach (StrSettingsProprety item in Enums<StrSettingsProprety>.GetValuesArray()) {
					writer.WriteLine(item.ToString() + "=" + settings.GetValue(item));
				}

				foreach (FltSettingsProprety item in Enums<FltSettingsProprety>.GetValuesArray()) {
					writer.WriteLine(item.ToString() + "=" + settings.GetValue(item));
				}

				foreach (BoolSettingsProprety item in Enums<BoolSettingsProprety>.GetValuesArray()) {
					writer.WriteLine(item.ToString() + "=" + settings.GetValue(item));
				}

				writer.WriteLine("[End]");

				writer.Close();
				writer.Dispose();
				writer = null;

			} catch (Exception e) {

				Debug.LogError("Не удалось создать конфигурации! [" + configFile + "]");
				Debug.LogException(e);

			}
			
		}

	}

}
