﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract10
{
    public class UserSeller
    {
        ModelOfWorker seller = new ModelOfWorker();
        List<ALlProduct> allProd = new List<ALlProduct>();
        List<SellerALlProduct> selledProducts = new List<SellerALlProduct>();
        private ModelOfWorker workers;
        private List<ModelOfWorker> con;

        public UserSeller(ModelOfWorker seller, List<ALlProduct> allProducts)
        {
            this.seller = seller;
            this.allProd = allProducts;
        }

        public UserSeller(ModelOfWorker workers, List<ModelOfWorker> con)
        {
            this.workers = workers;
            this.con = con;
        }

        internal enum Post
        {
            S = ConsoleKey.S,
            Plus = ConsoleKey.OemPlus,
            Minus = ConsoleKey.OemMinus,
            Enter = ConsoleKey.Enter,
            UpArrow = ConsoleKey.UpArrow,
            DownArrow = ConsoleKey.DownArrow,
        }
        public void Interface()
        {
            int pose = 2;
            int max = allProd.Count() + 1;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(pose);
                Console.SetCursorPosition(0, pose);
                Console.WriteLine("->");

                InterfaceForUsers.PrintInterface(seller);
                PrintOrder();

                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == (ConsoleKey)Post.UpArrow)
                {
                    if (pose <= 2)
                    {
                        pose += max - 2;
                    }
                    else
                    {
                        pose--;
                    }
                }
                else if (key.Key == (ConsoleKey)Post.DownArrow)
                {
                    if (pose >= max)
                    {
                        pose -= max - 2;
                    }
                    else
                    {
                        pose++;
                    }
                }
                else if (key.Key == (ConsoleKey)Post.Plus)
                {
                    AddProduct(pose);
                }
                else if (key.Key == (ConsoleKey)Post.Minus)
                {
                    DeleteProd(pose);
                }
                else if (key.Key == (ConsoleKey)Post.S)
                {
                    Save();
                }
            }
        }
        public void DeleteProd(int id)
        {
            string path; int len; string json;
            
            path = Directory.GetCurrentDirectory();
            len = path.Length - 17;
            json = path.Substring(0, len) + "\\Product.json";
            List<ALlProduct> con = Converter.Des<List<ALlProduct>>(json);
            List<int> ids = new List<int>();
            
            foreach (ALlProduct prod in con)
            {
                ids.Add(prod.id);
            }

            ALlProduct aLlProduct = con[ids.IndexOf(id)];
            if (selledProducts.Count() != 0)
            {
                foreach (SellerALlProduct i in selledProducts)
                {
                    if (i.id == aLlProduct.id)
                    {
                        if (i.count-- >= 0)
                        {
                            SellerALlProduct vrem = i;
                            selledProducts.Remove(i);
                            vrem.count--;
                            selledProducts.Add(vrem);
                        
                            ALlProduct vrem2 = aLlProduct;
                            con.Remove(aLlProduct);
                            vrem.count++;
                            con.Insert(id, vrem2);

                            allProd = con;
                        }
                    }
                }
            }
        }
        public void AddProduct(int id)
        {
            string path; int len; string json;
            path = Directory.GetCurrentDirectory();
            len = path.Length - 17;
            json = path.Substring(0, len) + "\\Product.json";
            List<ALlProduct> con = Converter.Des<List<ALlProduct>>(json);
            List<int> ids = new List<int>();
            
            foreach (ALlProduct prod in con)
            {
                ids.Add(prod.id);
            }
            
            ALlProduct aLlProduct = con[ids.IndexOf(id)];
            if (selledProducts.Count() != 0)
            {
                foreach (SellerALlProduct i in selledProducts)
                {
                    if (i.id == aLlProduct.id)
                    {
                        if (aLlProduct.count >= i.count++)
                        {
                            SellerALlProduct vrem = i;
                            selledProducts.Remove(i);
                            vrem.count++;
                            selledProducts.Add(vrem);
                        
                            ALlProduct vrem2 = aLlProduct;
                            con.Remove(aLlProduct);
                            vrem.count--;
                            con.Insert(id, vrem2);

                            allProd = con;
                        }
                    }
                }
            }
            else
            {
                SellerALlProduct item = new SellerALlProduct();
                item.name = aLlProduct.name;
                item.id = aLlProduct.id;
                item.price = aLlProduct.price;
                item.count = aLlProduct.count--;
                item.UseCount++;
                selledProducts.Add(item);
                        
                ALlProduct vrem2 = aLlProduct;
                con.Remove(aLlProduct);
                vrem2.count--;
                con.Insert(id, vrem2);

                allProd = con;
            }
        }
        public void Save()
        {
            Console.WriteLine("Введите имя файла для обновления склада");
            string filename = Console.ReadLine();
            Converter.Ser<List<ALlProduct>>(allProd, filename);

            Console.WriteLine("Введите название файла для сохранения проданного товара");
            string filenameSelledProd = Console.ReadLine();
            Converter.Ser<List<SellerALlProduct>>(selledProducts, filenameSelledProd);
            
            List<Accounting> buh = new List<Accounting>();

            foreach (SellerALlProduct i in selledProducts)
            {
                Accounting newAcc = new Accounting();
                newAcc.id = i.id;
                newAcc.name = i.name;
                newAcc.sumPrice = i.count * i.price;
                newAcc.adds = 1;
                buh.Add(newAcc);
            }
            Console.WriteLine("Введите имя файла");
            string filenameForBuh = Console.ReadLine();
            Converter.Ser<List<Accounting>>(buh, filenameForBuh);

            selledProducts.Clear();
        }
        public void PrintOrder()
        {
            foreach (ALlProduct i in selledProducts)
            {
                Console.WriteLine($"Name: {i.name},  Count: {i.count}  Price(1pc): {i.price}");
            }
        }  
    }
}
