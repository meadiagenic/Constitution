namespace System.Extensions
{
    using System;

    public static class TypeExtensions
    {
        public static T Create<T>(this Type type)
        {
            return (T)Activator.CreateInstance(type);
        }
    }
}
