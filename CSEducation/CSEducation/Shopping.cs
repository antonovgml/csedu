using System;
using System.Collections;
using System.Collections.Generic;
namespace Shopping
{

    public class Shop
    {

        Random rnd = new Random();
        private TestDataGenerator tdg;

        public List<Product> products { get; }
        public List<Customer> customers { get; }
        public List<Order> orders = new List<Order>();

        public Shop()
        {
            tdg = new TestDataGenerator();

            products = tdg.generateProducts();
            customers = tdg.generateCustomers(10);
        }

        public void Display(IEnumerable list)
        {
            if (list == null)
                return;

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        public void DisplayProducts() { Display(products); }
        public void DisplayCustomers() { Display(customers); }
        public void DisplayOrders() { Display(orders); }

    }

    /* Shopping Experience extensions */

    public static class ShoppingExperience
    {
        /*Emulate process of buing products by a customer*/
        public static void BuyProducts(this Shop shop, Customer customer, Product[] products)
        {
            Random rnd = new Random();
            if ((customer == null) || (products == null))
                return;
            if (products.Length == 0)
                return;

            Order order = new Order(customer);

            int buyCount;
            foreach (var prod in products)
            {
                buyCount = rnd.Next(1, ((int)prod.amountAvailable) + 1);
                Console.WriteLine($"{customer.fullName} ({customer.id}) is buying {buyCount} of {prod.name}");
                order.AddProduct(prod, (ulong)buyCount);
            }

            if (!order.empty)
            {
                shop.orders.Add(order);
            }
            
        }


    }

    /* ORDER */

    public class Order
    {

        private static ulong seqNum = 0;

        private ulong id;
        private Customer customer;

        decimal total
        {
            get {
                decimal result = 0;

                foreach(var item in this.items)
                {
                    result += item.Item2 * item.Item3;
                }

                return result;

            }
        }

        public bool empty { get { return items.Count == 0; } }

        // List of customer - product - price - count
        List<Tuple<string, decimal, ulong>> items = new List<Tuple<string, decimal, ulong>>();

        DateTime buyDate = DateTime.Now;

        public Order(Customer customer)
        {
            this.customer = customer;

            this.id = ++seqNum;
        }

        public void AddProduct(Product product, ulong count)
        {
            if (product.amountAvailable >= count)
            {
                items.Add(Tuple.Create(product.name, product.price, count));
                product.amountAvailable -= count;
            }
            else
            {
                Console.WriteLine("Unable to buy! Not enough pieces available in stock. Required: {0}, available: {1}", count, product.amountAvailable);
            }
        }

        public override string ToString()
        {
            String result = String.Format("Order #{0,5} for {1, -30} on {2}", id, customer.fullName, buyDate);
            foreach(var item in items)
            {
                result += String.Format("\n  {0,20} | {1,5} | {2:C2}, ", item.Item1, item.Item3, item.Item2);
            }

            result += "\n  ------------------------------------";
            result += String.Format("\n                          Total: {0:C2}", this.total);

            return result;
        }
    }

    /*PERSON base class*/

    public class Person
    {
        protected string firstName;
        protected string lastName;
        public string fullName { get { return firstName + " " + lastName; } }

        public Person(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }
    }

    /* CUSTOMER */

    public class Customer: Person
    {
        protected static ulong seqNum = 0;

        public ulong id { get; }
        string email;

        public Customer(string firstName, string lastName, string email): base(firstName, lastName)
        {
            this.email = email;
            this.id = ++seqNum;
        }

        public override string ToString()
        {
            return String.Format("{0,5} | {1, 20} | {2,50}", id, fullName, email);
        }

    }

    /* PRODUCT */

    public class Product
    {
        private static ulong seqNum = 0;

        private ulong id;
        public string name { get; }
        public decimal price { get; }
        public ulong amountAvailable { get; set; }

        public Product(string name, decimal price, ulong amountAvailable)
        {
            this.name = name;
            this.price = price;
            this.amountAvailable = amountAvailable;

            this.id = ++seqNum;
        }

        public override string ToString()
        {
            return String.Format("{0,5} | {1, 20} | {2,8:C2} | {3}", id, name, price, amountAvailable);
        }
    }


    class TestDataGenerator
    {
        Random rnd = new Random();

        string[] productNames = { "Mobile Phone", "Tablet", "TV", "Shoes", "Dress" };
        string[] firstNames = { "John", "Peter", "Thomas", "Alice", "Joan", "Veronica" };
        string[] lastNames = { "Red", "Orange", "Yellow", "Green", "Blue", "Black", "White" };


        /*generate Products test data*/
        public List<Product> generateProducts()
        {
            List<Product> products = new List<Product>();

            foreach (var name in productNames)
            {
                products.Add(new Product(name, rnd.Next(1, 101), (ulong)rnd.Next(1, 10)));
            }

            return products;
        }

        /*generate Customers test data*/
        private Customer generateCustomer()
        {
            return new Customer(firstNames[rnd.Next(firstNames.Length)], lastNames[rnd.Next(lastNames.Length)], Guid.NewGuid().ToString()+"@gmail.com");
        }

        public List<Customer> generateCustomers(uint count)
        {
            List<Customer> customers = new List<Customer>();

            for (int i = 0; i < count; i++)
            {
                customers.Add(this.generateCustomer());
            }

            return customers;
        }

    }

}
