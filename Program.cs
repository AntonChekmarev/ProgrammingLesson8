using System.Reflection;

// меняется под каждый семинар---------------
int taskCount = 5; // кол-во задач текущего семинара (от 1 до 9). надеюсь более 9 задач не будет =)

string MenuItemName(int taskNamber) // наименования пунктов меню
{
    switch (taskNamber)
    {
        case 1: return "Задача № 54"; // Task1.cs
        case 2: return "Задача № 56"; // Task2.cs
        case 3: return "Задача № 58"; // Task3.cs
        case 4: return "Задача № 60"; // Task4.cs
        case 5: return "Задача № 62"; // Task5.cs
        case 6: return ""; // Task6.cs
        case 7: return ""; // Task7.cs
        case 8: return ""; // Task8.cs
        case 9: return ""; // Task9.cs
        default: return "?";
    }
}
// ------------------------------------------


// не меняяется==============================
try
{
    PrintMainMenu();

    ConsoleKey key;

    int taskNumber = 0;
    
    do
    {
        key = Console.ReadKey(true).Key;

        if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1) taskNumber = 1; // [1]
        if (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2) taskNumber = 2; // [2]
        if (key == ConsoleKey.D3 || key == ConsoleKey.NumPad3) taskNumber = 3; // [3]
        if (key == ConsoleKey.D4 || key == ConsoleKey.NumPad4) taskNumber = 4; // [4]
        if (key == ConsoleKey.D5 || key == ConsoleKey.NumPad5) taskNumber = 5; // [5]
        if (key == ConsoleKey.D6 || key == ConsoleKey.NumPad6) taskNumber = 6; // [6]
        if (key == ConsoleKey.D7 || key == ConsoleKey.NumPad7) taskNumber = 7; // [7]
        if (key == ConsoleKey.D8 || key == ConsoleKey.NumPad8) taskNumber = 8; // [8]
        if (key == ConsoleKey.D9 || key == ConsoleKey.NumPad9) taskNumber = 9; // [9]
        
        if (taskNumber >= 1 && taskNumber <= taskCount)
        {
            try
            {
                Assembly asm = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location); // исполняемый файл
                Type t = asm.GetExportedTypes().Where(a => a.FullName.Contains($"Task{taskNumber}")).FirstOrDefault()!; // нужная задача
                t.GetMethod("Start")!.Invoke(t.GetConstructor(Type.EmptyTypes)!.Invoke(new object[] { }), null); //запуск задачи
                // t.GetMethod("Start")!.Invoke(null, null); // онли статика
            }
            catch (Exception e)
            {
                ExceptionMessage(e.Message);                   
            }
            taskNumber = 0;
            PrintMainMenu();
        }
    }
    while (key != ConsoleKey.Escape);
}
catch (Exception e)
{
    ExceptionMessage(e.Message);
}



// отрисовка главного меню
void PrintMainMenu()
{
    Console.Clear();
    Console.ResetColor();

    string keys = "";
    for (int i = 1; i <=taskCount; i++)
    {
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"[{i}]");
        keys += $" [{i}]";
        Console.ResetColor();
        Console.WriteLine(" - " + MenuItemName(i));
    }

    Console.WriteLine("");
    Console.WriteLine("");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write($"* инициируйте Задачу клавишами{keys} или нажмите [Esc] для выхода из программы...");
    Console.ResetColor();
}

// вывод исключения
void ExceptionMessage(string eMessage)
{
    Console.WriteLine("");
    Console.WriteLine("");
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.WriteLine($"критическая ошибка: {eMessage}");    
    Console.WriteLine("");
    Console.Write("* нажмите любую клавишу...");
    Console.ResetColor();
    Console.ReadKey(true);
}
