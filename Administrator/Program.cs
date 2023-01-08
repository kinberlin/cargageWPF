using System;
using System.Collections.Generic;
using DAO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Administrator
{
    class Program
    {
        static int tableWidth = 93;
        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
        static void Main(string[] args)
        {
            bool loop = true;//Determine whether or not loop should coontinue 
            int option = 0;
            //Repository Objects
            UserRepository userrepo = new UserRepository();
            ProductRepository prorepo = new ProductRepository();
            CategoryRespository categoryrepo = new CategoryRespository();
            SaleRespository salerepo = new SaleRespository();
            
            //Object List
            List<User> users = new List<User>();
            List<Category> categories = new List<Category>();
            List<Product> products = new List<Product>();
            List<Sales> sales = new List<Sales>();
            while (loop)
            {
                Console.Clear();
                Console.WriteLine("\t\t***********  Welcome to Dashboard Admin ************* \n");
                Console.WriteLine("\t ----- View ------- \n1) View Products \n2) View Customer List \n3) ViewProduct Category List \n4) Sales History\n");
                Console.WriteLine("\t ----- Ajouter ------- \n5) Add a Category \n6) Add a Product \n");
                Console.WriteLine("\t ----- Mettre a Jour ------- \n7) Update a Product \n8) Update a Category \n9) Quit Program \n");
                Console.Write("Please Choose an option and enter a value ");
                int.TryParse(Console.ReadLine(), out option);
                switch (option)
                {
                    case 1:
                        {
                            Console.Clear();
                            Console.Write("\t\t -*-*-*-*-*  La Liste de Tout les Produits  -*-*-*-*-* \n\n");
                            products = prorepo.Get();
                            PrintLine();
                            PrintRow("Name", "Price", "Quantity", "Image");
                            PrintLine();
                            foreach (var p in products)
                            {
                                var ims = "Has no image";
                                if (p.image != null || p.image.Length > 0) { ims = "Has an Image"; }
                                //Console.WriteLine(p.nom + " | Prix : " + p.prix + " | Quantite : " + p.quantite + " | "+ims);
                                PrintRow(p.nom, p.prix.ToString(), p.quantite.ToString(), ims);
                                PrintLine();
                            }
                            Console.Write("Press any key to go Back to Menu");
                            Console.ReadLine();
                        }
                        break;
                    case 2:
                        {
                            Console.Clear();
                            Console.Write("\t\t -*-*-*-*-*  Customer List  -*-*-*-*-* \n\n");
                            users = userrepo.Get();
                            PrintLine();
                            PrintRow("Id", "Name","Login", "Password");
                            PrintLine();
                            foreach (var u in users)
                            { 
                                //Console.WriteLine(p.nom + " | Prix : " + p.prix + " | Quantite : " + p.quantite + " | "+ims);
                                PrintRow(u.id.ToString(), u.nom+" "+u.prenom, u.login, u.password);
                                PrintLine();
                            }
                            Console.Write("Press any key to go Back to Menu");
                            Console.ReadLine();
                        }
                        break;
                    case 3:
                        {
                            Console.Clear();
                            Console.Write("\t\t -*-*-*-*-*  Category List  -*-*-*-*-* \n\n");
                            categories = categoryrepo.Get();
                            PrintLine();
                            PrintRow("Id", "Name", "Description");
                            PrintLine();
                            foreach (var c in categories)
                            {
                                //Console.WriteLine(p.nom + " | Prix : " + p.prix + " | Quantite : " + p.quantite + " | "+ims);
                                PrintRow(c.id.ToString(), c.nom, c.description);
                                PrintLine();
                            }
                            Console.Write("Press any key to go Back to Menu");
                            Console.ReadLine();
                        }
                        break;
                    case 4:
                        {
                            Console.Clear();
                            Console.Write("\t\t -*-*-*-*-*  Sale History  -*-*-*-*-* \n\n");
                            sales = salerepo.Get();
                            PrintLine();
                            PrintRow("Id", "Product", "Quantity", "Date") ;
                            PrintLine();
                            foreach (var s in sales)
                            {
                                var pr_ = prorepo.Get(s.product);
                                //Console.WriteLine(p.nom + " | Prix : " + p.prix + " | Quantite : " + p.quantite + " | "+ims);
                                PrintRow(s.id.ToString(), pr_.nom, s.quantity.ToString(), s.sale_date.ToString());
                                PrintLine();
                            }
                            Console.Write("Press any key to go Back to Menu");
                            Console.ReadLine();
                        }
                        break;
                    case 5:
                        {
                            try
                            {
                                Console.Clear();
                                Console.Write("\t\t -*-*-*-*-*  Add a Category  -*-*-*-*-* \n\n");
                                var newcategory = new Category();
                                Console.Write("Enter category name : ");
                                newcategory.nom = Console.ReadLine();
                                Console.Write("\nEnter a description for this Category : ");
                                newcategory.description = Console.ReadLine();
                                categoryrepo.Add(newcategory);
                                Console.WriteLine("***************************************");
                                Console.WriteLine("*\t Operation was Successfull ");
                                Console.WriteLine("***************************************");
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine("***************************************");
                                Console.WriteLine("*\t An Error Occured ");
                                Console.WriteLine("*"+ex.Message);
                                Console.WriteLine("***************************************");
                            }
                            Console.Write("Press any key to go Back to Menu");
                            Console.ReadLine();
                        }
                        break;
                    case 6:
                        {
                            try
                            {
                                Console.Clear();
                                Product newproduct = new Product();
                                Console.Write("\t\t -*-*-*-*-*  Add a Product  -*-*-*-*-* \n\n");
                                Console.WriteLine("\t First of all choose a category from the list below to which you will like the product to belong ");
                                categories = categoryrepo.Get();
                                PrintLine();
                                PrintRow("Id", "Name", "Description");
                                PrintLine();
                                foreach (var c in categories)
                                {
                                    //Console.WriteLine(p.nom + " | Prix : " + p.prix + " | Quantite : " + p.quantite + " | "+ims);
                                    PrintRow(c.id.ToString(), c.nom, c.description);
                                    PrintLine();
                                }
                                Console.Write("make your choice by entering the id of the Category you choosed : ");
                                int _cat = -1, pric = 0, quantity=0 ;
                                var isint = int.TryParse(Console.ReadLine(), out _cat);
                                 while(!isint)
                                {
                                    Console.WriteLine("xxxxxx   Sorry, you probably entered an invalid number   xxxxxx \n Please Try Again");
                                    Console.Write("Enter the id of the Category you have choosen : ");
                                    isint = int.TryParse(Console.ReadLine(), out _cat);
                                }
                                 var _category = categories.FirstOrDefault(x => x.id == _cat);
                                 if(_category != null) {
                                    newproduct.category = _category.id;
                                Console.Write("\nEnter the product name : ");
                                newproduct.nom = Console.ReadLine();
                                Console.Write("\nEnter a price for this product : ");
                                    isint = int.TryParse(Console.ReadLine(), out pric);
                                    while (!isint)
                                    {
                                        Console.WriteLine("xxxxxx   Sorry, you probably entered an invalid number   xxxxxx \n Please Try Again");
                                        Console.Write("Enter a price for this product : ");
                                        isint = int.TryParse(Console.ReadLine(), out pric);
                                    }
                                    newproduct.prix = pric;
                                    Console.Write("\nQuantity : ");
                                    isint = int.TryParse(Console.ReadLine(), out quantity);
                                    while (!isint)
                                    {
                                        Console.WriteLine("xxxxxx   Sorry, you probably entered an invalid number   xxxxxx \n Please Try Again");
                                        Console.Write("\n Quantity :  ");
                                        isint = int.TryParse(Console.ReadLine(), out quantity);
                                    }
                                    newproduct.quantite = quantity;
                                Console.Write("\nEnter a description for this Product : ");
                                newproduct.description = Console.ReadLine();
                                Console.Write("\nEnter a valid image path : ");
                                string path = Console.ReadLine();
                                var fexist = File.Exists(path);
                                    while (!fexist)
                                    {
                                        Console.Write("\nEnter a valid image path : ");
                                       path = @Console.ReadLine();
                                        fexist = File.Exists(path);
                                    }
                                 newproduct.image =  File.ReadAllBytes(path);
                                    newproduct.id = 0;
                                    prorepo.Add(newproduct);
                                    Console.WriteLine("***************************************");
                                Console.WriteLine("*\t Operation was Successfull ");
                                Console.WriteLine("***************************************");
                                }else
                                {
                                    Console.WriteLine("***************************************");
                                    Console.WriteLine("*\t Category couldn't be found in list");
                                    Console.WriteLine("***************************************");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("***************************************");
                                Console.WriteLine("*\t An Error Occured ");
                                Console.WriteLine("*" + ex.Message);
                                Console.WriteLine("***************************************");
                            }
                            Console.Write("Press any key to go Back to Menu");
                            Console.ReadLine();
                        }
                        break;

                    case 9:
                        // code block
                        break;
                    default:
                        // code block
                        break;
                }
            }
        }
    }
}
