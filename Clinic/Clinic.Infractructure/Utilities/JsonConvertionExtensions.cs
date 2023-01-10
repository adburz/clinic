using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Clinic.Infrastructure.Utilities;

public static class JsonConvertionExtensions
{
    public static PropertyBuilder<T?> HasJsonConversion<T>(this PropertyBuilder<T?> propertyBuilder) where T: class
    {
        var converter = new ValueConverter<T?, string>(
            v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<T>(v)
            );

        var comparer = new ValueComparer<T>(
            (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
            v=>JsonConvert.SerializeObject(v).GetHashCode(),
            v=>JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(v))!
            );

        propertyBuilder.HasConversion(converter);
        propertyBuilder.Metadata.SetValueConverter(converter);
        propertyBuilder.Metadata.SetValueComparer(comparer);

        return propertyBuilder;
    }
}
