using System;
using System.Collections.Generic;
using System.Text;
using Engine.DB.Migrations.Base;

namespace Engine.DB.Migrations.Impls
{
    
    public class MessageObjectConvertor : ScriptConvertor
    {

        private StringBuilder builder = new StringBuilder();
        public override string Name => "message_object";
        
        public override ICollection<string> DoConvert(ref string script, IList<string> args)
        {
            if (args.Count != 3 && args.Count != 4)
                throw new ArgumentException("message command must be 3 or 4 arguments, example:\r\n" +
                                            "message_object \"key\",\"text of message\", \"text description\";\r\n" +
                                            "message_object \"i18n_ru-ru\",\"key\",\"text of message\",\"text description\";");

            string table;
            string key;
            string name;
            string desc;

            if (args.Count == 4)
            {
                table = args[0];
                key = args[1];
                name = args[2];
                desc = args[3];
            }
            else
            {
                table = Executer.Variables["lang"];
                key = args[0];
                name = args[1];
                desc = args[2];
            }

            var list = new List<string>(2);
            
            builder.Append("INSERT INTO `");
            builder.Append(table);
            builder.Append("`(`key`,`value`) VALUES ('");
            builder.Append(key + "_name");
            builder.Append("', '");
            builder.Append(name);
            builder.Append("');");
            list.Add(builder.ToString());
            builder.Clear();
            
            builder.Append("INSERT INTO `");
            builder.Append(table);
            builder.Append("`(`key`,`value`) VALUES ('");
            builder.Append(key + "_desc");
            builder.Append("', '");
            builder.Append(desc);
            builder.Append("');");
            list.Add(builder.ToString());
            builder.Clear();
            
            return list;
        }
        
    }
    
}