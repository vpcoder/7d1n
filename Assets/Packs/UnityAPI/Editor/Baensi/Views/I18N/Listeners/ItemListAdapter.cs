using System.Collections.Generic;
using UnityEditor.Sdon.Controls;
using UnityEngine;
using UnityEditor.Sdon.I18N.Model;

namespace UnityEditor.Sdon.I18N {

	public class ItemListAdapter : ListAdapter<Item> {

		private List<IItemSelectListener> itemSelectListeners = new List<IItemSelectListener>();

		public void AddItemSelectListener(IItemSelectListener itemSelectListener) {
			itemSelectListeners.Add(itemSelectListener);
		}

		public override Item ConstructItem() {
			return new Item("id","value");
		}

		public override void DrawItem(Rect position, Item item) {

			position.width /= 2;
            item.ID    = GUI.TextField(position,item.ID);
            position.x = position.xMax + 4;
			position.width -= (26+4);
            item.Value = GUI.TextField(position,item.Value);

			position.x = position.xMax + 4;
			position.width = 26;
			if (GUI.Button(position,IconsFactory.Instance.GetIcon(Icons.ClipboardCopy))) {

				EditorGUIUtility.systemCopyBuffer = item.ID;

				foreach(IItemSelectListener listener in itemSelectListeners) {
					listener.SelectItem(item);
				}

			}
			
		}

	}

}
