using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameEngineStage5
{
    /// <summary>
    /// Класс, описывающий набор спрайтов, расположенных в одном изображении
    /// </summary>
    public class SpriteSheet
    {
        /// <summary>
        /// Исходное изображение
        /// </summary>
        private Image image;

        /// <summary>
        /// Ширина листа спрайтов (в спрайтах)
        /// </summary>
        private int tw;

        /// <summary>
        /// Высота листа спрайтов (в спрайтах)
        /// </summary>
        private int th;

        /// <summary>
        /// Промежуток между спрайтами
        /// </summary>
        private int spacing;

        /// <summary>
        /// Ширина границы вокруг спрайта
        /// </summary>
        private int margin;

        public SpriteSheet(Image image, int tw, int th, int spacing, int margin)
        {
            this.image = image;
            this.tw = tw;
            this.th = th;
            this.spacing = spacing;
            this.margin = margin;
        }

        /// <summary>
        /// Получить количество спрайтов в строке
        /// </summary>
        /// <returns>количество спрайтов в строке</returns>
        public int getHorizontalCount()
        {
            return tw;
        }

        /// <summary>
        /// Получить количество спрайтов по высоте
        /// </summary>
        /// <returns>количество спрайтов по высоте</returns>
        public int getVerticalCount()
        {
            return th;
        }

        /// <summary>
        /// Вернуть один спрайт из матрицы спрайтов
        /// </summary>
        /// <param name="x">координата Х в матрице</param>
        /// <param name="y">координата У в матрице</param>
        /// <returns>изображение спрайта</returns>
        public Image getSprite(int x, int y)
        {
            // TODO: сделать!!!
            // http://stackoverflow.com/questions/734930/how-to-crop-an-image-using-c
            //Image sprite = Image.
            return null;
        }
    }
}
