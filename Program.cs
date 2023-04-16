using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application
{
    class Program
    {
        static string[] userName = new string[100];
        static string[] userPasswords = new string[100];
        static string[] userRole = new string[100];
        static string[] bookNameFree = new string[100];
        static string[] bookCategoryFree = new string[100];
        static string[] bookNamePaid = new string[100];
        static string[] bookCategoryPaid = new string[100];
        static int[] bookPrice = new int[100];
        static string[] authorFree = new string[100];
        static string[] authorPaid = new string[100];
        static int userCount = 0,bookCount=0,fbx=0,pbx=0,del=-1,del1=-1,up=-1,up1=-1;

        static void Main(string[] args)
        {
            int option=5;
            string name, password, role;
            string freeName, freeCategory,paidName,paidCategory,price;
            int authorOption=6;
            readUserData();
            readFreeBooksDetail();
            readPaidBooksDetail();
            while (option!=0)
            {
                Console.Clear();
                header();
                option = loginMenu();
                if (option == 1)
                {
                    Console.Clear();
                    header();
                    Console.WriteLine("SIGN-IN MENU");
                    Console.WriteLine("-------------------");
                    Console.WriteLine("Enter Your Name:");
                    name = Console.ReadLine();
                    Console.WriteLine("Enter Your Password:");
                    password = Console.ReadLine();
                    string rolechecker = signIn(name, password);
                    if (rolechecker == "reader" || rolechecker == "Reader" || rolechecker == "READER")
                    {
                        Console.WriteLine("Signed-Up successfully READER");
                        Console.WriteLine("Press any key to continue:");
                        Console.ReadKey();
                    }
                    else if (rolechecker == "author" || rolechecker == "Author" || rolechecker == "AUTHOR")
                    {
                        Console.WriteLine("Signed-Up successfully Author");
                        Console.WriteLine("Press any key to continue:");
                        Console.ReadKey();
                        while(authorOption!=0)
                        {
                            Console.Clear();
                            header();
                            authorOption = authorMenu();
                            if (authorOption==1)
                            {
                                Console.Clear();
                                header();
                                Console.WriteLine("Enter Your Book Name:");
                                freeName = Console.ReadLine();
                                Console.WriteLine("Enter Category(Science,History,Relegious or Literature):");
                                freeCategory = Console.ReadLine();
                                bool validCategory = categoryChecker(freeCategory);
                                bool validName = isValidBookName(freeName);
                                bool alreadyExists = isBookAlreadyExists(freeName);
                                if (validCategory==true && validName==true && alreadyExists==true)
                                {
                                    authorFree[fbx] = name;
                                    freeBookDetail(freeName, freeCategory);
                                    saveFreeBooksDetail(name, freeName, freeCategory);
                                    Console.WriteLine("Book added Successfully");
                                    Console.Write("Press any key to Continue:");
                                    Console.ReadKey();
                                } 
                                else if (validName==false)
                                {
                                    Console.WriteLine("Invalid Book Name");
                                    Console.Write("Press any key to Continue:");
                                    Console.ReadKey();
                                }
                                else if (alreadyExists==false)
                                {
                                    Console.WriteLine("Book Already Exists");
                                    Console.Write("Press any key to Continue:");
                                    Console.ReadKey();
                                }
                                else if (validCategory == false)
                                {
                                    Console.WriteLine("Invalid Category");
                                    Console.Write("Press any key to Continue:");
                                    Console.ReadKey();
                                }
                            }
                            else if (authorOption==2)
                            {
                                Console.Clear();
                                header();
                                Console.WriteLine("Enter Your Book Name:");
                                paidName = Console.ReadLine();
                                Console.WriteLine("Enter Category(Science,History,Relegious or Literature):");
                                paidCategory = Console.ReadLine();
                                Console.WriteLine("Enter book price:");
                                price = Console.ReadLine();
                                bool validCategory = categoryChecker(paidCategory);
                                bool validName = isValidBookName(paidName);
                                bool alreadyExists = isBookAlreadyExists(paidName);
                                bool validPrice = priceChecker(price);
                                if (validCategory == true && validName == true && alreadyExists == true && validPrice==true)
                                {
                                    authorPaid[pbx] = name;
                                    paidBookDetail(paidName, price, paidCategory);
                                    savePaidBooksDetail(name, paidName, paidCategory, price);
                                    Console.WriteLine("Book added Successfully");
                                    Console.Write("Press any key to Continue:");
                                    Console.ReadKey();
                                }
                                else if (validName == false)
                                {
                                    Console.WriteLine("Invalid Book Name");
                                    Console.Write("Press any key to Continue:");
                                    Console.ReadKey();
                                }
                                else if (alreadyExists == false)
                                {
                                    Console.WriteLine("Book Already Exists");
                                    Console.Write("Press any key to Continue:");
                                    Console.ReadKey();
                                }
                                else if (validCategory == false)
                                {
                                    Console.WriteLine("Invalid Category");
                                    Console.Write("Press any key to Continue:");
                                    Console.ReadKey();
                                }
                                else if (validPrice==false)
                                {
                                    Console.WriteLine("Invalid Price");
                                    Console.Write("Press any key to Continue:");
                                    Console.ReadKey();
                                }


                            }
                            else if (authorOption==3)
                            {
                                Console.Clear();
                                header();
                                authorLibrary(name);
                                Console.WriteLine("Press any key to continue:");
                                Console.ReadKey();
                            }
                            else if (authorOption==4)
                            {
                                Console.Clear();
                                header();
                                Console.WriteLine("Enter the name of book:");
                                string n = Console.ReadLine();
                                Console.WriteLine("Enter the category of book:");
                                string c = Console.ReadLine();
                                bool isValid = deleteBooksChecker(name, n, c);
                                if (isValid==true)
                                {
                                    deleteBook(name, n, c);
                                    Console.WriteLine("Book was deleted successfully");
                                    Console.ReadKey();
                                }
                                else if (isValid==false)
                                {
                                    Console.WriteLine("Book does not found");
                                    Console.ReadKey();
                                }
                                
                            }
                            else if (authorOption==5)
                            {
                                Console.Clear();
                                header();
                                Console.WriteLine("Enter the name of the book you want to update:");
                                string n = Console.ReadLine();
                                bool exist = isExists(n);
                                if (exist == true)
                                {
                                    Console.WriteLine("Enter the new price of the book:");
                                    string newPrice = Console.ReadLine();
                                    Console.WriteLine("Enter the new category of the book:");
                                    string newCategory = Console.ReadLine();
                                    bool validCategory = categoryChecker(newCategory);
                                    bool validPrice = priceChecker(newPrice);
                                    if (validCategory == true && validPrice==true)
                                    {
                                        updatePaid(newPrice,newCategory,name);
                                        Console.WriteLine("Book was updated successfully");
                                        Console.WriteLine("Press any key to continue");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid credentials");
                                        Console.WriteLine("Press any key to continue:");
                                        Console.ReadKey();
                                    }

                                }
                                

                            }
                        }
                    }
                    else if (rolechecker == "false")
                    {
                        Console.WriteLine("Invalid Ceredentials! Try again");
                        Console.WriteLine("Press any key to continue:");
                        Console.ReadKey();
                    }
                }
                else if (option == 2)
                {
                    Console.Clear();
                    header();
                    Console.WriteLine("SIGN-UP MENU");
                    Console.WriteLine("-------------------");
                    Console.WriteLine("Enter Your name:");
                    name = Console.ReadLine();
                    Console.WriteLine("Enter Your Password:");
                    password = Console.ReadLine();
                    Console.WriteLine("Enter Your Role(Reader or Author):");
                    role = Console.ReadLine();
                    bool isValid = isValidUserName(name);
                    bool isExists = isUserAlreadyExists(name);
                    bool isRole = isValidRole(role);
                    if (isValid == true && isExists == true && isRole == true)
                    {
                        saveUsersData(name, password, role);
                        signUp(name, password, role);
                        Console.WriteLine("Signed Up Successfully");
                        Console.WriteLine("Press any key to continue:");
                        Console.ReadKey();
                    }
                    else if (isValid == false)
                    {
                        Console.WriteLine("Invalid credentials!");
                        Console.WriteLine("Please don't use special character in name");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                    }
                    else if (isExists == false)
                    {
                        Console.WriteLine("User already exists!");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                    }
                    else if (isRole == false)
                    {
                        Console.WriteLine("Invalid Role!");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                    }
                }
               
            }
            Console.ReadKey();
        }
        static void header()
        {
            Console.WriteLine("***********************************************************************");
            Console.WriteLine("***********************************************************************");
            Console.WriteLine("**   EEEEE     L         III BBBB  RRRR       A     RRRR  Y       Y  **");
            Console.WriteLine("**   E         L          I  B   B R   R     A A    R   R   Y   Y    **");
            Console.WriteLine("**   EEEEE --- L          I  BBB   RRR      A A A   RRR       Y      **");
            Console.WriteLine("**   E         L          I  B   B R  R    A     A  R  R      Y      **");
            Console.WriteLine("**   EEEEE     L L L L L III BBBB  R    R A       A R    R    Y      **");
            Console.WriteLine("***********************************************************************");
            Console.WriteLine("***********************************************************************");
        }
        static int loginMenu()
        {
            int choice=3;
            Console.WriteLine("MAIN MENU");
            Console.WriteLine("----------------");
            Console.WriteLine("1. Sign-In to E-Library");
            Console.WriteLine("2. Sign-Up to E-Library");
            Console.WriteLine("0. Exit");
            Console.Write("Enter Your Choice:");
            choice=int.Parse(Console.ReadLine());
            return choice;
        }
        static void signUp(string name, string password, string role)
        {
            userName[userCount] = name;
            userPasswords[userCount] = password;
            userRole[userCount] = role;
            userCount++;
        }
        static bool isUserAlreadyExists(string name)
        {
            bool flag = true;
            for (int i = 0; i < userCount; i++)
            {
                if (name == userName[i])
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
        static bool isBookAlreadyExists(string name)
        {
            bool flag = true; 
            for (int i=0;i<bookCount;i++)
            {
                if (name==bookNameFree[i] || name==bookNamePaid[i])
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
        static bool isValidUserName(string name)
        {
            bool flag = true;
            for (int i=0;i<name.Length;i++)
            {
                if ((name[i]<65 && name[i]>32) || (name[i]>90 && name[i]<97) || name[i]>122 || name[i]<32)
                {
                    flag = false;
                }
            }
            return flag;
        }
        static bool isValidBookName(string name)
        {
            bool flag = true;
            for (int i=0;i<name.Length;i++)
            {
                if ((name[i]<47 && name[i]>32 ) || (name[i]>90 && name[i]<97) || (name[i]>57 && name[i]<65) || name[i]>122 || name[i]<32)
                {
                    flag = false;
                }
            }
            return flag;
        }
        static bool isValidRole(string role)
        {
            bool flag = false;
            for (int i=0;i<role.Length;i++)
            {
                if (role=="reader" || role=="Reader" || role=="READER" || role=="Author" || role=="AUTHOR" || role=="author")
                {
                    flag = true;
                }
            }
            return flag;
        }
        static bool priceChecker(string price)
        {
            bool flag = true;
            for (int i = 0; i < price.Length; i++)
            {
                if (price[i] > 57 || price[i] < 49)
                {
                    flag = false;
                }
            }
            return flag;
        }
        static void saveUsersData(string name,string password,string role)
        {
            string path = "D:\\OOP\\application\\userData.txt";
            StreamWriter fileVariable = new StreamWriter(path,true);
            fileVariable.WriteLine("{0},{1},{2}", name, password, role);
            fileVariable.Flush();
            fileVariable.Close();
        }
        static void saveFreeBooksDetail(string author,string book,string category)
        {
            string path = "D:\\OOP\\application\\freeBooks.txt";
            StreamWriter fileVariable = new StreamWriter(path, true);
            fileVariable.WriteLine("{0},{1},{2}", author, book, category);
            fileVariable.Flush();
            fileVariable.Close();
        }
        static void savePaidBooksDetail(string author,string book,string category,string price)
        {
            string path = "D:\\OOP\\application\\paidBooks.txt";
            StreamWriter fileVariable = new StreamWriter(path, true);
            fileVariable.WriteLine("{0},{1},{2},{3}", author, book, category,price);
            fileVariable.Flush();
            fileVariable.Close();
        }
        static void savePaidDetailDelete()
        {
            string path = "D:\\OOP\\application\\paidBooks.txt";
            StreamWriter fileVariable = new StreamWriter(path, false);
            for (int i=0;i<pbx-1;i++)
            {
                fileVariable.WriteLine("{0},{1},{2},{3}", authorPaid[i], bookNamePaid[i], bookCategoryPaid[i], bookPrice[i]);
            }
            
            fileVariable.Flush();
            fileVariable.Close();
        }
        static void saveFreeDetailDelete()
        {
            string path = "D:\\OOP\\application\\freeBooks.txt";
            StreamWriter fileVariable = new StreamWriter(path, false);
            for (int i = 0; i < fbx - 1; i++)
            {
                fileVariable.WriteLine("{0},{1},{2}", authorFree[i], bookNameFree[i], bookCategoryFree[i]);
            }

            fileVariable.Flush();
            fileVariable.Close();
        }
        static void readUserData()
        {
            string path = "D:\\OOP\\application\\userData.txt";
            string record = "";
           
            if (File.Exists(path))
            {
                StreamReader fileVariable = new StreamReader(path);
                while((record=fileVariable.ReadLine())!=null)
                {
                    userName[userCount] = parseData(record, 1);
                    userPasswords[userCount] = parseData(record, 2);
                    userRole[userCount] = parseData(record, 3);
                    userCount++;
                }
                fileVariable.Close();
            }
            else
            {
                Console.WriteLine("File does not exists");
            }
        }
        static void readFreeBooksDetail()
        {
            string path = "D:\\OOP\\application\\freeBooks.txt";
            string record = "";

            if (File.Exists(path))
            {
                StreamReader fileVariable = new StreamReader(path);
                while ((record = fileVariable.ReadLine()) != null)
                {
                    authorFree[fbx] = parseData(record, 1);
                    bookNameFree[fbx] = parseData(record, 2);
                    bookCategoryFree[fbx] = parseData(record, 3);
                    fbx++;
                    bookCount++;
                }
                fileVariable.Close();
            }
            else
            {
                Console.WriteLine("File does not exists");
            }

        }
        static void readPaidBooksDetail()
        {
            string path = "D:\\OOP\\application\\paidBooks.txt";
            string record = "";

            if (File.Exists(path))
            {
                StreamReader fileVariable = new StreamReader(path);
                while ((record = fileVariable.ReadLine()) != null)
                {
                    authorPaid[pbx] = parseData(record, 1);
                    bookNamePaid[pbx] = parseData(record, 2);
                    bookCategoryPaid[pbx] = parseData(record, 3);
                    bookPrice[pbx] = int.Parse(parseData(record, 4));
                    pbx++;
                    bookCount++;
                }
                fileVariable.Close();
            }
            else
            {
                Console.WriteLine("File does not exists");
            }
        }
        static string parseData(string words, int number)
        {
            int count = 1;
            string line = "";
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] == ',')
                {
                    count++;
                }
                else if (count == number)
                {
                    line = line + words[i];
                }
            }
            return line;
        }
        static string signIn(string name, string Password)
        {
            string flag = "false";
            for (int i = 0; i < userCount; i++)
            {
                if (name == userName[i] && Password == userPasswords[i])
                {
                    flag = userRole[i];
                    break;
                }
            }
            return flag;
        }
        static int authorMenu()
        {
            int choice;
            Console.WriteLine("Author Menu");
            Console.WriteLine("-------------------");
            Console.WriteLine("1.To add free books");
            Console.WriteLine("2.To add paid books");
            Console.WriteLine("3.View my Books");
            Console.WriteLine("4.To Delete Books");
            Console.WriteLine("5.To Update price of books");
            Console.WriteLine("0.Exit");
            Console.Write("Enter Your Option:");
            choice = int.Parse(Console.ReadLine());
            return choice;
        }
        static bool categoryChecker(string category)
        {
            bool flag = false;
            if (category == "Science" || category == "science" || category == "SCIENCE" || category == "history" ||
             category == "HISTORY" || category == "History" || category == "literature" || category == "Literature"
             || category == "LITERATURE" || category == "RELEGIOUS" || category == "Relegious" || category == "relegious")
            {
                flag = true;
            }
            return flag;
        }
        static void freeBookDetail(string name, string category)
        {
            bookNameFree[fbx] = name;
            bookCategoryFree[fbx] = category;
            fbx++;
            bookCount++;
        }
        static void paidBookDetail(string name, string price, string category)
        {
            bookNamePaid[pbx] = name;
            bookPrice[pbx] = int.Parse(price);
            bookCategoryPaid[pbx] = category;
            pbx++;
            bookCount++;
        }
        static void authorLibrary(string name)
        {
            int count = 0;
            Console.WriteLine("------My LIBRARY-----");
            Console.WriteLine("   Book Name\t\tBook Price\t\tBook Category");
            for (int i = 0; i < bookCount; i++)
            {
                if (name == authorPaid[i])
                {
                    Console.WriteLine("{0}.{1}\t\t{2}\t\t{3}",count+1,bookNamePaid[i],bookPrice[i],bookCategoryPaid[i]);
                    count++;
                }
            }
            for (int i = 0; i < bookCount; i++)
            {
                if (name == authorFree[i])
                {
                    Console.WriteLine("{0}.{1}\t\tFree\t\t{2}", count + 1, bookNameFree[i], bookCategoryFree[i]);
                    count++;
                }
            }
            if (count == 0)
            {
                Console.WriteLine("You did not add any book");
                Console.WriteLine("Press any key to continue:");
                Console.ReadKey();
            }
        }
        static bool deleteBooksChecker(string name,string bookName,string bookCategory)
        {
            bool flag = false;
            for (int i=0;i<pbx;i++)
            {
                if(name==authorPaid[i] && bookName==bookNamePaid[i] && bookCategory==bookCategoryPaid[i])
                {
                    flag = true;
                    del = i;
                    break;
                }
            }
            for (int i=0;i<fbx;i++)
            {
                if (name==authorFree[i] && bookName==bookNameFree[i] && bookCategory==bookCategoryFree[i])
                {
                    flag = true;
                    del1 = i;
                    break;
                }
            }
            return flag;
        }
        static void deleteBook(string name,string bookName,string bookCategory)
        {
            if (del != -1)
            {
                for (int i = del; i < pbx; i++)
                {
                    authorPaid[i] = authorPaid[i + 1];
                    bookNamePaid[i] = bookNamePaid[i + 1];
                    bookCategoryPaid[i] = bookCategoryPaid[i + 1];
                    bookPrice[i] = bookPrice[i + 1];
                }
                savePaidDetailDelete();
                pbx--;
                bookCount--;
            }
            if (del1!=-1)
            {
                for (int i = del1; i < fbx; i++)
                {
                    authorFree[i] = authorFree[i + 1];
                    bookNameFree[i] = bookNameFree[i + 1];
                    bookCategoryFree[i] = bookCategoryFree[i + 1];
                }
                saveFreeDetailDelete();
                fbx--;
                bookCount--;
            }   
        }
        static bool isExists(string name)
        {
            bool flag = false;
            for (int i=0;i<pbx;i++)
            {
                if (name==bookNamePaid[i])
                {
                    flag = true;
                    up = i;
                    break;
                }
            }
            for (int i=0;i<fbx;i++)
            {
                if (name==bookNameFree[i])
                {
                    flag = true;
                    up1 = i;
                    break;
                }
            }
            return flag;
        }
        static void updatePaid(string newPrice,string newCategory,string name)
        {
            int price = int.Parse(newPrice);
            if (up != -1)
            {
                if (price == 0)
                {
                    bookCategoryFree[fbx + 1] = newCategory;
                }
                else
                {
                    bookPrice[up] = price;
                    bookCategoryPaid[up] = newCategory;
                }
            }
            if (up1!=-1)
            {
                if (price>0)
                {
                    string free = bookNameFree[up1];
                    string freeC = bookCategoryFree[up1];
                    bool flag = deleteBooksChecker(name, free, freeC);
                    deleteBook(name, free, freeC);
                    bookNamePaid[pbx + 1] = bookNameFree[up1];
                    bookCategoryPaid[pbx + 1] = newCategory;
                    bookPrice[pbx + 1] = price;
                    authorPaid[pbx + 1] = name;
                    
                }
                else
                {
                    bookCategoryFree[up1] = newCategory;
                }
            }
            
        }
    }
}
