using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract10
{
    internal class UserWarehouseManager : User
    {
        
        ModelOfWorker warehouseManager = new ModelOfWorker();
        List<ALlProduct> allProducts = new List<ALlProduct>();
        public UserWarehouseManager(ModelOfWorker warehouseManager, List<ALlProduct> allProducts)
        {
            this.warehouseManager = warehouseManager;
            this.allProducts = allProducts;
        }
        internal enum Post
        {
            F1 = ConsoleKey.F1,
            F2 = ConsoleKey.F2,
            F3 = ConsoleKey.F3,
            F4 = ConsoleKey.F4,
            Enter = ConsoleKey.Enter,
            UpArrow = ConsoleKey.UpArrow,
            DownArrow = ConsoleKey.DownArrow,
        }
        
        public void Interface()
        {
            int pos = 2;
            int max = allProducts.Count() + 1;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(pos);
                Console.SetCursorPosition(0, pos);
                Console.WriteLine("->");

                InterfaceForUsers.PrintInterface(warehouseManager);

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == (ConsoleKey)Post.F1)
                {
                    Console.Clear();
                    Create();
                }
                else if (key.Key == (ConsoleKey)Post.F2)
                {
                    Console.Clear();
                    Search();
                }

                else if (key.Key == (ConsoleKey)Post.F3)
                {
                    Console.Clear();
                    Delete();
                }
                else if (key.Key == (ConsoleKey)Post.F4)
                {
                    Console.Clear();
                    Read(pos);
                }
                else if (key.Key == (ConsoleKey)Post.UpArrow)
                {
                    if (pos <= 2)
                    {
                        pos += max - 2;
                    }
                    else
                    {
                        pos--;
                    }
                }
                else if (key.Key == (ConsoleKey)Post.DownArrow)
                {
                    if (pos >= max)
                    {
                        pos -= max - 2;
                    }
                    else
                    {
                        pos++;
                    }
                }
                else if (key.Key == (ConsoleKey)Post.Enter)
                {
                    Console.Clear();
                    ALlProduct aLlProduct = allProducts[pos - 2];
                    Update(aLlProduct.id);
                }
            }
        }
        public void Create()
        {
            string startupPath = Directory.GetCurrentDirectory();
            int len = startupPath.Length - 17;
            string json = startupPath.Substring(0, len) + "\\Product.json";
            List<ALlProduct> con = Converter.Des<List<ALlProduct>>(json);

            Console.WriteLine("Введите название");
            string name = Console.ReadLine();
            Console.WriteLine("ВВедите цену");
            int price = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите количество");
            int count = Convert.ToInt32(Console.ReadLine());

            int id = con[con.Count - 1].id + 1;

            ALlProduct prod = new ALlProduct();
            prod.id = id;
            prod.name = name;
            prod.price = price;
            prod.count = count;

            con.Add(prod);
            allProducts.Insert(id, prod);

            Console.WriteLine("ВВЕДИ ИМЯ ФАЙЛА");
            string filename = Console.ReadLine();
            Converter.Ser<List<ALlProduct>>(con, filename);
        }
        public void Read(int id)
        {
            List<int> ids = new List<int>();

            foreach (ALlProduct i in allProducts)
            {
                ids.Add(i.id);
            }
            ALlProduct user = allProducts[ids.IndexOf((id))];
            Console.WriteLine(user.id);
            Console.WriteLine(user.name);
            Console.WriteLine(user.price);
            Console.WriteLine(user.count);
            Console.WriteLine();

            Console.WriteLine("Нажми любую кнопку для выхода");
            Console.ReadKey();
        }
        public void Delete()
        {
            string Path = Directory.GetCurrentDirectory();
            int len = Path.Length - 17;
            string json = Path.Substring(0, len) + "\\Product.json";
            List<ALlProduct> con = Converter.Des<List<ALlProduct>>(json);
            List<int> ids = new List<int>();
            
            foreach (ALlProduct user in con)
            {
                ids.Add(user.id);
            }


            Console.WriteLine("Введи id продукта который надо удалить");
            int id = Convert.ToInt32(Console.ReadLine());

            if (ids.Contains(id))
            {
                ALlProduct aLlProduct = con[ids.IndexOf(id)];
                con.Remove(aLlProduct);

                Console.WriteLine("Enter file name");
                string filename = Console.ReadLine();
                Converter.Ser<List<ALlProduct>>(con, filename);
            }
            else
            {
                Console.WriteLine("Такого пользователя нет, нажмите любую клавишу, чтобы выйти");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void Search()
        {
            Console.WriteLine("Введите ID товара");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("ВВедите имя товара");
            string name = Console.ReadLine();
            Console.WriteLine("Введите стоимость");
            int price = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите количество товара на складе");
            int count = Convert.ToInt32(Console.ReadLine());

            ALlProduct aLlProducts = new ALlProduct();
            aLlProducts.id = id;
            aLlProducts.name = name;
            aLlProducts.price = price;
            aLlProducts.count = count;

            if (allProducts.Contains(aLlProducts))
            {
                Console.Clear();
                Console.WriteLine(aLlProducts.id);
                Console.WriteLine(aLlProducts.name);
                Console.WriteLine(aLlProducts.price);
                Console.WriteLine(aLlProducts.count);
                Console.WriteLine();

                Console.WriteLine("Нажми любую кнопку для выхода");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Нету такого, нажми чтобьы выйти");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void Update(int id)
        {
            List<int> ids = new List<int>();
            
            foreach (ALlProduct i in allProducts)
            {
                ids.Add(i.id);
            }

            Console.WriteLine("Введи имя товара");
            string name = Console.ReadLine();
            Console.WriteLine("Введи стоимость товара");
            int price = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введи количество товара на складе");
            int count = Convert.ToInt32(Console.ReadLine());

            ALlProduct aLlProduct = allProducts[ids.IndexOf((id))];
            allProducts.Remove(aLlProduct);
            aLlProduct.name = name;
            aLlProduct.price = price;
            aLlProduct.count = count;
            
            allProducts.Insert(id, aLlProduct);
            Console.WriteLine("Введи имя файла");
            string filename = Console.ReadLine();
            Converter.Ser<List<ALlProduct>>(allProducts, filename);
        }
    }
}
 