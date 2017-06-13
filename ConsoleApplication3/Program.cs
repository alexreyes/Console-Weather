using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace JsonApiClient
{
    public class Program
    {
        public static void Main(String[] args)
        {
            Database database = new Database();

            WeatherApiClient weatherClient = new WeatherApiClient();
            weatherClient.GetWeatherForecast();

            WeatherApiClient.GetWeatherForecastAsync();
            Console.ReadLine();
        }
    }
}
