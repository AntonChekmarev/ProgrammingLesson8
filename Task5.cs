using System;

namespace Task5
{
    public class Task
    {
        public void Start() // === START ===
        {
            PrintStartTask("62", "на входе размерность матрицы, на выходе матрица спирально заполненная натуральными числами по возрастанию.");

            int rowsCount = InputInt("Введите кол-во строк");
            int columnsCount = InputInt("Введите кол-во столбцов");

            Console.WriteLine();
            Console.WriteLine("Спиральная матрица:");
            MatrixPrint(SpiralMatrix(rowsCount, columnsCount));

            PrintFinishTask();
        } // === FINISH ===


        /// <summary>
        /// формирование матрицы, заполненной спирально по возрастанию натуральными числами
        /// </summary>
        /// <param name="rowsCount"> кол-во строк</param>
        /// <param name="columnsCount">кол-во столбцов</param>
        /// <returns>матрица</returns>
        int[,] SpiralMatrix(int rowsCount, int columnsCount)
        {
            
            int[,] array = new int[rowsCount, columnsCount];

            int nowRow = 0;
            int nowCol = 0;
            int dx = 1;
            int dy = 0;
            int dirChanges = 0;
            int visits = columnsCount;

            for (int i = 0; i < array.Length; i++)
            {
                array[nowRow, nowCol] = i + 1;
                if (--visits == 0)
                {
                    visits = columnsCount * (dirChanges % 2) + rowsCount * ((dirChanges + 1) % 2) - (dirChanges / 2 - 1) - 2;
                    int temp = dx;
                    dx = -dy;
                    dy = temp;
                    dirChanges++;
                }

                nowCol += dx;
                nowRow += dy;
            }

            return array;
        }

        /// <summary>
        /// вывод на экран матрицы в виде таблицы
        /// </summary>
        /// <param name="array">матрица</param>
        void MatrixPrint(int[,] array)
        {
            string str = "";

            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (i == 0) Console.WriteLine(HorizontalLine(array, true)); //первая горизонтальная линия
                else Console.WriteLine(HorizontalLine(array, null)); // средние горизонтальные линии

                for (int j = 0; j < array.GetLength(1); j++)
                {
                    str = array[i, j].ToString();
                    Console.Write("\u2502 " + str + SpacesForValue(str.Length, MaxValueLenghtInMatrix(array)) + " "); // строка с элементами массива
                }
                Console.Write("\u2502 "); // |
                Console.WriteLine();
            }
            Console.WriteLine(HorizontalLine(array, false)); //последняя горизонтальная линия

            // получение получение нехватающих пробелов для элемента
            static string SpacesForValue(int nowValueLong, int maxValueLong)
            {
                int spacesCount = maxValueLong - nowValueLong;

                if (spacesCount == 0) return "";

                return " " + SpacesForValue(nowValueLong + 1, maxValueLong);
            }

            // получение максимальной "длины" среди элементов матрицы.
            static int MaxValueLenghtInMatrix(int[,] array)
            {
                int rezult = 0;
                foreach (var value in array) if (value.ToString().Length > rezult) rezult = value.ToString().Length;

                return rezult;
            }

            //формирование горизонтальной линии
            static string HorizontalLine(int[,] array, bool? firstLine)
            {
                int valueLenght = MaxValueLenghtInMatrix(array) + 2; //длина элемента + боковые пробелы
                int valueCount = array.GetLength(1); //кол-во элементов
                int allCharLenght = valueLenght * valueCount + valueCount + 1; // общая длина строки

                int[] intersectionPoints = new int[valueCount + 1]; // список координат пересечения
                int intersectionPointsIndex = 0;
                for (int i = 0; i < intersectionPoints.Length; i++) //заполнение списка координат пересечения
                {
                    intersectionPoints[i] = intersectionPointsIndex;
                    intersectionPointsIndex += (valueLenght + 1);
                }

                string rezult = "";

                for (int i = 0; i < allCharLenght; i++) // посимвольное формирование горизонтальной линии
                {
                    if (intersectionPoints.Contains(i)) // пересечение линий
                    {
                        switch (firstLine)
                        {
                            case true:
                                if (i == 0) rezult += "\u250C"; // ┌
                                else if (i == allCharLenght - 1) rezult += "\u2510"; // ┐
                                else rezult += "\u252C"; // ┬
                                break;
                            case false:
                                if (i == 0) rezult += "\u2514"; // └
                                else if (i == allCharLenght - 1) rezult += "\u2518"; // ┘
                                else rezult += "\u2534"; // ┴
                                break;
                            default:
                                if (i == 0) rezult += "\u251C"; // ├
                                else if (i == allCharLenght - 1) rezult += "\u2524"; // ┤
                                else rezult += "\u253C"; // ┼
                                break;
                        }
                    }
                    else rezult += "\u2500"; // ─
                }
                return rezult;
            }
        }

        /// <summary>
        /// ввод числа типа integer с контролем
        /// </summary>
        /// <param name="inputText">сообщение перед вводом</param>
        /// <returns>число</returns>
        static int InputInt(string inputText)
        {
            int rezult;

            Console.WriteLine("");
            do
            {
                Console.ResetColor();
                Console.Write(inputText + ": ");

                string str = Console.ReadLine()!.Trim();

                if (int.TryParse(str, out rezult) == false) // преобразование
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("err: неcоответствие типу Integer!");

                    continue;
                }

                if (rezult <= 0) // доп проверка
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("err: количество должно быть натуральным числом!");

                    continue;
                }

                break;
            } while (true);

            return rezult;
        }

        /// <summary>
        /// отрисовка заголовка задачи
        /// </summary>
        /// <param name="taskNumber">название задачи</param>
        /// <param name="taskText">текст задачи</param>
        /// <param name="infoText">дополнительная информация (необязательный параметр)</param>
        static void PrintStartTask(string taskNumber, string taskText, string infoText = "")
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"ЗАДАЧА {taskNumber}: " + taskText);
            if (infoText != "")
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("info: " + infoText);
            }
            Console.ResetColor();
        }

        /// <summary>
        /// отрисовка завершения задачи
        /// </summary>
        static void PrintFinishTask()
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("* для завершения задачи нажмите любую клавишу...");
            Console.ResetColor();
            Console.ReadKey(true);
        }







        //На случай запуска как самостоятельно проекта, не из под Главного Меню
        class Program
        {
            static void Main()
            {
                Task task = new();
                task.Start();
            }
        }
    }
}