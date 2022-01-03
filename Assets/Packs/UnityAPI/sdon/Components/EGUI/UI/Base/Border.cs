using System;
using UnityEngine;

namespace Engine.EGUI {

	/// <summary>
	/// Класс границ виджета
	/// </summary>
	[Serializable]
	public class Border : IBorder {

		/// <summary>
		/// Левая вертикальная граница
		/// </summary>
		[SerializeField] private float left;

		/// <summary>
		/// Верхняя горизонтальная граница
		/// </summary>
		[SerializeField] private float top;

		/// <summary>
		/// Правая вертикальная граница
		/// </summary>
		[SerializeField] private float right;

		/// <summary>
		/// Нижняя горизонтальная граница
		/// </summary>
		[SerializeField] private float bottom;

			/// <summary>
			/// Пустой конструктор
			/// </summary>
			public Border() {
				left   = 0;
				top    = 0;
				right  = 0;
				bottom = 0;
			}

			/// <summary>
			/// Конструктор
			/// </summary>
			/// <param name="left">Левая вертикальная граница</param>
			/// <param name="top">Верхняя горизонтальная граница</param>
			/// <param name="right">Права вертикальная граница</param>
			/// <param name="bottom">Нижняя горизонтальная граница</param>
			public Border(float left, float top, float right, float bottom){
				this.left   = left;
				this.top    = top;
				this.right  = right;
				this.bottom = bottom;
			}


		/// <summary>
		/// Устанавливает все параметры границы
		/// </summary>
		/// <param name="x1">Left</param>
		/// <param name="y1">Top</param>
		/// <param name="x2">Width</param>
		/// <param name="y2">Height</param>
		public void Set(float x1, float y1, float x2, float y2) {
			this.left   = x1;
			this.top    = y1;
			this.right  = x2;
			this.bottom = y2;
		}

		/// <summary>
		/// Возвращает ширину вертикальных границ
		/// </summary>
		/// <value>Ширина вертикальных границ</value>
		public float Width {
			get {
				return left + right;
			}
		}

		/// <summary>
		/// Возвращает высоту горизонтальных границ
		/// </summary>
		/// <value>Высота горизонтальных границ</value>
		public float Height {
			get {
				return top + bottom;
			}
		}

		/// <summary>
		/// Левая вертикальная граница
		/// </summary>
		/// <value>The left.</value>
		public float Left {
			get {
				return left;
			}
			set {
				if (value < 0) {
					return;
				}
				this.left = value;
			}
		}

		/// <summary>
		/// Правая вертикальная граница
		/// </summary>
		/// <value>The right.</value>
		public float Right {
			get {
				return right;
			}
			set {
				if (value < 0) {
					return;
				}
				this.right = value;
			}
		}

		/// <summary>
		/// Верхняя горизонтальная граница
		/// </summary>
		/// <value>The top.</value>
		public float Top {
			get {
				return top;
			}
			set {
				if (value < 0) {
					return;
				}
				this.top = value;
			}
		}

		/// <summary>
		/// Нижняя горизонтальная граница
		/// </summary>
		/// <value>The bottom.</value>
		public float Bottom {
			get {
				return bottom;
			}
			set {
				if (value < 0) {
					return;
				}
				this.bottom = value;
			}
		}

		/// <summary>
		/// Метод сревнивает два экземпляра класса
		/// </summary>
		/// <param name="b1">левый экземпляр</param>
		/// <param name="b2">правый экземпляр</param>
		private static bool equals(IBorder b1, IBorder b2){
			if (b1 == b2) {
				return true;
			}
			if (b1 == null || b2 == null) {
				return false;
			}
			return b1.Left   == b2.Left  &&
				   b1.Right  == b2.Right &&
				   b1.Top    == b2.Top   &&
				   b1.Bottom == b2.Bottom;
		}

		public override int GetHashCode() {
			int prime = 31;
			float result = 1;

				result = prime * result + left;
				result = prime * result + right;
				result = prime * result + top;
				result = prime * result + bottom;

			return Mathf.FloorToInt(result);
		}

		public override bool Equals(object obj) {
			return equals(this, obj as IBorder);
			
		}

		public static Border operator +(Border b1, Border b2){
			return new Border(b1.left   + b2.left,
							  b1.top    + b2.top,
							  b1.right  + b2.right,
							  b1.bottom + b2.bottom);
		}

		public static Border operator -(Border b1, Border b2){
			return new Border(b1.left   - b2.left,
							  b1.top    - b2.top,
							  b1.right  - b2.right,
							  b1.bottom - b2.bottom);
		}

		public static Border operator *(Border b1, float value){
			return new Border(b1.left   * value,
							  b1.top    * value,
							  b1.right  * value,
							  b1.bottom * value);
		}

		public static Border operator /(Border b1, float value){
			if (value == 0) {
				throw new DivideByZeroException();
			}
			return new Border(b1.left   / value,
							  b1.top    / value,
							  b1.right  / value,
							  b1.bottom / value);
		}

		public static bool operator ==(Border b1, Border b2){
			return equals(b1, b2);
		}

		public static bool operator !=(Border b1, Border b2){
			return !equals(b1, b2);
		}

	}

}

