using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ClientWebService;

namespace BikeBook.Views.MockupPages
{
    /**
     * Dummy profile for testing UI without using the webService
     */
    class DummyProfile
    {
        public static readonly User User =
        new User()
            {
                name = "Chad Chadderson",
                email = "chad@dummy.com",

                styleSport = "true",
                styleTouring = "true",
                styleCruising = "true",
                styleAdventure = "true",
                styleTrack = "true",
                styleCommuting = "true",

                //styleSport = "false",
                //styleTouring = "false",
                //styleCruising = "false",
                //styleAdventure = "false",
                //styleTrack = "false",
                //styleCommuting = "false",

                //styleSport = "true",
                //styleTouring = "false",
                //styleCruising = "false",
                //styleAdventure = "false",
                //styleTrack = "true",
                //styleCommuting = "false",

                picture = ImageSerializer.SAMPLEIMAGE,
                hashed_password = "WHATEVERDOESNTMATTER",
                created_at = "Tue May 16 2017 22:56:08 GMT + 0000(UTC)",

                description = "Just a guy, doing stuff I guess. I dropped my sunglasses in the ocean once, hit me up if you find them. Text me if you wanna jam 250-222-3333.",

                temp_password = string.Empty,
                temp_password_time = string.Empty
            };

        public static readonly Bike Bike =
            new Bike()
            {
                owner = User.email,
                picture = ImageSerializer.SAMPLEIMAGE,
                make = "Suzuki",
                model = "Hayabusa",
                year = "2011",
                color = "Red",
                milage = "312",
                exhaust = "Two Brothers Slip-on",
                accessories = "Flip up license plate",
                description = "this my baby, she go so fast",
            };

        public static readonly Gear Gear =
            new Gear()
            {
                owner = User.email,
                picture = ImageSerializer.SAMPLEIMAGE,
                make = "Icon",
                model = "AKORP",
                color = "BLACK",
                year = "2017",
                category = "Top",
                description = "Batman Villian Jacket, yo",

            };

        public static readonly Post Post =
            new Post()
            {
                owner = User.email,
                picture = ImageSerializer.SAMPLEIMAGE,
                content = "test post, yo",
                created_at = "Tue May 16 2017 22:56:08 GMT + 0000(UTC)",
            };
    }

}
