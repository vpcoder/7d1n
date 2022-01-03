using UnityEngine;

namespace Engine
{

    /// <summary>
    /// Временный звуковой эффект расположенный в мире (объёмный звук)
    /// </summary>
    public class AudioTimedFragment : MonoBehaviour
    {

        #pragma warning disable IDE0051

        /// <summary>
        /// Ссылка на проигрыватель
        /// </summary>
        private AudioSource audioSource;

        private bool started = false;

        /// <summary>
        /// Выполняет инициализацию и воспроизведение звукового эффекта
        /// </summary>
        /// <param name="worldPosition">Точка в мире, где воспроизводится звук</param>
        /// <param name="audioSource">Проигрыватель который будет воспроизводить звук</param>
        /// <param name="clip">Сам звук, который надо воспроизвести</param>
        public void Init(Vector3 worldPosition, AudioSource audioSource, AudioClip clip)
        {
            this.transform.position = worldPosition;
            this.audioSource = audioSource;
            this.audioSource.clip = clip;
            this.audioSource.Play();
            this.started = true;
        }

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

    }

}
