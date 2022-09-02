using Engine.Data;
using Engine.EGUI;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Locations
{

    /// <summary>
    ///
    /// Управление текущими доступными очками действия (ОД) персонажа
    /// ---
    /// Managing the current available character action points (AP)
    /// 
    /// </summary>
    public class BattleApController : Panel
    {

        #region Hidden Fields
        
        [SerializeField] private Text txtAP;

        private BattleContext context;

        #endregion

        #region Properties
        
        private int AP
        {
            get
            {
                if (context == null)
                {
                    context = Game.Instance.Runtime.BattleContext;
                }
                return context.CurrentCharacterAP;
            }
        }
        
        #endregion

        private void UpdateInfo()
        {
            txtAP.text = Localization.Instance.Get("msg_ap") + ": " + AP;
        }
        
        private void Update()
        {
            UpdateInfo();
        }

    }

}
