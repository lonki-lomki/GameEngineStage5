using System.Drawing;
using System.Windows.Forms;

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
        public delegate void ButtonAction(Entity ent, string eType);

        /// <summary>
        /// Внешняя функция, которая вызывается при нажатии кнопки
        /// </summary>
        private ButtonAction action;

        public Button(string id, string style, string label/*, font ???? */, GameData gd) : base(id, gd)
        {
            this.id = id;
            this.style = style;
            this.label = label;
            this.gd = gd;
            setImage(gd.rm.getImage(style));
        }

        /// <summary>
        /// Добавить внешнюю функцию, которая будет вызываться для обработки события нажатия кнопки.
        /// </summary>
        /// <param name="ba">функция - делегат</param>
        public void AddCallBack(ButtonAction ba)
        {
            action = ba;
        }

        public override void render(Graphics g)
        {
            // Вывести кнопку и, поверх, надпись с центрованием текста
            g.DrawImage(gd.rm.getImage(style), getPosition().X, getPosition().Y, getSize().Width, getSize().Height);
            Font font = new Font("Arial", 15);
            SizeF strSize = g.MeasureString(label, font);
            g.DrawString(label, font, Brushes.Yellow, getPosition().X + getSize().Width / 2 - strSize.Width / 2, getPosition().Y + getSize().Height / 2 - strSize.Height / 2);
        }

        public override void OnLeftMouseButtonClick(MouseEventArgs args)
        {
            // Проверить нахождение курсора мыши в пределах площади кнопки
            PointF pos = getPosition();
            SizeF size = getSize();
            if (args.X >= pos.X && args.X <= (pos.X + size.Width))
            {
                if (args.Y >= pos.Y && args.Y <= (pos.Y + size.Height))
                {
                    // Клавиша мыши нажата внутри прямоугольника экранной кнопки - выполняем действие
                    action(this, "LeftMouse");
                }
            }
        }

        public override void OnRightMouseButtonClick(MouseEventArgs args)
        {
            // Проверить нахождение курсора мыши в пределах площади кнопки
            PointF pos = getPosition();
            SizeF size = getSize();
            if (args.X >= pos.X && args.X <= (pos.X + size.Width))
            {
                if (args.Y >= pos.Y && args.Y <= (pos.Y + size.Height))
                {
                    // Клавиша мыши нажата внутри прямоугольника экранной кнопки - выполняем действие
                    action(this, "RightMouse");
                }
            }
        }

    }
}
