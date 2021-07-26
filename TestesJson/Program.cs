using System;
using System.IO;
using System.Net;
using System.Text.Json;

namespace TestesJson
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Insira um CEP sem ífen:");
                string cep = Console.ReadLine();
                Requisicao(cep);
            }
        }

        public class Modelo
        {
            public string cep { get; set; }
            public string logradouro { get; set; }
            public string bairro { get; set; }
            public string localidade { get; set; }
            public string uf { get; set; }
        }

        public static void Formatar(string Json)
        {
            Modelo modelo = JsonSerializer.Deserialize<Modelo>(Json);

            Console.WriteLine($"CEP: {modelo.cep}");
            Console.WriteLine($"Rua: {modelo.logradouro}");
            Console.WriteLine($"Bairro: {modelo.bairro}");
            Console.WriteLine($"Cidade: {modelo.localidade}");
            Console.WriteLine($"Estado: {modelo.uf}");
            Console.WriteLine("__________________________________________________\n");
        }

        public static void Requisicao(string cep)
        {
            var requisicao = WebRequest.CreateHttp($"https://viacep.com.br/ws/{cep}/json/");
            requisicao.Method = "GET";
            try
            {
                var resposta = requisicao.GetResponse();
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                string objResposta = reader.ReadToEnd().ToString();
                Formatar(objResposta);
            }
            catch
            {
                Exception e;
            }
        }
    }
}
