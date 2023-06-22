 using TrabajandoJson;
 using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using convertirBit;


namespace TrabajandoJson
{
    partial class Program
    {

        //https://json2csharp.com/

        static void Main(string[] args)
        {
            GetMoneda();
        }


        private static void GetMoneda()
        {
            var url = $"https://api.coindesk.com/v1/bpi/currentprice.json";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            Root Monedas = JsonSerializer.Deserialize<Root>(responseBody);
                            Console.WriteLine("Moneda: "+Monedas.bpi.EUR.description+" precio: "+ Monedas.bpi.EUR.symbol + Monedas.bpi.EUR.rate_float);
                            Console.WriteLine("Moneda: "+Monedas.bpi.USD.description+" precio: "+ Monedas.bpi.USD.symbol + Monedas.bpi.USD.rate_float);
                            Console.WriteLine("Moneda: "+Monedas.bpi.GBP.description+" precio: "+ Monedas.bpi.GBP.symbol + Monedas.bpi.GBP.rate_float);
                            Console.WriteLine("Caracteristicas: "+Monedas.bpi.EUR.code+" "+Monedas.bpi.EUR.rate+" "+Monedas.bpi.EUR.symbol);

                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Problemas de acceso a la API");
            }
        }
       
    }
}