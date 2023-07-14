using UnityEngine;

namespace UnityEditor.Menu
{
    
    public static class PositionActionMenuItem
    {

        private static float WEB_SIZE = 5f;

        private static float TransformByWeb(float value, float webSize)
        {
            int count = Mathf.RoundToInt(value / webSize);
            return count * webSize;
        }

        private static Vector3 TransformByWeb(Vector3 value, float webSize)
        {
            return new Vector3(
                TransformByWeb(value.x, webSize),
                TransformByWeb(value.y, webSize),
                TransformByWeb(value.z, webSize)
            );
        }
        
        [MenuItem("7d1n/Position/Web Align")]
        public static void WebAlignAction()
        {
            var selected = Selection.activeGameObject;
            if(selected == null)
                return;
            selected.transform.position = TransformByWeb(selected.transform.position, WEB_SIZE);
        }
        
    }
    
}