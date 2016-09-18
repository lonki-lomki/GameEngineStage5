using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameEngineStage5
{
    /// <summary>
    /// Класс, описывающий анимированное изображение
    /// </summary>
    public class Animation
    {
        /// <summary>
        /// Набор спрайтов - кадров анимации
        /// </summary>
        private SpriteSheet sprites;

        /// <summary>
        /// Координата Х первого спрайта/кадра анимации
        /// </summary>
        private int x1;

        /// <summary>
        /// Координата У первого спрайта/кадра анимации
        /// </summary>
        private int y1;

        /// <summary>
        /// Координата Х последнего спрайта/кадра анимации
        /// </summary>
        private int x2;

        /// <summary>
        /// Координата У последнего спрайта/кадра анимации
        /// </summary>
        private int y2;

        /// <summary>
        /// Длительность показа одного кадра анимации, в милисекундах
        /// </summary>
        private int duration;

        /// <summary>
        /// Флаг, указывающий, что анимация стартовала
        /// </summary>
        private bool started = false;

        /// <summary>
        /// Флаг, указывающий, что анимация зациклена
        /// </summary>
        private bool isLooped = false;

        /// <summary>
        /// Текущий кадр анимации
        /// </summary>
        private Image curFrame;

        /// <summary>
        /// Время, оставшееся на отображение текущего кадра инимации
        /// </summary>
        private int frameTimeout;

        /// <summary>
        /// Массив кадров анимации
        /// </summary>
        private List<Image> frames = new List<Image>();

        /// <summary>
        /// Индекс по массиву кадров
        /// </summary>
        private int frameIndex = 0;

        public Animation(SpriteSheet sprites, int x1, int y1, int x2, int y2, int duration)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.duration = duration;
            this.sprites = sprites;

            // Загрузка кадров анимации во внутренний массив
            int x = x1;
            int y = y1;
            // Цикл по матрице спрайтов
            while (y <= y2)
            {
                // Добавить текущий спрайт в массив
                frames.Add(sprites.getSprite(x, y));
                // Переход на следующий спрайт
                x++;
                // Переход на следующую строку спрайтов, если достигли края
                if (x >= sprites.getHorizontalCount())
                {
                    x = 0;
                    y++;
                }
                // Проверить условие выхода из цикла
                if (y >= y2 && x >= x2)
                {
                    break;
                }
            }

            // Загрузить стартовый спрайт
            frameIndex = 0;
            curFrame = frames[frameIndex];
            // Установить таймаут
            frameTimeout = duration;
        }

        /// <summary>
        /// Отображение текущего состояния анимации
        /// </summary>
        /// <param name="g">графический контекст</param>
        /// <param name="x">координата Х</param>
        /// <param name="y">координата У</param>
        public void render(Graphics g, int x, int y)
        {
            if (curFrame != null)
            {
                g.DrawImage(curFrame, x, y, curFrame.Size.Width, curFrame.Size.Height);
            }
        }

        /// <summary>
        /// Обновить анимацию
        /// </summary>
        /// <param name="delta">квант времени, прошедший после прошлого вызова этого метода</param>
        public void update(int delta)
        {
            // Проверить, что анимация стартовала
            if (started == true)
            {
                // есть массив кадров, есть длитнльность отображения одного кадра
                // переменная - индекс тек. кадра
                // переменная - обратный отсчет времени отображения тек. кадра
                // если время еще не истекло, уменьшить время на дельту и выйти
                // если время истекло - выполнить смену кадра анимации

                // уменьшить время ожидания окончания отображения кадра анимации
                frameTimeout -= delta;

                // Проверить: окончилась ли время отображения текущего кадра
                if (frameTimeout <= 0)
                {
                    // Время окончено, смена кадра, если есть
                    if (frameIndex + 1 < frames.Count)
                    {
                        frameIndex++;
                        curFrame = frames[frameIndex];
                        frameTimeout += duration;
                    }
                }



            }
        }

        /// <summary>
        /// Запустить проигрывание анимации
        /// </summary>
        public void start()
        {
            started = true;
        }

        /// <summary>
        /// Прекратить проигрывание анимации
        /// </summary>
        public void stop()
        {
            started = false;
        }

        /// <summary>
        /// Проверить: окончена ли уже анимация (либо по истечении времени анимации, либо непосредственно остановлена)
        /// </summary>
        /// <returns>true, если анимация уже остановлена</returns>
        public bool isStopped()
        {
            return (started == false);
        }

        /// <summary>
        /// Устаноить/снять признак зацикленности анимации
        /// </summary>
        /// <param name="value">true - зациклить анимацию, false - отменить зацикливание</param>
        public void setLooping(bool value)
        {
            isLooped = value;
        }

    }

}
