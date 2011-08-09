using System;
using System.Collections;

namespace MasterSecrets.OpenGenerics
{
    public class Challenge
    {
        // given a type parameter, return a Type instance
        public static Type GetType<T>()
        {
            throw new NotImplementedException("How do I do this?");
        }


        // given a Type instance, return an IList
        public static IList GetList(Type type)
        {
            throw new NotImplementedException("How do I do this?");
        }

        
        // given a Type instance, return an IList populated with 5 default(T) instances
        public static IList GetFiveDefaults(Type type)
        {
            throw new NotImplementedException("How do I do this?");
        }
    }
}