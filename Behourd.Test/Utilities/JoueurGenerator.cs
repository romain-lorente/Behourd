using System;
using System.Collections.Generic;
using System.Linq;

namespace Behourd.Test.Utilities
{
    internal class JoueurGenerator
    {
        //public static IEnumerable<IJoueur> GenerateSpecific(Func<JoueurBuilder, JoueurBuilder> configuration)
        //{
        //    while (true)
        //    {
        //        var builder = new JoueurBuilder();
        //        configuration(builder);
        //        yield return builder.Build();
        //    }
        //}

        public static IEnumerable<IJoueur> Generate()
        {
            while (true)
            {
                yield return JoueurBuilder.Stub;
            }
        }

        //public static IEnumerable<IJoueur> GenerateSpecific(Func<JoueurBuilder, JoueurBuilder> configuration, int elements)
        //    => GenerateSpecific(configuration).Take(elements);

        public static IEnumerable<IJoueur> Generate(int elements)
            => Generate().Take(elements);
    }
}
