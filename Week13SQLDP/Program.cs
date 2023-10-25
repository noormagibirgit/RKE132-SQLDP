using System.Collections.Specialized;
using System.Data.SQLite;

ReadData(CreateConnection());
//InsertCustomer(CreateConnection());

//RemoveCustomer(CreateConnection());
FindCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source =mydb.db; Version=3; New = True; Compress = True;");

    try
   {
    connection.Open();
    Console.WriteLine("DB found.");

     }
   catch
    {
    Console.WriteLine("DB not found.");
     }
    return connection;
}

static void ReadData(SQLiteConnection myconnection)  //myconnection siin on sama mis createconnection seal üleval 
{
    Console.Clear();
    SQLiteDataReader reader;  //reader loeb ridade kaupa 
    SQLiteCommand command;  //käsk

    command= myconnection.CreateCommand();
    command.CommandText = "SELECT rowid, * FROM customer ";

    reader = command.ExecuteReader(); // hakka lugema andmeid ja salvesta readerisse 

    while (reader.Read())
    {
        string readerRowId = reader["rowid"].ToString();
        string readerStringFirstName = reader.GetString(1); 
        string readerStringLastName = reader.GetString(2);  
        string readerStringDoB = reader.GetString(3);

        Console.WriteLine($"{readerRowId}. Full name: {readerStringFirstName} {readerStringLastName}; DoB: {readerStringDoB} ");

    }
    myconnection.Close();
}

static void InsertCustomer(SQLiteConnection myconnection)  // ANDMETE LISAMINE 
{
    SQLiteCommand command;
    string fName, lName, dob;

    Console.WriteLine("Enter first Name:");
    fName = Console.ReadLine();
    Console.WriteLine("Enter last Name");
    lName = Console.ReadLine();
    Console.WriteLine("Enter date of birth (mm-dd-yyy):");
    dob = Console.ReadLine();

    command =myconnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateofBirth) " +
       $"VALUES ('{fName}', '{lName}''{dob}')";

   int rowInserted = command.ExecuteNonQuery(); //käsk mis lisab andmeid tabelisse
    Console.WriteLine($"Row inserted: {rowInserted}");

  

    ReadData(myconnection); 
}

static void RemoveCustomer(SQLiteConnection myconnection)
{

    SQLiteCommand command;

    string idToDelete;
    Console.WriteLine("Enter an id to delete a customer:");
    idToDelete = Console.ReadLine();

    command = myconnection.CreateCommand();
    command.CommandText = $"Delete from customer Where rowid = {idToDelete}";
    int rowRemoved = command.ExecuteNonQuery();
    Console.WriteLine($"{rowRemoved} was removed from the table customer.");

    ReadData(myconnection);

}

static void FindCustomer()
{

}