using System;

namespace Engine.Serialization.IO {

	/// <summary>
	/// Интерфейс записи в поток
	/// </summary>
	public interface ISerializeOutputStream<T> {

		/// <summary>
		/// Открывает поток записи
		/// </summary>
		/// <param name="url">Путь к потоку</param>
		void open(string url);

		/// <summary>
		/// Записывает следующий элемент
		/// </summary>
		/// <param name="item">Записываемый элемент</param>
		void write(T item);

		/// <summary>
		/// Закрывает поток
		/// </summary>
		void close();

	}

}
