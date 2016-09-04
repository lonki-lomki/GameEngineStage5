using System;
using System.Drawing;

namespace GameEngineStage5
{
	public class Monster : Entity
	{
		/// <summary>
		/// Уникальный идентификатор монстра в пределах текущего этапа
		/// </summary>
		private int id = 0;
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

		// Текущая тайловая позиция монстра
		private int tileCurPos;

		// Следующая тайловая позиция монстра
		private int tileNextPos;

		// Счётчик времени
		private float time_count = 0.0f;

		// Путь, по которому идёт монстр
		private int[] path = null;

		// Текущее положение монстра на пути
		private int path_pos = 0;

		private GameData gd;

		// Позиция монстра в мировых координатах (для плавного перемещения между клетками)
		private PointF startPos;
		private PointF endPos;

		// Длина текущего отрезка, по которому движется монстр
		private float distance;

		// Угол поворота спрайта по направлению движения
		private float angle;

		private bool isDead = false;	// Флаг, показывающий, что данны монстр уже уничтожен

		public Monster ()
		{
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
			this.lastInWave = value;
		}

		/// <summary>
		/// Мёртв ли монстр?
		/// </summary>
		/// <returns><c>true</c> если монстр не живой; иначе, <c>false</c>.</returns>
		public bool IsDead()
		{
			return this.isDead;
		}

		/// <summary>
		/// Функция, которая выполняет действия при событии, когда монстр завершает полный путь
		/// </summary>
		public void OnFinish()
		{
			
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

