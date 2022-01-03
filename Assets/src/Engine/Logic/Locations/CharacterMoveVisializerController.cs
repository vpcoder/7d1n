using Engine.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Визуализация перемещений персонажа игрока
    /// </summary>
    public class CharacterMoveVisializerController : MonoBehaviour
    {

        [SerializeField] private LineRenderer activeLine;
        [SerializeField] private LineRenderer errorLine;
        [SerializeField] private Text txtPathCost;
        [SerializeField] private Text txtActivePathCost;

        /// <summary>
        /// Формирует и отображает линейку с затратами ОД за передвижение
        /// </summary>
        /// <param name="path">Путь который надо визуализировать</param>
        /// <param name="ap">Число доступных ОД</param>
        public SmartPath ShowPath(List<Vector3> path, int ap)
        {
            var result = PathHelper.GetSmartPath(path, ap);
            if (result.EdgePoint.IsFullPath)
            {
                txtPathCost.enabled = false;
                txtActivePathCost.enabled = true;
                txtActivePathCost.text = result.ActivePathAP.ToString();
                txtActivePathCost.transform.position = path[path.Count - 1];

                activeLine.positionCount = path.Count;
                activeLine.SetPositions(path.ToArray());

                errorLine.positionCount = 0;
                errorLine.SetPositions(Arrays<Vector3>.Empty);
            }
            else
            {
                txtPathCost.enabled = true;
                txtActivePathCost.enabled = true;

                txtActivePathCost.transform.position = result.ErrorPath[0];
                txtPathCost.transform.position = result.ErrorPath[result.ErrorPath.Count - 1];
                txtActivePathCost.text = result.ActivePathAP.ToString();
                txtPathCost.text = result.FullPathAP.ToString();

                activeLine.positionCount = result.ActivePath.Count;
                activeLine.SetPositions(result.ActivePath.ToArray());
                errorLine.positionCount = result.ErrorPath.Count;
                errorLine.SetPositions(result.ErrorPath.ToArray());
            }
            return result;
        }

        /// <summary>
        /// Прячет ранее сформированный и отрисованный путь
        /// </summary>
        public void HidePath()
        {
            txtPathCost.enabled = false;
            txtActivePathCost.enabled = false;
            activeLine.positionCount = 0;
            activeLine.SetPositions(Arrays<Vector3>.Empty);
            errorLine.positionCount = 0;
            errorLine.SetPositions(Arrays<Vector3>.Empty);
        }

        private void FixedUpdate()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Game.Instance.Runtime.BattleContext.OrderIndex != EnemyGroup.PlayerGroup) // Не ход игрока?
                return;

            if (Game.Instance.Runtime.ActionMode != ActionMode.Move)
                return;

            if (DeviceInput.TouchCount != 1)
                return;

            var battleManager = ObjectFinder.Find<BattleManager>();
            var battleActions = battleManager.BattleActions;

            if (battleActions.AttackContext.AttackMarker != null)
                return;

            var point = DeviceInput.TouchPosition;
            var ray = Camera.main.ScreenPointToRay(point);

            var character = ObjectFinder.Find<LocationCharacter>();

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                var nextMovePoint = hit.point;
                var path = new NavMeshPath();
                if (character.Agent.CalculatePath(nextMovePoint, path))
                {
                    var pathInfo = ShowPath(path.corners.ToList(), Game.Instance.Runtime.BattleContext.CurrentCharacterAP);
                    UpdateActionInfo(pathInfo);
                }
                else
                {
                    HidePath();
                }
            }
        }

        private void UpdateActionInfo(SmartPath pathInfo)
        {
            var needAp = pathInfo.ActivePathAP;

            var battleManager = ObjectFinder.Find<BattleManager>();
            var battleActions = battleManager.BattleActions;

            if (battleActions.AttackContext.AttackMarker != null)
                return;


            battleActions.NeedAP = needAp;

            battleActions.Show(); // Отображаем панель действия, чтобы пользователь сказал - совершать его или нет
            battleActions.Action = BattleAction.Move;
            battleActions.MoveContext.Points = pathInfo.ActivePath;

            battleActions.UpdateState();

            var handsActionsController = ObjectFinder.Find<HandsActionsController>();
            handsActionsController.DoUnselectActions();
            handsActionsController.HideActions();
        }

    }

}
