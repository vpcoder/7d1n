using Engine.Data.Factories;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Engine
{

    public class AudioController
    {

        #region Singleton
        private static readonly Lazy<AudioController> instance = new Lazy<AudioController>(() => new AudioController());
        public static AudioController Instance { get { return instance.Value; } }
        private AudioController() { }
        #endregion

        private AudioSource sounds;
        private AudioSource musics;

        private readonly IDictionary<MixerType, AudioMixerGroup> mixers = new Dictionary<MixerType, AudioMixerGroup>();

        public void PlaySound(AudioSource source, AudioClip sound)
        {
            if (sound == null)
                Debug.LogError("null");

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

        public AudioMixerGroup GetMixer(MixerType type)
        {
            if (mixers.TryGetValue(type, out AudioMixerGroup master))
                return master;

            var mixer = Resources.Load<AudioMixer>(type.ToString());
            master = mixer.FindMatchingGroups("Master")[0];

            float level;
            switch (type)
            {
                case MixerType.Sounds:
                    level = GameSettings.Instance.Settings.SoundsEnabled ? GameSettings.Instance.Settings.SoundsVolume : 0;
                    break;
                case MixerType.Musics:
                    level = GameSettings.Instance.Settings.MusicsEnabled ? GameSettings.Instance.Settings.MusicsVolume : 0;
                    break;
                default:
                    throw new NotSupportedException();
            }
            if (level > 100)
                level = 100;
            if (level <= 0)
                level = 0.0001f;
            else
                level = Mathf.Log10(level / 100f) * 20f;
            mixer.SetFloat("Volume", level);

            mixers[type] = master;
            return master;
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
