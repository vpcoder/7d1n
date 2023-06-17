using UnityEditor;
using UnityEngine;

namespace Engine
{
    
    [CustomEditor(typeof(AudioBehaviour), true)]
    public class AudioBehaviourEditor : CustomEditorT<AudioBehaviour>
    {

        public override void OnAdditionEditor()
        {
            var data = target.Target.FadeData;
            if (data == null)
                return;

            var source = target.Target.Source;
            if (GUILayout.Button("save to fade data"))
                data.DownloadFromSource(source);

            if (GUILayout.Button("load to fade data"))
                data.UploadToSource(source);

        }
        
    }
    
}