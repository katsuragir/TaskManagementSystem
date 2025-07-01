using System;
using Npgsql;

class Program
{
    static void Main()
    {
        var cs = "Host=db.oolkixbjpebwhskhuwwg.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=subagioAQ123;SslMode=Require;Trust Server Certificate=true";
        using var con = new NpgsqlConnection(cs);
        con.Open();
        Console.WriteLine("✅ Connected to Supabase!");
    }
}