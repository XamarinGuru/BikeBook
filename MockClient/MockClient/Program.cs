using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using ClientWebService;

namespace MockClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "bob";
            string email = "bob@bob";
            string user = "plumps@plumps";
            string pass = "pass";
            //ClientWebService.Service.Instance.RegisterLocal(name, email, pass);
            ClientWebService.Service.Instance.LoginLocal(email, pass);
            //ClientWebService.Service.Instance.RegisterLocal(name, email, true, false, true, false, true, false, pass);
            //Console.ReadLine();
            User changes = new User();
            changes.styleSport = "false";
            changes.styleTrack = "false";

            ClientWebService.Service.Instance.ChangeProfile(email, changes);
            //ClientWebService.Service.Instance.SetProfileDescription(email, "new desk");
            //ClientWebService.Service.Instance.TestToken(email);
            //ClientWebService.Service.Instance.SendMessage(email, "alex@alex", "hey im responding from test client", "fdsadsfadsfaafdsa");
            //ClientWebService.Service.Instance.GetMessages(email);
            //ClientWebService.Service.Instance.GetUser(email, user);
            //ClientWebService.Service.Instance.CreateAdd(email, "fz09", "image64", "new and fast", "kelowna", "BC");
            //ClientWebService.Service.Instance.CreateAdd(email, "sv650", "9807fdsasfda987", "new and fast", "kelowna", "BC");
            //ClientWebService.Service.Instance.CreateAdd(email, "bikename", "image634", "desc", "kelowna", "Bc");
            //ClientWebService.Service.Instance.CreatePost(email, "test post 4", "test content 4");
            //ClientWebService.Service.Instance.GetAds(email);

            //ClientWebService.Service.Instance.GetPost(email);
            //ClientWebService.Service.Instance.GetAllUsers(email);
            Console.ReadLine();
        }
    }
}
