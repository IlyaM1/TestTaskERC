using AccountWebAPI.Database.Models;

namespace AccountWebAPI.Dtos;

public abstract record BasePatchDto<T>
    where T : BaseModel
{
    /// <summary>
    ///     Этот метод присваивает значения из DTO в модель базы данных.
    ///     Если не переопределять метод, то он находит свойства по их именам
    ///     (поэтому для стандартного использования нужно в DTO использовать те же названия, что и в модели базы данных)
    /// </summary>
    /// <param name="objectFromDb">Объект из базы данных, который нужно изменить</param>
    public virtual void Apply(T objectFromDb)
    {
        var typeOfDto = GetType();
        var dtoProperties = typeOfDto.GetProperties();

        var modelProperties = typeof(T).GetProperties();

        foreach (var property in dtoProperties)
        {
            var value = property.GetValue(this);

            if (value is not null)
                modelProperties.First(prop => prop.Name == property.Name).SetValue(objectFromDb, value);
        }
    }
}
