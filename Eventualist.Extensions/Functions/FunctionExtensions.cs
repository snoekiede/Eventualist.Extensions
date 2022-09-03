using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Eventualist.Extensions.Functions
{
    public static class FunctionExtensions
    {
        public static Func<TReturnType> Memoize<TReturnType>(this Func<TReturnType> func)
        {
            TReturnType cache = func();
            return () =>
            {
                if (cache == null)
                {
                    cache = func();
                }

                return cache;
            };
        }

        public static Func<TParameterType, TReturnType> Memoize<TParameterType, TReturnType>(
            this Func<TParameterType, TReturnType> func)
        {
            Dictionary<TParameterType, TReturnType> dictionary = new Dictionary<TParameterType, TReturnType>();
            return r =>
            {
                if (!dictionary.ContainsKey(r))
                {
                    dictionary[r] = func(r);
                }

                return dictionary[r];
            };
        }

        public static Func<TFirstParameterType, TSecondParameterType, TReturnType> Memoize<TFirstParameterType,
            TSecondParameterType, TReturnType>(this Func<TFirstParameterType, TSecondParameterType, TReturnType> func)
        {
            Dictionary<Tuple<TFirstParameterType, TSecondParameterType>, TReturnType> twoparamdictionary =
                new Dictionary<Tuple<TFirstParameterType, TSecondParameterType>, TReturnType>();

            return (f, s) =>
            {
                var t = Tuple.Create(f, s);
                if (!twoparamdictionary.ContainsKey(t))
                {
                    twoparamdictionary[t] = func(f, s);
                }

                return twoparamdictionary[t];

            };
        }


        public static Func<TFirstParameterType, TSecondParameterType, TThirdParameterType, TReturnType> Memoize<TFirstParameterType,
            TSecondParameterType, TThirdParameterType, TReturnType>(this Func<TFirstParameterType, TSecondParameterType, TThirdParameterType, TReturnType> func)
        {
            Dictionary<Tuple<TFirstParameterType, TSecondParameterType, TThirdParameterType>, TReturnType> threeparamdictionary =
                new Dictionary<Tuple<TFirstParameterType, TSecondParameterType, TThirdParameterType>, TReturnType>();

            return (f, s, t) =>
            {
                var result = Tuple.Create(f, s, t);
                if (!threeparamdictionary.ContainsKey(result))
                {
                    threeparamdictionary[result] = func(f, s, t);
                }

                return threeparamdictionary[result];

            };
        }

        public static Func<TFirstParameterType, TSecondParameterType, TThirdParameterType, TFourthParameterType, TReturnType> Memoize<TFirstParameterType,
            TSecondParameterType, TThirdParameterType, TFourthParameterType, TReturnType>(this Func<TFirstParameterType, TSecondParameterType, TThirdParameterType, TFourthParameterType, TReturnType> func)
        {
            Dictionary<Tuple<TFirstParameterType, TSecondParameterType, TThirdParameterType, TFourthParameterType>, TReturnType> fourparamdictionary =
                new Dictionary<Tuple<TFirstParameterType, TSecondParameterType, TThirdParameterType, TFourthParameterType>, TReturnType>();

            return (f, s, t, fo) =>
            {
                var result = Tuple.Create(f, s, t, fo);
                if (!fourparamdictionary.ContainsKey(result))
                {
                    fourparamdictionary[result] = func(f, s, t, fo);
                }

                return fourparamdictionary[result];

            };
        }

        public static Func<TFirstParameterType, TSecondParameterType, TThirdParameterType, TFourthParameterType, TFifthParameterType, TReturnType> Memoize<TFirstParameterType,
            TSecondParameterType, TThirdParameterType, TFourthParameterType, TFifthParameterType, TReturnType>(this Func<TFirstParameterType, TSecondParameterType, TThirdParameterType, TFourthParameterType, TFifthParameterType, TReturnType> func)
        {
            Dictionary<Tuple<TFirstParameterType, TSecondParameterType, TThirdParameterType, TFourthParameterType, TFifthParameterType>, TReturnType> fiveparamdictionary =
                new Dictionary<Tuple<TFirstParameterType, TSecondParameterType, TThirdParameterType, TFourthParameterType, TFifthParameterType>, TReturnType>();

            return (f, s, t, fo, fi) =>
            {
                var result = Tuple.Create(f, s, t, fo, fi);
                if (!fiveparamdictionary.ContainsKey(result))
                {
                    fiveparamdictionary[result] = func(f, s, t, fo, fi);
                }

                return fiveparamdictionary[result];

            };
        }

        public static Func<TFirstParameterType, TSecondParameterType, TThirdParameterType, TFourthParameterType, TFifthParameterType, TSixthParameterType, TReturnType> Memoize<TFirstParameterType,
            TSecondParameterType, TThirdParameterType, TFourthParameterType, TFifthParameterType, TSixthParameterType, TReturnType>(this Func<TFirstParameterType, TSecondParameterType, TThirdParameterType, TFourthParameterType, TFifthParameterType, TSixthParameterType, TReturnType> func)
        {
            Dictionary<Tuple<TFirstParameterType, TSecondParameterType, TThirdParameterType, TFourthParameterType, TFifthParameterType, TSixthParameterType>, TReturnType> sixparamdictionary =
                new Dictionary<Tuple<TFirstParameterType, TSecondParameterType, TThirdParameterType, TFourthParameterType, TFifthParameterType, TSixthParameterType>, TReturnType>();

            return (f, s, t, fo, fi, si) =>
            {
                var result = Tuple.Create(f, s, t, fo, fi, si);
                if (!sixparamdictionary.ContainsKey(result))
                {
                    sixparamdictionary[result] = func(f, s, t, fo, fi, si);
                }

                return sixparamdictionary[result];

            };
        }


    }
}

