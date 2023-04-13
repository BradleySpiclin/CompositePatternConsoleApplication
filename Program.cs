using System.IO;
using System.Xml.Linq;

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
            // Create the root directory
            Directory root = new Directory("root");
            while (true)
            {
                Console.Clear();
                root.Display(0);
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
                        AddDirectory(root);
                        break;
                    case 2:
                        CopyDirectory(root);
                        break;
                    case 3:
                        DeleteDirectory(root);
                        break;
                    case 4:
                        AddFile(root);
                        break;
                    case 5:
                        CopyFile(root);
                        break;
                    case 6:
                        DeleteFile(root);
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
        // This method prompts the user for the name of the new directory to be added,
        // and whether it should be added to the root directory or to an existing directory.
        // If it is to be added to an existing directory, it prompts the user for the name of the existing directory.
        // If the directory already exists, it adds the new directory to it.
        static void AddDirectory(Directory root)
        {
            var directoryName = PromptForString("Enter directory name: ");
            Directory newDirectory = new Directory(directoryName);
            if (root.CountDirectories() == 0) 
            {
                root.Add(newDirectory);
                return;
            }
            var addToRoot = PromptForString("Add to root directory (Press Y): ");
            if (addToRoot == "Y" || addToRoot == "y")
            {
                if (root.HasElementNamed(newDirectory.GetName())) 
                {
                    Console.WriteLine($"A directory with this name already exists in the {root.GetName()} directory.");
                }
                root.Add(newDirectory);
                return;
            }
            else
            {
                var existingDirectoryName = PromptForString("Enter destination directory name: ");
                var existingDirectory = FindDirectory(root, existingDirectoryName);
                if (existingDirectory == null)
                {
                    Console.WriteLine($"Destination directory does not exist.");
                }
                else if (existingDirectory.HasElementNamed(newDirectory.GetName())) 
                {
                    Console.WriteLine($"A directory with this name already exists in the {existingDirectory.GetName()} directory.");
                }
                else 
                {
                    existingDirectory.Add(newDirectory);                          
                }
            }
            Console.ReadKey();
        }
        // This method prompts the user for the name of the directory to be copied, and whether it should be copied to the root directory or to an existing directory.
        // If it is to be copied to an existing directory, it prompts the user for the name of the existing directory.
        // If the directory already exists, it copies the directory to the specified destination.
        static void CopyDirectory(Directory root) 
        {
            var directoryToCopy = FindDirectory(root, PromptForString("Directory to copy: "));
            if (directoryToCopy != null) 
            {
                var userInput = PromptForString("Add to root (Y/N): ");
                if (userInput == "Y" || userInput == "y" && !root.HasElementNamed(directoryToCopy.GetName()))
                {
                    directoryToCopy.CopyTo(root);
                    return;
                }
                else 
                {
                    Console.WriteLine($"A directory with this name already exists in the {root.GetName()} directory.");
                }
                var destinationDirectory = FindDirectory(root, PromptForString("Desination directory: "));
                if (destinationDirectory == null) 
                {
                    Console.WriteLine($"Destination directory does not exist.");
                }
                else if (destinationDirectory.HasElementNamed(directoryToCopy.GetName()))
                {
                    Console.WriteLine($"A directory with this name already exists in the {destinationDirectory.GetName()} directory.");
                }
                else 
                {
                    directoryToCopy.CopyTo(destinationDirectory);
                    return;
                }
            }
            else 
            {
                Console.WriteLine("Directory does not exist");
            }
            Console.ReadKey();
        }
        // This method prompts the user for the name of the directory to be deleted. If the directory exists, it removes it from the file system.
        static void DeleteDirectory(Directory root)
        {
            var directoryToRemove = FindDirectory(root, PromptForString("Directory to delete: "));
            if (directoryToRemove != null)
            {
                root.Remove(directoryToRemove);
                return;
            }
            else 
            {
                Console.WriteLine($"Directory does not exist.");
            }
            Console.ReadKey();
        }
        // This method prompts the user for the name and size of the new file,
        // and the name of the directory where it should be added. If the directory exists, it creates a new file and adds it to the directory.
        static void AddFile(Directory root)
        {
            // Get file details
            var fileName = PromptForString("Enter file name: ");
            var fileSize = PromptForLong("Enter file size (in bytes): ");
            // Create new File object
            File file = new File(fileName, fileSize);
            // If we have only the root we have no directory to add to
            // So we add file to root
            if (root.CountDirectories() == 0 && !root.HasElementNamed(fileName)) 
            {
                root.Add(file);
                return;
            }
            var directory = FindDirectory(root, PromptForString("Enter directory: "));
            if(directory == null) 
            {
                Console.WriteLine($"Destination directory does not exist.");
            }
            else if (directory.HasElementNamed(fileName)) 
            {
                Console.WriteLine($"A directory with this name already exists in the {directory.GetName()} directory.");
            }
            else 
            {
                directory.Add(file);
                return;
            }
            Console.ReadKey();
        }
        // This method prompts the user for the name of the file to be copied, and the name of the directory where the copy should be created.
        // If the directory exists, it creates a new file with the same name and size as the original file, and adds it to the specified directory.
        static void CopyFile(Directory root)
        {
            // Get file to copy
            var fileToCopy = FindFile(root, PromptForString("File to copy: "));
            if (fileToCopy != null)
            {
                // Get destination directory for the copy
                var destinationDirectory = FindDirectory(root, PromptForString("Destination directory: "));
                if (destinationDirectory == null)
                {
                    Console.WriteLine($"Destination directory does not exist.");
                }
                // Check if the destination directory already has a file with the same name
                else if (destinationDirectory.HasElementNamed(fileToCopy.GetName()))
                {
                    Console.WriteLine($"A directory with this name already exists in the {destinationDirectory.GetName()} directory.");
                }
                else
                {
                    // Create a copy of the file
                    File copy = new File(fileToCopy.GetName(), fileToCopy.GetSize());

                    // Add the copy to the destination directory
                    destinationDirectory.Add(copy);
                    return;
                }
            }
            else 
            {
                Console.WriteLine("File does not exist");
            }
            Console.ReadKey();
        }
        // This method prompts the user for the name of the file to be deleted. If the file exists, it removes it from the file system.
        static void DeleteFile(Directory root)
        {
            var fileName = PromptForString("Enter file name: ");
            var fileToDelete = FindFile(root, fileName);
            if (fileToDelete != null)
            {
                var parentDirectory = FindParentDirectory(root, fileToDelete);
                if (parentDirectory != null) 
                {
                    parentDirectory.Remove(fileToDelete);
                    return;
                }
            }
            Console.WriteLine($"File not found.");
            Console.ReadKey();
        }
        // This method recursively searches the file system for a directory with the specified name.
        // If it finds one, it returns it; otherwise, it returns null.
        static Directory? FindDirectory(Directory root, string name)
        {
            if (root.GetName() == name)
            {
                return root;
            }
            foreach (FileSystemElement element in root.GetElements())
            {
                if (element is Directory)
                {
                    Directory directory = (Directory)element;
                    Directory? result = FindDirectory(directory, name);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }
        // This method recursively searches the file system for a file with the specified name.
        // If it finds one, it returns it; otherwise, it returns null.
        static File? FindFile(Directory root, string name)
        {
            foreach (FileSystemElement element in root.GetElements())
            {
                if (element is File file && file.GetName() == name)
                {
                    return file;
                }
                else if (element is Directory directory)
                {
                    File? result = FindFile(directory, name);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }
        // Returns the parent directory of the specified file, or null if the file is not found
        static Directory? FindParentDirectory(Directory root, File file)
        {
            foreach (var element in root.GetElements())
            {
                if (element == file)
                {
                    return root;
                }
                else if (element is Directory directory)
                {
                    var parent = FindParentDirectory(directory, file);
                    if (parent != null)
                    {
                        return parent;
                    }
                }
            }
            return null;
        }
        // Prompts the user for a string input and returns the input if it is not null, empty or whitespace.
        // If the input is invalid, the method displays an error message and prompts the user again.
        static string PromptForString(string prompt)
        {
            while (true) 
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid value.");
                }
            }
        }
        // Prompts the user for a long integer input and returns the input if it is a valid long integer.
        // If the input is invalid, the method displays an error message and prompts the user again.
        static long PromptForLong(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (long.TryParse(input, out long result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid long value.");
                }
            }
        }
    }
}