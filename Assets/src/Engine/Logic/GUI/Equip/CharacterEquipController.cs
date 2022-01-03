using Engine.EGUI;
using UnityEngine;

namespace Engine.Logic
{

    public class CharacterEquipController : Panel
    {

        public override void Show()
        {
            base.Show();
            ObjectFinder.Find<CharacterStateController>().UpdateInfo();
            ObjectFinder.Find<CharacterClothCellController>().UpdateInfo();
        }

    }

}
