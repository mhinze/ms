using System;
using System.Collections.Generic;

namespace MasterSecrets.OpenGenerics
{
    public class DataBase
    {
        private static readonly Random _random = new Random();

        public static IEnumerable<Product> GetAll()
        {
            for (int i = 0; i < 20; i++)
            {
                yield return new Product {Price = _random.Next(1, 600)};
            }
        }
    }
}