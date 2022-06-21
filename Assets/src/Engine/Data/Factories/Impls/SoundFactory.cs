using System;
using UnityEngine;

namespace Engine.Data.Factories
{

    /// <summary>
    /// 
    /// Фабрика звуков
    /// Позволяет получать короткие звуковые клипы для воспроизведения, кеширует ранее полученные клипы.
    /// ---
    /// Sound Factory.
    /// Allows you to get short sound clips for playback, caches previously obtained clips.
    /// 
    /// </summary>
    public class SoundFactory : PrefabFactory<AudioClip>
    {

        private static readonly Lazy<SoundFactory> instance = new Lazy<SoundFactory>(() => new SoundFactory());
        public static SoundFactory Instance { get { return instance.Value; } }
        private SoundFactory() { }

        public override string Directory => "Sounds";

    }

}
