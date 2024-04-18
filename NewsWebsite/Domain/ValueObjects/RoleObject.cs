
namespace NewsWebsite.Domain.ValueObjects
{
    public enum RoleEnum
    {
        USER, ADMIN
    }

    public class RoleObject : ValueObject
    {
        public RoleEnum Value { get; set; } = RoleEnum.USER;
        public int ValueNum { get; set; }

        public RoleObject() { }
        public RoleObject(RoleEnum roleEnum)
        {
            Value = roleEnum;
        }

        public RoleObject(string value)
        {
            switch (value)
            {
                case "admin": Value = RoleEnum.ADMIN; ValueNum = 1; break;
                case "user": Value = RoleEnum.USER; ValueNum = 0; break;
                default: Value = RoleEnum.USER; break;
            }
        }

        public override string ToString()
        {
            switch (Value)
            {
                case RoleEnum.USER:
                    return "User";
                case RoleEnum.ADMIN:
                    return "User";
                default: return "";
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return ValueNum;
        }
    }
}
