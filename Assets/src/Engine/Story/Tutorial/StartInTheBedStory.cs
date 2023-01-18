using Engine.Logic.Dialog;
using src.Engine.Scenes.Loader;
using UnityEngine;

namespace Engine.Story.Tutorial
{
    
    public class StartInTheBedStory : MonoBehaviour
    {
        private void Start()
        {
            LoadFactory.Instance.Complete += InitStory;
        }

        private void InitStory()
        {
            var topPanel = ObjectFinder.TopPanel;
            var dlg = new DialogQueue();
            dlg.Run(() =>
            {
                topPanel.SetActive(false);
            });
            dlg.Text("test1");
            dlg.Text("test2");
            dlg.Text("test3");
            dlg.Text("test4");
            dlg.Run(() =>
            {
                topPanel.SetActive(true);
            });
            ObjectFinder.DialogBox.SetDialogQueueAndRun(dlg.Queue, 0, this);
        }
    }
    
}