using Engine.Logic.Dialog;
using UnityEngine;

namespace Engine.Story.Nyasevsk
{
    
    public class WomenDeadStoryCatcher : StoryBase
    {
        
        public override string StoryID => "main.nyasevsk1.start_women_dead";

        [SerializeField] private Transform woomen;
        [SerializeField] private Transform wt;
        
        public override void CreateDialog(DialogQueue dlg)
        {
            dlg.Run(() =>
            {
                Camera.main.SetState(PlayerEyePos, woomen);
            });
            dlg.Delay(2.5f);
            dlg.Text("Кажется она того... Не двигается.");
            dlg.Text("Ну, как говорится, здесь наши полномочия - всё...");
            dlg.Run(() =>
            {
                Camera.main.SetState(PlayerEyePos, wt);
            });
            dlg.Text("А это что за...");
        }

    }
    
}
