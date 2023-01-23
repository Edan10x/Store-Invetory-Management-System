using Dapper;
using Store_Invetory_Management_System;
using System.Data.SqlClient;

internal class Program
{

    // get access to all methods 
    static SqlConnection? connection;
    static bool quitProgram = false;


    private static void Main(string[] args)
    {

        var cs = @"Server=DESKTOP-MASQELN\EDANROSENSQL;Database=Learning;Trusted_Connection=True;";

        connection = new SqlConnection(cs);
        connection.Open();

        var products = connection.Query<Product>("SELECT * FROM product").ToList();
        var employees = connection.Query<Employee>("SELECT * FROM employee").ToList();
        var stors = connection.Query<Store>("SELECT * FROM store").ToList();



        Console.WriteLine("Welcome to C# store");

        while (quitProgram == false){

            Console.WriteLine("Enter store location? ");
            var userinput = Console.ReadLine();



         

        }


    }

    // function to add an item to the store
    public static void AddProduct()
    {
        Console.WriteLine("Product name? ");

        // get the user input
        var productName = Console.ReadLine();

        Console.WriteLine("Store ID? ");
        var storeId = Console.ReadLine();

        // store the user input in the Product table in database
        connection.Query($"INSERT INTO [dbo].[Product]\r\nVALUES ('{productName}', '{storeId}');");
    }

    // function to remove an item from the database
    public static void RemoveProduct()
    {

        Console.WriteLine("Whice product would you like to delete ");
        string productId = Console.ReadLine();

        // this would delete the ID of the product
        connection.Query($"DELETE FROM[Learning].[dbo].[Product] WHERE Id = {productId};");
    }

    // function to show the items in the database
    public static List<Product> ListProducts(int storeId)
    {

        var products = connection.Query<Product>("SELECT * FROM product").ToList();
        connection.Query($"SELECT* FROM[dbo].[Product] WHERE UserId = '{storeId}';");

        //this will show the NAME and ID of each product
        foreach (var item in products)
        {
            Console.WriteLine("===========================");
            Console.WriteLine("Item: " + item.NameOfProduct);
            Console.WriteLine("ID: " + item.Id);
            Console.WriteLine("User ID: " + item.StoreID);
            Console.WriteLine("===========================\n");
        }

        return products;
    }

    // function to quit the program
    public static void Quit()
    {
        quitProgram = true;
    }
}