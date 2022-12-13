using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Collections.ObjectModel;
using Npgsql;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// https://www.dotnetperls.com/serialize-list
// https://www.daveoncsharp.com/2009/07/xml-serialization-of-collections/

// Written by Rudy Liljeberg
// Reviewed by Shabbar Kazmi
namespace UWOsh_InteractiveMap
{

    /// <summary>
    /// This is the database class
    /// </summary>
    public class PlantDatabase : IDatabase
    {


        String connectionString;
        /// <summary>
        /// A local version of the database
        /// </summary>
        public ObservableCollection<Plant> Plants = new ObservableCollection<Plant>();

        JsonSerializerOptions options;

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Here or thereabouts initialize a connectionString that will be used in all the SQL calls
        /// </summary>
        public PlantDatabase()
        {
            connectionString = InitializeConnectionString();
        }


        /// <summary>
        /// Finds a specific entry
        /// </summary>
        /// <param name="id">id to find</param>
        /// <returns>the Entry (if available)</returns>
        public Plant FindEntry(int id)
        {
            foreach (Plant entry in Plants)
            {
                if (entry.Id == id)
                {
                    return entry;
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves all the entries
        /// </summary>
        /// <returns>all of the entries</returns>
        public ObservableCollection<Plant> GetPlants()
        {
            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            var sql = "SELECT * FROM \"plants\" limit 30;";

            using var cmd = new NpgsqlCommand(sql, con);

            using NpgsqlDataReader reader = cmd.ExecuteReader();

            // Columns are popularname, scientificname, description, coordinates, imageurl, and id in that order ...
            // Show all data
            Plants = new ObservableCollection<Plant>();
            while (reader.Read())
            {
                for (int colNum = 0; colNum < reader.FieldCount; colNum++)
                {
                    Console.Write(reader.GetName(colNum) + "=" + reader[colNum] + " ");
                }
                Console.Write("\n");
                Plants.Add(new Plant((long)reader[0], reader[1] as String, reader[2] as String, reader[3] as String, reader[4] as String, reader[5] as String));
            }

            con.Close();

            return Plants;
        }

        /// <summary>
        /// Creates the connection string to be utilized throughout the program
        /// 
        /// </summary>
        public String InitializeConnectionString()
        {
            var bitHost = "db.bit.io";
            var bitApiKey = "v2_3w6n8_KUHYQcFKDQn5ndg3rETaTpR"; // from the "Password" field of the "Connect" menu

            var bitUser = "wastartb";
            var bitDbName = "wastartb/UWOsh";

            return connectionString = $"Host={bitHost};Username={bitUser};Password={bitApiKey};Database={bitDbName}";
        }
    }
}