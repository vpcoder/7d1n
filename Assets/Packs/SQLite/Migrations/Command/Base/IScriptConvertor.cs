using System.Collections.Generic;

namespace Engine.DB.Migrations.Base
{

    public interface IScriptConvertor
    {

        MigrationScriptExecuter Executer { get; set; }

        char Delimeter { get; }
        char Escaping { get; }

        string Name { get; }

        ICollection<string> Convert(ref string script);

    }
    
}