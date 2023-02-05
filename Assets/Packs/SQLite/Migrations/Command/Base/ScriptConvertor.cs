using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.DB.Migrations.Base
{
    
    public abstract class ScriptConvertor : IScriptConvertor
    {
        private StringBuilder builder = new StringBuilder(4096);
        private const char DEFAULT_DELIMETER = ',';
        private const char DEFAULT_GROUP = '\"';
        private const char DEFAULT_ESCAPING = '\\';

        public virtual char Delimeter => DEFAULT_DELIMETER;
        public virtual char Escaping  => DEFAULT_ESCAPING;
        public abstract string Name { get; }
        
        public MigrationScriptExecuter Executer { get; set; }
        
        public abstract ICollection<string> DoConvert(ref string script, IList<string> args);

        public ICollection<string> Convert(ref string script)
        {
            if (string.IsNullOrWhiteSpace(script) || script.Length <= Name.Length + 2)
                return new List<string>{ script };

            var escapeOpen = false;
            var args = new List<string>();
            builder.Clear();
            for (int i = Name.Length + 1; i < script.Length - 1; i++)
            {
                var c = script[i];
                switch (c)
                {
                    case DEFAULT_ESCAPING:
                        i++;
                        if (i >= script.Length)
                            throw new ArgumentException("escape sequence '\\x' must be 2 char!");
                        builder.Append(script[i]);
                        break;
                    case DEFAULT_GROUP:
                        escapeOpen = !escapeOpen;
                        break;
                    case DEFAULT_DELIMETER:
                        if (escapeOpen)
                        {
                            builder.Append(c);
                            break;
                        }
                        args.Add(TrimArgument(builder.ToString()));
                        builder.Clear();
                        break;
                    default:
                        builder.Append(c);
                        break;
                }
            }

            if (builder.Length > 0)
                args.Add(TrimArgument(builder.ToString()));

            builder.Clear();
            return DoConvert(ref script, args);
        }

        private string TrimArgument(string argument)
        {
            argument = argument.Trim();
            if (argument.StartsWith("\"") && argument.EndsWith("\""))
                argument = argument.Substring(1, argument.Length - 2);
            return argument;
        }

    }
    
}