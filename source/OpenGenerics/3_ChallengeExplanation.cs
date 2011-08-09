using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap;

namespace MasterSecrets.OpenGenerics
{
    public interface IQuerier
    {
        TResult GetResult<TResult>(IQuery<TResult> query);
    }

    public interface IQuery<TResult>
    {
    }

    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult>
    {
        TResult Execute(TQuery query);
    }

    public class ExpensiveProducts : IQuery<IEnumerable<Product>>
    {
        public ExpensiveProducts(decimal minPrice)
        {
            MinPrice = minPrice;
        }

        public decimal MinPrice { get; private set; }
    }

    public class ExpensiveProductsQueryHandler : IQueryHandler<ExpensiveProducts, IEnumerable<Product>>
    {
        public IEnumerable<Product> Execute(ExpensiveProducts query)
        {
            return DataBase.GetAll().Where(x => x.Price > query.MinPrice);
        }
    }

    public class Querier : IQuerier
    {
        private readonly IContainer _container;

        public Querier(IContainer container)
        {
            _container = container;
        }

        public TResult GetResult<TResult>(IQuery<TResult> query)
        {
            Type handlerType = typeof (IQueryHandler<,>).MakeGenericType(query.GetType(), typeof (TResult));

            #region RedHerring

//            var queryHandler = _container.GetInstance<IQueryHandler<IQuery<TResult>, TResult>>();
//
//            return queryHandler.Execute(query);

            #endregion

            object handlerInstance = _container.GetInstance(handlerType);

            Type helperType = typeof (Helper<,>).MakeGenericType(query.GetType(), typeof (TResult));

            var helperInstance = ((Helper) Activator.CreateInstance(helperType));

            return (TResult) helperInstance.ExecuteHandler(handlerInstance, query);
        }

        private interface Helper
        {
            object ExecuteHandler(object handler, object query);
        }

        private class Helper<TQuery, TResult> : Helper where TQuery : IQuery<TResult>
        {
            public object ExecuteHandler(object handler, object query)
            {
                return ((IQueryHandler<TQuery, TResult>) handler).Execute((TQuery) query);
            }
        }
    }
}