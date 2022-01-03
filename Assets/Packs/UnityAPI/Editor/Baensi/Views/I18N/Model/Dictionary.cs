using System.Collections.Generic;

namespace UnityEditor.Sdon.I18N.Model {

	public class Dictionary {

		private string name; // имя словаря
		private List<Lang> langs = new List<Lang>(); // области языков

		public Dictionary(string name) : base() {
			this.name=name;
			langs = new List<Lang>();
		}

		public Dictionary(string name, List<Lang> langs) : base() {
			this.name=name;
			this.langs=langs;
		}

		/// <summary>
		/// Название словаря
		/// </summary>
		public string Name {
			get { return name; }
			set { this.name = value; }
		}

		/// <summary>
		/// Список языков
		/// </summary>
		public List<Lang> Langs {
			get { return langs; }
			set { this.langs = value; }
		}
		
		#region HASH

		public override bool Equals(object obj) {
			Dictionary dictionary = obj as Dictionary;

			if (obj == null) {
				return false;
			}

			if (!name.Equals(dictionary.name)) {
				return false;
			}

			if (langs.Count != dictionary.langs.Count) {
				return false;
			}

			for(int i=0;i<langs.Count;i++) {
				if (!langs[i].Equals(dictionary.langs[i])) {
					return false;
				}
			}

			return true;
		}
		
		public override int GetHashCode() {
			int result = name.GetHashCode();
				foreach(Lang lang in langs) {
					result+=lang.GetHashCode()*31;
				}
			return result;
		}

		#endregion

	}

}
