using System.Reflection;

namespace TodoAPI.Extensions
{
    public static class Extensitionmethods
    {
        public static T2 ToViewModel<T1,T2>(this T1 entity)
        {
            PropertyInfo[] properties = entity.GetType().GetProperties();
            var responseItem = (T2)Activator.CreateInstance(typeof(T2));
            foreach (var property in properties)
            {
                //var entityProperty = typeof(T1).GetProperty(property.Name);
                var viewModelProperty = typeof(T2).GetProperty(property.Name);
                if (viewModelProperty.Name == property.Name && viewModelProperty.PropertyType == property.PropertyType)
                {
                    var value = property.GetValue(entity);
                    viewModelProperty.SetValue(responseItem, value);
                }
            }
            return responseItem;
        }
    }
}
