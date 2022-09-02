using UnityEngine;

namespace Engine
{

    /// <summary>
    ///
    /// Временный звуковой эффект расположенный в мире (объёмный звук)
    /// ---
    /// Temporary sound effect located in the world (surround sound)
    /// 
    /// </summary>
    public class AudioTimedFragment : MonoBehaviour
    {

        #region Hidden Fields
        
        /// <summary>
        ///     Ссылка на источник звука (проигрыватель)
        ///     ---
        ///     Reference to the sound source (player)
        /// </summary>
        private AudioSource audioSource;

        /// <summary>
        ///     Флаг старта, показывает что воспроизведения клипа уже идёт
        ///     ---
        ///     The start flag shows that the clip is already playing
        /// </summary>
        private bool started;

        #endregion
        
        /// <summary>
        ///     Выполняет инициализацию и воспроизведение звукового эффекта
        ///     ---
        ///     Performs initialization and playback of the sound effect
        /// </summary>
        /// <param name="worldPosition">
        ///     Точка в мире, где воспроизводится звук
        ///     ---
        ///     A point in the world where sound is reproduced
        /// </param>
        /// <param name="audioSource">
        ///     Проигрыватель который будет воспроизводить звук
        ///     ---
        ///     The player that will play the sound
        /// </param>
        /// <param name="clip">
        ///     Сам звук, который надо воспроизвести
        ///     ---
        ///     The sound itself to be played
        /// </param>
        public void Init(Vector3 worldPosition, AudioSource audioSource, AudioClip clip)
        {
            this.transform.position = worldPosition;
            this.audioSource = audioSource;
            this.audioSource.clip = clip;
            this.audioSource.Play();
            this.started = true;
        }

        #region Unity Events
        
        private void Update()
        {
            if (!started)
                return;

            if (audioSource != null && !audioSource.isPlaying) // Следим за проигрывателем, до тех пор, пока звук не закончит воспроизводиться
            {
                started = false;
                audioSource = null;
                GameObject.Destroy(gameObject); // Удаляем звуковой фрагмент (с проигрывателем и с самим объектом)
            }
        }
        
        #endregion

    }

}
