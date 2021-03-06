using Engine.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Engine.Logic.Locations.Battle.Actions;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Визуализация перемещений персонажа игрока
    /// ---
    /// Visualization of player character movements
    /// 
    /// </summary>
    public class CharacterMoveVisializerController : MonoBehaviour,
                                                     IDragHandler,
                                                     IPointerDownHandler
    {

        #region Hidden Fields

        /// <summary>
        ///     Ломаная линия активного пути - пути, который персонаж сможет преодолеть за доступные ОД
        ///     ---
        ///     The line of the active path - the path that the character will be able to overcome for the available APs
        /// </summary>
        [SerializeField] private LineRenderer activeLine;

        /// <summary>
        ///     Ломаная линия недостижимого пути - пути, на который недостаточно ОД
        ///     ---
        ///     The line of an unattainable path, a path for which there is insufficient APs
        /// </summary>
        [SerializeField] private LineRenderer errorLine;

        /// <summary>
        ///     Общая стоимость пути в ОД
        ///     ---
        ///     Total cost of path in APs
        /// </summary>
        [SerializeField] private Text txtPathCost;

        /// <summary>
        ///     Стоимость достижимой части пути за доступные ОД
        ///     ---
        ///     The cost of the achievable part of the path for the available APs
        /// </summary>
        [SerializeField] private Text txtActivePathCost;

        #endregion

        #region Methods

        /// <summary>
        ///     Формирует и отображает линейку с затратами ОД за передвижение
        ///     ---
        ///     Forms and displays the ruler with AP costs for movement
        /// </summary>
        /// <param name="path">
        ///     Путь который надо визуализировать
        ///     ---
        ///     The path to visualize
        /// </param>
        /// <param name="ap">
        ///     Число доступных ОД
        ///     ---
        ///     Number of available APs
        /// </param>
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
        ///     Прячет ранее сформированный и отрисованный путь
        ///     ---
        ///     Hides the previously formed and drawn path
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

        /// <summary>
        ///     Обновляет информацию о пути и затратах при совершении этого пути
        ///     ---
        ///     Updates information about the path and costs when making this path
        /// </summary>
        /// <param name="pathInfo">
        ///     Информация о пути
        ///     ---
        ///     Path information
        /// </param>
        private void UpdateActionInfo(SmartPath pathInfo)
        {
            var needAp = pathInfo.ActivePathAP;

            var battleManager = ObjectFinder.Find<BattleManager>();
            var battleActions = battleManager.BattleActions;

            if (battleActions.AttackContext.AttackMarker != null)
                return;

            battleActions.NeedAP = needAp;

            battleActions.Show(); // Отображаем панель действия, чтобы пользователь сказал - совершать его или нет
            battleActions.Action = CharacterBattleAction.Move;
            battleActions.MoveContext.Points = pathInfo.ActivePath;

            battleActions.UpdateState();

            var handsActionsController = ObjectFinder.Find<HandsActionsController>();
            handsActionsController.DoUnselectActions();
            handsActionsController.HideActions();
        }

        #endregion

        #region Unity Events

        private void CheckEvents()
        {
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

        public void OnDrag(PointerEventData eventData)
        {
            CheckEvents();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            CheckEvents();
        }

        #endregion

    }

}
