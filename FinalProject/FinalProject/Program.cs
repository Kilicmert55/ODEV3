/* Christa Phelps
 * CSC 253 090
 * Final Project
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Book> bookList = new List<Book>() {
            new Book() { ISBN = "90202101", Title = "Chamber of Secrets", Author = "Rowling", Pages = 328, CategoryID = 1 } ,
            new Book() { ISBN = "34812346", Title = "A Brief History of Time", Author = "Hawking", Pages = 310, CategoryID = 3 } ,
            new Book() { ISBN = "45634631", Title = "The Road Less Traveled", Author = "Peck", Pages = 175, CategoryID = 4 },
            new Book() { ISBN = "89101202", Title = "Where the Wild Things Are", Author = "Sendak", Pages = 333, CategoryID = 1 } ,
            new Book() { ISBN = "30203032", Title = "It", Author = "King", Pages = 390, CategoryID = 2 } ,
            new Book() { ISBN = "50235488", Title = "A Man on the Moon", Author = "Chaikin", Pages = 212, CategoryID = 3 } ,
            new Book() { ISBN = "87240402", Title = "Dracula", Author = "Stoker", Pages = 287, CategoryID = 2 } ,
            new Book() { ISBN = "58019199", Title = "The Four Agreements", Author = "Ruiz", Pages = 410, CategoryID = 4 }
            };

            List<Category> categoryList = new List<Category>() {
            new Category(){ CategoryID = 1, CategoryName="Children"},
            new Category(){ CategoryID = 2, CategoryName="Horror"},
            new Category(){ CategoryID = 3, CategoryName="Non Fiction"},
            new Category(){ CategoryID = 4, CategoryName="Self Help"}
            };

            // 1. Display title where pages are greater than 300 and category is 3 or 4 (greater than 2)
            var bookTitlesThreeHundredPageCat = from Book in bookList
                        where Book.Pages > 300 && Book.CategoryID > 2
                        select new { Title = Book.Title };
            Console.WriteLine("List of books with more than 300 pages and a Category of 3 or 4");
            bookTitlesThreeHundredPageCat.ToList().ForEach(Book
                => Console.WriteLine(Book.Title));
            Console.WriteLine("------------------------------------------------------------------");

            // 2. Display title of all books between 300 - 400 pages
            var ThreeToFourHundredPages = from Book in bookList
                                          where Book.Pages > 300 && Book.Pages < 400
                                          select new { Title = Book.Title };
            Console.WriteLine("Books with pages greater than 300 but less than 400");
            ThreeToFourHundredPages.ToList().ForEach(Book => Console.WriteLine(Book.Title));
            Console.WriteLine("------------------------------------------------------------------");

            // 3. Display ISBN, title, author, pages of books order by ISBN
            var ISBNOrder = from Book in bookList
                            orderby Book.ISBN
                            select new { Title = Book.Title, Author = Book.Author, ISBN = Book.ISBN, Pages = Book.Pages};
            Console.WriteLine("Books ordered by ISBN");
            ISBNOrder.ToList().ForEach(Book => Console.WriteLine("ISBN: {0}, Title: {1},  Author:{2}, Pages: {3}", Book.ISBN, Book.Title, Book.Author, Book.Pages));
            Console.WriteLine("------------------------------------------------------------------");

            // 4. Display ISBN, title, author, pages of books order by category ID and title

            var catAndTitle = from Book in bookList
                              orderby Book.CategoryID, Book.Title
                              select new { ISBN = Book.ISBN, Title = Book.Title, Author = Book.Author, Pages = Book.Pages };
            Console.WriteLine("Books organized by category ID and title");
            catAndTitle.ToList().ForEach(Book => Console.WriteLine("ISBN: {0}, Title: {1}, Author: {2}, Pages: {3}", Book.ISBN, Book.Title, Book.Author, Book.Pages));
            Console.WriteLine("------------------------------------------------------------------");

            // 5. Display groups and titles of different categories
            var categoryListInfo = from Book in bookList
                                   group Book by Book.CategoryID into bg
                                   orderby bg.Key
                                   select new { bg.Key, bg };
            Console.WriteLine("List of titles grouped by category");
            foreach(var bookTitle in categoryListInfo)
            {
                Console.WriteLine("Category {0}:", bookTitle.Key);
                bookTitle.bg.ToList().ForEach(Book => Console.WriteLine(Book.Title));
            }
            Console.WriteLine("------------------------------------------------------------------");
            // 6. Use left outer join to group categories but with category descriptions
            var booksGroup = from Category in categoryList
                             join Book in bookList
                             on Category.CategoryID equals Book.CategoryID
                             into bg
                             select new { CategoryName = Category.CategoryName, Books = bg };
            Console.WriteLine("Left outer join");

            foreach(var group in booksGroup)
            {
                Console.WriteLine(group.CategoryName + ":");
                group.Books.ToList().ForEach(Book => Console.WriteLine(Book.Title));

            }
            Console.WriteLine("------------------------------------------------------------------");
            // 7. Use inner join to display titles and categories
            var innerGroups = from Book in bookList
                              join Category in categoryList
                              on Book.CategoryID equals Category.CategoryID
                              select new { Title = Book.Title, CategoryName = Category.CategoryName };
            Console.WriteLine("Inner Join");
            innerGroups.ToList().ForEach(Book => Console.WriteLine("Title: {0}, Category: {1}", Book.Title, Book.CategoryName));

            // A final note from a student who is very thankful
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("Don, thank you for everything over the past two years. You have taught me so much and have been nothing but helpful in my journey to begin a new career as a Software Developer. Thank you for all you do for us! -Jade");
            Console.ReadLine();
        } // end main
    } // end class

    class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public int CategoryID { get; set; }
    } // end class

    class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    } // end class
}//end namespace
