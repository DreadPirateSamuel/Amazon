/*
using Microsoft.VisualBasic.FileIO;
using Amazon.MAUI.Models;
using Amazon.MAUI.Services;
using System.Data.Common;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using System.Collections.Specialized;

namespace Amazon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var itemService = ItemServiceProxy.Current;
            var cartService = ShoppingCartServiceProxy.Current;
            var cart = new ShoppingCart();
            menu();

            void menu()
            {
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1. Inventory Management");
                Console.WriteLine("2. Shop");
                Console.WriteLine("3. Exit");
                Console.WriteLine("Enter an option: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int choiceInput))
                {
                    if (choiceInput == 1)
                    {
                        Inventory();
                    }
                    else if (choiceInput == 2)
                    {
                        Shop();
                    }
                    else if (choiceInput != 3)
                    {
                        Console.WriteLine("Choose an option on the main menu.");
                        menu();
                    }
                }
                else
                {
                    Console.WriteLine("Enter an appropriate integer option.");
                    menu();
                }


            }

            void Inventory()
            {
                string? name = "";
                string? desc = "";
                decimal price = 0;
                int id = 0;
                int quantity = 0;

                Console.WriteLine("Welcome! What would you like to do?");
                Console.WriteLine("Inventory Management Menu:");
                Console.WriteLine("(C)reate an item.");
                Console.WriteLine("(R)ead a list of the items and their information.");
                Console.WriteLine("(U)pdate an item's information.");
                Console.WriteLine("(D)elete an item.");
                Console.WriteLine("(E)xit to the main menu.");
                char choice = Console.ReadLine()[0];

                switch (choice)
                {
                    case 'C':
                    case 'c':
                        {
                            Console.WriteLine("Enter a name for the item:");
                            name = Console.ReadLine();
                            Console.WriteLine("Enter a description for the item:");
                            desc = Console.ReadLine();
                            Console.WriteLine("Enter a price for the item:");
                            price = decimal.Parse(Console.ReadLine());
                            Console.WriteLine("Enter a quantity for the item:");
                            quantity = int.Parse(Console.ReadLine());

                            var item = new Item
                            {
                                Name = name,
                                Description = desc,
                                Price = price,
                                Quantity = quantity
                            };

                            itemService.AddOrUpdate(item);
                            menu();
                            break;
                        }

                    case 'R':
                    case 'r':
                        {
                            if (!itemService.Items.Any())
                            {
                                Console.WriteLine("No items found in the inventory.");
                            }
                            else
                            {
                                itemService.ListInventory();
                            }

                            menu();
                            break;
                        }

                    case 'U':
                    case 'u':
                        {
                            Console.WriteLine("Enter the ID of the item you wish to update:");
                            id = int.Parse(Console.ReadLine());
                            var specific = itemService.Specific(id);

                            Console.WriteLine("Enter a new name for the item:");
                            name = Console.ReadLine();
                            Console.WriteLine("Enter a new description for the item:");
                            desc = Console.ReadLine();
                            Console.WriteLine("Enter a new price for the item:");
                            price = decimal.Parse(Console.ReadLine());
                            Console.WriteLine("Enter a quantity for the item:");
                            quantity = int.Parse(Console.ReadLine());

                            //Updating the items information here.
                            specific.Name = name;
                            specific.Description = desc;
                            specific.Price = price;
                            specific.Quantity = quantity;

                            itemService.AddOrUpdate(specific);
                            menu();
                            break;
                        }

                    case 'D':
                    case 'd':
                        {
                            Console.WriteLine("Enter the ID of the item you wish to delete:");
                            id = int.Parse(Console.ReadLine());

                            itemService.Delete(id);
                            menu();
                            break;
                        }

                    case 'E':
                    case 'e':
                        {
                            menu();
                            break;
                        }

                    default:
                        Console.WriteLine("Invalid option. Returning to the main menu.");
                        menu();
                        break;
                }
            }

            void Shop()
            {
                int index = 0;
                int counter = 0;  //Used in integer list to keep track of how many items bought.
                string? name = "";
                string? desc = "";
                decimal price = 0;
                int id = 0;

                Console.WriteLine("Welcome! What would you like to do?");
                Console.WriteLine("Shop Menu:");
                Console.WriteLine("(P)lace an item in shopping cart.");
                Console.WriteLine("(R)emove an item from shopping cart.");
                Console.WriteLine("(C)heck out.");
                Console.WriteLine("(S)how a list of items in cart.");
                Console.WriteLine("(E)xit to the main menu.");
                Console.WriteLine("Enter an option:");
                char choice = Console.ReadLine()[0];

                switch (choice)
                {
                    case 'P':
                    case 'p':
                        {
                            Console.WriteLine("Enter the ID of the item you wish to add to your cart:");
                            id = int.Parse(Console.ReadLine());
                            var specific = itemService.Specific(id);
                            Console.WriteLine("How many are you buying?");
                            counter = int.Parse(Console.ReadLine());
                            cartService.AddItem(cart, specific, counter, itemService);

                            menu();
                            break;
                        }

                    case 'R':
                    case 'r':
                        {

                            Console.WriteLine("Enter the ID of the item you wish to remove from your cart:");
                            id = int.Parse(Console.ReadLine());

                            Console.WriteLine("How many are you removing?");
                            counter = int.Parse(Console.ReadLine());
                            cartService.DeleteItem(cart, id, counter, itemService);

                            menu();
                            break;
                        }

                    case 'C':
                    case 'c':
                        {
                            cartService.Receipt(cart);
                            break;

                        }

                    case 'S':
                    case 's':
                        {
                            cartService.ListContents(cart);

                            menu();
                            break;
                        }

                    case 'E':
                    case 'e':
                        {
                            menu();
                            break;
                        }

                    default:
                        Console.WriteLine("Invalid option. Returning to the main menu.");
                        menu();
                        break;
                }
            }
        }
    }
}
*/