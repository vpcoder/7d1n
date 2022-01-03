using System;


namespace Engine.EGUI {

	/// <summary>
	/// Интерфейс проводника UI композитов для IconViewer
	/// </summary>
	/// <typeparam name="T">Тип данных иконки</typeparam>
	public interface IIconViewerCompositeProvider<T> : ICompositeProvider<IIconContainer<T>> where T : class {

		void ResizeViewer(IIconViewer<IIconContainer<T>> viewer, IIconContainer<T>[] items);

	}

}
