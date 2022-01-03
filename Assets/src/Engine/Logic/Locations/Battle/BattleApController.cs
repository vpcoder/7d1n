using Engine.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Управление текущими доступными очками действия персонажа
    /// </summary>
    public class BattleApController : MonoBehaviour
    {

        [SerializeField] private Text txtAP;
        [SerializeField] private GameObject body;

        private BattleContext context;

        public void Show()
        {
            body.SetActive(true);
        }

        public void Hide()
        {
            body.SetActive(false);
        }

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

        private void Update()
        {
            txtAP.text = Localization.Instance.Get("msg_ap") + ": " + AP;
        }

    }

}
