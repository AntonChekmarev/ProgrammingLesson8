using System;

namespace Task3
{

    public class Task
    {
        public void Start() // === START ===
        {
            PrintStartTask("58", "на входе размерность квадратной матрицы, на выходе две матрицы со случайными целыми числами и еще одна, являющаяся произведением первых двух.");

            int rc = InputInt("Введите 'длину' сторон квадратной матрицы");

            int[,] array1 = MatrixRandom(rc, rc, -99, 99); // формирование матрицы 1
            int[,] array2 = MatrixRandom(rc, rc, -99, 99); // формирование матрицы 2

            Console.WriteLine();
            Console.WriteLine("Матрица 1:");
            MatrixPrint(array1); 

            Console.WriteLine();
            Console.WriteLine("Матрица 2:");
            MatrixPrint(array2);

            Console.WriteLine();
            Console.WriteLine("Результирующая матрица:");
            MatrixPrint(MultiplicationMatrix(array1, array2));

            PrintFinishTask();
        } // === FINISH ===


        /// <summary>
        /// произведение двух матриц
        /// </summary>
        /// <param name="array1">первая матрица</param>
        /// <param name="array2">вторая матрица</param>
        /// <returns>матрица</returns>
        static int[,] MultiplicationMatrix(int[,] array1, int[,] array2)
        {
            int[,] rezult = new int[array1.GetLength(0), array2.GetLength(1)];
            
            for (int i1 = 0; i1 < array1.GetLength(0); i1++) for (int j2 = 0; j2 < array2.GetLength(1); j2++) for (int i2 = 0; i2 < array2.GetLength(0); i2++)
                    {
                        rezult[i1, j2] += array1[i1, i2] * array2[i2, j2];
                    }
            return rezult;
        }

        /// <summary>
        /// формирование матрицы со случайными элеменатми в задаваемом дипазоне
        /// </summary>
        /// <param name="rowsCount">количество строк</param>
        /// <param name="columnsCount">количество колонок</param>
        /// <param name="min">минимальное значение элементов</param>
        /// <param name="max">максимальное значение элементов</param>
        /// <returns>матрица</returns>
        static int[,] MatrixRandom(int rowsCount, int columnsCount, int min, int max)
        {
            int[,] rezult = new int[rowsCount, columnsCount];

            Random rnd = new();
            for (int i = 0; i < rowsCount; i++) for (int j = 0; j < columnsCount; j++)
                {
                 rezult[i,j] = rnd.Next(min, max + 1);
                }
            return rezult;
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
                    str = array[i,j].ToString();
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