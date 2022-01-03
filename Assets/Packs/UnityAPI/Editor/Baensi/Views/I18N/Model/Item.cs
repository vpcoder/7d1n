
namespace UnityEditor.Sdon.I18N.Model {

	public class Item {

		private string id;
		private string value;

		public Item(string id, string value) : base() {
			this.id=id;
			this.value=value;
		}

		public Item(Item item) : base() {
			this.id=item.id;
			this.value=item.value;
		}

		public string ID {
			get { return id;}
			set { this.id = value;}
		}

		public string Value {
			get { return value;}
			set { this.value = value;}
		}

		#region HASH

		public override bool Equals(object obj) {
			Item item = obj as Item;

			if (obj == null) {
				return false;
			}

			if (!id.Equals(item.id)) {
				return false;
			}

			if (!value.Equals(item.value)) {
				return false;
			}

			return true;
		}
		
		public override int GetHashCode() {
			return id.GetHashCode()+value.GetHashCode()*13;
		}

		#endregion

	}

}
