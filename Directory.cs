using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePattern
{
    // The Composite class
    class Directory : FileSystemElement
    {
        // List of child elements
        private List<FileSystemElement> elements = new List<FileSystemElement>();

        // Get all child elements
        public List<FileSystemElement> GetElements()
        {
            return elements;
        }

        // Count the number of directories within this directory
        public int CountDirectories()
        {
            int count = 0;
            foreach (FileSystemElement element in elements)
            {
                if (element is Directory)
                {
                    // Increment count for each directory found and its subdirectories
                    count++;
                    count += ((Directory)element).CountDirectories();
                }
            }
            return count;
        }

        // Constructor
        public Directory(string name) : base(name)
        {
        }

        // Add a child element
        public override void Add(FileSystemElement element)
        {
            elements.Add(element);
        }

        // Remove a child element
        public override void Remove(FileSystemElement element)
        {
            elements.Remove(element);
        }

        // Display the directory and its child elements
        public override void Display(int depth)
        {
            Console.WriteLine($"{new string('-', depth)}+{name} - {this.GetSize()} bytes - Directory created: {this.dateCreated}");
            // Recursively display child elements
            foreach (FileSystemElement element in elements)
            {
                element.Display(depth + 2);
            }
        }

        // Get the size of the directory and its child elements
        public override long GetSize()
        {
            long size = 0;
            foreach (FileSystemElement element in elements)
            {
                size += element.GetSize();
            }
            return size;
        }

        // Copy the directory and its child elements to a destination directory
        public void CopyTo(Directory destination)
        {
            // Create a copy of the directory
            Directory copy = new Directory(name);

            // Recursively copy child elements
            foreach (FileSystemElement element in elements)
            {
                if (element is File)
                {
                    // Copy file
                    File file = (File)element;
                    copy.Add(new File(file.GetName(), file.GetSize()));
                }
                else if (element is Directory)
                {
                    // Copy subdirectory
                    Directory subdirectory = (Directory)element;
                    subdirectory.CopyTo(copy);
                }
            }

            // Add copy to destination directory
            destination.Add(copy);
        }

        // Check if the directory has a child element with the specified name
        public override bool HasElementNamed(string directoryName)
        {
            foreach (FileSystemElement element in elements)
            {
                if (element is Directory && element.GetName() == directoryName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
