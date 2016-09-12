using System;
using System.Collections.Generic;
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
        private SpriteSheet frames;

        /// <summary>
        /// Координата Х первого спрайта/кадра анимации
        /// </summary>
        int x1;

        /// <summary>
        /// Координата У первого спрайта/кадра анимации
        /// </summary>
        int y1;

        /// <summary>
        /// Координата Х последнего спрайта/кадра анимации
        /// </summary>
        int x2;

        /// <summary>
        /// Координата У последнего спрайта/кадра анимации
        /// </summary>
        int y2;

        /// <summary>
        /// Длительность показа одного кадра анимации, в милисекундах
        /// </summary>
        int duration;
    }
}
