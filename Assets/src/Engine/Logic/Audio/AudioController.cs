using Engine.Data.Factories;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Engine
{

    /// <summary>
    /// 
    /// Аудиоконтроллер, позволяющий воспроизводить звуки и музыку в мире
    /// ---
    /// Audio controller that allows you to play sounds and music in the world
    /// 
    /// </summary>
    public class AudioController
    {

        #region Singleton
        private static readonly Lazy<AudioController> instance = new Lazy<AudioController>(() => new AudioController());
        public static AudioController Instance { get { return instance.Value; } }
        private AudioController() { }
        #endregion

        #region Hidden Fields
        
        /// <summary>
        ///     Общий проигрывать для звуков
        ///     ---
        ///     General playback for sounds
        /// </summary>
        private AudioSource sounds;
        
        /// <summary>
        ///     Общий проигрыватель для музыки
        ///     ---
        ///     Shared music player
        /// </summary>
        private AudioSource musics;

        /// <summary>
        ///     Словаь миксеров, с отстроенными параметрами воспроизведения
        ///     ---
        ///     Word mixers, with tuned playback parameters
        /// </summary>
        private readonly IDictionary<MixerType, AudioMixerGroup> mixers = new Dictionary<MixerType, AudioMixerGroup>();

        #endregion
        
        public void PlaySound(AudioSource source, AudioClip sound)
        {
            if (sound == null)
                Debug.LogError("null");

            source.outputAudioMixerGroup = GetMixer(MixerType.Sounds);
            source.clip = sound;
            source.Play();
        }

        public void PlaySound(AudioClip sound)
        {
            if (sounds == null)
                sounds = InitSounds();

            sounds.clip = sound;
            sounds.Play();
        }

        public void PlaySound(AudioSource source, string name)
        {
            PlaySound(source, SoundFactory.Instance.Get(name));
        }

        public void PlaySound(string name)
        {
            PlaySound(SoundFactory.Instance.Get(name));
        }

        private AudioSource InitSounds()
        {
            var audio = GetAudio();
            Transform sounds = audio.transform.Find("Sounds");
            if (sounds == null)
            {
                var soundsObj = new GameObject("Sounds");
                sounds = soundsObj.transform;
                sounds.SetParent(audio.transform);
                var source = soundsObj.AddComponent<AudioSource>();
                source.outputAudioMixerGroup = GetMixer(MixerType.Sounds);
                return source;
            }

            return sounds.gameObject.GetComponent<AudioSource>();
        }

        public void PlayMusic(AudioClip music)
        {
            if (musics == null)
                musics = InitMusics();

            musics.clip = music;
            musics.Play();
        }

        public void PlayMusic(string name)
        {
            PlayMusic(MusicFactory.Instance.Get(name));
        }

        public void PlayMusic()
        {
            musics.Play();
        }

        public void PauseMusic()
        {
            musics.Pause();
        }

        public void StopMusic()
        {
            musics.Stop();
        }

        private AudioSource InitMusics()
        {
            var audio = GetAudio();
            Transform musics = audio.transform.Find("Musics");
            if (musics == null)
            {
                var soundsObj = new GameObject("Musics");
                musics = soundsObj.transform;
                musics.SetParent(audio.transform);
                var source = soundsObj.AddComponent<AudioSource>();
                source.outputAudioMixerGroup = GetMixer(MixerType.Musics);
                return source;
            }

            return musics.gameObject.GetComponent<AudioSource>();
        }

        private GameObject GetAudio()
        {
            return GameObject.Find("Audio") ?? new GameObject("Audio");
        }

        /// <summary>
        ///     Получение миксера по его типу.
        ///     Миксеры инициализируются настройками звука, из меню настроек, которое конфигурированно игроком.
        ///     В первую очередь миксер ищется в кеше, если его там нет, инициализируется и кешируется.
        ///     ---
        ///     Getting a mixer by its type.
        ///     Mixers are initialized with sound settings, from the settings menu, which is configured by the player
        ///     The mixer is first searched for in the cache; if it is not there, it is initialized and cached.
        /// </summary>
        /// <param name="type">
        ///     Тип миксера, который необходимо получить
        ///     ---
        ///     Type of mixer to be obtained
        /// </param>
        /// <returns>
        ///     Инициализированный экземпляр миксера.
        ///     ---
        ///     Initialized mixer instance.
        /// </returns>
        /// <exception cref="NotSupportedException">
        ///     Данный тип миксера не поддерживается
        ///     ---
        ///     This type of mixer is not supported
        /// </exception>
        public AudioMixerGroup GetMixer(MixerType type)
        {
            if (mixers.TryGetValue(type, out AudioMixerGroup master))
                return master;

            var mixer = Resources.Load<AudioMixer>(type.ToString());
            master = mixer.FindMatchingGroups("Master")[0];
            mixer.SetFloat("Volume", GetExpLevel(type));

            mixers[type] = master;
            return master;
        }

        private float GetLevel(MixerType type)
        {
            switch (type)
            {
                case MixerType.Sounds:
                    return GameSettings.Instance.Settings.SoundsEnabled ? GameSettings.Instance.Settings.SoundsVolume : 0;
                case MixerType.Musics:
                    return GameSettings.Instance.Settings.MusicsEnabled ? GameSettings.Instance.Settings.MusicsVolume : 0;
                default:
                    throw new NotSupportedException();
            }
        }

        public float GetExpLevel(MixerType type)
        {
            float level = GetLevel(type);
            if (level > 100)
                level = 100;
            if (level <= 0)
                level = 0.0001f;
            else
                level = Mathf.Log10(level / 100f) * 20f;
            return level;
        }
        
        public float GetLowLevel(MixerType type)
        {
            return GetLevel(type) / 100f;
        }

        public AudioTimedFragment CreateTimedFragment(Vector3 worldPosition, MixerType type, string sound)
        {
            var obj = new GameObject();
            var fragment = obj.AddComponent<AudioTimedFragment>();
            var source = CreateAudioSource(obj, type);
            fragment.Init(worldPosition, source, SoundFactory.Instance.Get(sound));
            return fragment;
        }

        public AudioSource CreateAudioSource(GameObject parent, MixerType type)
        {
            var source = parent.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = GetMixer(type);
            return source;
        }

    }

}
