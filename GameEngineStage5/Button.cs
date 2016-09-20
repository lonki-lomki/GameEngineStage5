using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngineStage5
{
    /// <summary>
    /// Класс, описывающий графическую интерактивную кнопку
    /// </summary>
    public class Button : Entity
    {
        /// <summary>
        /// Стиль отображения кнопки
        /// </summary>
        private string style;

        /// <summary>
        /// Надпись на кнопке
        /// </summary>
        private string label;

        /// <summary>
        /// Объявление класса-делегата для указания функции обработки события нажатия кнопки
        /// </summary>
        public delegate void ButtonAction();

        /// <summary>
        /// Внешняя функция, которая вызывается при нажатии кнопки
        /// </summary>
        private ButtonAction action;

        public Button(string id, string style, string label/*, font ???? */ ) : base(id, gd)
        {
            this.id = id;
            this.style = style;
            this.label = label;
        }

        /// <summary>
        /// Добавить внешнюю функцию, которая будет вызываться для обработки события нажатия кнопки.
        /// </summary>
        /// <param name="ba">функция - делегат</param>
        public void AddCallBack(ButtonAction ba)
        {
            action = ba;
        }


    }
}
