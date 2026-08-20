using System;

class Program
{
  static void Main()
  {
    Console.WriteLine("Привет! Программа на C# успешно работает в Termux!\nВведите любой текст");
    string str = Console.ReadLine();
    Console.WriteLine($"твоя строка: {str}");
    Console.WriteLine("Введите число");
    str = Console.ReadLine();
    int a = Convert.ToInt32(str);
    Console.WriteLine($"число в квадрате\n{a} = {a*a}");
    
  }
}