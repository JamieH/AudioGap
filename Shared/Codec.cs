using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AudioGap.Shared;

namespace AudioGap.Shared
{
    public class Codec
    {
        public static Dictionary<string, Type> CodecTypes
        {
            get
            {
                return
                    Assembly.GetExecutingAssembly()
                        .GetTypes()
                        .Where(c => c.GetInterfaces().Contains(typeof (ICodec)))
                        .ToDictionary(o => o.Name);
            }
        }

        public static ICodec GetCodec(string name)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type codec in types.Where(codec => codec.GetInterfaces().Contains(typeof(ICodec))))
            {
                if (codec.Name == name)
                {
                    return (ICodec)Activator.CreateInstance(codec);
                }
            }

            throw new Exception("The codec you were looking for can't be found!");
        }
    }
}
