using UnityEngine;

namespace Engine.EGUI
{

    public interface IPanel
    {

        GameObject Body { get; }

        void Show();

        void Hide();

        bool Visible { get; set; }

    }

}
