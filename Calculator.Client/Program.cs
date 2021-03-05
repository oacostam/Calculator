using Calculator.Sdk.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Calculator.Client
{
    class Program
    {
        static ServiceConfig config = new ServiceConfig();
        static HttpClient client = new HttpClient();
        static bool run = true;

        static void ShowProduct(OperationDto operation)
        {
            Console.WriteLine($"{nameof(operation.Id)}: {operation.Id}" +
                $"\t{nameof(operation.CreationDate)}: {operation.CreationDate}" +
                $"\t{nameof(operation.OperationType)}: {operation.OperationType}" +
                $"\t{nameof(operation.Values)}: {string.Join(',', operation.Values)}" +
                $"\t{nameof(operation.Result)}: {operation.Result}");
        }


        static async Task<OperationDto> DoOperation<T>(T dto, int? opId)
        {
            try
            {
                const string ID_HEADER = "X­-Evi-­Tracking-­Id";
                if (opId.HasValue)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(ID_HEADER, opId.Value.ToString());
                }
                else
                {
                    client.DefaultRequestHeaders.Remove(ID_HEADER);
                }
                HttpResponseMessage response = await client.PostAsJsonAsync("Calculator/Sum", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<OperationDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not process the request: {ex.Message}");
                return null;
            }
        }

        static async Task Main(string[] args)
        {

            Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelHandler);
            using IHost host = CreateHostBuilder(args).Build();
            ConfigureHttpClient();
            while (run)
            {
                Console.WriteLine("Let's do some calculations! Select an operation to do. To exit, press ctrl+c");
                foreach (Operations op in (Operations[])Enum.GetValues(typeof(Operations)))
                {
                    Console.WriteLine($"{(int)op} {op.GetEnumDescription()}");
                }
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (!Enum.IsDefined(typeof(Operations), result))
                    {
                        Console.WriteLine("Operation not found");
                        continue;
                    }
                    Operations requestedOp = (Operations)result;
                    switch (requestedOp)
                    {
                        case Operations.Addition:
                            await Addition();
                            break;
                        case Operations.Subtraction:
                            break;
                        case Operations.Multiplication:
                            break;
                        case Operations.Division:
                            break;
                        case Operations.SquareRoot:
                            break;
                        case Operations.RecoverOp:
                            break;
                        default:
                            break;
                    }
                }
            }
            await host.RunAsync();
        }

        private static void ConfigureHttpClient()
        {
            client.BaseAddress = new Uri(config.ServiceUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected static void CancelHandler(object sender, ConsoleCancelEventArgs args)
        {
            run = false;
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, configuration) =>
            {
                configuration.Sources.Clear();
                    //IHostEnvironment env = hostingContext.HostingEnvironment;
                    configuration
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

                    IConfigurationRoot configurationRoot = configuration.Build();
                configurationRoot.GetSection(nameof(ServiceConfig)).Bind(config);
            });

        private static async Task Addition()
        {
            Console.WriteLine("Please, specify Addends separated by spaces.");
            AditionDto dto = new AditionDto();
            dto.Addends = Console.ReadLine().Split(' ').Select(v => double.Parse(v)).ToList();
            Console.WriteLine("If you want to store the calculation, please, specify a new id.");
            int? id = null;
            if (int.TryParse(Console.ReadLine(), out int tmp))
            {
                id = tmp;
            }
            await DoOperation(dto, id);
        }
    }
}
