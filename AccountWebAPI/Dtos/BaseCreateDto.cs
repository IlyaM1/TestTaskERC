using AccountWebAPI.Database.Models;

namespace AccountWebAPI.Dtos;

public abstract record BaseCreateDto<T> 
    where T : BaseModel, new()
{
    public virtual T Map()
    {
        var typeOfDto = GetType();
        var dtoProperties = typeOfDto.GetProperties();

        var modelProperties = typeof(T).GetProperties();
        var result = new T();

        foreach (var property in dtoProperties)
        {
            var value = property.GetValue(this);

            if (value is not null)
                modelProperties.First(prop => prop.Name == property.Name).SetValue(result, value);
        }

        return result;
    }
}
