using System;

namespace UnityEditor {

	/// <summary>
	/// Перечисление доступных иконок для редактора
	/// </summary>
	public enum Icons : int {

		Empty         = 0x00,

		Delete        = 0x01,
		Add           = 0x02,
		Remove        = 0x03,

		Edit          = 0x04,
		EditOff       =-0x04,

		Save          = 0x05,
		SaveOff       =-0x05,

		Open          = 0x06,
		Refresh       = 0x07,

		ClipboardCopy = 0x08,

		Info          = 0x09,
		InfoOff       =-0x09,

		Error         = 0xff

	};

}
