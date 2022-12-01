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
        long id;

        public String PopularName
        {
            get { return popularname; }
        }

        public String ScientificName
        {
            get { return scientificname; }
        }

        public String Description
        {
            get { return description; }
        }

        public String Coordinates
        {
            get { return coordinates;  }
        }

        public String ImageUrl
        {
            get { return imageurl;  }
        }

        public long Id
        {
            get { return id; }
        }

        public Plant(long id, String popularname, String scientificname, String description,
                     String coordinates, String imageurl)
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

