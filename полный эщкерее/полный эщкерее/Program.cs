using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        Order order;
        do
        {
            order = new Order();
            order.StartOrderingProcess();
            Console.WriteLine("\nХотите сделать еще один заказ? (да/нет)");
        } while (Console.ReadLine().ToLower() == "да");
    }
}

class Order
{
    private List<MenuItem> selectedItems = new List<MenuItem>();
    private int TotalPrice { get; set; } = 0;

    public void StartOrderingProcess()
    {
        Console.Clear();
        Console.WriteLine("Заказ тортов в \"Спириты непобедимы\", торты на ваш выбор!\n");
        Console.WriteLine("Выберите параметр торта");
        Console.WriteLine("------------------------");

        var categories = new List<string> { "Форма торта", "Размер торта", "Вкус коржей", "Количество коржей", "Глазурь", "Декор", "Самовывоз или доставка", "Конец заказа" };

        int index = 0;
        ConsoleKey key;
        do
        {
            Console.Clear();
            Console.WriteLine("Заказ тортов в \"ГЛАЗУРЬКА\", торты на ваш выбор!\n");
            Console.WriteLine("Выберите параметр торта");
            Console.WriteLine("------------------------");

            for (int i = 0; i < categories.Count; i++)
            {
                if (i == index)
                {
                    Console.WriteLine($"> {categories[i]}");
                }
                else
                {
                    Console.WriteLine($"  {categories[i]}");
                }
            }

            Console.WriteLine("\nЦена: " + TotalPrice);
            Console.WriteLine("Ваш торт:");
            foreach (var item in selectedItems)
            {
                Console.WriteLine($"{item.Description} - {item.Price}");
            }

            key = Console.ReadKey().Key;
            if (key == ConsoleKey.UpArrow && index > 0)
            {
                index--;
            }
            else if (key == ConsoleKey.DownArrow && index < categories.Count - 1)
            {
                index++;
            }

            if (key == ConsoleKey.Enter)
            {
                if (categories[index] != "Конец заказа")
                {
                    var selectedItem = ArrowMenu.SelectFromSubMenu(categories[index]);
                    if (selectedItem != null)
                    {
                        selectedItems.Add(selectedItem);
                        TotalPrice += selectedItem.Price;
                    }
                }
            }

        } while (key != ConsoleKey.Enter || categories[index] != "Конец заказа");

        SaveOrderToFile();
    }

    private void SaveOrderToFile()
    {
        using (StreamWriter writer = new StreamWriter("Заказы.txt", append: true))
        {
            writer.WriteLine($"Заказ от {DateTime.Now.ToShortDateString()}");
            writer.WriteLine($"Заказ:");
            foreach (var item in selectedItems)
            {
                writer.WriteLine($"{item.Description}");
            }
            writer.WriteLine($"Цена: {TotalPrice}\n");
        }
    }

}

static class ArrowMenu
{
    public static MenuItem SelectFromSubMenu(string category)
    {
        var subMenus = new Dictionary<string, List<MenuItem>>
        {
            {"Форма торта", new List<MenuItem>
                {
                    new MenuItem("Круг", 1000),
                    new MenuItem("Квадрат", 1500),
                    new MenuItem("Прямоугольник", 2500),
                    new MenuItem("Сердечко", 5700)
                }
            },
            {"Размер торта", new List<MenuItem>
                {
                    new MenuItem("Маленький", 1300),
                    new MenuItem("Средний", 3500),
                    new MenuItem("Большой", 6700)
                }
            },
            {"Вкус коржей", new List<MenuItem>
                {
                    new MenuItem("Ванильный", 2200),
                    new MenuItem("Шоколадный", 2250),
                    new MenuItem("Красный бархат", 4300)
                }
            },
            {"Количество коржей", new List<MenuItem>
                {
                    new MenuItem("Один корж", 1100),
                    new MenuItem("Два коржа", 2200),
                    new MenuItem("Три коржа", 2800)
                }
            },
            {"Глазурь", new List<MenuItem>
                {
                    new MenuItem("Шоколадная", 1250),
                    new MenuItem("Ванильная", 2000)
                }
            },
            {"Декор", new List<MenuItem>
                {
                    new MenuItem("Орехи", 100),
                    new MenuItem("Кокосовая стружка", 1150),
                    new MenuItem("Шоколадные крошки", 2280)
                }
            },
            {"Самовывоз или доставка", new List<MenuItem>
                {
                    new MenuItem("Самовывоз", 100),
                    new MenuItem("Доставка", 500)
                }
            }
        };

        var items = subMenus[category];
        int subIndex = 0;
        ConsoleKey subKey;

        do
        {
            Console.Clear();
            Console.WriteLine($"Выбор: {category}");
            Console.WriteLine("Для выхода нажмите Escape");
            for (int i = 0; i < items.Count; i++)
            {
                if (i == subIndex)
                {
                    Console.WriteLine($"> {items[i].Description} - {items[i].Price}");
                }
                else
                {
                    Console.WriteLine($"  {items[i].Description} - {items[i].Price}");
                }
            }

            subKey = Console.ReadKey().Key;
            if (subKey == ConsoleKey.UpArrow && subIndex > 0)
            {
                subIndex--;
            }
            else if (subKey == ConsoleKey.DownArrow && subIndex < items.Count - 1)
            {
                subIndex++;
            }

            if (subKey == ConsoleKey.Escape)
            {
                return null;
            }

        } while (subKey != ConsoleKey.Enter);

        return items[subIndex];
    }
}

class MenuItem
{
    public string Description { get; set; }
    public int Price { get; set; }

    public MenuItem(string description, int price)
    {
        Description = description;
        Price = price;
    }
}
