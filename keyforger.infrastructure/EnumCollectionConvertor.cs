using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace keyforger.infrastructure
{
  public class EnumCollectionConvertor<TEnum> :
      ValueConverter<List<TEnum>, string> where TEnum : struct
  {
    // create method to convert back/forth enum to string
    // TODO - this was written by Seba, try to understand whats happening here
    public EnumCollectionConvertor() : base(
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
}