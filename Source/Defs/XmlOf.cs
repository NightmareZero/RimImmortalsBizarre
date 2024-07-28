using RimWorld;

namespace NzRimImmortalBizarre
{
    [DefOf]
    public class XmlOf
    {

        public static DefDismemberSelf defDismemberSelf;


        static XmlOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(XmlOf));
        }
    }


}