using System;

namespace Behourd.Test.Utilities
{
    internal class JoueurBuilder
    {
        //private int _weight;
        //private IArme _arme;
        public static IJoueur Stub => new JoueurBuilder().Build();

        //public JoueurBuilder WithWeight(int weight)
        //{
        //    _weight = weight;
        //    return this;
        //}

        //public JoueurBuilder WithArme(Func<ArmeBuilder, ArmeBuilder> configuration)
        //{
        //    var armeBuilder = new ArmeBuilder();
        //    configuration(armeBuilder);
        //    _arme = armeBuilder.Build();

        //    return this;
        //}

        public IJoueur Build() => new Joueur(/*_weight, _arme*/);
    }
}
