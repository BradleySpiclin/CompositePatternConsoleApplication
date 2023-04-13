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
        protected string _name;
        protected DateTime _dateCreated;

        public FileSystemElement(string name)
        {
            this._name = name;
            this._dateCreated = DateTime.Now;
        }
        public string GetName() => _name;
        public abstract void Add(FileSystemElement element);
        public abstract void Remove(FileSystemElement element);
        public abstract void Display(int depth);
        public abstract long GetSize();
        public abstract bool HasElementNamed(string elementName);
    }
}
