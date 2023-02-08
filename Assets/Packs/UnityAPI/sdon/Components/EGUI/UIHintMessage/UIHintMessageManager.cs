using UnityEngine;

namespace Engine.EGUI
{

    public static class UIHintMessageManager
    {

        public static UIHintMessage Show(GameObject prefab, Vector3 pos, string text, float delay = 5f)
        {
            var obj = Object.Instantiate(prefab, ObjectFinder.Canvas.transform);
            var message = obj.GetComponent<UIHintMessage>();

            if(message == null)
            {
                throw new MissingReferenceException();
            }

            message.Position = pos;
            message.Text = text;
            message.Delay = delay;

            return message;
        }
        
        public static UIHintMessage ShowQuestHint(GameObject prefab, Vector3 pos)
        {
            var obj = Object.Instantiate(prefab, ObjectFinder.Canvas.transform);
            var message = obj.GetComponent<UIHintMessage>();

            if(message == null)
            {
                throw new MissingReferenceException();
            }

            message.Position = pos;
            message.Text = "*";
            message.Delay = -1;

            return message;
        }

    }

}
