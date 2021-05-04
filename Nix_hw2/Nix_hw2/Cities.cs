using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_hw2
{
    struct City //Сама структура осталась без изменений
    {
        public string Name;
        public int Distance;
    }

    class CityKeyValueCollection : KeyedCollection<string, City> //класс коллекции городов, созданный на основе KeyedCollection
                                                     //и с переопределенным методом GetKeyForItem, чтобы Название города служило ключом
    {
        protected override string GetKeyForItem(City item)
        {
            return item.Name;
        }        
    }

    class Task3
    {
        private CityKeyValueCollection cities; //коллекция городов

        public Task3()
        {
            //Значение дистанции между городами теперь записываются не как дистанция между двумя соседними городами, а как дистанция до
            //стартовой точки, в данном случае Киева. Таким образом дистанцию можно будет вычислить простой разницей значений дистанции
            //между двумя городами. Т.к. у нас все города стоят на одной дороге, то города в другую от Киева сторону можно записывать с
            //отрицательным значением дистанции и логика работы функции определения дистанции между городами не нарушится
            cities = new CityKeyValueCollection();
            cities.Add(new City { Name = "Kiev", Distance = 0 }); 
            cities.Add(new City { Name = "Borispol", Distance = 38 });
            cities.Add(new City { Name = "Piryatin", Distance = 165 });
            cities.Add(new City { Name = "Lubny", Distance = 212 });
            cities.Add(new City { Name = "Horol", Distance = 253 });
            cities.Add(new City { Name = "Reshetylivka", Distance = 326 });
            cities.Add(new City { Name = "Poltava", Distance = 365 });
            cities.Add(new City { Name = "Chutovo", Distance = 417 });
            cities.Add(new City { Name = "Valki", Distance = 455 });
            cities.Add(new City { Name = "Lubotin", Distance = 492 });
            cities.Add(new City { Name = "Pisochyn", Distance = 507 });
            cities.Add(new City { Name = "Kharkov", Distance = 518 });

            //если добавлять город с существующим названием вызовется исключение ArgumentException
            //поэтому каждое новое добавление желательно оборачивать блоком try-catch:
           /*try //Проверка добавления города с существующим назанием в блоке try-catch
            {
                cities.Add(new City { Name = "Horol", Distance = 333 });
            }

            catch (ArgumentException e)
            {
                Console.WriteLine("Город с таким названием уже существует");
            }*/

        }

        /*static void Swap<T>(ref T obj1, ref T obj2) //функция Swap более не используется
        {
            var temp = obj1;
            obj1 = obj2;
            obj2 = temp;
        }*/

        public int GetDistance(string cityFrom, string cityTo)
        {
            int res = -1;
            if (cities.Contains(cityFrom) && cities.Contains(cityTo)) //проверка на существование городов с заданными названями в коллекции
            {
                var from = cities[cityFrom];
                var to = cities[cityTo];
                res = Math.Abs(to.Distance - from.Distance); //Порядок городов неважен т.к. минус отброситься функцией Math.Abs
            }
            return res;
        } //в итоге получаем конечное число действий (по идее ~8, если мы зашли в if, и ~5 если не зашли), то есть O(1) - константа

        public void Print() //добавил функцию вывода всех городов для проверки корректного заполнения коллекции
        {
            foreach (var c in cities)
            {
                Console.WriteLine($"{c.Name} - {c.Distance}");
            }
        }
    }

}
