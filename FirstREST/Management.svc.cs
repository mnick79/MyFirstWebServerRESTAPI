using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Http;

namespace FirstREST
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Management
    {
        //********* ADD list of customers*************
        private static List<Customer> custmr = new List<Customer>
        {
            new Customer("Иванов Иван","Москва", true),
            new Customer("Петров Петр","Волгоград", true),
            new Customer("Сидоров Сидор","Екатеринбург", true),
            new Customer("Петров Петр","Краснодар", true),
            new Customer("Лавров Сергей","Тюмень", true),
        };
        //********* GET  customers*************
        [WebGet(UriTemplate ="/Customer")]
        public string GetAllCustomer()
        {
            return String.Join("|", custmr.Select(x => x.GetAllValue()));
        }
        //********* GET ID customers*************
        [WebGet(UriTemplate= "/Customer/{CustomerID}")]
        public string GetCustomerById(string CustomerID)
        {
            int itemID;
            if (!int.TryParse(CustomerID, out itemID))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return custmr[itemID].GetAllValue();
            //return tmp.ToString();
        }
        //********* POST customers*************
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json,
            UriTemplate = "/Customer", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        public void AddCustomer(string str)
        {
            string[] addItem = str.Split(';');

            custmr.Add(new Customer(addItem[0], addItem[1], Convert.ToBoolean(addItem[2])));
        }
        //********* DELETE customers*************
        [WebInvoke(Method ="DELETE", BodyStyle =WebMessageBodyStyle.Wrapped, RequestFormat =WebMessageFormat.Json,
            ResponseFormat =WebMessageFormat.Json, UriTemplate ="/Customer/{CustomerID}")]
        public void DeleteItemCustomer(string CustomerID)
        {
            int itemsID;
            if(!int.TryParse(CustomerID, out itemsID))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            custmr.RemoveAt(itemsID);
        }




        // Чтобы использовать протокол HTTP GET, добавьте атрибут [WebGet]. (По умолчанию ResponseFormat имеет значение WebMessageFormat.Json.)
        // Чтобы создать операцию, возвращающую XML,
        //     добавьте [WebGet(ResponseFormat=WebMessageFormat.Xml)]
        //     и включите следующую строку в текст операции:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        public void DoWork()
        {
            // Добавьте здесь реализацию операции
            return;
        }

        // Добавьте здесь дополнительные операции и отметьте их атрибутом [OperationContract]
    }
}
