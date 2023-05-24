using CQD4CS_HFT_2022232.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQD4CS_HFT_2022232.Client
{
    class CrudService
    {
        private RestService rest;

        public CrudService(RestService rest)
        {
            this.rest = rest;
        }

        public void Create<T>()
        {
            var properties = typeof(T).GetProperties().Where(p => p.GetAccessors().All(a => !a.IsVirtual) && p.Name != "Id");
            T instance = (T)Activator.CreateInstance(typeof(T));
            foreach (var property in properties)
            {
                Console.Write($"{property.Name} = ");
                string input = Console.ReadLine();
                if (property.PropertyType == typeof(int))
                {
                    property.SetValue(instance, int.Parse(input));
                }
                else
                {
                    property.SetValue(instance, input);
                }
            }
            try
            {
                rest.Post(instance, typeof(T).Name);

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                Thread.Sleep(2500);
            }

        }
        public void List<T>()
        {
            var properties = typeof(T).GetProperties().Where(p => p.GetAccessors().All(a => !a.IsVirtual));
            var items = rest.Get<T>(typeof(T).Name);

            foreach (var property in properties)
            {
                Console.Write($"{property.Name,-20}\t");
            }
            Console.Write("\n");


            foreach (var item in items)
            {
                foreach (var property in properties)
                {
                    Console.Write($"{property.GetValue(item),-20}\t");
                }
                Console.Write("\n");
            }

            Console.ReadLine();
        }
        public void Update<T>()
        {
            Console.WriteLine("Enter Entity's Id to update:");
            int id = int.Parse(Console.ReadLine());
            try
            {
                var instance = rest.Get<T>(id, typeof(T).Name);
                var properties = typeof(T).GetProperties().Where(p => p.GetAccessors().All(a => !a.IsVirtual) && p.Name != "Id");
                foreach (var property in properties)
                {
                    Console.Write($"New {property.Name} [Old: {property.GetValue(instance)}]= ");
                    string input = Console.ReadLine();
                    if (property.PropertyType == typeof(int))
                    {
                        property.SetValue(instance, int.Parse(input));
                    }
                    else
                    {
                        property.SetValue(instance, input);
                    }
                }
                try
                {
                    rest.Put(instance, typeof(T).Name);

                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                    Thread.Sleep(2500);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Thread.Sleep(2500);
            }


        }
        public void Delete<T>()
        {
            Console.WriteLine("Enter Entity's id to delete:");
            int id = int.Parse(Console.ReadLine());
            try
            {
                rest.Get<T>(id, typeof(T).Name);
                rest.Delete(id, typeof(T).Name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Thread.Sleep(2500);
            }

        }
    }
}
