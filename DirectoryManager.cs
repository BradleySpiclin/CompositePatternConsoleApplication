using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CompositePattern
{
    internal class DirectoryManager
    {
        private Directory _root;
        private const string RootName = "Root";

        public Directory Root
        {
            get { return _root; }
        }

        public DirectoryManager()
        {
            _root = new Directory(RootName);
        }
        // This method prompts the user for the name of the new directory to be added,
        // and whether it should be added to the root directory or to an existing directory.
        // If it is to be added to an existing directory, it prompts the user for the name of the existing directory.
        // If the directory already exists, it adds the new directory to it.
        public void AddDirectory()
        {
            var directoryName = ConsoleHelper.PromptForString("Enter directory name: ");
            Directory newDirectory = new Directory(directoryName);
            if (_root.CountDirectories() == 0)
            {
                _root.Add(newDirectory);
                return;
            }
            var existingDirectoryName = ConsoleHelper.PromptForString("Enter destination directory name: ");
            var existingDirectory = FindDirectory(Root, existingDirectoryName);
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
                return;
            }
            Console.ReadKey();
        }
        // This method prompts the user for the name of the directory to be copied, and whether it should be copied to the root directory or to an existing directory.
        // If it is to be copied to an existing directory, it prompts the user for the name of the existing directory.
        // If the directory already exists, it copies the directory to the specified destination.
        public void CopyDirectory()
        {
            var directoryToCopy = FindDirectory(Root, ConsoleHelper.PromptForString("Directory to copy: "));
            if (directoryToCopy != null)
            {
                var destinationDirectory = FindDirectory(Root, ConsoleHelper.PromptForString("Desination directory: "));
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
        public void DeleteDirectory()
        {
            var directoryToRemove = FindDirectory(Root, ConsoleHelper.PromptForString("Directory to delete: "));
            if (directoryToRemove != null)
            {
                _root.Remove(directoryToRemove);
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
        public void AddFile()
        {
            // Get file details
            var fileName = ConsoleHelper.PromptForString("Enter file name: ");
            var fileSize = ConsoleHelper.PromptForLong("Enter file size (in bytes): ");
            // Create new File object
            File file = new File(fileName, fileSize);
            // If we have no directories we are at root level and add the file
            if (Root.CountDirectories() == 0)
            {
                Root.Add(file); // Treated as FileSystemElement object
                return;
            }
            var directory = FindDirectory(Root, ConsoleHelper.PromptForString("Enter directory: "));
            if (directory == null)
            {
                Console.WriteLine($"Destination directory does not exist.");
            }
            else if (directory.HasElementNamed(fileName))
            {
                Console.WriteLine($"A file with this name already exists in the {directory.GetName()} directory.");
            }
            else
            {
                directory.Add(file); // Treated as FileSystemElement object
                return;
            }
            Console.ReadKey();
        }
        // This method prompts the user for the name of the file to be copied, and the name of the directory where the copy should be created.
        // If the directory exists, it creates a new file with the same name and size as the original file, and adds it to the specified directory.
        public void CopyFile()
        {
            // Get file to copy
            var fileToCopy = FindFile(Root, ConsoleHelper.PromptForString("File to copy: "));
            if (fileToCopy != null)
            {
                // Get destination directory for the copy
                var destinationDirectory = FindDirectory(Root, ConsoleHelper.PromptForString("Destination directory: "));
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
        public void DeleteFile()
        {
            var fileName = ConsoleHelper.PromptForString("Enter file name: ");
            var fileToDelete = FindFile(Root, fileName);
            if (fileToDelete != null)
            {
                var parentDirectory = FindParentDirectory(Root, fileToDelete);
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
        private Directory? FindDirectory(Directory root, string name)
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
        private File? FindFile(Directory root, string name)
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
        private Directory? FindParentDirectory(Directory root, FileSystemElement file)
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
    }
}
