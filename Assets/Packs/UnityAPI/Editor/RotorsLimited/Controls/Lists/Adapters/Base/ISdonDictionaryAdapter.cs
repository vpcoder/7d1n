
namespace UnityEditor.Sdon.Controls {
	
	public interface ISdonDictionaryAdapter<K,V> {

		K ConstructKey();

		V ConstructValue();

	}

}
