using Engine;
using Engine.DB;

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
        
    }
    
}