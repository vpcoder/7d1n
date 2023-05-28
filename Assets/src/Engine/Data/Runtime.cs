using Engine.Logic.Locations.Generator;
using Engine.Scenes;
using System;
using UnityEngine;

namespace Engine.Data
{

    public class Runtime
    {

        public long PlayerID { get; set; }
        public bool BattleFlag { get; set; }
        public Mode Mode { get; set; } = Mode.Game;
        public ActionMode ActionMode { get; set; } = ActionMode.Move;

        /// <summary>
        ///     Возвращает текущую сцену
        ///     ---
        ///     Returns the current scene
        /// </summary>
        public SceneName Scene
        {
            get
            {
                var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
                try
                {
                    var value = Enums<SceneName>.Parse(scene.name);
                    return value;
                }
                catch (Exception ex)
                {
                    Debug.LogError("scene '" + scene.name + "' is not exists in enum!");
                    throw ex;
                }
            }
        }

        public Generator.LocationInfo Location { get; set; } = new Generator.LocationInfo();
        public BuildLocationInfo GenerationInfo { get; set; } = new BuildLocationInfo();

        public Vector3 PlayerPosition { get; set; } = Vector3.zero;
        public Vector3 CharacterPosition { get; set; } = Vector3.zero;

        public MoveContext   PlayerContext { get; set; } = new MoveContext();
        public MoveContext   CharacterContext { get; set; } = new MoveContext();
        public BattleContext BattleContext { get; } = new BattleContext();

        public int CurrentCellX { get; set; }
        public int CurrentCellY { get; set; }

    }

}
