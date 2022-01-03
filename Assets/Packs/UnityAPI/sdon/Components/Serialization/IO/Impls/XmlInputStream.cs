using System;
using System.IO;
using System.Xml;

namespace Engine.Serialization.IO {

	public class XmlInputStream : SerializeFileInputStream<XmlDocument> {

		#region Hidden Fields

		/// <summary>
		/// Хранит ссылку на экземпляр открытого xml документа
		/// </summary>
		private XmlDocument doc;

		/// <summary>
		/// Хранит ссылку на экземпляр потока чтения xml документа
		/// </summary>
		private XmlTextReader reader;

		#endregion


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
		public override void open(string url) {
			base.open(url);
			reader = new XmlTextReader(Stream);
			doc = new XmlDocument();
			doc.Load(reader);
		}

		/// <summary>
		/// Закрывает поток
		/// </summary>
		public override void close() {
			base.close();
			if (reader == null) {
				return;
			}
			reader.Close();

			reader = null;
			doc    = null;
		}

		/// <summary>
		/// Читает следующий элемент
		/// </summary>
		/// <returns>Возвращает прочитанный элемент</returns>
		public override XmlDocument read() {
			return doc;
		}

	}

}
