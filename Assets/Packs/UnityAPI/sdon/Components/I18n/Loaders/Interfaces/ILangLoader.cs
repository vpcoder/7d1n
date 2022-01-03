using System.Collections.Generic;

namespace Engine.I18N {

	/// <summary>
	/// Интерфейс класса-загрузчика языковых данных
	/// </summary>
	public interface ILangLoader {

		/// <summary>
		/// Заполняет словарь data и список локализаций localizations
		/// </summary>
		/// <param name="data">Словарь с парами - keyword-перевод</param>
		/// <param name="localizations">Список доступных локализаций</param>
		void GetData(ref SortedDictionary<string,string> data, ref List<string> localizations);

	}

}