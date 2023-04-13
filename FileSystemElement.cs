using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePattern
{
    // The Component abstract class
    abstract class FileSystemElement
    {
        protected string name;
        protected DateTime dateCreated;

        public FileSystemElement(string name)
        {
            this.name = name;
            this.dateCreated = DateTime.Now;
        }
        public string GetName() => name;
        public abstract void Add(FileSystemElement element);
        public abstract void Remove(FileSystemElement element);
        public abstract void Display(int depth);
        public abstract long GetSize();
        public abstract bool HasElementNamed(string elementName);
    }
}
