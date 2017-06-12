using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;

namespace JsonApiClient
{
    public static class WeatherApiClient
    {
        public static void GetWeatherForecast()
        {
            Console.Write("Name a city");
            string city = Console.ReadLine();

            string jsonCurrentWeather = AccessWebPage.HttpGet("http://api.openweathermap.org/data/2.5/weather?q=" + city + "&APPID=47992ad1b3261b707350bf13aac83023");
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(WeatherData.CurrentRoot));

            var syncClient = new WebClient();
            var content = syncClient.DownloadString(url);

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(WeatherData));

            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(content)))
            {
                // deserialize the JSON object using the WeatherData type.
                var weatherData = (WeatherData)serializer.ReadObject(ms);
                Console.WriteLine(weatherData);
            }
        }
    }
}
