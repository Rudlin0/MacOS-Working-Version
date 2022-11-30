using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace UWOsh_InteractiveMap
{
    public class Plant : ObservableObject
    {

        const int MAX_DIFFICULTY = 2;
        String popularname;
        String scientificname;
        String description;
        String coordinates;
        String imageurl;
        int id;

        public String PopularName
        {
            get { return popularname; }
            set { SetProperty(ref popularname, value); }
        }

        public String ScientificName
        {
            get { return scientificname; }
            set { SetProperty(ref scientificname, value); }
        }

        public String Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        public String Coordinates
        {
            get { return coordinates;  }
            set { SetProperty(ref coordinates, value); }
        }

        public String ImageUrl
        {
            get { return imageurl;  }
            set { SetProperty(ref imageurl, value); }
        }

        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        public Plant(String popularname, String scientificname, String description,
                     String coordinates, String imageurl, int id)
        {
            this.popularname = popularname;
            this.scientificname = scientificname;
            this.description = description;
            this.coordinates = coordinates;
            this.imageurl = imageurl;
            this.id = id;
        }

    }
}

