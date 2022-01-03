using System;
using System.IO;

namespace Engine.Serialization.IO {

	public abstract class SerializeFileInputStream<T> : ISerializeInputStream<T> {

		private Stream stream;

		public Stream Stream {
			get {
				return stream;
			}
		}

		/// <summary>
		/// Открывает поток чтения
		/// </summary>
		/// <param name="url">Путь к потоку</param>
		/// <exception cref="System.ArgumentException">ArgumentException</exception>
		/// <exception cref="System.NotSupportedException">NotSupportedException</exception>
		/// <exception cref="System.ArgumentNullException">ArgumentNullException</exception>
		/// <exception cref="System.Security.SecurityException">SecurityException</exception>
		/// <exception cref="System.IO.FileNotFoundException">FileNotFoundException</exception>
		/// <exception cref="System.IO.IOException">IOException</exception>
		/// <exception cref="System.IO.DirectoryNotFoundException">DirectoryNotFoundException</exception>
		/// <exception cref="System.IO.PathTooLongException">PathTooLongException</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">ArgumentOutOfRangeException</exception>
		public virtual void open(string url) {
			stream = new FileStream(url, FileMode.Open);
		}

		/// <summary>
		/// Закрывает поток
		/// </summary>
		public virtual void close() {
			if (stream == null) {
				return;
			}
			stream.Close();
			stream = null;
		}

		/// <summary>
		/// Читает следующий элемент
		/// </summary>
		/// <returns>Возвращает прочитанный элемент</returns>
		public abstract T read();

	}

}
