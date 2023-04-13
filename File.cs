using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CompositePattern
{
    // The Leaf class
    // Represents a leaf node in the Composite pattern, which cannot have any child elements.
    // In this case, a File is a leaf node in the FileSystemElement hierarchy.
    // It inherits from the abstract FileSystemElement class and overrides its methods.
    // It has a name and a size in bytes, which are set in the constructor.
    // It also has methods to display the file's details, get its size, and check if it has an element with a specific name.
    class File : FileSystemElement
    {
        private long _size;

        // Constructor that sets the name and size of the file.
        public File(string name, long size) : base(name)
        {
            this._size = size;
        }

        // Overrides the Add method from the base class, as a file cannot have any child elements.
        public override void Add(FileSystemElement element)
        {
            Console.WriteLine("Cannot add to a file.");
        }

        // Overrides the Remove method from the base class, as a file cannot have any child elements to remove.
        public override void Remove(FileSystemElement element)
        {
            Console.WriteLine("Cannot remove from a file.");
        }

        // Overrides the Display method from the base class, to display the details of the file.
        // The depth parameter is used to determine the indentation level for the file's details.
        public override void Display(int depth)
        {
            Console.WriteLine($"{new string('-', depth)}{_name} - {this.GetSize()} bytes - File created: {this._dateCreated}");
        }

        // Overrides the GetSize method from the base class, to return the size of the file.
        public override long GetSize()
        {
            return _size;
        }

        // Overrides the HasElementNamed method from the base class, to check if the file has a specific name.
        public override bool HasElementNamed(string fileName)
        {
            return _name.Equals(fileName);
        }
    }
}
