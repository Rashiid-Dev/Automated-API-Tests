using API.Models;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace API;
public class APICalls
{

    static void Main()
    {

    }
    
    public async Task<string> APIRequestAsync(string requestUrl)
    {

        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                string apiUrl = requestUrl;

                Task<HttpResponseMessage> responseTask = httpClient.GetAsync(apiUrl);

                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return content;
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error has occured during request. Error Code: {(int)response.StatusCode}. Error response: {errorContent}");
                }

                            }
            catch (HttpRequestException ex)
            {
                return $"Request Error: {ex.Message}";
            }
        }
    }


    public async Task<string> APIPostAsync(string requestUrl, string jsonContent)
    {
        HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                string apiUrl = requestUrl;

                Task<HttpResponseMessage> responseTask = httpClient.PostAsync(apiUrl, content);

                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    return responseContent;
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception( $"Error has occured during request. Error Code: {(int)response.StatusCode}. Error response: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                return $"Request Error: {ex.Message}";
            }
        }

    }

    public async Task<string> APIPatchAsync(string requestUrl, string clientID, string jsonContent)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                string apiUrl = requestUrl;

                var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                Task<HttpResponseMessage> responseTask = httpClient.PatchAsync(apiUrl, httpContent);

                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return content;
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error has occured during request. Error Code: {(int)response.StatusCode}. Error response: {errorContent}");
                }

            }
            catch (HttpRequestException ex)
            {
                return $"Request Error: {ex.Message}";
            }
        }
    }

    public string ReadAndReplaceJSON(string clientName = "", ClientTypeEnum clientType = ClientTypeEnum.active, string dateOfBirth = "", string jurisdictions = "", 
        string jsonFile = "", string callType = "", string clientID = "", double price = 00, string description = "", int quantity = 0, string productID = "")
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string targetDirectory = @$"{Directory.GetParent(currentDirectory)?.Parent?.Parent?.Parent?.FullName}\API\Models\JSON\";

            string filePath = @$"{targetDirectory}{jsonFile}";

        if (File.Exists(filePath))
        {
            try
            {
                
                string jsonContent = File.ReadAllText(filePath);

                switch (callType)
                {
                    case "CreateClient":
                        jsonContent = jsonContent.Replace("{Name}", clientName)
                       .Replace("{clientType}", clientType.ToString().ToLower())
                       .Replace("{dateOfBirth}", dateOfBirth)
                       .Replace("{jurisdictions}", jurisdictions);
                        break;
                    case "PromoteClient":
                        jsonContent = jsonContent.Replace("{clientID}", clientID);
                        break;
                    case "CreateProduct":
                        jsonContent = jsonContent.Replace("{Name}", clientName)
                       .Replace("{Description}", description)
                       .Replace("{Price}", price.ToString())
                       .Replace("{Jurisdictions}", jurisdictions);
                        break;
                    case "CreateSale":
                        jsonContent = jsonContent.Replace("{clientID}", clientID)
                       .Replace("{quantity}", quantity.ToString())
                       .Replace("{productID}", productID)
                       .Replace("{description}", description);
                        break;
                    default:
                        return "No callType was provided";
                }

                return jsonContent;
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }
        else
        {
            return "File not found.";
        }
    }

    public ClientModel GetClient(string clientID)
    {
        var requestURL = $"https://sikoia-qa-interview.azurewebsites.net/v1/entities/clients/{clientID}";
        var content = APIRequestAsync(requestURL).Result;
        ClientModel client = JsonConvert.DeserializeObject<ClientModel>(content);
        return client;
    }

    public ClientModel CreateClient(string clientName, ClientTypeEnum clientType, string dateOfBirth, string jurisdictions)
    {
        var jsonContent = ReadAndReplaceJSON(clientName, clientType, dateOfBirth, jurisdictions, "CreateClientJson.json", "CreateClient");
        var requestURL = "https://sikoia-qa-interview.azurewebsites.net/v1/entities/clients";
        var content = APIPostAsync(requestURL, jsonContent).Result;
        ClientModel client = JsonConvert.DeserializeObject<ClientModel>(content);
        return client;
    }
    public ClientModel CreateClientUsingModelToJSON(string clientName, ClientTypeEnum clientType, string dateOfBirth, string jurisdictions)
    {
        var clientCreate = new CreateClientModelRequest()
        {
            name = clientName,
            DateOfBirth = DateTime.Parse(dateOfBirth),
            ClientType = clientType      
        };
        clientCreate.jurisdictions.Add(jurisdictions);

        string jsonContent = JsonConvert.SerializeObject(clientCreate);
        var requestURL = "https://sikoia-qa-interview.azurewebsites.net/v1/entities/clients";
        var content = APIPostAsync(requestURL, jsonContent).Result;
        ClientModel client = JsonConvert.DeserializeObject<ClientModel>(content);


        return client;
    }

    public ClientModel PromoteClient(string clientID)
    {
        var jsonContent = ReadAndReplaceJSON(jsonFile: "PromoteClient.json", callType: "PromoteClient", clientID: clientID);
        var requestURL = "https://sikoia-qa-interview.azurewebsites.net/v1/entities/clients/promote-prospect";
        var content = APIPatchAsync(requestURL, clientID, jsonContent).Result;
        ClientModel client = JsonConvert.DeserializeObject<ClientModel>(content);

        return client;
    }

    public ProductModel CreateProduct(string name, string jurisdictions, double price, string description)
    {
        var jsonContent = ReadAndReplaceJSON(clientName: name, jurisdictions: jurisdictions, jsonFile: "CreateProduct.json", callType: "CreateProduct", price: price, description: description);
        var requestURL = "https://sikoia-qa-interview.azurewebsites.net/v1/entities/products";
        var content = APIPostAsync(requestURL, jsonContent).Result;
        ProductModel product = JsonConvert.DeserializeObject<ProductModel>(content);

        return product;
    }

    public ProductModel GetProduct(string productId)
    {
        var requestURL = $"https://sikoia-qa-interview.azurewebsites.net/v1/entities/products/{productId}";
        var content = APIRequestAsync(requestURL).Result;
        ProductModel product = JsonConvert.DeserializeObject<ProductModel>(content);
        return product;
    }

    public SalesModel CreateSale(string description, int quantity, string clientID, string productID)
    {
        var jsonContent = ReadAndReplaceJSON(clientID: clientID, productID: productID, jsonFile: "CreateSale.json", callType: "CreateSale", quantity: quantity, description: description);
        var requestURL = "https://sikoia-qa-interview.azurewebsites.net/v1/entities/sales";
        var content = APIPostAsync(requestURL, jsonContent).Result;
        SalesModel sale = JsonConvert.DeserializeObject<SalesModel>(content);
        return sale;
    }

    public SalesModel GetSales(string saleID)
    {
        var requestURL = $"https://sikoia-qa-interview.azurewebsites.net/v1/entities/sales/{saleID}";
        var content = APIRequestAsync(requestURL).Result;
        SalesModel sale = JsonConvert.DeserializeObject<SalesModel>(content);
        return sale;
    }

}
