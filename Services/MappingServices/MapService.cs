using BanooClub.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace BanooClub.Services.MappingServices
{
    public class MapService
    {
        private readonly HttpClient client;
        public MapService(HttpClient client)
        {
            this.client = client;
            //this.client.BaseAddress = new Uri(BaseAdress);
            this.client.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        //public string ReverseGeoCoding(double latitude, double longitude)
        //{
        //    string Address;
        //    string ApiKey = "AIzaSyBI7hG4_ydg6U7ELts40xbsWTh_kWEdeik";
        //    var httpResponse = client.GetAsync($"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&key={ApiKey}").Result;
        //    httpResponse.EnsureSuccessStatusCode();
        //    if (!httpResponse.IsSuccessStatusCode)
        //    {
        //        return null;
        //    }


        //    HttpContent content = httpResponse.Content;
        //    string stringContent = content.ReadAsStringAsync().Result;

        //    //var result = JsonSerializer.Deserialize<DTO>(stringContent);
        //    //var DTO = new List<>();

        //    Address = stringContent;

        //    return Address;
        //}

        public dataDTO GetPositionStack(double latitude, double longitude)
        {
            var ApiKey = "9fa3ae05a4d69ef0662da1421c13ac75";
            var httpResponse = client.GetAsync($"http://api.positionstack.com/v1/reverse?access_key={ApiKey}&query={latitude},{longitude}").Result;
            httpResponse.EnsureSuccessStatusCode();
            if (!httpResponse.IsSuccessStatusCode)
            {
                return null;
            }
            HttpContent content = httpResponse.Content;
            string stringContent = content.ReadAsStringAsync().Result;
            dataDTO data = new dataDTO()
            {
                data = new List<MapOutputDTO>()

            };
            data = JsonSerializer.Deserialize<dataDTO>(stringContent);
            return data;
        }

        public string BigDataReverseGeoCoding(double latitude, double longitude)
        {
            var httpResponse = client.GetAsync($"https://api.bigdatacloud.net/data/reverse-geocode-client?latitude={latitude}&longitude={longitude}&localityLanguage=fa").Result;
            httpResponse.EnsureSuccessStatusCode();
            if (!httpResponse.IsSuccessStatusCode)
            {
                return null;
            }
            HttpContent content = httpResponse.Content;
            string stringContent = content.ReadAsStringAsync().Result;

            return stringContent;
        }

        public string FourSquareReverseGeoCoding(double latitude, double longitude)
        {
            var httpResponse = client.GetAsync($"https://api.foursquare.com/v2/venues/search?ll={latitude},{longitude}&oauth_token=5TJR4WQZSOW0ZWTE4ENMXKO3Y415252GITEMRPQIVPMEGCYK&v=20120723&limit=1").Result;
            httpResponse.EnsureSuccessStatusCode();
            if (!httpResponse.IsSuccessStatusCode)
            {
                return null;
            }
            HttpContent content = httpResponse.Content;
            string stringContent = content.ReadAsStringAsync().Result;

            return stringContent;
        }
    }
}
