using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;

namespace JsonApiClient
{
    class WeatherApiClient
    {
        public void GetWeatherForecast()
        {
            Console.Write("Enter a zip code: ");
            string zipCode = Console.ReadLine();

            if (zipCode.Equals("")) {
                zipCode = "17601";
            }
            Console.WriteLine("");

            string jsonCurrentWeather = AccessWebPage.HttpGet("http://api.openweathermap.org/data/2.5/weather?q=" + zipCode + "&APPID=47992ad1b3261b707350bf13aac83023");
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(WeatherData.CurrentRoot));

            WeatherData.CurrentRoot root = new WeatherData.CurrentRoot();

            using (Stream s = GenerateStreamFromString(jsonCurrentWeather))
            {
                root = (WeatherData.CurrentRoot)ser.ReadObject(s);
            }
            
            Console.WriteLine("Do you want your temperature in Fahrenheit(F), Celcius(C), or Kelvin(K)");
            string choice = Console.ReadLine().ToLower();
            Console.WriteLine("");

            if (choice.Equals("")) {
                choice = "f";
            }
            ConverTimeTo convertTime = new ConverTimeTo();

            weatherFormatChoice(choice, root);

            Console.WriteLine("Sunrise: {0}. Sunset: {1}", convertTime.FromUnixTime(root.sys.sunrise), convertTime.FromUnixTime(root.sys.sunset));
            string finalReadLine = Console.ReadLine();

            if (finalReadLine == "help") {
                Console.WriteLine("You can type: settings, to go back to the settings.");
                Console.ReadLine();
            }
        }

        public void weatherFormatChoice(string choice, WeatherData.CurrentRoot theRoot)
        {
            if (choice.Equals("f"))
            {
                double kelvin = theRoot.main.temp;

                ConvertTempTo convertTempTo = new ConvertTempTo();
                double fahrenheit = convertTempTo.Fahrenheit(kelvin);

                Console.WriteLine("Weather outside is: {0}", fahrenheit);
                Console.WriteLine("Temperature min: {0}. Temperature max: {1}", convertTempTo.Fahrenheit(theRoot.main.temp_min), convertTempTo.Fahrenheit(theRoot.main.temp_max));
            }
            if (choice.Equals("c"))
            {
                double kelvin = theRoot.main.temp;

                ConvertTempTo convertTempTo = new ConvertTempTo();
                double celcius = convertTempTo.Celcius(kelvin);

                Console.WriteLine("Weather outside is: {0}", celcius);
                Console.WriteLine("Temperature min: {0}. Temperature max: {1}", convertTempTo.Celcius(theRoot.main.temp_min), convertTempTo.Celcius(theRoot.main.temp_max));
            }
            if(choice.Equals("k"))
            {
                double kelvin = theRoot.main.temp;

                Console.WriteLine("(Default: K) Weather outside is: {0}", kelvin);
                Console.WriteLine("Temperature min: {0}. Temperature max: {1}", theRoot.main.temp_min, theRoot.main.temp_max);
            }
        }
        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0; 
            return stream; 
        }
    }
}
