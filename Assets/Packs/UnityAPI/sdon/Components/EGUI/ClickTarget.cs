using UnityEngine.UI;

namespace UnityEngine
{

    [RequireComponent(typeof(CanvasRenderer))]
    public class ClickTarget : Graphic
    {
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }

}
