using System;

using ConsoleUI;

class Program
{
    static void Main(string[] args)
    {

        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        GameStart.Start();
        /*
        // всякоя для UI
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        //Console.Clear(); // Обязательно для заливки всей консоли цветом

        Console.WriteLine("\nбелые♔♕♖♗♘♙  \nчёрные♚♛♜♝♞♟");
        Console.WriteLine("белые ◻▢\nчёрные ◼⬛ddjejue");
        Console.WriteLine("Привет! Программа на C# успешно работает в Termux!\nВведите любой текст");
        Console.ResetColor();

        string str = Console.ReadLine();
        Console.WriteLine($"твоя строка: {str}");
        Console.WriteLine("Введите число");
        str = Console.ReadLine();
        int a = Convert.ToInt32(str);
        Console.WriteLine($"число в квадрате\n{a} = {a*a}");
        */
    }
}