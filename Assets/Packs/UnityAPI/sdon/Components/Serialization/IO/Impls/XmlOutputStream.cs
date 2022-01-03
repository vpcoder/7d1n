using System;
using System.IO;
using System.Xml;

namespace Engine.Serialization.IO {

	public class XmlOutputStream : SerializeFileOutputStream<XmlDocument> {

		#region Hidden Fields

		/// <summary>
		/// Хранит ссылку на экземпляр открытого xml документа
		/// </summary>
		private XmlDocument doc;

		#endregion

		/// <summary>
		/// Читает следующий элемент
		/// </summary>
		/// <returns>Возвращает прочитанный элемент</returns>
		public override void write(XmlDocument doc) {
			doc.Save(Stream);
			Stream.Flush();
		}

	}

}
