using System.Globalization;

namespace KT37
{
    public class Order
    {
        public string CustomerName { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public Order(string customerName, decimal amount, DateTime date)
        {
            CustomerName = customerName;
            Amount = amount;
            Date = date;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Order> orders = new List<Order>
            {
                new Order("наталья", 1500, new DateTime(2026, 10, 5)),
                new Order("александр", 3000, new DateTime(2026, 10, 7)),
                new Order("мария", 2500, new DateTime(2026, 10, 10)),
                new Order("женя", 2000, new DateTime(2026, 10, 12)),
                new Order("максим", 4000, new DateTime(2026, 10, 15)),
                new Order("анастасия", 1500, new DateTime(2026, 10, 18)),
                new Order("александра", 3500, new DateTime(2026, 10, 20)),
                new Order("дмитрий", 2500, new DateTime(2026, 10, 22)),
                new Order("илья", 3000, new DateTime(2026, 10, 25)),
                new Order("лейла", 2000, new DateTime(2026, 10, 28)),
                new Order("алексей", 5000, new DateTime(2026, 9, 15)),
                new Order("дарья", 6000, new DateTime(2026, 11, 5))
            };

            Console.Write("введите начальную дату периода(дд.мм.гггг): ");
            DateTime startDate = DateTime.ParseExact(
                Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

            Console.Write("введите конечную дату периода (дд.мм.гггг): ");
            DateTime endDate = DateTime.ParseExact(
                Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

            if (startDate > endDate)
            {
                Console.WriteLine("ошибка: начальная дата больше конечной!");
                return;
            }

            Console.Write("выберите сортировку по сумме: ");
            int sortChoice = int.Parse(Console.ReadLine());

            Console.Write("введите количество клиентов для вывода: ");
            int count = int.Parse(Console.ReadLine());

            var filteredOrders = orders
                .Where(o => o.Date >= startDate && o.Date <= endDate)
                .GroupBy(o => o.CustomerName)
                .Select(g => new
                {
                    Customer = g.Key,
                    TotalAmount = g.Sum(o => o.Amount)
                });

            var result = sortChoice == 1
                ? filteredOrders.OrderByDescending(x => x.TotalAmount).Take(count)
                : filteredOrders.OrderBy(x => x.TotalAmount).Take(count);

            Console.WriteLine();
            Console.WriteLine($"Итоговая сводка({startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy})");

            foreach (var customer in result)
            {
                Console.WriteLine($"клиент: {customer.Customer,-12} | суммарные траты: {customer.TotalAmount} руб.");
            }

        }
    }
}
