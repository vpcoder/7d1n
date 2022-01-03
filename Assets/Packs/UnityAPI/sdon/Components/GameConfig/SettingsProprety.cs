using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Config {

	/// <summary>
	/// Список настроек со значениями вещественного типа
	/// </summary>
	public enum FltSettingsProprety : int {

		SoundsVolume = 0x00,
		MusicsVolume = 0x01

	};

	/// <summary>
	/// Список настроек со значениями строкового типа
	/// </summary>
	public enum StrSettingsProprety : int {

		Localization = 0x00

	};

	/// <summary>
	/// Список настроек со значениями логического типа
	/// </summary>
	public enum BoolSettingsProprety : int {

		Mute = 0x00

	};

}
