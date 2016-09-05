using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameEngineStage5
{
    /// <summary>
    /// Менеджер ресурсов
    /// </summary>
    public class ResourceManager
    {
        public static ResourceManager instance; // единственный экземпляр данного класса

        /// <summary>
        /// Хранилище изображений
        /// </summary>
        private Hashtable imageMap;

        /// <summary>
        /// Кеш для хранилища изображений
        /// </summary>
        private Hashtable imageMapCache;

        public ResourceManager()
        {
            // Создание хранилищ
            imageMap = new Hashtable();

            // Создание кешей для хранилищ
            imageMapCache = new Hashtable();
        }

        /// <summary>
        /// Вернуть единственный экземпляр данного класса
        /// </summary>
        public static ResourceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ResourceManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// Занесение нового элемента - изображения
        /// </summary>
        /// <param name="id">идентификатор ресурса</param>
        /// <param name="path">путь к имени файла, из которого загружать изображение</param>
        public void addElementAsImage(string id, string path)
        {
            // Проверка правильности пути к файлу
            if (path == null || path.Length == 0)
            {
                return;
            }

            // Проверить наличие изображения с таким идентификатором в хранилище
            if (imageMap.ContainsKey(id) == true)
            {
                return;
            }

            // Проверка наличия изображения в кеше
            if (imageMapCache.ContainsKey(id) == true)
            {
                // Взять изображение из кеша
                imageMap.Add(id, imageMapCache[id]);
            } else
            {
                // В кеше нет такого изображения - загрузить из файла
                Image image = Image.FromFile(path);
                // Занести в хранилище
                imageMap.Add(id, image);
            }
        }

        /// <summary>
        /// Получить изображение из кеша
        /// </summary>
        /// <param name="id">идентификатор изображения</param>
        /// <returns></returns>
        public Image getImage(string id)
        {
            return (Image)imageMap[id];
        }

        /// <summary>
        /// Предочистка хранилища - основные массивы очищаются, а все данные из них переносятся в кеш
        /// </summary>
        public void clear()
        {
            // Переместить все элементы из хранилищ в кеши
            foreach (string str in imageMap.Keys)
            {
                imageMapCache.Add(str, imageMap[str]);
            }
            // Очистить основное хранилище
            imageMapCache.Clear();
        }

        /// <summary>
        /// Полная очистка хранилища
        /// </summary>
        public void clearAll()
        {
            imageMap.Clear();
            imageMapCache.Clear();
        }
    }
}
