using System.Reflection;

namespace Campaigns.WebAPI.Ifrastructure.Models
{
    public class UpdateDocument<T>
    {
        private readonly Type TType;
        public UpdateDocument()
        {
            TType = typeof(T);
        }
        public List<Directive> Directives { get; set; }

        public Type GetDataType(string name)
        {
            PropertyInfo Prop = TType.GetProperties().Single(p => p.Name.Equals(name));
            return Prop.PropertyType;
        }

        public string GetValueFromDirective(string properyName)
        {
            return Directives.Single(d => d.PropertyName.Equals(properyName)).Value;
        }

    }
}
