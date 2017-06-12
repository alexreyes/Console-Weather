using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace JsonApiClient
{
    public static class Program
    {
        public static void Main(String[] args) {
            WeatherApiClient.GetWeatherForecast();
            Console.ReadLine();
        }
    }
}
