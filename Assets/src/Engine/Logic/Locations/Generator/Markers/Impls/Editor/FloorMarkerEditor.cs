using UnityEditor;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Markers
{
    
    [CustomEditor(typeof(FloorMarker), true)]
    public class FloorMarkerEditor : CustomEditorT<FloorMarker>
    {

        public override void OnAdditionEditor()
        {
            if(GUILayout.Button("s00"))
            {
                if (target.Target.Segments.ContainsKey(TileSegmentType.S00))
                    target.Target.Segments.Remove(TileSegmentType.S00);
                else
                    target.Target.Segments.Add(TileSegmentType.S00, Color.magenta);
            }
            if(GUILayout.Button("s01"))
            {
                if (target.Target.Segments.ContainsKey(TileSegmentType.S01))
                    target.Target.Segments.Remove(TileSegmentType.S01);
                else
                    target.Target.Segments.Add(TileSegmentType.S01, Color.yellow);
            }
            if(GUILayout.Button("s10"))
            {
                if (target.Target.Segments.ContainsKey(TileSegmentType.S10))
                    target.Target.Segments.Remove(TileSegmentType.S10);
                else
                    target.Target.Segments.Add(TileSegmentType.S10, Color.red);
            }
            if(GUILayout.Button("s11"))
            {
                if (target.Target.Segments.ContainsKey(TileSegmentType.S11))
                    target.Target.Segments.Remove(TileSegmentType.S11);
                else
                    target.Target.Segments.Add(TileSegmentType.S11, Color.green);
            }
        }
        
    }
    
}