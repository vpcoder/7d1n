using System;
using System.Collections.Generic;

namespace Engine.EGUI {

	/// <summary>
	/// Класс-переключатель. Позволяет переключать состояния группы слоёв (подобно поведению RadioGroup).
	/// </summary>
	public class UISwitcherFader : IFaderSwitcher {

		private List<IFader> faderList = new List<IFader>();

		public UISwitcherFader() {
			
		}

		/// <summary>
		/// Добавляет фейдер в список переключателя
		/// </summary>
		/// <param name="fader">Фейдер, который необходимо добавить</param>
		public void AddFader(IFader fader) {
			if (fader == null) {
				return;
			}
			faderList.Add(fader);
			fader.Switcher = this;
		}

		/// <summary>
		/// Добавляет все фейдеры в список переключателя
		/// </summary>
		/// <param name="faders">Фейдеры, который необходимо добавить</param>
		public void AddFaders(IEnumerable<IFader> faders) {
			if (faders == null) {
				return;
			}
			faderList.AddRange(faders);
			foreach (IFader fader in faders) {
				fader.Switcher = this;
			}
		}

		/// <summary>
		/// Удаляет фейдер из списка переключателя
		/// </summary>
		/// <param name="fader">Фейдер который необходимо удалить</param>
		public void RemoveFader(IFader fader) {
			if (fader == null) {
				return;
			}
			fader.Switcher = null;
			faderList.Remove(fader);
		}

		/// <summary>
		/// Отображает выбранный фейдер, и прячет остальные фейдеры в переключателе
		/// </summary>
		/// <param name="fader">Фейдер, который надо отобразить</param>
		public void OnSwitch(IFader fader) {
			OnHide(fader);
			if (fader != null) {
				fader.OnShow();
			}
		}

		/// <summary>
		/// Прячет все фейдеры, кроме exclude
		/// </summary>
		/// <param name="exclude">Указанный фейдер не будет спрятан</param>
		public void OnHide(IFader exclude = null) {
			if (exclude != null && exclude.Rect != null) {
				exclude.Rect.SetAsLastSibling();
			}
			foreach (IFader item in faderList) {
				if (item == exclude) {
					continue;
				}
				item.OnHide();
			}
		}

	}

}
