using System.Collections.Generic;

namespace UnityEditor.Sdon.I18N.Model {

	/// <summary>
	/// Класс области данных
	/// </summary>
	public class Data {

		public bool itemsVisible = false;
        public bool visible      = false;

		private string     name;

		private List<Data> datas = new List<Data>();
		private List<Item> items = new List<Item>();

		public Data(string name) : base() {
			this.items = new List<Item>();
			this.datas = new List<Data>();
			this.name=name;
		}

		public Data(string name, List<Data> datas) : base() {
			this.items = new List<Item>();
			this.datas=datas;
			this.name=name;
		}

		public Data(string name, List<Item> items) : base() {
			this.datas = new List<Data>();
			this.items=items;
			this.name=name;
		}

		public Data(string name, List<Data> datas, List<Item> items) : base() {
			this.datas=datas;
			this.items=items;
			this.name=name;
		}

		/// <summary>
		/// Имя/метка/раздел области данных
		/// </summary>
		public string Name {
			get { return name;}
			set { this.name = value; }
		}

		/// <summary>
		/// Набор слов и фраз внутри области данных
		/// </summary>
		public List<Item> Items {
			get { return items; }
			set { items = value; }
		}

		/// <summary>
		/// Набор областей данных внутри текущей области
		/// </summary>
		public List<Data> Datas {
			get { return datas; }
			set { this.datas = value; }
		}

		#region HASH

		public override bool Equals(object obj) {
			Data data = obj as Data;

			if (obj == null) {
				return false;
			}

			if (!name.Equals(data.name)) {
				return false;
			}

			if (datas.Count != data.datas.Count) {
				return false;
			}

			if (items.Count != data.items.Count) {
				return false;
			}

			for(int i=0;i<datas.Count;i++) {
				if (!datas[i].Equals(data.datas[i])) {
					return false;
				}
			}

			for(int i=0;i<items.Count;i++) {
				if (!items[i].Equals(data.items[i])) {
					return false;
				}
			}

			return true;
		}
		
		public override int GetHashCode() {
			int result = name.GetHashCode();
				foreach(Data data in datas) {
					result+=data.GetHashCode()*41;
				}
				foreach(Item item in items) {
					result+=item.GetHashCode()*57;
				}
			return result;
		}

		#endregion

	}

}
