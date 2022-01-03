using System.Collections.Generic;

namespace UnityEditor.Sdon
{

	public interface ITableCustomRowEditListener<T> {

		void OnEdit(List<T> data, int index, T item);

	}

}
