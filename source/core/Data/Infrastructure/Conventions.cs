using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Color.Core.Data.Infrastructure
{
	public class DefaultTableNameConvention : IClassConvention
	{
		public void Apply(IClassInstance target)
		{
			target.Table(Inflector.Net.Inflector.Pluralize(target.EntityType.Name));
		}
	}
}