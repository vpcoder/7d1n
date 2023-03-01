using Engine.Logic.Dialog;
using Engine.Logic.Locations;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class WakeUpZombieStory : StoryBase
    {
        
        [SerializeField] private Transform zombiePoint;

        public override void CreateDialog(DialogQueue dlg)
        {
            var background = ObjectFinder.SceneViewImage;
            dlg.Text("Что происходит?");

            dlg.Delay(2f, true);
            dlg.Run(() =>
            {
                StoryActionHelper.Fade(background, Color.white, Color.clear, 0.8f);
            });
            
        }

    }
    
}