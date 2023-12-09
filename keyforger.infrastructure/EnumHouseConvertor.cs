using keyforger.domain;

namespace keyforger.infrastructure;

// from other converter class, just for reference...
/*
public class EnumHouseConvertor<TEnum> : 
  ValueConverter<List<TEnum>, string> where TEnum : struct
{
    // create method to convert back/forth enum to string
    // TODO - this was written by Seba, try to understand whats happening here
    public EnumHouseConvertor() : base(
        e => string.Join("|", e.Select(x => x.ToString())),
        s => string.IsNullOrEmpty(s)
            ? new List<TEnum>()
            : s.Split("|",
                    StringSplitOptions.None)
                .Select(e => Enum.Parse<TEnum>(e))
                .ToList()
    )
    {
        
    }
}
*/

public static class EnumHouseConvertor
{
  public static string? ConvertToString(this House house)
  {
    return Enum.GetName(house.GetType(), house);
  }

  public static House ConvertToEnum(this string house)
  {
    return (House)Enum.Parse(typeof(House), house);
  }
}