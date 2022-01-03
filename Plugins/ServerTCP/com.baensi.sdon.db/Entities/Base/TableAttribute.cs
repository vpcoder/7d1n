using System;

namespace com.baensi.sdon.db.entities
{

    public class TableAttribute : Attribute
    {

        public string Table { get; }

        public TableAttribute(string table)
        {
            this.Table = table;
        }

    }

}
