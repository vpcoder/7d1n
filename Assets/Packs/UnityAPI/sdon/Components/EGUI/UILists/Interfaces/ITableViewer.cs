using System;
using UnityEngine;

namespace Engine.EGUI {

	/// <summary>
	/// Интерфейс Viewer для представления композитов в виде таблицы
	/// </summary>
	public interface ITableViewer {

		/// <summary>
		/// Устанавливает/Возвращает тип привязки композитов внутри контейнера
		/// </summary>
		/// <value>Тип привязки комопзитов внутри контейнера</value>
		InnerAlignType InnerAlignType {
			get;
			set;
		}

		/// <summary>
		/// Ширина и высота отступов между UI композитами
		/// </summary>
		/// <value>Ширина и высота отступов между композитами</value>
		Vector2 CellBorder {
			get;
			set;
		}

	}

}

