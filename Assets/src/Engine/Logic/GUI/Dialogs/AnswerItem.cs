using Engine.Logic.Base;
using Engine.Logic.Dialog.Action.Impls;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Dialog
{
    
    public class AnswerItem : ListItem<SelectVariant>
    {

        [SerializeField] private Text textField;
        
        public override void Construct(SelectVariant variant)
        {
            base.Construct(variant);
            textField.text = variant.Text;
        }
        
        public void OnClick()
        {
            ObjectFinder.DialogBox.SelectVariant(Model);
        }

        public override void Destruct()
        { }
        
    }
    
}