using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

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

        public Cell()
        {

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
        private bool foundInArray(Cell arg_cell, Cell[] arg_array)
        {
            // Цикл по объектам в массиве
            for (int index = 0; index < arg_array.Length; index++)
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

        /**
     * Реконструкция найденного пути. Путь строится в обратном порядке, от конечной
     * до начальной ячейки (используется поле came_from).
     * @param {Object} arg_goal объект - конечная ячейка пути
     * @return {Array} массив ячеек, составляющих путь от исходной ячейки к целевой
     */

        private Cell[] reconstruct_path(Cell arg_goal)
        {
            SortedList<int, Cell> path = new SortedList<int, Cell>();
            int i = 0;
            Cell curr_cell = arg_goal;     // Поиск начинается с конечной ячейки

            // Цикл по ячейкам, составляющим путь
            while (curr_cell != null)
            {
                path.Add(i++, new Cell(curr_cell)); // Добавить ячейку в массив
                curr_cell = curr_cell.cameFrom;
            }
            return path.reverse();  // ???
        }


        /**
     * Функция расчёта примерной стоимости данной ячейки относительно расстояния от неё до целевой ячейки
     * @param {Object} arg_from данная ячейка
     * @param {Object} arg_to целевая ячейка
     * @return {Number} расстояние между ячейками по теореме Пифагора
     */
        private float heuristic_cost_estimate(Object arg_from, Object arg_to)
        {
            return (float)Math.sqrt(Math.pow(arg_to.x - arg_from.x, 2) + Math.pow(arg_to.y - arg_from.y, 2));
        }

        /**
 * Функция определяет стоимость перемещения между соседними ячейками
 * (в данном случае у нас только 4 равноправных перемещения, поэтому возвращаем одинаковое значение в любом случае)
 * @param {Object} arg_from исходная ячейка
 * @param {Object} arg_to сосед исходной ячейки
 * @return {Number} стоимость перемещения из исходной ячейки в соседнюю
 */
        private int dist_between(Object arg_from, Object arg_to)
        {
            return 10;
        }

        /**
         * Проверка проходимости выбранной ячейки
         * @param {Object} arg_cell базовая ячейка, относительно которой проверяется сосед
         * @param {Object} arg_neig соседняя ячейка, перемещение в которую необходимо проверить
         * @param {Array} arg_map матрица игрового поля
         * @param {Array} arg_canMoveElements массив типом ячеек, по которым можно перемещаться
         * @returns {Boolean} true, если перемещение разрешено, false - если перемещение запрещено
         */
        private bool validate_cell(Object arg_cell, Object arg_neig, Object arg_map, Object arg_canMoveElements)
        {
            // Проверить на проходимость текущей ячейки
            if (arg_map[arg_cell.y][arg_cell.x] in arg_canMoveElements && arg_map[arg_cell.y][arg_cell.x] !== pitIndex
                    && arg_map[arg_neig.y][arg_neig.x] in arg_canMoveElements && arg_map[arg_neig.y][arg_neig.x] !== pitIndex) {
                // Дополнительные проверки
                // Вариант, когда невозможно подняться вверх в воздух, если внизу не лестница
                if ((arg_cell.y - arg_neig.y) > 0
                        && arg_map[arg_neig.y][arg_neig.x] in mapAir2
                        && arg_map[arg_cell.y][arg_cell.x] !== stairsIndex) {
                    return false;
                }
                // Нельзя запрыгнуть на верёвку
                if ((arg_cell.y - arg_neig.y) > 0
                        && arg_map[arg_neig.y][arg_neig.x] === lineIndex
                        && arg_map[arg_cell.y][arg_cell.x] in canMoveElements) {
                    return false;
                }
                // Нельзя запрыгнуть на лестницу
                if ((arg_cell.y - arg_neig.y) > 0
                        && arg_map[arg_neig.y][arg_neig.x] === stairsIndex
                        && arg_map[arg_cell.y][arg_cell.x] in mapAir2) {
                    return false;
                }
                // Нельзя с верёвки подняться на лестницу
                if ((arg_cell.y - arg_neig.y) > 0
                        && arg_map[arg_neig.y][arg_neig.x] === stairsIndex
                        && arg_map[arg_cell.y][arg_cell.x] === lineIndex)
                {
                    return false;
                }
                // Имитация гравитации
                if ((arg_cell.x - arg_neig.x) !== 0
                        && (arg_map[arg_cell.y][arg_cell.x] in mapAir2 && arg_map[arg_cell.y + 1][arg_cell.x] in mapAir2
                              || arg_map[arg_cell.y][arg_cell.x] in mapAir2 && arg_map[arg_cell.y + 1][arg_cell.x] === lineIndex)) {
                    return false;
                }
                // Дошли сюда - пройти можно
                return true;
            }
            return false;
        }

        /**
         * Функция формирует массив всех соседей данной ячейки
         * @param {Object} arg_cell исходная ячейка
         * @param {Array} arg_map матрица карты, чтобы получить размерность
         * @param {Array} arg_canMoveElements массив типом ячеек, по которым можно перемещаться
         * @returns {Array}
         */
        private Object neighbor_nodes(Object arg_cell, Map arg_map, Object arg_canMoveElements)
        {
            var ret = [];
            var width = arg_map[0].length;  // Ширина карты
            var heigth = arg_map.length;    // Высота карты
                                            // В цикле добавить четыре соседние ячейки
            for (var i = -1; i < 2; i += 2)
            {
                if ((arg_cell.x + i) >= 0 && (arg_cell.x + i) < width)
                {
                    ///////ret.push({ x: (arg_cell.x + i), y: arg_cell.y, valid: false});
                }
                if ((arg_cell.y + i) >= 0 && (arg_cell.y + i) < heigth) {
                    //////ret.push({x:arg_cell.x, y:(arg_cell.y + i), valid:false});
                }
            }
    // Валидация добавленных в массив ячеек
    for (var idx in ret) {
                ret[idx].valid = validate_cell(arg_cell, ret[idx], arg_map, arg_canMoveElements);
            }
            return ret;
        }


        // Функция поиска пути в массиве, описывающем прямоугольный мир от текущей точки до указанной точки
        // (алгоритм A* с учётом гравитации)
        // Вход: arg_map - массив уровня
        //       arg_from - точка, от которой искать путь
        //       arg_to - точка, до которой искать путь
        //       arg_sprite_size - текущий размер тайла
        //       arg_canMoveElements - массив типов элементов карты, по которым можно перемещаться
        //       arg_mapAir - массив типов элементов карты, которые представляют собой воздух
        public Object pathFinderAstar(Map arg_map, Point arg_from, Point arg_to, Object arg_sprite_size, Object arg_canMoveElements, Object arg_mapAir)
        {
            // Параметры поиска пути
            var x1 = Math.round(arg_from.x / arg_sprite_size);
            var y1 = Math.round(arg_from.y / arg_sprite_size);
            var x2 = Math.round(arg_to.x / arg_sprite_size);
            var y2 = Math.round(arg_to.y / arg_sprite_size);

            // Рабочие переменные
            var closed = [];  // Массив уже обработанных вершин
            var opened = [];  // Массив вершин, которые еще предстоит обработать
            var path_map = [];  // Карта пройденных вершин

            // Объект, описывающий целевую ячейку
            var goal = { };
            goal.x = x2;
            goal.y = y2;

            // Объект, описывающий стартовую ячейку
            var start = { };
            start.x = x1;     // Координаты стартовой ячейки
            start.y = y1;
            start.g = 0;      // Стоимость перемещения из стартовой ячейки в текущую ячейку (для стартовой ячейки равно 0)
            start.h = heuristic_cost_estimate(start, goal);    // Эвристическая оценка расстояние до цели
            start.f = start.g + start.h;  // Стоимость перемещения для ткущей ячейки
            start.came_from = null;
            // Заносим стартовую ячейку в массив открытых
            opened.push(start);

            // Главный цикл алгоритма - обрабатываем массив opened
            while (opened.length > 0)
            {
                // Сортируем массив opened по возрастанию значения поля f
                opened.sort(function(a, b) {
                    return a.f - b.f;
                });

                // Выбираем из массива ячейку с минимальным значением поля f
                var cell = opened[0];
                // Проверить условие успешного окончания поиска пути
                if (cell.x === goal.x && cell.y === goal.y)
                {
                    // Вернуть найденный путь
                    //log.value += "path found\n";
                    return reconstruct_path(cell);
                }

                // Удалить из массива opened выбранный элемент
                opened.shift();
                // Занести этот элемент в массив уже обработанных элементов
                closed.push(cell);

                // Получить массив всех соседей для данной ячейки
                var neig = neighbor_nodes(cell, arg_map, arg_canMoveElements);

        // Цикл по соседним ячейкам для данной ячейки
        for (var idx in neig)
                {
                    // Пропустить недоступные ячейки
                    if (neig[idx].valid === false)
                    {
                        continue;
                    }
                    // Проверить, что ячейки neig[idx] еще нет в массиве closed
                    if (foundInArray(neig[idx], closed) === true)
                    {
                        // Если есть - пропускаем
                        continue;
                    }

                    var tentative_g_score = cell.g + dist_between(cell, neig[idx]);  // Предварительная стоимость перемещения для обрабатываемого соседа
                    var tentative_is_better = false;   // Предварительно считаем, что данная стоимость хуже

                    // Если сосед еще не в открытом списке
                    if (foundInArray(neig[idx], opened) === false)
                    {
                        // Занести этого соседа в открытый список
                        opened.push(neig[idx]);
                        // Предварительно считаем это лучшей ячейкой
                        tentative_is_better = true;
                    }
                    else
                    {
                        // Сосед уже в открытом списке, значит мы уже знаем его параметры
                        if (tentative_g_score < neig[idx].g)
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
                    if (tentative_is_better === true)
                    {
                        neig[idx].came_from = cell; //Вершина с которой мы пришли. Используется для реконструкции пути.
                        neig[idx].g = tentative_g_score;
                        neig[idx].h = heuristic_cost_estimate(neig[idx], goal);
                        neig[idx].f = neig[idx].g + neig[idx].h;
                    }
                }
            }

            // Сюда попадаем, только ести перебрали все возможные ячейки, а путь так и не нашли
            // Возвращаем пустой маршрут
            //log.value += "path not found\n";
            return [];
        }




    }
}
