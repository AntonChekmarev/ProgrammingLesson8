using System;

namespace Task4
{

    public class Task
    {
        public void Start() // === START ===
        {
            PrintStartTask("60", "на входе размерность кубического массива, который заполняется случайными не повторяющимися положительными двузначными числами, на выходе построчный вывод всех элементов с индексами.");

            int abc = InputInt("Введите 'длину' сторон кубического массива");

            int[,,] array = CubicArrayRandomWithoutDuplicates(abc, 10, 99); // формирование трехмерного массива

            Console.WriteLine();
            Console.WriteLine("Элементы сформированного массива:");
            CubicArrayToStringMatrix(array, abc);
            MatrixPrint(CubicArrayToStringMatrix(array, abc));

            PrintFinishTask();
        } // === FINISH ===



        /// <summary>
        /// формирование кубического массива со случайными не повторяющимися элеменатами 
        /// </summary>
        /// <param name="abc">длина сторон массива</param>
        /// <param name="min">минимальное значение элементов</param>
        /// <param name="max">максимальное значение элементов</param>
        /// <returns>кубический массив</returns>
        static int[,,] CubicArrayRandomWithoutDuplicates(int abc, int min, int max)
        {
            int[,,] rezult = new int[abc, abc, abc];

            Random rnd = new();

            int valueCount = abc * abc * abc; // кол-во элементов массива
            int tempRnd = 0;
            List<int> listRandom = new(); // список неповторяющихся чисел

            for (int i = 0; i < valueCount; i++) // заполнение списка неповторяющимися числами
            {
                do
                {
                    tempRnd = rnd.Next(min, max + 1);
                } while (listRandom.Contains(tempRnd));
                listRandom.Add(tempRnd);
            }

            //заполнение кубического массива
            for (int i = 0; i < rezult.GetLength(0); i++) for (int j = 0; j < rezult.GetLength(1); j++) for (int k = 0; k < rezult.GetLength(2); k++)
                    {
                        rezult[i, j, k] = listRandom[0];
                        listRandom.RemoveAt(0);
                    }    
            return rezult;
        }

        /// <summary>
        /// преобразование кубического массива в двумерную таблицу string. кол-во столбцов = длине стороны куба
        /// </summary>
        /// <param name="array">кубический массив</param>
        /// <param name="abc">длина сторон</param>
        /// <returns></returns>
        static string[,] CubicArrayToStringMatrix(int[,,] array, int abc)
        {
            string[,] rezult = new string[abc * abc,abc];
           
            int rowIndex = 0;
            int columnIndex = 0;
            for (int i = 0; i < array.GetLength(0); i++) for (int j = 0; j < array.GetLength(1); j++) for (int k = 0; k < array.GetLength(2); k++)
                    {
                        if (columnIndex == abc)
                        {
                            columnIndex = 0;
                            rowIndex++;
                        }
                        rezult[rowIndex, columnIndex] = $"{array[i,j,k]} ({i}, {j}, {k})";
                        columnIndex++;
                    }

            return rezult;
        }

        /// <summary>
        /// вывод на экран матрицы в виде таблицы
        /// </summary>
        /// <param name="array">матрица</param>
        void MatrixPrint(string[,] array)
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
                Console.Write("\u2502 "); // | последняя часть строки 
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
            static int MaxValueLenghtInMatrix(string[,] array)
            {
                int rezult = 0;
                foreach (var value in array) if (value.ToString().Length > rezult) rezult = value.ToString().Length;

                return rezult;
            }

            //формирование горизонтальной линии
            static string HorizontalLine(string[,] array, bool? firstLine)
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

                if (rezult > 4) // доп проверка
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("err: кол-во не более 4, иначе полож. двузначных чисел не хватит!");

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