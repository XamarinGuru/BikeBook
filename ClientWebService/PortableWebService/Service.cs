using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ClientWebService
{
    public class Service
    {
        private static Service m_instance;

        protected Service() { }
        public static Service Instance
        {
            get
            {
                if(m_instance == null)
                {
                    m_instance = new Service();
                }
                return m_instance;
            }
        }

        private string m_token;

        private string m_email;
        public string Email
        {
            get { return m_email; }
        }

       
        private RestClient m_client;

        public int LoginLocal(string email, string password)
        {
            m_client = new RestClient(Consts.URL + ":" + Consts.PORT);
            RestRequest request = new RestRequest("/api/v1/login", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { email = email, password = password });
            IRestResponse response = m_client.Execute(request);

            if((int)response.StatusCode == 200)
            {
                Login loginResponse = JsonConvert.DeserializeObject<Login>(response.Content);
                m_token = loginResponse.Token;
                m_email = email;
            }
            return (int)response.StatusCode;
        }


        public int RegisterLocal(string username, string email, string picture, bool styleSport, bool styleTouring, 
            bool styleCruising, bool styleAdventure, bool styleTrack, bool styleCommuting, string password)
        {
            m_client = new RestClient(Consts.URL + ":" + Consts.PORT);
            RestRequest request = new RestRequest("/api/v1/users/register", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { name = username , email = email, picture = picture,  styleSport = styleSport, styleTouring = styleTouring,
                styleCruising = styleCruising, styleAdventure = styleAdventure, styleTrack = styleTrack,
                styleCommuting = styleCommuting, password = password });
            IRestResponse response = m_client.Execute(request);
            return (int)response.StatusCode;
        }


        public int TestToken(string email)
        {
            m_client = new RestClient(Consts.URL + ":" + Consts.PORT);
            RestRequest request = new RestRequest("/api/v1/users/checkToken", Method.GET);
            request.AddHeader("x-access-token", m_token);
            request.AddHeader("email", email);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = m_client.Execute(request);
            return (int)response.StatusCode;
        }


        public int SetProfileDescription(string email, string newDescription)
        {
            m_client = new RestClient(Consts.URL + ":" + Consts.PORT);
            RestRequest request = new RestRequest("/api/v1/users/profileDescription", Method.PUT);
            request.AddHeader("x-access-token", m_token);
            request.AddHeader("email", email);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { profileDescription = newDescription });
            IRestResponse response = m_client.Execute(request);
            return (int)response.StatusCode;
        }

        public int ChangeProfile(string email, User newProfileContent)
        {
            m_client = new RestClient(Consts.URL + ":" + Consts.PORT);
            RestRequest request = new RestRequest("/api/v1/users/updateProfile", Method.POST);
            request.AddHeader("x-access-token", m_token);
            request.AddHeader("email", email);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new
            {
                name = newProfileContent.name,
                email = email,
                picture = newProfileContent.picture,
                styleSport = newProfileContent.styleSport,
                styleTouring = newProfileContent.styleTouring,
                styleCruising = newProfileContent.styleCruising,
                styleAdventure = newProfileContent.styleAdventure,
                styleTrack = newProfileContent.styleTrack,
                styleCommuting = newProfileContent.styleCommuting,
            });
            IRestResponse response = m_client.Execute(request);
            return (int)response.StatusCode;
        }


        public string GetProfileDescription(string email)
        {
            m_client = new RestClient(Consts.URL + ":" + Consts.PORT);
            RestRequest request = new RestRequest("/api/v1/users/profileDescription", Method.GET);
            request.AddHeader("x-access-token", m_token);
            request.AddHeader("email", email);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = m_client.Execute(request);

            if ((int)response.StatusCode == 200)
            {
                ProfileDescription descResponse = JsonConvert.DeserializeObject<ProfileDescription>(response.Content);
                return descResponse.Description;
            }
            return response.StatusCode.ToString();
        }


        public Users GetAllUsers(string email)
        {
            m_client = new RestClient(Consts.URL + ":" + Consts.PORT);
            RestRequest request = new RestRequest("/api/v1/users/getAllUsers", Method.GET);
            request.AddHeader("x-access-token", m_token);
            request.AddHeader("email", email);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = m_client.Execute(request);

            Users usersResponse = new Users();

            if ((int)response.StatusCode == 200)
            {
                usersResponse = JsonConvert.DeserializeObject<Users>(response.Content);
                return usersResponse;
            }
            return usersResponse;
        }

        public User GetUser(string email, string targetUserEmail)
        {
            m_client = new RestClient(Consts.URL + ":" + Consts.PORT);
            RestRequest request = new RestRequest("/api/v1/users/getUser", Method.GET);
            request.AddHeader("x-access-token", m_token);
            request.AddHeader("email", email);
            request.AddHeader("targetuseremail", targetUserEmail);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = m_client.Execute(request);

            Users userResponse = new Users();
            User user = new User();
            if((int)response.StatusCode == 200)
            {
                userResponse = JsonConvert.DeserializeObject<Users>(response.Content);
                user = userResponse.user[0];
                return user;
            }
            return user;
        }


        public int SendMessage(string email, string reciever, string message, string image)
        {
            m_client = new RestClient(Consts.URL + ":" + Consts.PORT);
            RestRequest request = new RestRequest("/api/v1/message/send", Method.POST);
            request.AddHeader("x-access-token", m_token);
            request.AddHeader("email", email);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { email = email, reciever = reciever, message = message, image = image });
            IRestResponse response = m_client.Execute(request);
            return (int)response.StatusCode;
        }


        public Messages GetMessages(string email)
        {
            m_client = new RestClient(Consts.URL + ":" + Consts.PORT);
            RestRequest request = new RestRequest("/api/v1/message/getMessages", Method.GET);
            request.AddHeader("x-access-token", m_token);
            request.AddHeader("email", email);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = m_client.Execute(request);

            Messages messageResponse = new Messages();

            if ((int)response.StatusCode == 200)
            {
                messageResponse = JsonConvert.DeserializeObject<Messages>(response.Content);
                return messageResponse;
            }
            return messageResponse;
        }


        public int CreateAdd(string email, string title, string image, string description, string city, string provence)
        {
            m_client = new RestClient(Consts.URL + ":" + Consts.PORT);
            RestRequest request = new RestRequest("/api/v1/ad/create", Method.POST);
            request.AddHeader("x-access-token", m_token);
            request.AddHeader("email", email);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { title = title, image = image, description = description, city = city, provence = provence });
            IRestResponse response = m_client.Execute(request);
            return (int)response.StatusCode;
        }


        public Ads GetAds(string email)
        {
            m_client = new RestClient(Consts.URL + ":" + Consts.PORT);
            RestRequest request = new RestRequest("/api/v1/ad/get", Method.GET);
            request.AddHeader("x-access-token", m_token);
            request.AddHeader("email", email);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = m_client.Execute(request);

            Ads adResponse = new Ads();

            if ((int)response.StatusCode == 200)
            {
                adResponse = JsonConvert.DeserializeObject<Ads>(response.Content);
                return adResponse;
            }
            return adResponse;
        }


        public int CreatePost(string email, string image, string content)
        {
            m_client = new RestClient(Consts.URL + ":" + Consts.PORT);
            RestRequest request = new RestRequest("/api/v1/post/create", Method.POST);
            request.AddHeader("x-access-token", m_token);
            request.AddHeader("email", email);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { image = image, content = content });
            IRestResponse response = m_client.Execute(request);
            return (int)response.StatusCode;
        }


        public Posts GetPost(string email)
        {
            m_client = new RestClient(Consts.URL + ":" + Consts.PORT);
            RestRequest request = new RestRequest("/api/v1/post/get", Method.GET);
            request.AddHeader("x-access-token", m_token);
            request.AddHeader("email", email);
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = m_client.Execute(request);

            Posts postsResponse = new Posts();

            if ((int)response.StatusCode == 200)
            {
                postsResponse = JsonConvert.DeserializeObject<Posts>(response.Content);
                return postsResponse;
            }
            return postsResponse;
        }
    }
}
