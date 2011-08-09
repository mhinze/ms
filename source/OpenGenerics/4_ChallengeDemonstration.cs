using System;
using System.Collections.Generic;
using NUnit.Framework;
using StructureMap;

namespace MasterSecrets.OpenGenerics
{
    [TestFixture]
    public class ChallengeDemonstration
    {
        [Test]
        public void ShowProductsOver500()
        {
            var query = new ExpensiveProducts(500m);

            IQuerier masterQuerier = GetQuerier();

            IEnumerable<Product> products = masterQuerier.GetResult(query);

            foreach (Product product in products)
            {
                Console.WriteLine(product.Price);
            }
        }

        #region Setup

        [SetUp]
        public void Setup()
        {
            ObjectFactory.Initialize(c => c.Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.ConnectImplementationsToTypesClosing(typeof (IQueryHandler<,>));
                }));
        }

        private static IQuerier GetQuerier()
        {
            return ObjectFactory.GetInstance<Querier>();
        }

        #endregion
    }
}