using Calculator.Sdk.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Calculator.Client
{
    class Program
    {
        static ServiceConfig config = new ServiceConfig();
        
        static bool run = true;

        static void PrintOperation(OperationDto operation)
        {
            Console.WriteLine($"{nameof(operation.Id)}: {operation.Id}" +
                $"\t{nameof(operation.CreationDate)}: {operation.CreationDate}" +
                $"\t{nameof(operation.OperationType)}: {operation.OperationType}" +
                $"\t{nameof(operation.Values)}: {string.Join(',', operation.Values)}" +
                $"\t{nameof(operation.Result)}: {operation.Result}");
        }


        

        static async Task Main(string[] args)
        {

            Console.CancelKeyPress += new ConsoleCancelEventHandler(CancelHandler);
            using IHost host = CreateHostBuilder(args).Build();
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
                    CalculatorService calculatorService = new CalculatorService(config);
                    OperationDto opDto = null;
                    switch (requestedOp)
                    {
                        case Operations.Addition:
                            opDto = await calculatorService.Addition();
                            break;
                        case Operations.Subtraction:
                            opDto = await calculatorService.Subtraction();
                            break;
                        case Operations.Multiplication:
                            opDto = await calculatorService.Multiplication();
                            break;
                        case Operations.Division:
                            opDto = await calculatorService.Division();
                            break;
                        case Operations.SquareRoot:
                            opDto = await calculatorService.SquareRoot();
                            break;
                        case Operations.RecoverOp:
                            opDto = await calculatorService.RecoverOp();
                            break;
                        default:
                            throw new InvalidOperationException("Operation not defined!");
                    }
                    if(opDto != null)
                        PrintOperation(opDto);
                }
            }
            await host.RunAsync();
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

        
    }
}
