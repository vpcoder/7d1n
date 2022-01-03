using UnityEngine;
using UnityEditor;
using Engine.IO;

namespace Engine.Editor {

	[CustomEditor(typeof(IODeviceBase), true)]
	public class ControllersEditor : CustomEditorT<IODeviceBase> {

		public override void OnStart() {
			base.OnStart();
			Icon = IconsFactory.LoadIcon("./Assets/UnityAPI/Baensi/Android/Editor/Icons/controller_icon.png");
		}

	}

}
