using System.Text;
using UnityEngine;

namespace Engine.Data.Stories
{

    /// <summary>
    /// Прокси для хранилища на основе файлов
    /// </summary>
    /// <typeparam name="T">Тип объекта в хранилище</typeparam>
    public abstract class FileStoryProxyBase<T> : StoryProxyBase<T> where T : class, IStoryObject
    {

        public abstract string DirName { get; }

        public abstract string FileName { get; }

        private string Folder
        {
            get
            {
                var path = Application.persistentDataPath + "/" + DirName;
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);
                return path;
            }
        }

        private string CreatePath(long index)
        {
            return Folder + "/" + FileName + "_" + index;
        }

        /// <summary>
        /// Создаёт объект по умолчанию, ранее не существовавший
        /// </summary>
        public abstract T CreateDefault();

        /// <summary>
        /// Удаляет объект из хранилища по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор удаляемого объекта</param>
        public override void Delete(long id)
        {
            var path = CreatePath(id);
            if(System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }

        /// <summary>
        /// Проверяет, существует ли объект с указанным идентификатором в хранилище
        /// </summary>
        /// <param name="id">Идентификатор проверяемого на существование объекта</param>
        /// <returns>Возвращает логическое значение "существует ли указанный объект в хранилище?"</returns>
        public override bool Exists(long id)
        {
            var path = CreatePath(id);
            return System.IO.File.Exists(path);
        }

        /// <summary>
        /// Достаёт предмет из хранилища по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор доставаемого предмета</param>
        /// <returns>Возвращает объект из хранилища</returns>
        public override T Get(long id)
        {
            T item = Read(id);
            if (item == null)
            {
                item = CreateDefault();
                item.ID = id;
            }
            return item;
        }

        /// <summary>
        /// Умное сохранение объекта в хранилище.
        /// Если объект уже существовал, то обновятся его параметры в хранилище.
        /// Если его не было, то добавится объект в хранилище.
        /// </summary>
        /// <param name="storyObject">Сохраняемый объект</param>
        public override void Save(T storyObject)
        {
            Write(storyObject);
        }

        private T Read(long index)
        {
            string data = ReadFile(index);
            if (data == null)
                return null;

            return JsonUtility.FromJson<T>(data);
        }

        private void Write(T item)
        {
            string data = JsonUtility.ToJson(item);
            WriteFile(item.ID, data); ;
        }

        private string ReadFile(long index)
        {
            var path = CreatePath(index);
            if (!System.IO.File.Exists(path))
                return null;
            return System.IO.File.ReadAllText(path, Encoding.Unicode);
        }

        private void WriteFile(long index, string data)
        {
            var path = CreatePath(index);
            System.IO.File.WriteAllText(path, data, Encoding.Unicode);
        }

    }

}
