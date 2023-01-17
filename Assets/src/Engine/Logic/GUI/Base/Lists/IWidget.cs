using UnityEngine;

namespace Engine.Logic.Base
{
    public interface IWidget
    {
        bool Visible { get; set; }
        RectTransform Rect { get; }
        GameObject Body { get; }


        void Show();

        void Hide();
    }
}