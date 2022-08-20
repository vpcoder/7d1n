using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEngine;

namespace GitIntegration.Items.Data
{
    public class ItemsEditorFactory
    {

        private static readonly Lazy<ItemsEditorFactory> instance = new Lazy<ItemsEditorFactory>(() => new ItemsEditorFactory(), true);
        public static ItemsEditorFactory Instance => instance.Value;
        private ItemsEditorFactory()
        {
            ReloadData();
        }
        
        private string ResourcesDataPath => Application.dataPath + "/Resources/Data";

        private static readonly Dictionary<string, XmlDocument> ActiveDocuments = new Dictionary<string, XmlDocument>();
        private static readonly Dictionary<string, string> ActiveDocumentPaths = new Dictionary<string, string>();
        private static readonly List<ItemRow> ActiveItems = new List<ItemRow>();
        private static string[] ActiveDocumentNames;
        
        private List<string> FindItemDictionariesInProject => Directory.GetFiles(ResourcesDataPath)
                                                                       .Where(file => file.ToLower().EndsWith(".txt") 
                                                                           && Path.GetFileName(file).ToLower().StartsWith("items_") 
                                                                           && file.ToLower().Contains("_data"))
                                                                       .ToList();

        public List<ItemRow> Items => ActiveItems;
        public string[] Documents => ActiveDocumentNames;

        public ItemRow GetElementById(long id)
        {
            return ActiveItems.FirstOrDefault(item => item.ID == id);
        }
        
        public XmlDocument GetDoc(string name)
        {
            return ActiveDocuments[name];
        }

        public string GetPath(string name)
        {
            return ActiveDocumentPaths[name];
        }

        public void Add(string name, XmlDocument doc, XmlElement element)
        {
            var item = new ItemRow()
            {
                Document = doc,
                Element = element,
                FileName = GetPath(name),
            };
            ActiveItems.Add(item);
        }
        
        public void ReloadData()
        {
            ActiveDocuments.Clear();
            ActiveItems.Clear();
            ActiveDocumentPaths.Clear();

            var activeDocumentNames = new List<string>();
            foreach (var filePath in FindItemDictionariesInProject)
            {
                var name = Path.GetFileName(filePath);
                var doc = new XmlDocument();
                doc.Load(filePath);
                activeDocumentNames.Add(name);
                ActiveDocuments.Add(name, doc);
                ActiveDocumentPaths.Add(name, filePath);
                
                foreach (XmlElement element in doc.GetElementsByTagName("Item"))
                {
                    ActiveItems.Add(new ItemRow()
                    {
                        Document = doc,
                        Element = element,
                        FileName = filePath,
                    });
                }
            }
            ActiveDocumentNames = activeDocumentNames.ToArray();
        }
        
    }
    
}