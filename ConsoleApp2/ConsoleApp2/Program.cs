using ConsoleApp2;
using Newtonsoft.Json.Linq;
using System;

namespace ChatApi
{
    public class Program
    {
        const string UrlAPI = "https://api.chat-api.com/instance288897/";
        const string Token = "gatxr0kt1tb3mmhx";
        const string ApiMensaje = "sendMessage";
        const string ApiArchivo = "sendFile";

        static void Main(string[] args)
        {
            EnviarMensaje();
            //EnviarArchivo();      
        }

        public static void EnviarMensaje()
        {
            Console.WriteLine("Ingrese número del destinatario:");
            string Numero = Console.ReadLine();

            Console.WriteLine("Escriba el mensaje:");
            string Mensaje = Console.ReadLine();

            JObject jParameters = new JObject
            {
                ["phone"] = Numero,
                ["body"] = Mensaje
            };

            string Url = UrlAPI + ApiMensaje + "?token=" + Token;
            JObject jChatApiResult = APIClient.Request(Url, APIClient.ContentType.JSON, null, jParameters, APIClient.RequestTypeHttp.POST);

            if ((bool)jChatApiResult["sent"])
                Console.WriteLine("Mensaje envíado correctamente");
            else
                Console.WriteLine("Ocurrió un error al enviar mensaje: " + jChatApiResult["message"]);
        }

        public static void EnviarArchivo()
        {
            Console.WriteLine("Ingrese número del destinatario:");
            string Numero = Console.ReadLine();

            Console.WriteLine("Escriba el mensaje(opcional):");
            string Mensaje = Console.ReadLine();

            Console.WriteLine("Ingrese la url del archivo:");
            string urlArchivo = Console.ReadLine();

            //Console.WriteLine("Ingrese el nombre del archivo:");
            //string nombreArchivo = Console.ReadLine();
            //https://empresas.blogthinkbig.com/wp-content/uploads/2019/11/Imagen3-245003649.jpg?w=800
            //https://empresas.blogthinkbig.com/wp-content/uploads/2019/11/Imagen3-245003649.jpg?w=800

            JObject jParameters = new JObject
            {
                ["phone"] = Numero,
                ["body"] = urlArchivo,
                ["filename"] = "4x4pixel.jpg",
                ["caption"] = Mensaje
            };

            string Url = UrlAPI + ApiArchivo + "?token=" + Token;
            JObject jChatApiResult = APIClient.Request(Url, APIClient.ContentType.JSON, null, jParameters, APIClient.RequestTypeHttp.POST);

            if ((bool)jChatApiResult["sent"])
                Console.WriteLine("Mensaje envíado correctamente");
            else
                Console.WriteLine("Ocurrió un error al enviar mensaje: " + jChatApiResult["message"]);
        }
    }
}
