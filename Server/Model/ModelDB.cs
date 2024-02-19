namespace hmw;

using System.Data.SQLite;
using System.Collections.Generic;

public class SQLLiteOrderRepository : IOrderRepository
{
    private string _connectionString;
    private List<Order> orders = new List<Order>();
    private const string CreateTableQuery = @"
        CREATE TABLE IF NOT EXISTS Orders (
            Id INTEGER PRIMARY KEY,
            Name TEXT NOT NULL,
            Price REAL NOT NULL
        )";
    public SQLLiteOrderRepository(string connectionString)
    {
         _connectionString = connectionString;
        InitializeDatabase();
        ReadDataFromDatabase();
    }

    private void ReadDataFromDatabase()
    {
        orders = GetAllOrderss();
    }

    private void InitializeDatabase()
    {
        SQLiteConnection connection = new SQLiteConnection(_connectionString); 
        Console.WriteLine("База данных :  " + _connectionString + " создана");
        connection.Open();
        SQLiteCommand command = new SQLiteCommand(CreateTableQuery, connection);
        command.ExecuteNonQuery();
    }

    public List<Order> GetAllOrders()
    {
        List<Order> Orders = new List<Order>();
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Orders";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order(reader["Name"].ToString(),Convert.ToDouble(reader["Price"]),Convert.ToInt32(reader["Id"])); 
                        orders.Add(order);
                    }
                }
            }
        }
        return orders;
    }

    public Order GetOrderByName(string name)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Orders WHERE Name = @Name";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        Order order = new Order(reader["Name"].ToString(),Convert.ToDouble(reader["Price"]),Convert.ToInt32(reader["Id"]));
                        return  order;
                    }
                    return null;
                }
            }
        }
    }


    public void AddOrder(Order order)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Orders (Name, Price, Id) VALUES (@Name, @Price, @Id)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", order.Name);
                command.Parameters.AddWithValue("@Price", order.Price);
                command.Parameters.AddWithValue("@Id", order.Id);
                command.ExecuteNonQuery();
            }
        }
    }


    public void DeleteOrder(string id)
    {
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            string query = "DELETE FROM Orders WHERE Id = @Id";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }


}