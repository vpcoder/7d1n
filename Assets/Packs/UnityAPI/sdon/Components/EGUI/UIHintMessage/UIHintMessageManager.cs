using UnityEngine;

namespace Engine.EGUI
{

    public static class UIHintMessageManager
    {

        public static UIHintMessage Show(GameObject prefab, Vector3 pos, string text)
        {
            var canvas = ObjectFinder.Get<Canvas>("Canvas");
            var obj = GameObject.Instantiate<GameObject>(prefab, canvas.transform);

            var message = obj.GetComponent<UIHintMessage>();

            if(message == null)
            {
                throw new MissingReferenceException();
            }

            message.Position = pos;
            message.Text = text;

            return message;
        }

    }

}
