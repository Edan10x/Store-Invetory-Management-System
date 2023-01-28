using Dapper;
using Store_Invetory_Management_System;
using System;
using System.Data.SqlClient;
using System.Threading.Channels;

internal class Program
{

    // get access to all methods 
    static SqlConnection? connection;
    static bool quitProgram = false;
    static bool quitLogin = false;


    private static void Main(string[] args)
    {

        var cs = @"Server=DESKTOP-MASQELN\EDANROSENSQL;Database=Store_Invetory_Management;Trusted_Connection=True;";

        connection = new SqlConnection(cs);
        connection.Open();

        var product = connection.Query<Product>("SELECT * FROM product").ToList();
        var employees = connection.Query<Employee>("SELECT * FROM employee").ToList();
        var store = connection.Query<Store>("SELECT * FROM store").ToList();
        var logins = connection.Query<Login>("SELECT * FROM login").ToList();



        Console.WriteLine("Welcome to C# supermarket");
        while (quitProgram == false)
        {
            while(quitLogin == false)
            {

                Console.WriteLine("Do you have a login info? yes/no");
                var yesNo = Console.ReadLine();
                yesNo = yesNo?.ToLower();

                if (yesNo == "yes")
                {
                    IsLogingValid();
                }
                else if (yesNo == "no")
                {
                    Console.WriteLine("Let's begin by creating one");
                    NewLogin();
                }
                else
                {
                    Console.WriteLine("Please enter yes/no");
                }
            }
           
        }
        // this would close the connection to the database
        connection.Close();

    }

    public static bool IsLogingValid() {

        Console.WriteLine("User name: ");
        var userName = Console.ReadLine();

        Console.WriteLine("Password: ");
        var password = Console.ReadLine();

        var loginVarefication = connection.Query<Login>($"SELECT * FROM [Login] WHERE Password = '{password}' and  UserName = '{userName}'; \r\n").ToList();

        if (loginVarefication.Count == 0)
        {
            Console.WriteLine("Worng password or user name! Please try again");
            return false;
        }
        else
        {

            Console.WriteLine("Great, logged in successfully");
            return true;
        }
        
    }

    public static void NewLogin()
    {

        Console.WriteLine("Enter a user name: ");
        var userName = Console.ReadLine();

        Console.WriteLine("Create a strong password: ");
        var userPassword = Console.ReadLine();

        // store the employee's new login info
        connection.Query($"INSERT INTO [dbo].[Login]\r\nVALUES ('{userName}', '{userPassword}');");


       
      
    } 

    //function to add employee to the store

    public static void AddEmployee()
    {

        Console.WriteLine("First name? ");

        // get the user input
        var firstName = Console.ReadLine();

        Console.WriteLine("Last name? ");
        var lastName = Console.ReadLine();

        Console.WriteLine("What is your title? ");
        var title = Console.ReadLine();

        // store the employee's info
        connection.Query($"INSERT INTO [dbo].[Employee]\r\nVALUES ('{firstName}', '{lastName}','{title}');");

    }

    // function to remove an item from the database
    public static void RemoveEmoloyee()
    {

        Console.WriteLine("Whice employee would you like to delete? ");
        string employeeId = Console.ReadLine();

        // this would delete the ID of the employee
        connection.Query($"DELETE FROM[Store_Invetory_Management].[dbo].[Employee]  WHERE Id = {employeeId};");
    }

    // function to show the items in the database
    public static List<Employee> ListEmployee(int id)
    {

        var employee = connection.Query<Employee>("SELECT * FROM employee").ToList();
        connection.Query($"SELECT* FROM[dbo].[Employee] WHERE id = '{id}';");

        //this will show the NAME and ID of each product
        foreach (var item in employee)
        {
            Console.WriteLine("===========================");
            Console.WriteLine("Employee's first name: " + item.FirstName);
            Console.WriteLine("Employee's last name: " + item.LastName);
            Console.WriteLine("Employee title: " + item.Title);
            Console.WriteLine("===========================\n");
        }

        return employee;
    }

    // function to add an item to the store
    public static void AddProduct()
    {
        Console.WriteLine("Product name? ");

        // get the user input
        var name = Console.ReadLine();

        Console.WriteLine("Store ID? ");
        var storeId = Console.ReadLine();

        // store the user input in the Product table in database
        connection.Query($"INSERT INTO [dbo].[Product]\r\nVALUES ('{name}', '{storeId}');");
    }

    // function to remove an item from the database
    public static void RemoveProduct()
    {

        Console.WriteLine("Whice product would you like to remove ");
        string productId = Console.ReadLine();

        // this would delete the ID of the product
        connection.Query($"DELETE FROM[Store_Invetory_Management].[dbo].[Product]  WHERE Id = {productId};");
    }

    // function to show the items in the database
    public static List<Product> ListProducts(int productId)
    {

        var products = connection.Query<Product>("SELECT * FROM product").ToList();
        connection.Query($"SELECT* FROM[dbo].[Inventory] WHERE ProductId = '{productId}';");

        //this will show the NAME and ID of each product
        foreach (var item in products)
        {
            Console.WriteLine("===========================");
            Console.WriteLine("Product name: " + item.Name);
            Console.WriteLine("product ID: " + item.Id);
            Console.WriteLine("Price: " + item.Price);
            Console.WriteLine("Store ID: " + productId);
            Console.WriteLine("===========================\n");
        }

        return products;
    }

    //this will show the inventory list
    public static List<Inventory> ListInventory(int storeId)
    {

        var inventory = connection.Query<Inventory>("SELECT * FROM inventory").ToList();
        // need to fix
        connection.Query($"SELECT* FROM[dbo].[Product] WHERE UserId = '{storeId}';");

       
        foreach (var item in inventory)
        {
            Console.WriteLine("===========================");
            Console.WriteLine("Store ID: " + item.StoreId);
            Console.WriteLine("product ID: " + item.ProductId);
            Console.WriteLine("Quantity: " + item.Quantity);
            Console.WriteLine("On aisle: " + item.Aisle);
            Console.WriteLine("===========================\n");
        }

        return inventory;
    }

    // function to log out of the employee login
    public static void QuitLogin()
    {
        quitLogin = true;
    }

    // function to quit the program
    public static void Quit()
    {
        quitProgram = true;
    }
}