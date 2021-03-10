using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestClient;

namespace Bibliotheca.App.Base
{
    public class BaseModel
    {
        public RestClient.RestClient BibliothecaClient;

        public BaseModel()
        {
            BibliothecaClient = RestClient.RestClient.CreateClient(Settings.BibliothecaApiEndpoint);
        }
    }
}
