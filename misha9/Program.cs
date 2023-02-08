using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;

class Program : t
{
    public static string path = Directory.GetCurrentDirectory();
    static void Main()
    {
        if (Directory.Exists("кнопки") == false)
        {
            Directory.CreateDirectory(path + "/кнопки");
        }
        if (File.Exists("кнопки\\текст.json") == false)
        {
            FileInfo fi = new FileInfo(path + "/кнопки" + "/текст.json");
            FileStream fs = fi.Create();
            fs.Close();
        }
        naz = JsonConvert.DeserializeObject<List<t>>(File.ReadAllText("кнопки\\текст.json")) ?? new List<t>();
        ConsoleKeyInfo k;
        while (true)
        {
            File.WriteAllText("результаты\\текст.json", JsonConvert.SerializeObject(naz));
            do
            {
                Console.Clear();
                Console.WriteLine("Ваши горячие клавиши:\n  Название\t\t\tКлавиша");
                foreach (t g in naz)
                {
                    Console.WriteLine($"  {g.name}\t\t\t\t{g.kl}");
                }
                Console.WriteLine("\nДобавить - +\nУдалить, изменить, открыть приложение - Стрелка вверх/внизу + Enter\n");
                k = Console.ReadKey(true);
            } while (k.Key != ConsoleKey.Add && k.Key != ConsoleKey.DownArrow && k.Key == ConsoleKey.UpArrow);
            if (k.Key == ConsoleKey.Add)
                ad();
            else if (k.Key == ConsoleKey.DownArrow || k.Key == ConsoleKey.UpArrow)
                iz();

        }
    }
}
class t : kk
{
    public string name;
    public ConsoleKey kl;
    public string sslka;
}
class kk
{
    public static List<t> naz;
    public static List<string> names = new List<string>();
    public static List<ConsoleKey> kls = new List<ConsoleKey>();
    public static void ad()
    {
        t k = new t();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Введите название вашего приложения, которое будет окрываться:\n ");
            k.name = Console.ReadLine();
            if (names.Contains(k.name) == false)
                break;
            else
                Console.WriteLine("Такое имя приложения уже есть!");
        }
        names.Add(k.name);
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Нажмите клавишу, которая будет являться горячей для приложения");
            k.kl = Console.ReadKey(true).Key;
            if (kls.Contains(k.kl) == false)
                break;
            else
                Console.WriteLine("Такая клавиша уже есть");
        }
        kls.Add(k.kl);
        Console.Clear();
        Console.WriteLine("Введите ссылку приложения: \n");
        k.sslka = Console.ReadLine();
        naz.Add(k);
    }
    public static void start(int f)
    {
        try
        {
            Process.Start(new ProcessStartInfo { FileName = naz[f].sslka, UseShellExecute = true });
        }
        catch
        {
            Console.WriteLine("Отказано в доступе");
        }

    }
    public static void iz()
    {
        if (naz.Count != 0)
        {
            int a = 2;
            ConsoleKeyInfo g;
            Console.SetCursorPosition(0, a);
            Console.WriteLine("»");
            do
            {
                g = Console.ReadKey(true);
                if (g.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(0, a);
                    Console.WriteLine(" ");
                    a++;
                    if (a == naz.Count + 2) a--;
                }
                if (g.Key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(0, a);
                    Console.WriteLine(" ");
                    a--;
                    if (a < 2) a++;
                }
                Console.SetCursorPosition(0, a);
                Console.WriteLine("»");
            } while (g.Key != ConsoleKey.Enter);
            Console.Clear();
            Console.WriteLine("1.Удалить\n2.Изменить\n3.Открыть");
            int j = Convert.ToInt32(Console.ReadLine());
            if (j == 1)
            {
                naz.RemoveAt(a - 2);
                names.RemoveAt(a - 2);
                kls.RemoveAt(a - 2);
            }
            if (j == 2)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Введите название вашего приложения, которое будет окрываться:\n ");
                    naz[a - 2].name = Console.ReadLine();
                    if (names.Contains(naz[a - 2].name) == false)
                        break;
                    else
                        Console.WriteLine("Такое имя приложения уже есть!");
                }
                names[a - 2] = naz[a - 2].name;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Нажмите клавишу, которая будет являться горячей для приложения");
                    naz[a - 2].kl = Console.ReadKey(true).Key;
                    if (kls.Contains(naz[a - 2].kl) == false)
                        break;
                    else
                        Console.WriteLine("Такая клавиша уже есть");
                }
                kls[a - 2] = naz[a - 2].kl;
            }
            if (j == 3)
            {
                ConsoleKeyInfo d;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Ваши горячие клавиши:\n  Название\t\t\tКлавиша");
                    foreach (t o in naz)
                    {
                        Console.WriteLine($"  {o.name}\t\t\t\t{o.kl}");
                    }
                    d = Console.ReadKey();
                } while (kls.Contains(d.Key) != true);
                for (int i = 0; i < kls.Count; i++)
                {
                    if (d.Key == kls[i])
                    {
                        start(i);
                    }
                }
            }
        }
    }
}