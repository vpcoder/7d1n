using System;
using System.Collections.Generic;
using System.Text;
using Engine.DB.Migrations.Base;

namespace Engine.DB.Migrations.Impls
{
    
    public class MessageConvertor : ScriptConvertor
    {

        private StringBuilder builder = new StringBuilder();
        public override string Name => "message";
        
        public override ICollection<string> DoConvert(ref string script, IList<string> args)
        {
            if (args.Count != 2 && args.Count != 3)
                throw new ArgumentException("message command must be 2 or 3 arguments, example:\r\n" +
                                            "message \"key\",\"text of message\";\r\n" +
                                            "message \"i18n_ru-ru\",\"key\",\"text of message\";");

            string table;
            string key;
            string value;

            if (args.Count == 3)
            {
                table = args[0];
                key = args[1];
                value = args[2];
            }
            else
            {
                table = Executer.Variables["lang"];
                key = args[0];
                value = args[1];
            }
            
            builder.Append("INSERT INTO `");
            builder.Append(table);
            builder.Append("`(`key`,`value`) VALUES ('");
            builder.Append(key);
            builder.Append("', '");
            builder.Append(value);
            builder.Append("');");

            var sqlText = builder.ToString();
            builder.Clear();
            return new List<string> { sqlText };
        }
        
    }
    
}