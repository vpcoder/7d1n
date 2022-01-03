using UnityEngine;

namespace Engine.Data.Factories
{

    /// <summary>
    /// Фабрика звуков
    /// </summary>
    public class SoundFactory : PrefabFactory<AudioClip>
    {

        private static readonly Lazy<SoundFactory> instance = new Lazy<SoundFactory>(() => new SoundFactory());
        public static SoundFactory Instance { get { return instance.Value; } }
        private SoundFactory() { }

        public override string Directory => "Sounds";

    }

}
