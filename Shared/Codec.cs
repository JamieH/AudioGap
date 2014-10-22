using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AudioGap.Shared
{
    public class Codec
    {
        private static readonly Dictionary<string, ICodec> Codecs;

        static Codec()
        {
            Codecs = Assembly.GetExecutingAssembly()
                             .GetTypes()
                             .Where(c => c.GetInterfaces().Contains(typeof(ICodec)))
                             .ToDictionary(t => t.Name, t => (ICodec)Activator.CreateInstance(t));
        }

        public static ICodec Get(string name)
        {
            ICodec codec;
            if (!Codecs.TryGetValue(name, out codec))
                throw new Exception("The codec you were looking for can't be found!");

            return codec;
        }

        public static IList<ICodec> List
        {
            get { return Codecs.Values.ToList(); }
        }
    }
}
