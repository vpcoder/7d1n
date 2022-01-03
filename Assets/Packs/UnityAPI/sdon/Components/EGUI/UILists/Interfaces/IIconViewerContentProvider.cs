using System;

namespace Engine.EGUI {

	/// <summary>
	/// Интерфейс проводника контента для IconViewer
	/// </summary>
	/// <typeparam name="T">Тип данных иконки</typeparam>
	public interface IIconViewerContentProvider<T>: IContentProvider<IIconContainer<T>> where T : class {
		
	}

}
