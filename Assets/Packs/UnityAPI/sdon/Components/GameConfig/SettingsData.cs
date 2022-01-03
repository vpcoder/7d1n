using System;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

namespace Engine.Config {

	/// <summary>
	/// Класс общих конфигураций 
	/// </summary>
	public class SettingsData {

		#region Shared Fields

		/// <summary>
		/// Аудиомиксер, отвечающий за звуки
		/// </summary>
		public AudioMixerGroup SoundsAudioGroup;

		/// <summary>
		/// Аудиомиксер, отвечающий за музыку
		/// </summary>
		public AudioMixerGroup MusicsAudioGroup;

		#endregion

		#region Hidden Fields

		/// <summary>
		/// Словарь со свойствами типа float
		/// </summary>
		private Dictionary<FltSettingsProprety, float> fltValues = new Dictionary<FltSettingsProprety, float>();

		/// <summary>
		/// Словарь со свойствами типа string
		/// </summary>
		private Dictionary<StrSettingsProprety, string> strValues = new Dictionary<StrSettingsProprety, string>();

		/// <summary>
		/// Словарь со свойствами типа bool
		/// </summary>
		private Dictionary<BoolSettingsProprety, bool> boolValues = new Dictionary<BoolSettingsProprety, bool>();

		#endregion


		#region Properties

		/// <summary>
		/// Возвращает значение типа V свойства property типа T из словаря dictionary T, V
		/// </summary>
		/// <typeparam name="T">Тип свойства, значение которого надо получить</typeparam>
		/// <typeparam name="V">Тип значения, которое надо получить</typeparam>
		/// <param name="dictionary">Словарь со списком свойств</param>
		/// <param name="property">Свойство, значение которого надо получить</param>
		/// <returns>Значение типа V свойства property типа T из словаря dictionary T, V</returns>
		private V getValue<T, V>(Dictionary<T, V> dictionary, T property) where T : struct {
			return dictionary[property];
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="V"></typeparam>
		/// <param name="dictionary"></param>
		/// <param name="property"></param>
		/// <param name="value"></param>
		private void setValue<T, V>(Dictionary<T, V> dictionary, T property, V value) where T : struct {
			dictionary[property] = value;
		}

		/// <summary>
		/// Возвращает float значение свойства property
		/// </summary>
		/// <param name="property">Свойство, значение которого надо вернуть</param>
		/// <returns>float значение свойства</returns>
		public float GetValue(FltSettingsProprety property) {
			return getValue<FltSettingsProprety,float>(fltValues,property);
		}

		/// <summary>
		/// Возвращает string значение свойства property
		/// </summary>
		/// <param name="property">Свойство, значение которого надо вернуть</param>
		/// <returns>string значение свойства</returns>
		public string GetValue(StrSettingsProprety property) {
			return getValue<StrSettingsProprety, string>(strValues, property);
		}

		/// <summary>
		/// Возвращает bool значение свойства property
		/// </summary>
		/// <param name="property">Свойство, значение которого надо вернуть</param>
		/// <returns>bool значение свойства</returns>
		public bool GetValue(BoolSettingsProprety property) {
			return getValue<BoolSettingsProprety,bool>(boolValues,property);
		}

		/// <summary>
		/// Устанавливает float значение свойства property
		/// </summary>
		/// <param name="property">Свойство, значение которого надо установить</param>
		/// <param name="value">Значение, которое надо установить свойству property</param>
		public void SetValue(FltSettingsProprety property, float value) {
			setValue<FltSettingsProprety, float>(fltValues, property, value);
		}

		/// <summary>
		/// Устанавливает string значение свойства property
		/// </summary>
		/// <param name="property">Свойство, значение которого надо установить</param>
		/// <param name="value">Значение, которое надо установить свойству property</param>
		public void SetValue(StrSettingsProprety property, string value) {
			setValue<StrSettingsProprety, string>(strValues, property, value);
		}

		/// <summary>
		/// Устанавливает bool значение свойства property
		/// </summary>
		/// <param name="property">Свойство, значение которого надо установить</param>
		/// <param name="value">Значение, которое надо установить свойству property</param>
		public void SetValue(BoolSettingsProprety property, bool value) {
			setValue<BoolSettingsProprety, bool>(boolValues, property, value);
		}

		#endregion

	}

}
