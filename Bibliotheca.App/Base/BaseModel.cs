using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotheca.App.Base
{
    public class BaseModel
    {
        public RestClient BibliothecaClient;

        public BaseModel()
        {
            BibliothecaClient = RestClient.CreateClient(Settings.BibliothecaApiEndpoint);
        }
    }
}
