using System.Collections.Generic;
using UnityEngine;
using Rotorz.ReorderableList;

namespace UnityEditor.Sdon.Controls {

	public abstract class ListAdapter<T> : IReorderableListAdaptor, ISdonListAdapter<T> {

		private List<T> data;

		public ListAdapter() {
			this.data = new List<T>();
		}

		public ListAdapter(List<T> data) {
			this.data = data;
		}
		
		public List<T> Data => data;
		
		public void SetData(List<T> data) {
			this.data = data;
		}

		public int Count => data.Count;
		public bool IsEmpty => Count == 0;
		public bool IsNotEmpty => !IsEmpty;

		#region Abstract

		public abstract T ConstructItem();

		public abstract void DrawItem(Rect position, T item);

		#endregion

		public virtual void BeginGUI() {

			if (data.Count == 0) {
				GUILayout.Label("<Пусто>",GUILayout.Width(64));
			}

		}

		public virtual bool CanDrag(int index) {
			return true;
		}

		public virtual bool CanRemove(int index) {
			return true;
		}

		public virtual void DrawItemBackground(Rect position, T item) { }
		
		public virtual void EndGUI() { }

		public virtual void Duplicate(int index) {
			data.Add(ConstructItem());
		}

		public virtual float GetItemHeight(T item) {
			return 21f;
		}

		// = Реализация ==============

		public void Add() {
			data.Add(ConstructItem());
		}

		public void DrawItem(Rect position, int index) {
			DrawItem(position, data[index]);
		}

		public void DrawItemBackground(Rect position, int index) {
			DrawItemBackground(position,data[index]);
		}

		public void Clear() {
			data.Clear();
		}

		public float GetItemHeight(int index) {
			return GetItemHeight(data[index]);
		}

		public void Insert(int index) {
			data.Insert(index,ConstructItem());
		}

		public void Move(int sourceIndex, int destIndex) {
			T item = data[sourceIndex];
			data.Remove(item);

			int direction = sourceIndex < destIndex ? +1 : 0;

			if (destIndex<data.Count) {
				data.Insert(destIndex-direction, item);
			} else {
				data.Add(item);
			}

		}

		public void Remove(int index) {
			data.Remove(data[index]);
		}

	}

}
