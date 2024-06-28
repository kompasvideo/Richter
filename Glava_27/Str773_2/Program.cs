Console.WriteLine("Hello, World!");

static Int64 DirectoryBytes(String path, String searchPattern,
    SearchOption searchOption)
{
    var files = Directory.EnumerateFiles(path, searchPattern,
        searchOption);
    Int64 masterTotal = 0;

    ParallelLoopResult result = Parallel.ForEach<String, Int64>(files,
        () =>
        { // localInit : вызывается в момент запуска задания
            // Инициализация: задача обработала 0 байтов
            return 0;
        },

        (file, loopState, index, taskLocalTotal) =>
        {
            // body: Вызывается один раз для каждого элемента
            // Получает размер файла и добавляет его к общему размеру
            Int64 fileLength = 0;
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(file);
                fileLength = fs.Length;
            }
            catch (IOException) {} // Игнорируем файлы, к которым нет доступа
            finally
            {
                if (fs != null) fs.Dispose();
            }
            return taskLocalTotal + fileLength;
        },

        taskLocalTotal =>
        { // localFinally: Вызывается один раз в конце задания
            // Атамарное прибавление размера из заданию к общему размеру
            Interlocked.Add(ref masterTotal, taskLocalTotal);
        });
    return masterTotal;
} 