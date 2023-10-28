using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    static List<Customer> customers = new List<Customer>();
    static List<Product> products = new List<Product>();
    static Customer loggedInCustomer = null;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. Giriş Yap");
            Console.WriteLine("2. Kayıt Ol");
            Console.WriteLine("3. Şifremi Unuttum");
            Console.WriteLine("4. Ürün Ekle");
            Console.WriteLine("5. Ürünleri Listele");
            Console.WriteLine("6. Ürün Sil");
            Console.WriteLine("7. Çıkış");

            Console.Write("Seçiminizi yapın: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Login();
                    break;
                case "2":
                    RegisterCustomer();
                    break;
                case "3":
                    ResetPassword();
                    break;
                case "4":
                    if (loggedInCustomer != null)
                    {
                        AddProduct();
                    }
                    else
                    {
                        Console.WriteLine("Giriş yapmadan ürün ekleyemezsiniz.");
                    }
                    break;
                case "5":
                    if (loggedInCustomer != null)
                    {
                        ListProducts();
                    }
                    else
                    {
                        Console.WriteLine("Giriş yapmadan ürün silemezsiniz.");
                    }
                    break;
                case "6":
                    if (loggedInCustomer != null)
                    {
                        DeleteProduct();
                    }
                    else
                    {
                        Console.WriteLine("Giriş yapmadan ürün silemezsiniz.");
                    }
                    break;
                case "7":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim. Tekrar deneyin.");
                    break;
            }
        }
    }

    static void Login()
    {
        Console.Write("Kullanıcı Adı: ");
        string username = Console.ReadLine();

        Console.Write("Şifre: ");
        string password = Console.ReadLine();
        
        var customer = customers.FirstOrDefault(c => c.Username == username && c.Password == password);
        if (customer != null)
        {
            loggedInCustomer = customer;
            Console.WriteLine($"Hoş geldiniz, {customer.Name} {customer.Surname}!");
        }
        else
        {
            Console.WriteLine("Kullanıcı adı veya şifre hatalı.");
        }
    }

    static void RegisterCustomer()
    {
        Console.Write("Müşteri Adı: ");
        string name = Console.ReadLine();

        Console.Write("Müşteri Soyadı: ");
        string surname = Console.ReadLine();

        Console.Write("E-Posta Adresi: ");
        string email = Console.ReadLine();

        Console.Write("Kullanıcı Adı: ");
        string username = Console.ReadLine();

        Console.Write("Şifre: ");
        string password = Console.ReadLine();

        Customer customer = new Customer
        {
            Id = customers.Count + 1,
            Name = name,
            Surname = surname,
            Email = email,
            Username = username,
            Password = password
        };
        customers.Add(customer);
        Console.WriteLine("Müşteri başarıyla kaydedildi.");
    }

    static void ResetPassword()
    {
        Console.Write("Kullanıcı Adı: ");
        string username = Console.ReadLine();

        var customer = customers.FirstOrDefault(c => c.Username == username);
        if (customer != null)
        {
            Console.Write("Yeni Şifre: ");
            string newPassword = Console.ReadLine();
            customer.Password = newPassword;
            Console.WriteLine("Şifreniz başarıyla sıfırlandı.");
        }
        else
        {
            Console.WriteLine("Belirtilen kullanıcı adına sahip bir müşteri bulunamadı.");
        }
    }

    static void AddProduct()
    {
        Console.Write("Ürün Adı: ");
        string name = Console.ReadLine();

        Console.Write("Ürün Fiyatı: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Product product = new Product
            {
                Id = products.Count + 1,
                Name = name,
                Price = price
            };
            products.Add(product);
            Console.WriteLine("Ürün başarıyla eklendi.");
        }
        else
        {
            Console.WriteLine("Geçersiz fiyat girişi.");
        }
    }

    static void ListProducts()
    {
        Console.WriteLine("Ürünler:");
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Ad: {product.Name}, Fiyat: {product.Price:C}");
        }
    }

    static void DeleteProduct()
    {
        Console.Write("Silmek istediğiniz ürünün ID'sini girin: ");
        if (int.TryParse(Console.ReadLine(), out int productId))
        {
            var product = products.Find(p => p.Id == productId);
            if (product != null)
            {
                products.Remove(product);
                Console.WriteLine("Ürün başarıyla silindi.");
            }
            else
            {
                Console.WriteLine("Belirtilen ID'ye sahip ürün bulunamadı.");
            }
        }
        else
        {
            Console.WriteLine("Geçersiz ID girişi.");
        }
    }
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }


}
