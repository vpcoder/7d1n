using System;
using System.Collections.Generic;

namespace Engine.EGUI {

	/// <summary>
	/// Интерфейс переключателя группы фейдеров (скрывающихся панелей)
	/// </summary>
	public interface IFaderSwitcher {

		/// <summary>
		/// Добавляет фейдер в список переключателя
		/// </summary>
		/// <param name="fader">Фейдер, который необходимо добавить</param>
		void AddFader(IFader fader);

		/// <summary>
		/// Добавляет все фейдеры в список переключателя
		/// </summary>
		/// <param name="faders">Фейдеры, который необходимо добавить</param>
		void AddFaders(IEnumerable<IFader> faders);

		/// <summary>
		/// Удаляет фейдер из списка переключателя
		/// </summary>
		/// <param name="fader">Фейдер который необходимо удалить</param>
		void RemoveFader(IFader fader);


		/// <summary>
		/// Прячет все фейдеры, кроме exclude
		/// </summary>
		/// <param name="exclude">Указанный фейдер не будет спрятан</param>
		void OnHide(IFader exclude);

		/// <summary>
		/// Отображает выбранный фейдер, и прячет остальные фейдеры в переключателе
		/// </summary>
		/// <param name="fader">Фейдер, который надо отобразить</param>
		void OnSwitch(IFader fader);

	}

}
