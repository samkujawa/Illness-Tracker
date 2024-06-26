using SQLite;

namespace SymptomTracker;
public class DB
{
    private static string DBName = "log.db";
    public static SQLiteConnection conn;

    public static void OpenConnection()
    {
        string libFolder = FileSystem.AppDataDirectory;
        string fname = System.IO.Path.Combine(libFolder, DBName);
        conn = new SQLiteConnection(fname);

        CreateTables(); // Ensure the tables are created
    }

    private static void CreateTables()
    {
        // This will create the table if it doesn't exist
        conn.CreateTable<Symptom>();
    }

    public static void ClearDatabase()
    {
        // Drop the Symptom table
        conn.DropTable<Symptom>();

        // Recreate the Symptom table
        CreateTables();
    }

}
