using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using SpookVooper.Api.Economy;
using Newtonsoft.Json;

namespace SVTransaction
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await TransactionHubAsync();
        }

        public static async Task TransactionHubAsync()
        {
            TransactionHub THub = new TransactionHub();
            THub.OnTransaction += HandleTransactionAsync;
            while (true)
            {
                await Task.Delay(1000);
            }
        }

        public static void HandleTransactionAsync(Transaction transaction)
        {
            Dictionary<string, dynamic> transactionDict = new Dictionary<string, dynamic>();
            transactionDict.Add("sender", transaction.FromAccount);
            transactionDict.Add("receiver", transaction.ToAccount);
            transactionDict.Add("amount", transaction.Amount);
            transactionDict.Add("tax", transaction.Tax);
            transactionDict.Add("detail", transaction.Detail);
            transactionDict.Add("force", transaction.Force);


            string json = JsonConvert.SerializeObject(transactionDict, Formatting.Indented);
            using (StreamWriter outputFile = File.AppendText("transactions.txt"))
            {
                outputFile.WriteLine(Environment.NewLine);
                outputFile.WriteLine(json);
            }

            Console.WriteLine(json);
        }
    }
}
