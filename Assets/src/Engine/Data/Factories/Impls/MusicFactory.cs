using System;
using UnityEngine;

namespace Engine.Data.Factories
{

    /// <summary>
    /// Фабрика музыки
    /// </summary>
    public class MusicFactory : PrefabFactory<AudioClip>
    {

        private static readonly Lazy<MusicFactory> instance = new Lazy<MusicFactory>(() => new MusicFactory());
        public static MusicFactory Instance { get { return instance.Value; } }
        private MusicFactory() { }

        public override string Directory => "Musics";

    }

}
