using System.Collections.Generic;
using Engine.Logic.Base.Impls;
using Engine.Logic.Dialog.Action.Impls;

namespace Engine.Logic.Dialog
{
    
    public class AnswerBox : VerticalListWidget<AnswerItem, SelectVariant>
    {

        private ICollection<SelectVariant> variants;
        
        public void CreateVariantsUI(ICollection<SelectVariant> variants)
        {
            this.variants = variants;
            Show();
        }
        
        protected override void HideDestruct()
        {
            base.HideDestruct();
            Destroy(gameObject);
        }

        public override ICollection<SelectVariant> CreateModels()
        {
            return variants;
        }

    }
    
}