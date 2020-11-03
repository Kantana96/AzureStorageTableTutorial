using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace AzureStorageTable
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Azure Table Storage Sample");
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=samplestorageaccount2020;AccountKey=Bx/REnpBFPCfSJci0y709/JQ/1NKeY+/cYDVobmWcZbCfvJeo86ndNq4gukpZOk3dBGUh8n+Lnrv5mHzy83PeQ==;EndpointSuffix=core.windows.net";
            string tableName = "demo2";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            CloudTable table = tableClient.GetTableReference(tableName);
            CustomerEntity customer = new CustomerEntity("Kanat", "Urazaliyev") { Email = "ukanatm@gmail.com", PhoneNumber = "+77477968024" };
            MergeUser(table, customer).Wait();
        }

        public static async Task MergeUser(CloudTable table, CustomerEntity customer)
        {
            TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(customer);
            TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
            CustomerEntity insertedCustomer = result.Result as CustomerEntity;
            Console.WriteLine("Added user successfully!");
        }
    }

    public class CustomerEntity : TableEntity
    {
        public CustomerEntity()
        {
        }
        public CustomerEntity(string lastName, string firstName)
        {
            PartitionKey = lastName;
            RowKey = firstName;
        }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
