namespace LessonsMainCSharp;
using System.ComponentModel;
using static System.Console;
using System.Threading;
using static System.Array;


class Program
{
    static void Main(string[] args)
    {
        OutputEncoding = System.Text.Encoding.UTF8;

        Hospital hospital = new Hospital();
        Random random = new Random();
        int patientIdCounter = 1;

        WriteLine("=== СИМУЛЯЦИЯ ОЧЕРЕДИ В БОЛЬНИЦЕ ===");
        WriteLine("Нажмите любую клавишу для старта...");
        ReadKey();

        // Бесконечный цикл симуляции работы поликлиники
        while (true)
        {
            Clear(); // Чистим экран на каждом шаге времени, чтобы было красиво

            WriteLine("=========================================================================");
            WriteLine($" [ТИК ВРЕМЕНИ] Людей в очереди сейчас: {hospital.GetWaitingCount()}");
            WriteLine("=========================================================================");

            // 1. Симулируем приход новых пациентов (с шансом 70% на каждом шаге приходит человек)
            if (random.Next(100) < 70)
            {
                Patient newPatient = PatientGenerator.GenerateRandomPatient(patientIdCounter++);
                hospital.RegisterPatient(newPatient);
            }
            else
            {
                WriteLine("[Регистратура] За этот интервал времени новые пациенты не подходили.");
            }

            WriteLine();

            // 2. Симулируем прием врача
            hospital.AdmitNextPatient();

            WriteLine("=========================================================================");
            WriteLine("Для выхода нажмите Ctrl + C. Симуляция продолжается...");

            // Задержка в 3 секунды между шагами, чтобы успевать читать текст в консоли
            Thread.Sleep(3000);
        }
    }
}



