using System;
using System.Drawing;

namespace GameEngineStage5
{
	public class Monster : Entity
	{
		/// <summary>
		/// Уникальный идентификатор монстра в пределах текущего этапа
		/// </summary>
		private int monster_id = 0;
		/// <summary>
		/// Скорость монстра.
		/// </summary>
		private float speed = 1.0f;
		/// <summary>
		/// Количество пунктов жизни монстра.
		/// </summary>
		private float hp = 1.0f;
		/// <summary>
		/// Урон замку.
		/// </summary>
		private float damage = 1.0f;
		/// <summary>
		/// Количество баллов, получаемых при уничтожении монстра.
		/// </summary>
		private float exp = 1.0f;

		///////private string type;	// Тип монстра (временно, для выбора изображения)

		/// <summary>
		/// Последний монстр в волне
		/// </summary>
		private bool lastInWave = false;

		// Индекс текущей позиции монстра в нумерации узлов пути
		private int curPathPos;

        // Счётчик времени движения монстра по отрезку пути
        private int timeCounter = 0;

		// Позиция монстра в мировых координатах (для плавного перемещения между клетками)
		private PointF startPos;
		private PointF endPos;

		// Длина текущего отрезка, по которому движется монстр
		private float distance;

		private bool isDead = false;	// Флаг, показывающий, что данны монстр уже уничтожен

        public Monster (float speed, float hp, float damage, float exp, bool last)
		{
            this.speed = speed;
            this.hp = hp;
            this.damage = damage;
            this.exp = exp;
            lastInWave = last;

            // Поличить объект с игровыми данными
            gd = GameData.Instance;

            // Присвоить монстру уникальный идентификатор
            monster_id = gd.curMonsterNumber++;

            // Ставим монстра в первую точку пути
            curPathPos = 0;
            // Получить координаты первого отрезка пути
            startPos = gd.aStarPath[curPathPos];
            endPos = gd.aStarPath[curPathPos + 1];
            // Расстояние между точками
            distance = gd.distance(startPos, endPos);
            // Определить угол поаорота спрайта
            angle = gd.getAngle(startPos, endPos);
            //gd.log.write("angle:" + angle);

            position = startPos;
		}

        public override void update(int delta)
        {
            // base.update(delta); - у нас не физический мир, пишем свою функцию движения

            // Выход, если монстр уже уничтожен
            if (isDead == true)
            {
                return;
            }

            // Расчет нового положения монстра
            timeCounter += delta; // TODO: * gd.gameSpeed;

            // Сравнить текущее время движения по отрезку с эталонным временем
            if (timeCounter >= distance/speed)
            {
                // Монстру пора переходить на следующий отрезок пути
                timeCounter -= (int) (distance / speed);

                if (curPathPos == gd.aStarPath.Count - 2)
                {
                    // Дошли до финиша
                    // Вызвать метод уничтожения монстра и нанесения урона игроку
                    OnFinish();
                    return;
                }
                else
                {
                    // Переходим к следующему отрезку пути
                    curPathPos++;
                }
                // Настройка переметров для движения по новому отрезку пути
                startPos = gd.aStarPath[curPathPos];
                endPos = gd.aStarPath[curPathPos + 1];
                distance = gd.distance(startPos, endPos);
                angle    = gd.getAngle(startPos, endPos);
                //gd.log.write("angle:" + angle);
            }
            else
            {
                // продолжение движения по текущему отрезку пути
                // необходимо вычислить позицию на отрезке пути по проценту (от 0% - старт до 100% - финиш)
                position = gd.lerp(startPos, endPos, timeCounter/(distance/speed));
            }
        }

        public override void render(Graphics g)
        {
            base.render(g);
        }


        /// <summary>
        /// Получить флаг последнего монстра в волне
        /// </summary>
        /// <returns><c>true</c>, если монстр последний в волне, иначе <c>false</c></returns>
        public bool isLastInWave()
		{
			return this.lastInWave;
		}

		/// <summary>
		/// Установка значения флага последнего монстра в волне
		/// </summary>
		/// <param name="value"><c>true</c>, если этот монстр последний в волне</param>
		public void setLastInWave(bool value)
		{
			lastInWave = value;
		}

		/// <summary>
		/// Мёртв ли монстр?
		/// </summary>
		/// <returns><c>true</c> если монстр не живой; иначе, <c>false</c>.</returns>
		public bool IsDead()
		{
			return isDead;
		}

		/// <summary>
		/// Функция, которая выполняет действия при событии, когда монстр завершает полный путь
		/// </summary>
		public void OnFinish()
		{
            // TODO: !!!! это временно !!!!
            curPathPos = 0;
//            startPos = gd.path[curPathPos];
//            endPos = gd.path[curPathPos + 1];
            startPos = gd.aStarPath[curPathPos];
            endPos = gd.aStarPath[curPathPos + 1];
            distance = gd.distance(startPos, endPos);
            angle = gd.getAngle(startPos, endPos);
        }

		/// <summary>
		/// Функция, которая выполняется при попадании в монстра снаряда 
		/// </summary>
		/// <param name="damage">урон монстру</param>
		public void OnHit(float damage)
		{
			
		}


	}
}

