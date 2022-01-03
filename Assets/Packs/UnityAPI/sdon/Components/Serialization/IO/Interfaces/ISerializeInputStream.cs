using System;

namespace Engine.Serialization.IO {

	/// <summary>
	/// Интерфейс чтения потока
	/// </summary>
	public interface ISerializeInputStream<T> {

		/// <summary>
		/// Открывает поток чтения
		/// </summary>
		/// <param name="url">Путь к потоку</param>
		void open(string url);

		/// <summary>
		/// Читает следующий элемент
		/// </summary>
		/// <returns>Возвращает прочитанный элемент</returns>
		T read();

		/// <summary>
		/// Закрывает поток
		/// </summary>
		void close();

	}

}
