using System.IO;
using System.Xml;
using Engine.Data;
using UnityEngine;

namespace GitIntegration.Items.Data
{
    
    public class ItemRow
    {
        public long ID
        {
            get
            {
                return Element == null ? -1L : long.Parse(Element.GetAttribute("ID"));
            }
        }
        
        public string Name
        {
            get
            {
                return Element == null ? "?" : Element.GetAttribute("Name");
            }
        }

        public GroupType Type
        {
            get
            {
                return Element == null ? GroupType.Item : Enums<GroupType>.Parse(Element.GetAttribute("Type"));
            }
        }

        public XmlDocument Document { get; set; }
        public XmlElement Element { get; set; }
        public string FileName { get; set; }

        public string DictionaryName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(FileName);
            }
        }
    }
    
}