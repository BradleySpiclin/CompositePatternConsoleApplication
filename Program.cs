namespace CompositePattern
{
    public enum MenuOptions 
    {
        AddDirectory = 1,
        CopyDirectory = 2,
        DeleteDirectory = 3,
        AddFile = 4,
        CopyFile = 5,
        DeleteFile = 6,
        Quit = 7
    }

    // The Client class
    public class Program
    {
        static void Main(string[] args)
        {
            // Create the file manager
            DirectoryManager directoryManager = new DirectoryManager();
            while (true)
            {
                Console.Clear();
                directoryManager.Root.Display(0);
                DisplayMenu();
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice) || !Enum.IsDefined(typeof(MenuOptions), choice))
                {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        directoryManager.AddDirectory();
                        break;
                    case 2:
                        directoryManager.CopyDirectory();
                        break;
                    case 3:
                        directoryManager.DeleteDirectory();
                        break;
                    case 4:
                        directoryManager.AddFile();
                        break;
                    case 5:
                        directoryManager.CopyFile();
                        break;
                    case 6:
                        directoryManager.DeleteFile();
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
        // Displays the menu options to the console
        static void DisplayMenu() 
        {
            Console.WriteLine("\nFile System:");
            Console.WriteLine($"{(int)MenuOptions.AddDirectory}. Add directory");
            Console.WriteLine($"{(int)MenuOptions.CopyDirectory}. Copy directory");
            Console.WriteLine($"{(int)MenuOptions.DeleteDirectory}. Delete directory");
            Console.WriteLine($"{(int)MenuOptions.AddFile}. Add file");
            Console.WriteLine($"{(int)MenuOptions.CopyFile}. Copy file");
            Console.WriteLine($"{(int)MenuOptions.DeleteFile}. Delete file");
            Console.WriteLine($"{(int)MenuOptions.Quit}. Quit");
            Console.Write(">> ");
        }      
    }
}