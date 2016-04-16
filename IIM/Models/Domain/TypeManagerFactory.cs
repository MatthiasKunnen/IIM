using System.ComponentModel;

namespace IIM.Models.Domain
{
    public static class TypeManagerFactory
    {
        public static ITypeManager CreateTypeManager(Type type)
        {
            switch (type)
            {
                case Type.Staff:
                    return new StaffManager();
                case Type.Student:
                    return new StudentManager();
                default:
                    throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(Type));
            }
        }
    }
}