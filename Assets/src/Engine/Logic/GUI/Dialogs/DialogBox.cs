using System.Collections.Generic;
using Engine.Data;
using Engine.Logic.Base;
using Engine.Logic.Dialog.Action;
using Engine.Logic.Dialog.Action.Impls;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Dialog
{
    public class DialogBox : Widget
    {
        [SerializeField] private AnswerBox answerBoxPrefab;

        [SerializeField] private Text textField;

        [SerializeField] private Image firstAvatar;

        [SerializeField] private Image secondAvatar;

        [SerializeField] private Sprite emptyAvatar;

        private AnswerBox answerBox;

        private readonly DialogRuntime runtime = new DialogRuntime();
        private float lastTimestamp;

        public object Locker { get; set; }

        public DialogRuntime Runtime => runtime;

        public void SelectVariant(SelectVariant variant)
        {
            if (answerBox != null)
            {
                answerBox.Hide();
                answerBox = null;
            }

            runtime.TryGoTo(variant.GoTo);
            runtime.Run();
        }

        public void SetVariants(ICollection<SelectVariant> variants)
        {
            answerBox = Instantiate(answerBoxPrefab, ObjectFinder.Canvas.transform);
            answerBox.CreateVariantsUI(variants);
        }

        public void SetText(string text)
        {
            if (text == null)
                text = "";
            textField.text = text;
        }

        public void SetFirstAvatar(Sprite avatar)
        {
            firstAvatar.sprite = avatar == null ? emptyAvatar : avatar;
        }

        public void SetSecondAvatar(Sprite avatar)
        {
            secondAvatar.sprite = avatar == null ? emptyAvatar : avatar;
        }

        protected override void HideDestruct()
        {
#if UNITY_EDITOR && DEBUG && DIALOG_DEBUG
            Debug.Log("hide dialog");
#endif

            firstAvatar.sprite = emptyAvatar;
            secondAvatar.sprite = emptyAvatar;
            Locker = null;
            Game.Instance.Runtime.Mode = Mode.Game;
        }

        public void SetDialogQueueAndRun([NotNull] IEnumerable<IActionCommand> dialogQueue, int startIndex, object locker)
        {
            if (Locker != null)
            {
                Debug.LogError("can't run '" + locker.GetType().FullName + "', locker '" + Locker.GetType().FullName +
                               "' already executing and not ended!");
                return;
            }

            Locker = locker;
            Game.Instance.Runtime.Mode = Mode.Dialog;

            Show();
            runtime.SetDialogQueueAndRun(this, dialogQueue, startIndex);
            runtime.Run();
        }

        public void NextAction()
        {
            if (runtime.IsWaitAnswer)
            {
                return;
            }
            if (runtime.IsEnd)
            {
                Hide();
                return;
            }

            var action = runtime.CurrentAction;
            if (action == null)
            {
                Hide();
                return;
            }
            if (runtime.IsWaitTime && Time.time - action.StartTime < action.WaitTime)
            {
                return;
            }
            runtime.Next();
            runtime.Run();
        }

        private void Update()
        {
            var delta = Time.time - lastTimestamp;
            if(delta < 0.1f)
                return;
            lastTimestamp = Time.time;

            if(runtime.IsEnd || !runtime.IsWaitTime)
                return;

            var action = runtime.CurrentAction;
            if (Time.time - action?.StartTime >= action?.WaitTime)
            {
                NextAction();
            }
        }
    }
}