
namespace UnityEditor.Sdon.I18N.Model {

	public class DataPath {

        public Data     data;
        public DataPath next = null;

        public DataPath(){
            this.data = null;
            this.next = null;
        }

        public DataPath(Data data) : base() {
            this.data = data;
            this.next = null;
		}

        public DataPath Add(Data data){
            
            if (next != null) {
                return next.Add(data);
            }

            next = new DataPath(data);

            return this;
        }

        public DataPath Clone(){
            DataPath result = new DataPath();

            DataPath iterator = this;
            for (;;) {
                
                if (iterator == null) {
                    break;
                }

                result.Add(iterator.data);

                iterator = iterator.next;
            }

            return result;
        }

	}

}
