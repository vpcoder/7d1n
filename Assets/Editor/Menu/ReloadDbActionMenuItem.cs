using Engine;
using Engine.Data.Factories;
using Engine.DB;
using GitIntegration.Items.Data;

namespace UnityEditor.Menu
{
    
    public static class ReloadDbActionMenuItem
    {

        [MenuItem("7d1n/Reset DB")]
        public static void ReloadDbAction()
        {
            try
            {
                Db.Instance.CreateEmptyDb();
                Db.Instance.DoFillDB();
                GameSettings.Instance.LoadSettings();
                Engine.Localization.Instance.ReloadDictionary();
            }
            catch { }
        }
        
        [MenuItem("7d1n/Reload items factory")]
        public static void ReloadItemsAction()
        {
            try
            {
                ItemFactory.Instance.ReloadFactory();
                ItemsEditorFactory.Instance.ReloadData();
            }
            catch { }
        }
        
    }
    
}