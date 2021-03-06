using Calculator.Sdk.Model;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Calculator.Client
{
    /// <summary>
    /// In a real wordl scenario, this class would have an interface which will be injected among all it's dependencies, but have been omited for simplicity.
    /// </summary>
    public class CalculatorService
    {
        const string AditionRoute = "api/Calculator/Sum";
        const string SubtractionRoute = "/api/Calculator/Sub";
        const string MultiplicationRoute = "/api/Calculator/Mult";
        const string DivisionRoute = "/api/Calculator/Div";
        const string SquareRootRoute = "/api/Calculator/Sqrt";
        const string RecoverOpRoute = "​/api​/Journal";
        private HttpClient client = new HttpClient();

        public CalculatorService(ServiceConfig config)
        {
            client.BaseAddress = new Uri(config.ServiceUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<OperationDto> DoOperation<T>(T dto, int? opId, string route)
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
                HttpResponseMessage response = await client.PostAsJsonAsync(route, dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<OperationDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not process the request: {ex.Message}");
                return null;
            }
        }

        public async Task<OperationDto> GetOperation(int opId)
        {
            try
            {
                string routeAndParams = $"{RecoverOpRoute}?id={opId}";
                HttpResponseMessage response = await client.GetAsync(routeAndParams);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<OperationDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not process the request: {ex.Message}");
                return null;
            }
        }

        public async Task<OperationDto> Addition()
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
            return await DoOperation(dto, id, AditionRoute);
        }

        internal async Task<OperationDto> Multiplication()
        {
            Console.WriteLine("Please, specify factors separated by spaces.");
            MultiplyDto dto = new MultiplyDto();
            dto.Factors = Console.ReadLine().Split(' ').Select(v => double.Parse(v)).ToList();
            Console.WriteLine("If you want to store the calculation, please, specify a new id.");
            int? id = null;
            if (int.TryParse(Console.ReadLine(), out int tmp))
            {
                id = tmp;
            }
            return await DoOperation(dto, id, MultiplicationRoute);
        }

        internal async Task<OperationDto> Division()
        {
            Console.WriteLine("Please, specify divididend.");
            DivisionDto dto = new DivisionDto();
            dto.Dividend = double.Parse(Console.ReadLine());
            Console.WriteLine("Please, specify divisor.");
            dto.Divisor = double.Parse(Console.ReadLine());
            Console.WriteLine("If you want to store the calculation, please, specify a new id.");
            int? id = null;
            if (int.TryParse(Console.ReadLine(), out int tmp))
            {
                id = tmp;
            }
            return await DoOperation(dto, id, DivisionRoute);
        }

        internal async Task<OperationDto> SquareRoot()
        {
            Console.WriteLine("Please, specify operand.");
            SqrtDto dto = new SqrtDto();
            dto.Operand = double.Parse(Console.ReadLine());
            Console.WriteLine("If you want to store the calculation, please, specify a new id.");
            int? id = null;
            if (int.TryParse(Console.ReadLine(), out int tmp))
            {
                id = tmp;
            }
            return await DoOperation(dto, id, SquareRootRoute);
        }

        internal async Task<OperationDto> Subtraction()
        {
            Console.WriteLine("Please, specify minuend.");
            SubtractionDto dto = new SubtractionDto();
            dto.Minuend = double.Parse(Console.ReadLine());
            Console.WriteLine("Please, specify subtrahend.");
            dto.Subtrahend = double.Parse(Console.ReadLine());
            Console.WriteLine("If you want to store the calculation, please, specify a new id.");
            int? id = null;
            if (int.TryParse(Console.ReadLine(), out int tmp))
            {
                id = tmp;
            }
            return await DoOperation(dto, id, SubtractionRoute);
        }

        internal async Task<OperationDto> RecoverOp()
        {
            Console.WriteLine("Please, specify operation id.");
            int id = int.Parse(Console.ReadLine());
            return await GetOperation(id);
        }
    }
}
