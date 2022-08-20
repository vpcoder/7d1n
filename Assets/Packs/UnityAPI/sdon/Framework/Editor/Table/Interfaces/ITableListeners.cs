
namespace UnityEditor.Sdon
{

	public interface ITableListeners<T> : ITableCustomRowConstructor<T>,
										  ITableCustomRowEditListener<T>,
										  ITableOnHandlers<T> {

	}

}
