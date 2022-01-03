using System;
using UnityEngine;


namespace Engine.EGUI {

	/// <summary>
	/// Интерфейс Viewer для представления композитов в виде матрицы иконок
	/// </summary>
	public interface IIconViewer<T> : IViewer<T> where T : class {

		int     CellCountX { get; set; }
		int     CellCountY { get; set; }
		Vector2 CellSize   { get; set; }

	}

}
