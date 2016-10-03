namespace GameEngineStage5
{
    /// <summary>
    /// Класс для управления волнами
    /// </summary>
    public class WaveManager
    {
        private static WaveManager instance;

        /// <summary>
        /// параметры текущей волны
        /// </summary>
        private Wave currentWave;

        /// <summary>
        /// индекс текущей волны в списке волн
        /// </summary>
        private int curWaveIndex;

        /// <summary>
        /// Признак последней волны этапа
        /// </summary>
        private bool lastWave = false;

        private GameData gd;

        /// <summary>
        /// Таймаут, по истечении которого формируется новый монстр
        /// </summary>
        private float timeToNextMob = 0.0f;
        /// <summary>
        /// Таймаут, по истечении которого запускается новая волна
        /// </summary>
        private float timeToNextWave = 0.0f;

        /// <summary>
        /// Приватный конструктор, чтобы невозможно было получить экземпляр данного класса обычным способом
        /// </summary>
        private WaveManager()
        {
            gd = GameData.Instance;
        }

        /// <summary>
        /// Геттер для получения единственного экземпляра данного класса
        /// </summary>
        /// <value>The instance.</value>
        public static WaveManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WaveManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// Сброс параметров менеджера и очистка всех загруженных волн
        /// </summary>
        public void Clean()
        {
            lastWave = false;
            timeToNextMob = 0.0f;
            timeToNextWave = 0.0f;
            curWaveIndex = 0;
            currentWave = null;
        }

        public Monster NextMonster(float delta)
        {
            // Генерация монстров через интервал CONST.timeBetweenMob
            // И генерация волн через интервал CONST.timeBetweenWaves

            Monster m = null;

            if (gd.currentGameState == GameData.GameState.Level)
            {
                // Проверить, что есть еще непустые волны, из которых можно сгенерировать монстра
                if (this.lastWave == true && this.currentWave.isEmpty() == true)
                {
                    // Последняя волна - пуста. Выходим.
                    //Debug.Log("Last Wave, Last Monster");
                    return null;
                }

                // Таймаут волны и таймаут монстра - независимы

                // Обновить переменные, которые отсчитывают таймаут
                this.timeToNextMob += delta * gd.gameSpeed;
                this.timeToNextWave += delta * gd.gameSpeed;

                // Проверить наличие монстров в текущей волне
                if (currentWave != null && currentWave.isEmpty() == false)
                {
                    // Проверить необходимость генерации нового монстра
                    if (this.timeToNextMob >= CONFIG.timeBetweenMob)
                    {
                        this.timeToNextMob -= CONFIG.timeBetweenMob;
                        // Новый монстр
                        m = this.currentWave.Next();
                        // Обнулить счётчик времени до генерации следующей волны
                        //Debug.Log("Next Monster");
                    }
                }
                else
                {
                    // Волна окончилась, ждём момента генерации новой волны
                    if (this.timeToNextWave >= CONFIG.timeBetweenWaves)
                    {
                        //this.timeToNextWave -= CONST.timeBetweenWaves;
                        this.timeToNextWave = 0.0f;
                        // Новая волна
                        NextWave();
                        // Обнулить счётчик времени для генерации монстров
                        this.timeToNextMob = 0.0f;
                    }
                }
            }

            // if game running
            return m;
        }

        /// <summary>
        /// Выбирает следующую волну текущего этапа
        /// </summary>
        public void NextWave()
        {
            // Если текущая волна последняя - выходим
            if (lastWave == true)
            {
                return;
            }

            // Проверка на первую волну этапа
            if (currentWave == null)
            {
                // это первая волна
                curWaveIndex = 0;
            }
            else
            {
                // Очередная волна. Проверить на границу списка.
                curWaveIndex++;
                if (curWaveIndex == (gd.waves.Count - 1))
                {
                    lastWave = true;
                }
            }

            // Получить параметры полны по текущему индексу
            currentWave = gd.waves[curWaveIndex];
        }

        /// <summary>
        /// Получение номера текущей волны
        /// </summary>
        /// <returns>номер текущей волны</returns>
        public int getWaveIndex()
        {
            return this.curWaveIndex + 1;
        }

        /// <summary>
        /// Получение времени до начала следующей волны
        /// </summary>
        /// <returns>время до начала следующей волны</returns>
        public float getTimeToNextWave()
        {
            return CONFIG.timeBetweenWaves - this.timeToNextWave;
        }

        /// <summary>
        /// Обнуление таймера, отмеряющего время между волнами, то есть, запускает новую волну
        /// </summary>
        public void forceNextWave()
        {
            if (this.currentWave == null || this.currentWave != null && this.currentWave.isEmpty())
            {
                this.timeToNextWave = CONFIG.timeBetweenWaves;
            }
        }

        /// <summary>
        /// Возвращает значение флага-индикатора последней волны
        /// </summary>
        public bool IsLastWave()
        {
            return lastWave;
        }
    }
}
