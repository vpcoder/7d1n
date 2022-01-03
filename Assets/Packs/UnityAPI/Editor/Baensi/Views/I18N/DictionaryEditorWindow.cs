using System.IO;

namespace UnityEditor.Sdon.I18N {

	public class DictionaryEditorWindow : EditorWindow {
		
		private DictionaryView dictionaryView = new DictionaryView();

		void OnEnable() {

			if (!Directory.Exists(Engine.I18N.Dictionary.EDITOR.I18N_DIRECTORY)) {
				Directory.CreateDirectory(Engine.I18N.Dictionary.EDITOR.I18N_DIRECTORY);
			}

			Show();
		}

		public void AddItemSelectListener(IItemSelectListener itemSelectListener) {
			dictionaryView.AddItemSelectListener(itemSelectListener);
		}

		void OnGUI() {

			if (dictionaryView == null) {
				return;
			}

			dictionaryView.OnDrawView();
		}

		void OnFocus() {
			//SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
			//SceneView.onSceneGUIDelegate += this.OnSceneGUI;
		}

		void OnDestroy() {
			//SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
		}

	}

}
