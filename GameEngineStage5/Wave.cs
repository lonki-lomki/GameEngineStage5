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

        // TODO: конструктор

        /// <summary>
        /// Создать следующего монстра волны
        /// </summary>
        /// <returns>следующий сгенерированный монстр</returns>
        public Monster Next()
        {

            // TODO: наполнение этого метода

            return null;
        }

        /// <summary>
        /// Получить признак пустой волны (когда монстры закончились)
        /// </summary>
        /// <returns><c>true</c>, если в волне больше нет монстров, иначе - <c>false</c></returns>
        public bool isEmpty()
        {
            return this.empty;
        }
    }
}
