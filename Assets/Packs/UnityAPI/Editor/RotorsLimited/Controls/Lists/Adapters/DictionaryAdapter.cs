using UnityEngine;
using System.Collections.Generic;
using Rotorz.ReorderableList;

namespace UnityEditor.Sdon.Controls {
	
	public abstract class DictionaryAdapter<K,V> : IReorderableListAdaptor, ISdonDictionaryAdapter<K,V> {

		protected Dictionary<K,V> data;

			public DictionaryAdapter() : base() {
				this.data = new Dictionary<K,V>();
			}

			public DictionaryAdapter(Dictionary<K,V> data) : base() {
				this.data=data;
			}

		public void SetData(Dictionary<K,V> data) {
			this.data=data;
		}

		public int Count {
			get {
				return data.Count;
			}
		}

		#region Abstract

		public abstract K ConstructKey();

		public abstract V ConstructValue();

		public abstract V DrawItem(Rect position, K key, V value);

		#endregion

		public virtual void BeginGUI() {

			if (data.Count == 0) {
				GUILayout.Label("<Пусто>",GUILayout.Width(64));
			}

		}

		public virtual bool CanDrag(int index) {
			return false;
		}

		public virtual bool CanRemove(int index) {
			return true;
		}

		public virtual void DrawItemBackground(Rect position, K key, V value) { }

		public virtual void EndGUI() { }

		public virtual void Duplicate(int index) {

			K key = ConstructKey();

			if (key == null || data.ContainsKey(key)) {
				return;
			}

			data.Add(key,ConstructValue());
		}

		public virtual float GetItemHeight(K key, V value) {
			return 21f;
		}

		// = Реализация ==============

		public void Add() {

			K key = ConstructKey();

			if (key == null || data.ContainsKey(key)) {
				return;
			}

			data.Add(key, ConstructValue());
		}

		public void DrawItem(Rect position, int index) {
			List<K> keys = new List<K>(data.Keys);
			DrawItem(position,keys[index],data[keys[index]]);
		}

		public void DrawItemBackground(Rect position, int index) {
			List<K> keys = new List<K>(data.Keys);
			DrawItemBackground(position,keys[index],data[keys[index]]);
		}

		public void Clear() {
			data.Clear();
		}

		public float GetItemHeight(int index) {
			List<K> keys = new List<K>(data.Keys);
			return GetItemHeight(keys[index],data[keys[index]]);
		}

		public void Insert(int index) {
			K key = ConstructKey();

			if (key == null || data.ContainsKey(key)) {
				return;
			}

			data.Add(key,ConstructValue());
		}

		public void Move(int sourceIndex, int destIndex) { }

		public void Remove(int index) {
			List<K> keys = new List<K>(data.Keys);
			data.Remove(keys[index]);
		}

	}

}
