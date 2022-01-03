using UnityEngine;
using UnityEngine.UI;
using Engine.I18N;

namespace Engine.EGUI {

	[ExecuteInEditMode]
	[RequireComponent(typeof(Text))]
	public class I18NText : MonoBehaviour {

		[Caption("Ключевая фраза (keyword)")]
		[Comments("По данной ключевой фразе из словарей будет выбрана фраза-перевод в соответствии с текущей локализацией GameConfig.Settings.GetValue(StrSettingsProprety.Localization)")]
		[SerializeField] private string textId;

		void Start() {
			GetComponent<Text>().text = CLang.getInstance().get(textId);
		}

	}

}
