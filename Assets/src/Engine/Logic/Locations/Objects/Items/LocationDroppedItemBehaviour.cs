using Engine.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.Logic.Locations.Objects
{

    /// <summary>
    /// 
    /// Предмет, выкинутый в локации
    /// Имеет все свойства и параметры предмета, по которому был создан этот объект.
    /// Выкинутый объект будет падать на пол, за счёт создаваемого RigidBody, однако, когда объект стабилизируется и успокоится,
    /// RigidBody будет удалён. Если же объект не успеет стабилизироваться за отведённый таймаут, RigidBody будет удалён принудительно.
    /// ---
    /// The item thrown in the location
    /// Has all the properties and parameters of the object by which the object was created.
    /// The thrown object will fall to the floor, due to the RigidBody created, however, when the object stabilizes and calms down,
    /// RigidBody will be removed. If the object does not have time to stabilize in the allotted timeout, the RigidBody will be forcibly removed.
    /// 
    /// </summary>
    [RequireComponent(typeof(BoxCollider))]
    public class LocationDroppedItemBehaviour : ItemBehaviour, ILocationDroppedItem
    {

        #region Consts

        /// <summary>
        ///     Максимальное количество хранимых позиций для расчёта текущего коэффициента стабильности
        ///     ---
        ///     Maximum number of stored positions to calculate the current stability coefficient
        /// </summary>
        private const int MAX_POSITIONS_COUNT = 10;

        /// <summary>
        ///     Время ожидания между итерациями по рассчёту стабильности предмета
        ///     ---
        ///     The waiting time between iterations of the item stability calculation
        /// </summary>
        private const float ITERATION_WAIT = 0.2f;

        #endregion

        #region Hidden Fields

        /// <summary>
        ///     Ссылка на тело объекта (меш предмета)
        ///     ---
        ///     Reference to the object body (object mash)
        /// </summary>
        [SerializeField] private GameObject body;

        /// <summary>
        ///     Максимальное время ожидания "спокойствия" позиции выкинутого предмета
        ///     ---
        ///     Maximum waiting time for the "calm" position of the thrown object
        /// </summary>
        [SerializeField] private float dropTimeout = 5f;

        /// <summary>
        ///     Средняя разница между позициями предмета, считаемая за разницу "спокойствия объекта" в пространстве
        ///     ---
        ///     The average difference between the positions of the object, counted as the difference of the "calmness of the object" in space
        /// </summary>
        [SerializeField] private float maxStabilityDelta = 0.001f;

        /// <summary>
        ///     Ссылка на созданный временный RigidBody
        ///     ---
        ///     Link to the created temporary RigidBody
        /// </summary>
        private Rigidbody rigidBody;

        /// <summary>
        ///     Время когда предмет был выкинут из сумки
        ///     ---
        ///     Time when the item was thrown out of the bag
        /// </summary>
        private float dropTime;

        /// <summary>
        ///     Время последней итерации
        ///     ---
        ///     Time of the last iteration
        /// </summary>
        private float lastIterationTimestamp;

        /// <summary>
        ///     Список последних позиций предмета после выбрасываний, по которому будим определять стабильность и спокойствие
        ///     ---
        ///     A list of the last positions of the item after discards, by which we will determine stability and calmness
        /// </summary>
        private List<Vector3> lastPositionsList;

        /// <summary>
        ///     Флаг стабильности и спокойствия положения объекта
        ///     ---
        ///     Flag of stability and calmness of the object's position
        /// </summary>
        private bool isStable = true;

        #endregion

        #region Unity Events

        private void Update()
        {
            if (isStable) // Не работаем со стабильным предметом
                return;

            var timestamp = Time.time;
            if(timestamp - dropTime >= dropTimeout)
            {
                DoStability();
                return;
            }

            if (Time.time - lastIterationTimestamp < ITERATION_WAIT)
                return;

            lastIterationTimestamp = Time.time;
            lastPositionsList.Add(transform.position);

            if (lastPositionsList.Count < MAX_POSITIONS_COUNT)
                return;

            if (lastPositionsList.Count > MAX_POSITIONS_COUNT)
                lastPositionsList.RemoveAt(0);

            DoStabilityIteration();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Инициализирует выкинутый предмет в локации
        ///     ---
        ///     Initializes a discarded item in the location
        /// </summary>
        /// <param name="item">
        ///     Сериализованные данные о предмете
        ///     ---
        ///     Serialized item data
        /// </param>
        /// <param name="worldPosition">
        ///     Положение выброшенного предмета в мире
        ///     ---
        ///     The position of the discarded object in the world
        /// </param>
        /// <param name="dropWithRandomPos">
        ///     Корректировать позицию выбрасывания случайно
        ///     ---
        ///     Adjust the ejection position randomly
        /// </param>
        public void Init(ItemInfo itemInfo, Vector3 worldPosition, bool dropWithRandomPos)
        {
            this.ItemInfo = itemInfo;

            if (dropWithRandomPos)
            {
                // К положению в мире добавляем рандомное смещение в радиусе 0.5ед., чтобы сымитировать небрежное выкидывание предмета
                this.transform.position = worldPosition + Random.insideUnitCircle.ToVector3() * 0.5f + Vector3.up * 1.5f;
            }
            else
            {
                // Оставляем позицию
                this.transform.position = worldPosition;
            }

            // Случайно вращаем предмет, типо он так упал
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 360f)));
            
            // Имитация бросания предмета физикой твёрдого тела
            this.rigidBody = gameObject.AddComponent<Rigidbody>();
            this.dropTime = Time.time;
            this.lastIterationTimestamp = Time.time;
            this.lastPositionsList = new List<Vector3>(MAX_POSITIONS_COUNT + 1);

            this.isStable = false; // По умолчанию после выбрасывания предмета считаем что он нестабилен.
            // Нестабильность означает что предмет может падать, кататься по полу, закатываться в какие нибудь уголки локации,
            // до тех пор, пока он не остановится на одном месте и перестанет достаточно интенсивно двигаться (чувствительность настраивается maxStabilityDelta)
            // если предмет не останавливается довольно продолжительное время, сработает таймаут стабилизации, предмет принудительно зависнет в водухе, и стабилизируется.
        }

        /// <summary>
        ///     Итерация операции расчёта стабильности предмета.
        ///     Если предмет достаточно стабилен (чувствительность настраивается maxStabilityDelta), предмет замирает, RigidBody удаляется
        ///     ---
        ///     Iterate the operation of calculating the stability of the object.
        ///     If the object is stable enough (sensitivity is set by maxStabilityDelta), the object freezes, RigidBody is removed
        /// </summary>
        private void DoStabilityIteration()
        {
            var currentDelta = CalculateStabilityDelta();
            if (currentDelta <= maxStabilityDelta)
                DoStability();
        }

        /// <summary>
        ///     Выполняет расчёт разности положений, чтобы определить уровень стабильности
        ///     ---
        ///     Performs a position difference calculation to determine the level of stability
        /// </summary>
        /// <returns>
        ///     Возвращает сумму разностей положений объекта в пространстве
        ///     ---
        ///     Returns the sum of the differences of the object positions in space
        /// </returns>
        private float CalculateStabilityDelta()
        {
            float sumDeltas = 0;
            for (int i = 0; i < MAX_POSITIONS_COUNT - 1; i++)
            {
                float currentDelta = Vector3.Distance(lastPositionsList[i], lastPositionsList[i + 1]);
                sumDeltas += Mathf.Abs(currentDelta);
            }
            return sumDeltas / MAX_POSITIONS_COUNT;
        }

        /// <summary>
        ///     Применяет стабилизацию, удаляет RigidBody
        ///     ---
        ///     Applies stabilization, removes RigidBody
        /// </summary>
        private void DoStability()
        {
            if (isStable)
                return;

            isStable = true;
            Destroy(this.rigidBody);
            lastPositionsList.Clear();
        }

        #endregion
        
        #if UNITY_EDITOR
        
        [ContextMenu("Move Mesh to Body")]
        private void DoMoveMeshToBody()
        {
            var bodyName = "Body";
            var body = transform.Childs().Where(item => item.name == bodyName).FirstOrDefault();
            if (body == null)
            {
                body = new GameObject(bodyName).transform;
                body.SetParent(transform);
            }

            this.body = body.gameObject;

            if (transform.GetComponent<BoxCollider>() == null)
                gameObject.AddComponent<BoxCollider>();

            var meshCollider = transform.GetComponent<MeshCollider>();
            if(meshCollider != null)
                DestroyImmediate(meshCollider);
            
            var meshFilter = transform.GetComponent<MeshFilter>();
            var meshRenderer = transform.GetComponent<MeshRenderer>();

            if (meshFilter != null)
            {
                var bodyMeshFilter = body.GetComponent<MeshFilter>();
                if (bodyMeshFilter == null)
                    bodyMeshFilter = body.gameObject.AddComponent<MeshFilter>();
                bodyMeshFilter.sharedMesh = meshFilter.sharedMesh;
                DestroyImmediate(meshFilter);
            }
            
            if (meshRenderer != null)
            {
                var bodyMeshRenderer = body.GetComponent<MeshRenderer>();
                if (bodyMeshRenderer == null)
                    bodyMeshRenderer = body.gameObject.AddComponent<MeshRenderer>();
                bodyMeshRenderer.sharedMaterials = meshRenderer.sharedMaterials;
                bodyMeshRenderer.shadowCastingMode = meshRenderer.shadowCastingMode;
                bodyMeshRenderer.receiveShadows = meshRenderer.receiveShadows;
                bodyMeshRenderer.lightProbeUsage = meshRenderer.lightProbeUsage;
                bodyMeshRenderer.lightProbeProxyVolumeOverride = meshRenderer.lightProbeProxyVolumeOverride;
                bodyMeshRenderer.additionalVertexStreams = meshRenderer.additionalVertexStreams;
                bodyMeshRenderer.enlightenVertexStream = meshRenderer.enlightenVertexStream;
                bodyMeshRenderer.receiveGI = meshRenderer.receiveGI;
                bodyMeshRenderer.scaleInLightmap = meshRenderer.scaleInLightmap;
                bodyMeshRenderer.stitchLightmapSeams = meshRenderer.stitchLightmapSeams;
                bodyMeshRenderer.motionVectorGenerationMode = meshRenderer.motionVectorGenerationMode;
                DestroyImmediate(meshRenderer);
            }
        }
        
        #endif

    }

}
