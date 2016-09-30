using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameEngineStage5
{
    /// <summary>
    /// Вспомогательный класс, описывающий ячейку пути по тайловой карте
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Координата Х ячейки карты
        /// </summary>
        public int X;

        /// <summary>
        /// Координата У ячейки карты
        /// </summary>
        public int Y;

        /// <summary>
        /// Ссылка на ячейку карты, из которой пришел путь
        /// </summary>
        public Cell cameFrom;

        /// <summary>
        /// Флаг, помечающий ячейку как валидную/невалидную
        /// </summary>
        public bool valid;

        public float G;

        public float H;

        public float F;

        public Cell()
        {

        }

        public Cell(Cell c)
        {
            X = c.X;
            Y = c.Y;
            cameFrom = c.cameFrom;
            valid = c.valid;
        }

        public Cell(int x, int y, bool valid)
        {
            X = x;
            Y = y;
            this.valid = valid;
        }

        /// <summary>
        /// Получить индекс данной ячейки в линейном массиве тайловой карты
        /// </summary>
        /// <param name="mapWidth">ширина карты (длина одной горизонатльной строки)</param>
        /// <returns>порядковый номер для данной ячейки</returns>
        public int getCellID(int mapWidth)
        {
            return Y * mapWidth + X;
        }
    }

    /// <summary>
    /// Класс, реализующий алгоритм поиска пути А*
    /// </summary>
    public class Astar
    {

        public Astar()
        {

        }

        /// <summary>
        /// Функция поиска объекта в массиве (сравнение объектов производится по значению полей x и y)
        /// </summary>
        /// <param name="arg_cell">объект, который будет искаться в массиве</param>
        /// <param name="arg_array">массив объектов</param>
        /// <returns>true - если объект найден, false - если объект не найден</returns>
        private bool foundInArray(Cell arg_cell, List<Cell> arg_array)
        {
            // Цикл по объектам в массиве
            for (int index = 0; index < arg_array.Count; index++)
            {
                // Проверить равенство искомого объекта и текущего объекта из массива
                if (arg_cell.X == arg_array[index].X && arg_cell.Y == arg_array[index].Y)
                {
                    // Объект найден, выходим
                    return true;
                }
            }
            // Если дошли сюда, значит объект в массиве не найден
            return false;
        }

        /// <summary>
        /// Реконструкция найденного пути. Путь строится в обратном порядке, от конечной до начальной ячейки (используется поле cameFrom).
        /// </summary>
        /// <param name="arg_goal">конечная ячейка пути</param>
        /// <returns>список ячеек, составляющих путь от исходной ячейки к целевой</returns>
        private List<Cell> reconstruct_path(Cell arg_goal)
        {
            List<Cell> path = new List<Cell>();
            //int i = 0;
            Cell curr_cell = arg_goal;     // Поиск начинается с конечной ячейки

            // Цикл по ячейкам, составляющим путь
            while (curr_cell != null)
            {
                path.Add(new Cell(curr_cell)); // Добавить ячейку в массив
                curr_cell = curr_cell.cameFrom;
            }
            // Поменять порядок элементов списка на обратный
            path.Reverse();
            return path;
        }

        /// <summary>
        /// Функция расчёта примерной стоимости данной ячейки относительно расстояния от неё до целевой ячейки
        /// </summary>
        /// <param name="arg_from">данная ячейка</param>
        /// <param name="arg_to">целевая ячейка</param>
        /// <returns>расстояние между ячейками по теореме Пифагора</returns>
        private float heuristic_cost_estimate(Cell arg_from, Cell arg_to)
        {
            return (float)Math.Sqrt(Math.Pow(arg_to.X - arg_from.X, 2) + Math.Pow(arg_to.Y - arg_from.Y, 2));
        }

        /// <summary>
        /// Функция определяет стоимость перемещения между соседними ячейками
        /// (в данном случае у нас только 4 равноправных перемещения, поэтому возвращаем одинаковое значение в любом случае)
        /// </summary>
        /// <param name="arg_from">исходная ячейка</param>
        /// <param name="arg_to">сосед исходной ячейки</param>
        /// <returns>стоимость перемещения из исходной ячейки в соседнюю</returns>
        private int dist_between(Cell arg_from, Cell arg_to)
        {
            return 10;
        }

        /// <summary>
        /// Проверка проходимости выбранной ячейки
        /// </summary>
        /// <param name="arg_cell">базовая ячейка, относительно которой проверяется сосед</param>
        /// <param name="arg_neig">соседняя ячейка, перемещение в которую необходимо проверить</param>
        /// <param name="arg_map">объект игрового поля</param>
        /// <param name="arg_canMoveElements">массив типов ячеек, по которым можно перемещаться</param>
        /// <returns>true, если перемещение разрешено, false - если перемещение запрещено</returns>
        private bool validate_cell(Cell arg_cell, Cell arg_neig, Map arg_map, List<int> arg_canMoveElements)
        {
            // Проверить на проходимость текущей ячейки
            if (arg_canMoveElements.Contains(arg_map.Layers["Layer 1"].Tiles[arg_cell.getCellID(arg_map.Width)])
             && arg_canMoveElements.Contains(arg_map.Layers["Layer 1"].Tiles[arg_neig.getCellID(arg_map.Width)])) {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Функция формирует массив всех соседей данной ячейки
        /// </summary>
        /// <param name="arg_cell">исходная ячейка</param>
        /// <param name="arg_map">тайловая карта, как источник размеров матрицы</param>
        /// <param name="arg_canMoveElements">массив типов ячеек, по которым можно перемещаться</param>
        /// <returns></returns>
        private List<Cell> neighbor_nodes(Cell arg_cell, Map arg_map, List<int> arg_canMoveElements)
        {
            List<Cell> ret = new List<Cell>();
            int width = arg_map.Width;      // Ширина карты
            int heigth = arg_map.Height;    // Высота карты

            // В цикле добавить четыре соседние ячейки
            for (int i = -1; i < 2; i += 2)
            {
                if ((arg_cell.X + i) >= 0 && (arg_cell.X + i) < width)
                {
                    ret.Add(new Cell((arg_cell.X + i), arg_cell.Y, false));
                }
                if ((arg_cell.Y + i) >= 0 && (arg_cell.Y + i) < heigth) {
                    ret.Add(new Cell(arg_cell.X, (arg_cell.Y + i), false));
                }
            }
            // Валидация добавленных в массив ячеек
            for (int idx = 0; idx < ret.Count; idx++)
            {
                ret[idx].valid = validate_cell(arg_cell, ret[idx], arg_map, arg_canMoveElements);
            }
            return ret;
        }

        /// <summary>
        /// Функция поиска пути в массиве, описывающем прямоугольный мир от текущей точки до указанной точки
        /// (алгоритм A*)
        /// </summary>
        /// <param name="arg_map">массив уровня</param>
        /// <param name="arg_from">точка, от которой искать путь (пиксельные координаты)</param>
        /// <param name="arg_to">точка, до которой искать путь (пиксельные координаты)</param>
        /// <param name="arg_sprite_size">текущий размер тайла</param>
        /// <param name="arg_canMoveElements">массив типов элементов карты, по которым можно перемещаться</param>
        /// <param name="arg_mapAir">массив типов элементов карты, которые представляют собой воздух</param>
        /// <returns>массив ячеек, которые составляют найденный путь, или null, если путь не найден</returns>
        public List<Cell> pathFinderAstar(Map arg_map, Point arg_from, Point arg_to, int arg_sprite_size, List<int> arg_canMoveElements, List<int> arg_mapAir)
        {
            // Параметры поиска пути (перевод в тайловые координаты)
            int x1 = (int)Math.Round((double)arg_from.X / arg_sprite_size);
            int y1 = (int)Math.Round((double)arg_from.Y / arg_sprite_size);
            int x2 = (int)Math.Round((double)arg_to.X / arg_sprite_size);
            int y2 = (int)Math.Round((double)arg_to.Y / arg_sprite_size);

            // Рабочие переменные
            List<Cell> closed = new List<Cell>();   // Массив уже обработанных вершин
            List<Cell> opened = new List<Cell>();   // Массив вершин, которые еще предстоит обработать
            List<Cell> path_map = new List<Cell>(); // Карта пройденных вершин

            // Объект, описывающий целевую ячейку
            Cell goal = new Cell(x2, y2, false);

            // Объект, описывающий стартовую ячейку
            Cell start = new Cell(x1, y1, false);
            start.G = 0;      // Стоимость перемещения из стартовой ячейки в текущую ячейку (для стартовой ячейки равно 0)
            start.H = heuristic_cost_estimate(start, goal);    // Эвристическая оценка расстояние до цели
            start.F = start.G + start.H;  // Стоимость перемещения для ткущей ячейки
            start.cameFrom = null;

            // Заносим стартовую ячейку в массив открытых
            opened.Add(start);

            // Главный цикл алгоритма - обрабатываем массив opened
            while (opened.Count > 0)
            {
                // Сортируем массив opened по возрастанию значения поля f
                opened.Sort(delegate (Cell a, Cell b)
                {
                    return (int)(a.F - b.F);
                });

                // Выбираем из массива ячейку с минимальным значением поля f
                Cell cell = opened[0];
                // Проверить условие успешного окончания поиска пути
                if (cell.X == goal.X && cell.Y == goal.Y)
                {
                    // Вернуть найденный путь
                    return reconstruct_path(cell);
                }

                // Удалить из массива opened выбранный элемент
                opened.RemoveAt(0);
                //opened.shift();
                // Занести этот элемент в массив уже обработанных элементов
                //closed.push(cell);
                closed.Add(cell);

                // Получить массив всех соседей для данной ячейки
                List<Cell> neig = neighbor_nodes(cell, arg_map, arg_canMoveElements);

                // Цикл по соседним ячейкам для данной ячейки
                for (int idx = 0; idx < neig.Count; idx++)
                {
                    // Пропустить недоступные ячейки
                    if (neig[idx].valid == false)
                    {
                        continue;
                    }
                    // Проверить, что ячейки neig[idx] еще нет в массиве closed
                    if (foundInArray(neig[idx], closed) == true)
                    {
                        // Если есть - пропускаем
                        continue;
                    }

                    float tentative_g_score = cell.G + dist_between(cell, neig[idx]);  // Предварительная стоимость перемещения для обрабатываемого соседа
                    bool tentative_is_better = false;   // Предварительно считаем, что данная стоимость хуже

                    // Если сосед еще не в открытом списке
                    if (foundInArray(neig[idx], opened) == false)
                    {
                        // Занести этого соседа в открытый список
                        //opened.push(neig[idx]);
                        opened.Add(neig[idx]);
                        // Предварительно считаем это лучшей ячейкой
                        tentative_is_better = true;
                    }
                    else
                    {
                        // Сосед уже в открытом списке, значит мы уже знаем его параметры
                        if (tentative_g_score < neig[idx].G)
                        {
                            // Предварительная стоимость оказалась лучше ранее просчитанной
                            // Поэтому помечаем необходимость пересчёта остальных параметров
                            tentative_is_better = true;
                        }
                        else
                        {
                            // Вычисленная стоимость оказалась хуже, чем та, что в opened.
                            // Это значит, что из вершины cell путь через этого соседа дороже.
                            // То есть, существует менее дорогой маршрут, пролегающий через этого соседа
                            // (из какой-то другой вершины)
                            // Поэтому данного соседа мы игнорируем
                            tentative_is_better = false;
                        }
                    }

                    // Обновление свойств соседа
                    if (tentative_is_better == true)
                    {
                        neig[idx].cameFrom = cell; //Вершина с которой мы пришли. Используется для реконструкции пути.
                        neig[idx].G = tentative_g_score;
                        neig[idx].H = heuristic_cost_estimate(neig[idx], goal);
                        neig[idx].F = neig[idx].G + neig[idx].H;
                    }
                }
            }

            // Сюда попадаем, только ести перебрали все возможные ячейки, а путь так и не нашли
            // Возвращаем пустой маршрут
            //log.value += "path not found\n";
            return null;
        }


        /// <summary>
        /// Отобрадение данного пути линиями и точками
        /// </summary>
        /// <param name="g">графический контекст</param>
        /// <param name="path">путь в виде массива координат ячеек</param>
        public void drawPath(Graphics g, List<Cell> path)
        {
            // Цикл по точкам пути
            for (int i = 0; i < (path.Count - 1); i++)
            {
                // Вывести текущую точку и линию до следующей точки
                g.DrawEllipse(Pens.White, CONFIG.START_X + path[i].X * CONFIG.TILE_SIZE - 2, CONFIG.START_Y + path[i].Y * CONFIG.TILE_SIZE - 2, 4, 4);
                g.DrawLine(Pens.White, CONFIG.START_X + path[i].X * CONFIG.TILE_SIZE - 1, CONFIG.START_Y + path[i].Y * CONFIG.TILE_SIZE - 1, CONFIG.START_X + path[i + 1].X * CONFIG.TILE_SIZE - 1, CONFIG.START_Y + path[i + 1].Y * CONFIG.TILE_SIZE - 1);
            }
        }
    }
}
