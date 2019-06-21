namespace ConsoleFileManager
{
    class MainClass
    {
        private static System.ConsoleColor default_back = System.Console.BackgroundColor;

        //FILE_MANAGER
        private static bool file_manager_run;
        private static int file_manager_element_index;
        private static int file_manager_elements_count;
        private static string[] file_manager_current_directories;
        private static string[] file_manager_current_files;
        private static string file_manager_current_directory;
        private static string file_manager_data_directory;

        private static int context_menu_current_option;
        private static string[] context_menu_options = {
            "Открыть", "Вырезать", "Копировать", "Удалить", "Свойства"
        };

        public static void Print(string to_print)
        {
            System.Console.WriteLine(to_print);
        }

        public static void PrintGreen(string to_print)
        {
            System.Console.ForegroundColor = System.ConsoleColor.Green;
            System.Console.Write(to_print);
            System.Console.ForegroundColor = System.ConsoleColor.White;
        }

        public static void PrintYellow(string to_print)
        {
            System.Console.ForegroundColor = System.ConsoleColor.Yellow;
            System.Console.Write(to_print);
            System.Console.ForegroundColor = System.ConsoleColor.White;
        }

        public static void PrintRed(string to_print)
        {
            System.Console.ForegroundColor = System.ConsoleColor.Red;
            System.Console.Write(to_print);
            System.Console.ForegroundColor = System.ConsoleColor.White;
        }

        public static void PrintBackYellow(string to_print)
        {
            System.Console.BackgroundColor = System.ConsoleColor.DarkYellow;
            System.Console.WriteLine(to_print);
            System.Console.BackgroundColor = default_back;
        }

        public static string ShortFileName(string path)
        {
            if (path?.Length > 0)
            {
                string[] splitted = path.Split('/');
                return splitted[splitted.Length - 1];
            }

            return string.Empty;
        }

        public static void Main(string[] args)
        {
            System.Console.Title = "Commander v0.1";
            System.Console.WriteLine("Aplication started!");
            file_manager_current_directory = System.IO.Directory.GetCurrentDirectory();
            file_manager_data_directory = file_manager_current_directory + "/Data";

            string[] data_directory_files = new string[1];

            string name = FileManager();
            System.Console.WriteLine(name);

        }

        private static void FileManagerPrintDirectory()
        {
            PrintYellow("Current directory ");
            Print(file_manager_current_directory);
        }

        private static void FileManagerUpdate()
        {
            file_manager_current_directory =
                System.IO.Directory.GetCurrentDirectory();
            file_manager_current_directories =
                System.IO.Directory.GetDirectories(file_manager_current_directory);
            file_manager_current_files =
                System.IO.Directory.GetFiles(file_manager_current_directory);
            file_manager_elements_count =
                file_manager_current_directories.Length + file_manager_current_files.Length;
        }

        private static void FileManagerShow()
        {
            string result = string.Empty;

            FileManagerUpdate();

            PrintYellow("Current directory ");
            System.Console.WriteLine(file_manager_current_directory);

            int index = 0;
            foreach (var dir in file_manager_current_directories)
            {

                if (index == file_manager_element_index)
                {
                    PrintBackYellow(ShortFileName(dir));
                }
                else
                {
                    Print(ShortFileName(dir));
                }

                ++index;
            }

            foreach (var file in file_manager_current_files)
            {
                if (index == file_manager_element_index)
                {
                    PrintBackYellow(ShortFileName(file));
                }
                else
                {
                    Print(ShortFileName(file));
                }
                ++index;
            }
        }

        private static string FileManagerChooser()
        {
            string choosen_file = string.Empty;

            file_manager_run = true;
            System.Console.CursorVisible = false;
            while (file_manager_run)
            {
                System.Console.Clear();
                FileManagerShow();
                System.ConsoleKeyInfo consoleInfo = System.Console.ReadKey();
                switch (consoleInfo.Key)
                {
                    case System.ConsoleKey.Enter:
                        {
                            FileManagerUpdate();
                            if (file_manager_element_index < file_manager_current_directories.Length)
                            {
                                System
                                .IO
                                .Directory
                                .SetCurrentDirectory(file_manager_current_directories[file_manager_element_index]);
                                file_manager_current_directory = System.IO.Directory.GetCurrentDirectory();
                                file_manager_element_index = 0;
                            }
                            else
                            {
                                string file_path =
                                    file_manager_current_files[file_manager_element_index - file_manager_current_directories.Length];
                                file_manager_element_index = 0;
                                return file_path;
                            }
                        }
                        break;
                    case System.ConsoleKey.Backspace:
                        {
                            FileManagerUpdate();
                            string new_current_directory =
                                System.IO
                                      .Directory
                                      .GetCurrentDirectory();
                            if (new_current_directory.Length > 5)
                            {
                                new_current_directory =
                                    new_current_directory.Substring(0,
                                        new_current_directory.LastIndexOf("/"));
                                System.IO
                                      .Directory.SetCurrentDirectory(new_current_directory);
                                file_manager_current_directory = System.IO.Directory.GetCurrentDirectory();
                                file_manager_element_index = 0;
                            }
                        }
                        break;
                    case System.ConsoleKey.DownArrow:
                        {
                            if (file_manager_element_index < (file_manager_elements_count - 1))
                                ++file_manager_element_index;
                        }
                        break;
                    case System.ConsoleKey.UpArrow:
                        {
                            if (file_manager_element_index != 0)
                                --file_manager_element_index;
                        }
                        break;
                    case System.ConsoleKey.Escape:
                        {
                            file_manager_run = false;
                        }
                        break;
                }
            }
            return choosen_file;
        }

        private static string FileManager()
        {
            string choosen_file = string.Empty;

            file_manager_run = true;
            System.Console.CursorVisible = false;
            while (file_manager_run)
            {
                System.Console.Clear();
                FileManagerShow();
                System.ConsoleKeyInfo consoleInfo = System.Console.ReadKey();
                switch (consoleInfo.Key)
                {
                    case System.ConsoleKey.Enter:
                        {
                            FileManagerUpdate();
                            if (file_manager_element_index < file_manager_current_directories.Length &&
                                file_manager_current_directories.Length != 0)
                            {
                                System
                                .IO
                                .Directory
                                .SetCurrentDirectory(file_manager_current_directories[file_manager_element_index]);
                                file_manager_current_directory = System.IO.Directory.GetCurrentDirectory();
                                file_manager_element_index = 0;
                            }
                            else
                            {
                                //call .method ShowContextMenu
                                ShowContextMenu();
                                file_manager_element_index = 0;
                            }
                        }
                        break;
                    case System.ConsoleKey.Backspace:
                        {
                            FileManagerUpdate();
                            string new_current_directory =
                                System.IO
                                      .Directory
                                      .GetCurrentDirectory();
                            if (new_current_directory.Length > 5)
                            {
                                new_current_directory =
                                    new_current_directory.Substring(0,
                                        new_current_directory.LastIndexOf("/"));
                                System.IO
                                      .Directory.SetCurrentDirectory(new_current_directory);
                                file_manager_current_directory = System.IO.Directory.GetCurrentDirectory();
                                file_manager_element_index = 0;
                            }
                        }
                        break;
                    case System.ConsoleKey.DownArrow:
                        {
                            if (file_manager_element_index < (file_manager_elements_count - 1))
                                ++file_manager_element_index;
                        }
                        break;
                    case System.ConsoleKey.UpArrow:
                        {
                            if (file_manager_element_index != 0)
                                --file_manager_element_index;
                        }
                        break;
                    case System.ConsoleKey.Escape:
                        {
                            file_manager_run = false;
                        }
                        break;
                }
            }
            return choosen_file;
        }

        /*
            Данная функция возвращает только контроль.
            Вся логика контекстного меню происходит целиком
            и полностью в данной функции.
        */
        private static void ShowContextMenuOptions()
        {
            int id = 0;
            foreach (var option in context_menu_options)
            {
                if (context_menu_current_option == id)
                {
                    PrintBackYellow(option);
                }
                else
                {
                    Print(option);
                }
                ++id;
            }
        }

        private static void ShowContextMenu()
        {
            string file_path = file_manager_current_files[
                                file_manager_element_index -
                                file_manager_current_directories.Length];
            string file_extenssion;
            if (file_path.Contains("."))
            {
                file_extenssion = file_path.Split('.')[1];
            }
            else
            {
                file_extenssion = ".txt";
            }

            bool Run = true;
            context_menu_current_option = 0;
            while (Run)
            {
                System.Console.Clear();
                FileManagerShow();
                ShowContextMenuOptions();
                System.ConsoleKeyInfo key = System.Console.ReadKey();
                switch (key.Key)
                {
                    case System.ConsoleKey.UpArrow:
                        {
                            if (context_menu_current_option > 0)
                            {
                                --context_menu_current_option;
                            }
                        }
                        break;
                    case System.ConsoleKey.DownArrow:
                        {
                            if (context_menu_current_option < 4)
                            {
                                ++context_menu_current_option;
                            }
                        }
                        break;
                    case System.ConsoleKey.Enter:
                        {
                            Run = false;
                            switch (context_menu_current_option)
                            {
                                case 0:
                                    {
                                        context_menu_current_option = 0;
                                        switch (file_extenssion)
                                        {
                                            case "txt":
                                            case "sh":
                                                {
                                                    System.Diagnostics.Process.Start("gedit " + file_path);
                                                }
                                                break;
                                            case "exe":
                                                {
                                                    System.Diagnostics.Process
                                                    .Start("/home/bies/" +
                                                    	"Документы/Programming/" +
                                                    	"CSharp/Console/ConsoleFileManager/" +
                                                    	"ConsoleFileManager/bin/Debug/" +
                                                    	"scripts/mono_run_script", 
                                                    file_path);
                                                    System.Console.WriteLine("Started: ", file_path);
                                                    System.Console.Read();
                                                }
                                                break;
                                            case "dll":
                                                {
                                                }
                                                break;
                                            case "blabla":
                                                {
                                                }
                                                break;
                                        }
                                    }
                                    break;
                                case 1:
                                    {
                                        context_menu_current_option = 0;
                                    }
                                    break;
                                case 2:
                                    {
                                        context_menu_current_option = 0;
                                    }
                                    break;
                                case 3:
                                    {
                                        context_menu_current_option = 0;
                                    }
                                    break;
                                case 4:
                                    {
                                        context_menu_current_option = 0;
                                        var last_write_date = System.IO.File.GetLastWriteTime(file_path);
                                        var creation_time = System.IO.File.GetCreationTime(file_path);
                                        System.Console.WriteLine("File attribute: {0}", System.IO.File.GetAttributes(file_path.ToString()));
                                        System.Console.WriteLine("Last write time: {0} {1}", last_write_date.ToShortDateString(), last_write_date.ToShortTimeString());
                                        System.Console.WriteLine("Creation time: {0} {1}", creation_time.ToShortDateString(), creation_time.ToShortTimeString());
                                        System.Console.Read();
                                    }
                                    break;
                            }
                        }
                        break;
                    case System.ConsoleKey.Backspace:
                    case System.ConsoleKey.Escape:
                        {
                            Run = false;
                        } break;
                }
            }

        }

    }
}