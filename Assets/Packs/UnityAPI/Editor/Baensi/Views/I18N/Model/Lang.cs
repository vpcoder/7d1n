using System.Collections.Generic;

namespace UnityEditor.Sdon.I18N.Model {

	public class Lang {

		private string     language;
		private List<Data> datas = new List<Data>();

		public Lang(string language) : base() {
			this.language=language;
			datas = new List<Data>();
		}

		public Lang(string language, List<Data> datas) : base() {
			this.language=language;
			this.datas=datas;
		}

		public string Language {
			get { return language; }
			set { this.language = value; }
		}

		public List<Data> Datas {
			get { return datas; }
			set { this.datas = value; }
		}

		#region HASH

		public override bool Equals(object obj) {
			Lang lang = obj as Lang;

			if (obj == null) {
				return false;
			}

			if (!language.Equals(lang.language)) {
				return false;
			}

			if (datas.Count != lang.datas.Count) {
				return false;
			}

			for(int i=0;i<datas.Count;i++) {
				if (!datas[i].Equals(lang.datas[i])) {
					return false;
				}
			}

			return true;
		}
		
		public override int GetHashCode() {
			int result = language.GetHashCode();
				foreach(Data data in datas) {
					result+=data.GetHashCode()*17;
				}
			return result;
		}

		#endregion

	}

}
