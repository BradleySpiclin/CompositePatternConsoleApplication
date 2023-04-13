#Composite Pattern Console Application
--
Class Structure
--
The program is written in C# and uses the Composite Design Pattern to represent part-whole hierarchies of file system elements. The program creates a root directory object and provides a menu for users to perform various operations on the file system elements.

FileSystemElement
--
An abstract class that defines the basic properties and behavior of a file system element, such as a file or directory.

File
--
A class that represents a file in the file system.

Directory
--
A class that represents a directory in the file system. It can contain child file system elements (files or directories) and provides methods for adding, removing, and searching for child elements, as well as copying the directory and its contents to a new location.

Program
--
The client class that interacts with the file system objects and demonstrates the use of the composite pattern to treat individual files and directories as well as compositions of files and directories uniformly.

Usage
--
To use the program, run the executable file and follow the prompts on the console to perform various operations on the file system elements. The menu options allow the user to add, remove, copy, and search for files and directories, as well as quit the program.
