using System;
using System.Collections.Generic;
using Engine.DB.Migrations.Base;

namespace Engine.DB.Migrations.Impls
{
    
    public class PropertyConvertor : ScriptConvertor
    {
        public override string Name => "property";
        
        public override ICollection<string> DoConvert(ref string script, IList<string> args)
        {
            if (args.Count != 2)
                throw new ArgumentException("property command must be 2 argument, example:\r\n" +
                                            "property \"dbType\",\"sqlite\";");

            string name = args[0];
            string value = args[1];
            
            Executer.Variables[name] = value;
            
            return null;
        }
        
    }
    
}