using System;
using Npgsql;

class Program
{
    static void Main()
    {
        // Connection string langsung ke Supabase (pastikan benar)
        var connString = "Host=db.oolkixbjpebwhskhuwwg.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=subagioAQ123;SslMode=Require;Trust Server Certificate=true";

        try
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            Console.WriteLine("✅ Connected to Supabase PostgreSQL successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Failed to connect:");
            Console.WriteLine(ex.Message);
        }
    }
}
