using System;
using System.Collections.Generic;
using Engine.Data;
using Engine.DB.Migrations.Base;
using Packs.UnityAPI.sdon.Framework.String;
using UnityEngine;

namespace Engine.DB.Migrations
{
    
    public class MigrationScriptExecuter
    {

        private readonly IDictionary<string, IScriptConvertor> commandNameToConverter;
        public IDictionary<string, string> Variables { get; } = new Dictionary<string, string>();

        public MigrationScriptExecuter()
        {
            commandNameToConverter = new Dictionary<string, IScriptConvertor>();
            foreach (var converter in AssembliesHandler.CreateImplementations<IScriptConvertor>())
            {
                converter.Executer = this;
                commandNameToConverter.Add(converter.Name, converter);
            }
            Variables["dbType"] = "sqlite";
            Variables["lang"] = "i18n_ru_ru";
            Variables["version"] = Game.Instance.Buildtime.Version.ToString();
        }

        private string TryGetName(ref string script)
        {
            var pos = script.IndexOf(' ');
            if (pos < 0)
                return null;
            return script.Substring(0, pos);
        }

        private IScriptConvertor TryFindConverter(ref string script)
        {
            var name = TryGetName(ref script);
            if (name == null)
                return null;
            commandNameToConverter.TryGetValue(name, out var converter);
            return converter;
        }
        
        private ICollection<string> TryFindVariables(ref string script)
        {
            var pos = 0;
            var list = new List<string>();
            for (;;)
            {
                var firstIndex = script.IndexOf("${", pos);
                if (firstIndex < 0)
                    break;
                var endIndex = script.IndexOf("}", firstIndex + 2);
                if (endIndex < 0)
                    throw new ArgumentException("property token parse exception, example property: ${name}");
                list.Add(script.Substring(firstIndex + 2, endIndex - firstIndex - 2));
                pos = endIndex + 1;
            }
            return list;
        }

        private string ProcessingVariables(ref string script)
        {
            var vars = script.GetIncludesInQuotes("${", "}");
            if (Lists.IsNotEmpty(vars)) {
                foreach (var variable in vars)
                {
                    if (!Variables.TryGetValue(variable, out var value))
                        value = "?";
                    script = script.Replace("${" + variable + "}", value);
                }
            }
            return script;
        }
        
        private string ProcessingIncludes(string script)
        {
            var includes = script.GetIncludesInQuotes("include {", "}");
            if (Lists.IsNotEmpty(includes))
            {
                foreach (var include in includes)
                {
                    script = script.Replace("include {" + include + "}", ReadScript(include));
                }
            }
            return script;
        }
        
        public ICollection<string> ConvertScriptToSQL(ref string script)
        {
            script = ProcessingVariables(ref script);
            var converter = TryFindConverter(ref script);
            return converter == null ? new List<string> { script } : converter.Convert(ref script);
        }

        private string GetPath(string scriptName)
        {
            return "Database/" + scriptName;
        }

        private string ReadScript(string scriptName)
        {
            var path = GetPath(scriptName);
            var sqlAsset = Resources.Load(path) as TextAsset;
            if (sqlAsset == null)
            {
                Debug.LogError("Не удалось найти проливку '" + path + "'!");
                throw new Exception("script '" + path + "' not founded!");
            }
            return ProcessingIncludes(sqlAsset.text);
        }
        
        public void ExecuteScriptFile(string scriptName)
        {
            var allBlocks = ReadScript(scriptName).Split(new[] { ";\r\n" }, StringSplitOptions.None);
            Exception e = null;

            Debug.Log("reload sql script '" + scriptName + "'...");
            Db.Instance.Do(connect =>
            {
                int counter = 0;

                foreach (var scriptBlock in allBlocks)
                {
                    string script = scriptBlock;

                    if (string.IsNullOrEmpty(script))
                        continue;

                    script = script.Trim();

                    if (string.IsNullOrEmpty(script) || script.StartsWith("#"))
                        continue;

                    var sqlList = ConvertScriptToSQL(ref script);
                    if(Lists.IsEmpty(sqlList))
                        continue;

                    foreach (var sql in sqlList)
                    {
                        try
                        {
                            counter += connect.Execute(sql);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError("script: " + script);
                            Debug.LogError("sql: " + sql);
                            e = ex;
                            throw ex;
                        }
                    }
                }

                Debug.Log("reload sql counter: " + counter);
            }, ex =>
            {
                Debug.LogException(ex);
                e = ex;
            });

#if UNITY_EDITOR
            if (e != null)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
#endif
        }
        
    }
    
}