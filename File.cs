using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CompositePattern
{
    // The Leaf class
    class File : FileSystemElement
    {
        private long size;

        public File(string name, long size) : base(name)
        {
            this.size = size;
        }

        public override void Add(FileSystemElement element)
        {
            Console.WriteLine("Cannot add to a file.");
        }
        public override void Remove(FileSystemElement element)
        {
            Console.WriteLine("Cannot remove from a file.");
        }

        public override void Display(int depth)
        {
            Console.WriteLine($"{new string('-', depth)}{name} - {this.GetSize()} bytes - File created: {this.dateCreated}");
        }

        public override long GetSize()
        {
            return size;
        }
        public override bool HasElementNamed(string fileName)
        {
            return name.Equals(fileName);
        }
    }
}
