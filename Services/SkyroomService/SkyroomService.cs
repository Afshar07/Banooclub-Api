using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BanooClub.Services.SkyroomService
{
    public class SkyroomService : ISkyroomService
    {
        IConfiguration _configuration;
        public SkyroomService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ReponseVM<long>> AddRoomUsers(InputModel<AddRoomUsersInputModel> input)
        {
            try
            {
                if (input != null)
                    input.action = "addRoomUsers";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(_configuration["SkyroomConfigUrl"]);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(input);

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return JsonConvert.DeserializeObject<ReponseVM<long>>(streamReader.ReadToEnd());
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<ReponseVM<long>> CreateRoome(InputModel<CreateRoomInputModel> input)
        {
            try
            {
                if (input != null)
                    input.action = "createRoom";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(_configuration["SkyroomConfigUrl"]);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(input);

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return JsonConvert.DeserializeObject<ReponseVM<long>>(streamReader.ReadToEnd());
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<ReponseVM<long>> CreateUser(InputModel<CreateUserInputModel> input)
        {
            try
            {
                if (input != null)
                    input.action = "createUser";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(_configuration["SkyroomConfigUrl"]);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(input);

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return JsonConvert.DeserializeObject<ReponseVM<long>>(streamReader.ReadToEnd());
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<ReponseVM<string>> GetRoomUrl(InputModel<GetRomeInfoInputModel> input)
        {
            try
            {
                if (input != null)
                    input.action = "getRoomUrl";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(_configuration["SkyroomConfigUrl"]);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(input);

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    return JsonConvert.DeserializeObject<ReponseVM<string>>(streamReader.ReadToEnd());
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<ReponseVM<SearchResultResponseVM>> GetUser(InputModel<GetUserInputModel> input)
        {
            try
            {
                if (input != null)
                    input.action = "getUser";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(_configuration["SkyroomConfigUrl"]);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(input);

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    return JsonConvert.DeserializeObject<ReponseVM<SearchResultResponseVM>>(streamReader.ReadToEnd());
            }
            catch
            {
                return null;
            }
        }
    }
}
