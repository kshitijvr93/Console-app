using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Generic;
namespace ConsoleApp1
{
    class Program
    {
        public static ArrayList bookRepository;

        public static Dictionary<string, string> authorInfo;
        public static Dictionary<string, string> publicationInfo;

        static void Main(string[] args)
        {
            bookRepository = new ArrayList();
            authorInfo = new Dictionary<string, string>();
            publicationInfo = new Dictionary<string, string>();

            string[] lines = File.ReadAllLines("file.txt");

            foreach(string line in lines)
            {
                string[] data = line.Split(' ');

                int bookID = Int32.Parse(data[0]);
                string title = data[1];
                string author = data[2];
                string publication = data[3];
                float cost = float.Parse(data[4]);

                Book currentBook = new Book(bookID, title, author, publication, cost);
                bookRepository.Add(currentBook);
            }
            Console.Write("\n");

            string text = System.IO.File.ReadAllText(@"README.md");

            // Display the file contents to the console. Variable text is a string.

            System.Console.WriteLine(text);
            

            while (true)
            {
                Console.WriteLine("------Enter a command-------");

                string userInput = Console.ReadLine();


                bool validInput = Commands.parseCommand(userInput);
                if (!validInput)
                {
                    if(userInput.Equals("end"))
                        return;
                    else
                    {
                        Console.WriteLine("Invalid command! Please enter end to quit");
                    }
                }
               
                
            }

            Console.ReadKey();
        }
    }

    class Book
    {
        public int bookID;
        public string title;
        public string author;
        public string publication;
        public float cost;

        public Book(int bid, string title, string author, string publication, float cost)
        {
            this.bookID = bid;
            this.title = title;
            this.author = author;
            this.publication = publication;
            this.cost = cost;
        }

    }

    class Commands
    {

        public static bool parseCommand(string command)
        {

            if(command.Contains("Show Books"))
            {
                if(command.Contains("-"))
                {
                    Book[] books = (Book[])Program.bookRepository.ToArray(typeof(Book));
                    MergeSort(books, 0, books.Length - 1);
                    if (command.Contains("-sa"))
                    {
                        // sort asc on cost
        
                        foreach(Book book in books)
                        {
                            printBook(book);
                        }
                        return true;
                    }
                    else if(command.Contains("-sd"))
                    {
                        Array.Reverse(books);
                        foreach (Book book in books)
                        {
                            printBook(book);
                        }
                        // sort desc on cost
                        return true;
                    }
                }
                else if(!command.Contains("-"))
                {
                    // everything
                    showAllBooks();
                    return true;
                }

            }else if(command.Contains("BookId"))
            {
                string[] commandSplit = command.Split('=');
                int bookIdSearch = Int32.Parse(commandSplit[1].Trim());
                bool bookExists = findBookById(bookIdSearch);

                if (!bookExists) Console.WriteLine("Book does not exist");

                return true;

            }
            else if (command.Contains("AddAuthorInfo"))
            {
                Console.Write(" enter author name with _:");
                string authorName = Console.ReadLine();
                Console.Write("\n enter author info ");
                Program.authorInfo[authorName] = Console.ReadLine();
                return true;
            }
            else if (command.Contains("AddPublicationInfo"))
            {
                Console.Write(" enter publication name with _:");
                string publicationName = Console.ReadLine();
                Console.Write("\n enter author info ");
                Program.publicationInfo[publicationName] = Console.ReadLine();
                return true;
            }
            else if(command.Contains("Author"))
            {
                string[] commandSplit = command.Split('=');
                String bookAuthorSearch = commandSplit[1];
                // findBook(bookAuthorSearch)
                if (Program.authorInfo.ContainsKey(bookAuthorSearch))
                {
                    Console.WriteLine("The author info we have :");
                    Console.WriteLine(Program.authorInfo[bookAuthorSearch]);
                }
                bool bookFound = showBooksByAuthor(bookAuthorSearch);
                
                if (!bookFound) Console.WriteLine("Sorry no book found by the author " + bookAuthorSearch);
                return true;
            }
            else if(command.Contains("Publication"))
            {
                string[] commandSplit = command.Split('=');
                string bookPublication = commandSplit[1];
                if (Program.publicationInfo.ContainsKey(bookPublication))
                {
                    Console.WriteLine("The publication info we have :");
                    Console.WriteLine(Program.publicationInfo[bookPublication]);
                }
                bool bookFound = showBooksByPublication(bookPublication);
                
                if (!bookFound) Console.WriteLine("Sorry no book found by the book publication " + bookPublication);
                //findBook(bookIdSearch)
                return true;
            }
            

            return false;
        }
        
        public static void showAllBooks()
        {
            foreach(Book book in Program.bookRepository)
            {
                printBook(book);
            }
        }
        public static bool showBooksByAuthor(string author)
        {
            bool bookFound = false;

            foreach(Book book in Program.bookRepository)
            {
                if(book.author.Equals(author))
                {
                    bookFound = true;
                    printBook(book);
                }
            }
            return bookFound;
        }
        public static bool showBooksByPublication(string publication)
        {
            bool bookFound = false;

            foreach (Book book in Program.bookRepository)
            {
                if (book.publication.Equals(publication))
                {
                    bookFound = true;
                    printBook(book);
                }
            }
            return bookFound;
        }

        public static Boolean findBookById(int bookId)
        {
            foreach(Book book in Program.bookRepository)
            {
                if(book.bookID == bookId)
                {
                    printBook(book);
                    return true;
                }
            }
            return false;
        }

        public static void printBook(Book book)
        {
            Console.WriteLine(book.bookID + " " + book.title + " written by " + book.author + " " + book.publication + " " + book.cost);
        }


        // sorting

        private static void Merge(Book[] input, int left, int middle, int right)
        {
            Book[] leftArray = new Book[middle - left + 1];
            Book[] rightArray = new Book[right - middle];

            Array.Copy(input, left, leftArray, 0, middle - left + 1);
            Array.Copy(input, middle + 1, rightArray, 0, right - middle);

            int i = 0;
            int j = 0;
            for (int k = left; k < right + 1; k++)
            {
                if (i == leftArray.Length)
                {
                    input[k] = rightArray[j];
                    j++;
                }
                else if (j == rightArray.Length)
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else if (leftArray[i].cost <= rightArray[j].cost)
                {
                    input[k] = leftArray[i];
                    i++;
                }
                else
                {
                    input[k] = rightArray[j];
                    j++;
                }
            }
        }

        private static void MergeSort(Book[] input, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                MergeSort(input, left, middle);
                MergeSort(input, middle + 1, right);

                Merge(input, left, middle, right);
            }
        }




        // sorting
    }

}
