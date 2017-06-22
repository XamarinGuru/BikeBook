using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ClientWebService
{
    public struct Login
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }

    public struct ProfileDescription
    {
        public string Description { get; set; }
    }

    public struct Messages
    {
        public List<Message> Message { get; set;}
    }

    public struct Message
    {
        public string from { get; set; }
        public string to { get; set; }
        public string content { get; set; }
        public string picture { get; set; }
        public string created_at { get; set; }
    }

    public struct Ads
    {
        public List<Ad> ad { get; set; }
    }

    public struct Ad
    {
        public string owner { get; set; }
        public string title { get; set; }
        public string picture { get; set; }
        public string description { get; set; }
        public string city { get; set; }
        public string provence { get; set; }
        public string created_at { get; set; }
    }

    public struct Posts
    {
        public List<Post> post { get; set; }
    }

    public struct Post
    {
        public string owner { get; set; }
        public string picture { get; set; }
        public string content { get; set; }
        public string created_at { get; set; }
    }
    
    public struct Users
    {
        public List<User> user {get; set;}
    }

    public struct User
    {
        public string name { get; set; }
        public string email { get; set; }
        public string description { get; set; }
        public string styleSport { get; set; }
        public string styleTouring { get; set; }
        public string styleCruising { get; set; }
        public string styleAdventure { get; set; }
        public string styleTrack { get; set; }
        public string styleCommuting { get; set; }
        public string picture { get; set; }
        public string hashed_password { get; set; }
        public string created_at { get; set; }
        public string temp_password { get; set; }
        public string temp_password_time { get; set; }
    }

    public struct Bike
    {
        public string owner { get; set; }
        public string picture { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string year { get; set; }
        public string color { get; set; }
        public string milage { get; set; }
        public string exhaust { get; set; }
        public string accessories { get; set; }
        public string description { get; set; }
    }

    public struct Gear
    {
        public string owner { get; set; }
        public string picture { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string year { get; set; }
        public string color { get; set; }
        public string category { get; set; }
        public string description { get; set; }
    }
}
