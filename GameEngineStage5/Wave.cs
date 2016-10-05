using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngineStage5
{
    /// <summary>
    /// Класс, описыващий одну волну монстров
    /// </summary>
    public class Wave
    {
        /// <summary>
        /// Текстовое представление волны в виде "<вид монстра>:<количество>,..."
        /// </summary>
        public string wave;

        private string[] waves;

        private int monsterTypeIndex;   // Индекс текущего типа монстров

        private string curMonsterType;  // Текущий тип генерируемых монстров
        private int monsterCount;       // Количество монстров текущего типа
        private int curMonsterIndex;    // Индекс последнего сгенерированного монстра

        private bool empty = false; // Признак окончания монстров в данной волне

        /// <summary>
        /// Конструктор с одним параметром
        /// </summary>
        /// <param name="value">строка, описывающая одну волну (разделитель - запятая)</param>
        public Wave(string value)
        {
            wave = value;
            waves = value.Split(',');
            curMonsterType = null;
        }

        /// <summary>
        /// Создать следующего монстра волны
        /// </summary>
        /// <returns>следующий сгенерированный монстр</returns>
        public Monster Next()
        {
            Monster m = null;

            // При попытке получить монстра из пустой волны
            if (empty == true)
            {
                return null;
            }

            if (curMonsterType == null)
            {
                // Это первый монстр из волны - получить его параметры
                monsterTypeIndex = 0;
                // Разбор строки текущего элемента волны
                string[] arr = waves[monsterTypeIndex].Split(':');
                // Выделить тип монстра
                curMonsterType = arr[0];
                // Выделить количество монстров данного типа
                if (int.TryParse(arr[1], out monsterCount) == false)
                {
                    monsterCount = 1;
                }
            }

            // Создать нового монстра
            // Выбор параметров монстра по его типу

            MobStruct ms = GameData.Instance.getMobParameters(curMonsterType);

            m = new Monster(ms.speed, ms.hp, ms.damage, ms.exp, false /* bool last */);

            // Получить индекс следующего монстра
            // (если текущий тип закончился, получить следующий тип из волны)
            curMonsterIndex++;
            // Проверить на окончание монстров текущего типа
            if (curMonsterIndex >= monsterCount)
            {
                // Монстры данного типа окончились - берем следующую группу монстров
                monsterTypeIndex++;
                curMonsterIndex = 0;
                // Проверка окончания волны
                if (monsterTypeIndex >= waves.Length)
                {
                    // Монстры окончились
                    empty = true;

                    // Пометить сгенерированного монстра как последнего в волне
                    m.setLastInWave(true);
                }
                else
                {
                    // Волна продолжается

                    // Разбор строки текущего элемента волны
                    string[] arr = waves[monsterTypeIndex].Split(':');
                    // Выделить тип монстра
                    curMonsterType = arr[0];
                    // Выделить количество монстров данного типа
                    if (int.TryParse(arr[1], out monsterCount) == false)
                    {
                        monsterCount = 1;
                    }
                }
            }
            return m;

        }

        /// <summary>
        /// Получить признак пустой волны (когда монстры закончились)
        /// </summary>
        /// <returns><c>true</c>, если в волне больше нет монстров, иначе - <c>false</c></returns>
        public bool isEmpty()
        {
            return empty;
        }
    }
}
