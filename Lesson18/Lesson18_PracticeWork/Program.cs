using LessonsMainCSharp;
using Serilog;
using Bogus;

class Program
{
  static void Main(string[] args)
  {
    OutputEncoding = Encoding.UTF8;
    string logFilePath = Path.Combine(AppContext.BaseDirectory, "logs", "log.json");
    string usersFilePath = Path.Combine(AppContext.BaseDirectory, "users.json");
    var users = new List<User>();
    int nextId = 1;

    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.File(new Serilog.Formatting.Json.JsonFormatter(), logFilePath)
        .CreateLogger();

    var userFaker = new Faker<User>("ru")
  .RuleFor(u => u.Id, f => nextId++)
  .RuleFor(u => u.Name, f => f.Name.FirstName())
  .RuleFor(u => u.SecondName, f => f.Name.LastName())
  .RuleFor(u => u.Login, f => f.Internet.UserName())
  .RuleFor(u => u.Password, f => f.Internet.Password(8))
  .RuleFor(u => u.Gmail, f => f.Internet.Email());




    try
    {
      Log.Information("Программа запущена.");

      while (true)
      {
        try
        {
          Clear();
          WriteLine("Доброго дня! Ви питаєтесь зайти на сайт зі страшною таємницею.");
          PrintMenu();

          int choice = int.Parse(ReadLine()!);

          if (choice < 0 || choice > 4)
          {
            WriteLine("Некорректный выбор. Попробуйте снова.");
            ReadKey();
            continue;
          }

          switch (choice)
          {
            case 1:
              Clear();
              WriteLine("Вы выбрали регистрацию.");
              Write("Введите Login: ");
              string? Rlogin = ReadLine();

              if (string.IsNullOrWhiteSpace(Rlogin))
              {
                WriteLine("Ошибка: Логин не может быть пустым.");
                Log.Warning("Попытка регистрации с пустым логином.");
                ReadKey();
                break;
              }

              bool loginExists = false;

              if (File.Exists(logFilePath))
              {
                using (FileStream fs = new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (StreamReader sr = new StreamReader(fs))
                {
                  string? line;
                  while ((line = sr.ReadLine()) != null)
                  {
                    try
                    {
                      using JsonDocument doc = JsonDocument.Parse(line);
                      if (doc.RootElement.TryGetProperty("Properties", out JsonElement props) &&
                          props.TryGetProperty("User", out JsonElement existingUser))
                      {
                        if (existingUser.GetProperty("Login").GetString() == Rlogin)
                        {
                          loginExists = true;
                          break;
                        }
                      }
                    }
                    catch { }
                  }
                }
              }

              if (loginExists)
              {
                WriteLine("Ошибка: Пользователь с таким логином уже существует.");
                Log.Warning("Попытка регистрации с уже существующим логином: {Login}.", Rlogin);
                ReadKey();
                break;
              }

              Write("Введите Password (обязательно): ");
              string? Rpassword = ReadLine();

              if (string.IsNullOrWhiteSpace(Rpassword))
              {
                WriteLine("Ошибка: Пароль не может быть пустым.");
                ReadKey();
                break;
              }

              Write("Введите Имя (Enter чтобы пропустить): ");
              string name = ReadLine() ?? string.Empty;

              Write("Введите Фамилию (Enter чтобы пропустить): ");
              string secondName = ReadLine() ?? string.Empty;

              Write("Введите Gmail (Enter чтобы пропустить): ");
              string gmail = ReadLine() ?? string.Empty;

              var newUser = new User
              {
                Id = nextId++,
                Login = Rlogin,
                Password = Rpassword,
                Name = name,
                SecondName = secondName,
                Gmail = gmail
              };

              users.Add(newUser);
              Log.Information("Зарегистрирован пользователь: {@User}", newUser);
              WriteLine($"Регистрация прошла успешно! Ваш ID: {newUser.Id}");
              ReadKey();
              break;




            case 2:
              Clear();
              WriteLine("Вы выбрали авторизацию.");
              Write("Введите Login: ");
              string Alogin = ReadLine()!;

              bool userFound = false;

              if (File.Exists(logFilePath))
              {
                using (FileStream fs = new FileStream(logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (StreamReader sr = new StreamReader(fs))
                {
                  string? line;
                  while ((line = sr.ReadLine()) != null)
                  {
                    try
                    {
                      using JsonDocument doc = JsonDocument.Parse(line);
                      if (doc.RootElement.TryGetProperty("Properties", out JsonElement props) &&
                          props.TryGetProperty("User", out JsonElement user))
                      {
                        if (user.GetProperty("Login").GetString() == Alogin)
                        {
                          userFound = true;
                          Write("Пользователь найден. Введите пароль: ");
                          string inputPassword = ReadLine()!;

                          if (user.GetProperty("Password").GetString() == inputPassword)
                          {
                            WriteLine("Авторизация успешна! Страшная тайна: C# — топ! Но я не могу рассказать вам больше, потому что это секрет.");
                            Log.Information("Пользователь {Login} успешно авторизовался.", Alogin);
                          }
                          else
                          {
                            WriteLine("Неверный пароль.");
                            Log.Warning("Неудачная попытка авторизации для пользователя {Login}.", Alogin);
                          }
                          break;
                        }
                      }
                    }
                    catch { }
                  }
                }
              }

              if (!userFound)
              {
                WriteLine("Пользователь с таким логином не найден.");
              }
              ReadKey();
              break;
            case 3:
              Clear();
              WriteLine("Сколько фейковых пользователей сгенерировать? ");
              Write("Введите число больше 0: ");
              int count = int.Parse(ReadLine()!);

              var faker = new Faker<User>()
                  .RuleFor(u => u.Id, f => nextId++)
                  .RuleFor(u => u.Name, f => f.Name.FirstName())
                  .RuleFor(u => u.SecondName, f => f.Name.LastName())
                  .RuleFor(u => u.Login, f => f.Internet.UserName())
                  .RuleFor(u => u.Password, f => f.Internet.Password(8))
                  .RuleFor(u => u.Gmail, f => f.Internet.Email());

              var generatedUsers = faker.Generate(count);
              users.AddRange(generatedUsers);

              FileInfo usersFile = new FileInfo(usersFilePath);

              using (StreamWriter writer = usersFile.CreateText())
              {
                string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                writer.Write(json);
              }

              Log.Information("Сгенерировано {Count} фейковых пользователей через Bogus.", count);
              WriteLine($"Успешно создано и сохранено {count} фейковых пользователей!");

              foreach (var u in generatedUsers)
              {
                WriteLine($"ID: {u.Id} | Login: {u.Login} | Pass: {u.Password} | Name: {u.Name} {u.SecondName}");
              }
              ReadKey();
              break;

            case 4:
              Clear();
              WriteLine("=== ПРОСМОТР ПОЛЬЗОВАТЕЛЕЙ ===");
              Log.Information("Запущен просмотр пользователей.");

              if (users.Count == 0)
              {
                WriteLine("Список пользователей пуст. Сначала зарегистрируйте или сгенерируйте их.");
                ReadKey();
                break;
              }

              int passedTests = 0;
              foreach (var u in users)
              {
                var found = users.FirstOrDefault(x => x.Login == u.Login && x.Password == u.Password);
                if (found != null)
                {
                  passedTests++;
                  Log.Information("Тест пройден для пользователя {Login} (ID: {Id}).", u.Login, u.Id);
                }
                else
                {
                  Log.Error("Тест провален для пользователя {Login}!", u.Login);
                }
              }

              WriteLine($"Тестирование завершено. Успешно прошли проверку: {passedTests} из {users.Count} пользователей.");
              ReadKey();
              break;

            case 0:
              Clear();
              WriteLine("Выход из программы.");
              return;
          }
        }
        catch (FormatException)
        {
          WriteLine("Ошибка: нужно ввести именно число!");
          ReadKey();
        }
      }
    }
    catch (Exception ex)
    {
      WriteLine($"\nКритическая ошибка: {ex.Message}");
      Log.Error(ex, "Произошла критическая ошибка при выполнении программы.");
      ReadKey();
    }
    finally
    {
      Log.Information("Программа завершена.");
      Log.CloseAndFlush();
    }
  }

  record User
  {
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string SecondName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string Gmail { get; set; } = string.Empty;
  }






  static void PrintMenu()
  {
    WriteLine("\n=== МЕНЮ ===");
    WriteLine("1. Зарегистрироваться");
    WriteLine("2. Авторизоваться");
    WriteLine("3. Тестировать пользователей");
    WriteLine("4. Просмотреть пользователей");
    WriteLine("0. Выйти");
    Write("Выберите действие: ");
  }
}
