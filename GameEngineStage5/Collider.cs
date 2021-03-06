﻿using System.Drawing;

namespace GameEngineStage5
{
    /// <summary>
    /// Базовый класс для коллайдеров
    /// </summary>
    public abstract class Collider
    {
        /// <summary>
        /// Координаты центра коллайдера (для привязки к объекту)
        /// </summary>
        protected PointF center;

        /// <summary>
        /// Угол поворота коллайдера относительно центра
        /// </summary>
        protected float angle;


        /// <summary>
        /// Базовый конструктор
        /// </summary>
        public Collider()
        {
        }

        /*
        virtual public bool hasCollision(Collider c)
        {
        }
        */

        /// <summary>
        /// Инкрементальный поворот коллайдера на указанный угол
        /// </summary>
        /// <param name="angle">угол поворота в градусах</param>
        virtual public void setAngle(float angle)
        {
        }

        /// <summary>
        /// Переместить центр коллайдера в указанную координату
        /// </summary>
        /// <param name="x">координата Х нового центра</param>
        /// <param name="y">координата У нового центра</param>
        virtual public void setCenter(float x, float y)
        {
        }

        virtual public bool hasCollision(Collider c)
        {
            return false;
        }

        /// <summary>
        /// Функция отрисовки границ коллайдера (для отладки)
        /// </summary>
        /// <param name="g">графический контекст</param>
        virtual public void render(Graphics g)
        {
        }
    }
}
