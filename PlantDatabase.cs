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

// Written by Rudy Liljeberg and Shabbar Kazmi 
// Reviewed by Shabbar Kazmi
namespace UWOsh_InteractiveMap
{

    /// <summary>
    /// This is the database class
    /// </summary>
    public class PlantDatabase : IDatabase
    {

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        String connectionString;

        JsonSerializerOptions options;

        /// <summary>
        /// Here or thereabouts initialize a connectionString that will be used in all the SQL calls
        /// </summary>
        public PlantDatabase()
        {
            connectionString = InitializeConnectionString();
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        Location compassLoc;

        public Location selectedLocation
        {
            get { return compassLoc; }
            set { compassLoc = value; }
        }

        /// <summary>
        /// Retrieves all the entries
        /// </summary>
        /// <returns>all of the entries</returns>
        public ObservableCollection<Plant> GetPlants()
        {

            ObservableCollection<Plant> plants;

            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            var sql = "SELECT * FROM \"plants\" limit 30;";

            using var cmd = new NpgsqlCommand(sql, con);

            using NpgsqlDataReader reader = cmd.ExecuteReader();

            // Columns are popularname, scientificname, description, coordinates, imageurl, and id in that order ...
            // Show all data
            plants = new ObservableCollection<Plant>();
            while (reader.Read())
            {
                for (int colNum = 0; colNum < reader.FieldCount; colNum++)
                {
                    Console.Write(reader.GetName(colNum) + "=" + reader[colNum] + " ");
                }
                Console.Write("\n");
                plants.Add(new Plant((long)reader[0], reader[1] as String, reader[2] as String, reader[3] as String, reader[4] as String, reader[5] as String));
            }

            con.Close();

            return plants;
        }

        /**
         * returns a list of List<String> of unique plant locations 
         * 
         */
        public List <String> GetPlantsLoc()
        {

            var plantsbyLoc = new List <String>();

            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            var sql = "SELECT DISTINCT COORDINATES FROM \"plants\";";
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            // Columns are id, common name, scientific name, description, coordinates, image url in that order ...
            // Show all data
            while (reader.Read())
            {
                plantsbyLoc.Add(reader[0] as String);               
            }
            con.Close();

            return plantsbyLoc;

        }

        /**
         * Retrieves entries according to input locations
         * <input> List <String>
         * <return></return> an ObservableCollection<ObservableCollection<Plant>> 
         * 
         */

        public ObservableCollection<ObservableCollection<Plant>> GetPlantFromLocation(List <String> plantLocs)
        {
           
            ObservableCollection<ObservableCollection<Plant>> plantAtLocList =
                new ObservableCollection<ObservableCollection<Plant>>();

            String plantCoordinates = plantLocs[0];

            using var con = new NpgsqlConnection(connectionString);
            con.Open();

            for (int i = 0; i < plantLocs.Count; i++)
            {
                ObservableCollection<Plant> plantByLoc = new ObservableCollection<Plant>();
                plantCoordinates = plantLocs[i] as String;

                String sql = $"SELECT * FROM \"plants\" WHERE COORDINATES = \'{plantCoordinates}\' ;";
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();

                // Columns are id, common name, scientific name, description, coordinates, image url in that order ...
                // Show all data
                while (reader.Read())
                {
                    plantByLoc.Add(new Plant((long)reader[0], reader[1] as String, reader[2] as String, reader[3] as String, reader[4] as String, reader[5] as String));
                }

                plantAtLocList.Add(plantByLoc);

            }

            con.Close();
            return plantAtLocList;

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