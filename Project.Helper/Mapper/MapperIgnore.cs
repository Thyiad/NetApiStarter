using System;
namespace Project.Helper.Mapper
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class MapperIgnore : Attribute
    {
    }
}
