using System;

namespace Color.Core.Domain.Infrastructure
{
	public class BaseEntity
	{
		protected Guid _id;

		public virtual Guid Id
		{
			get { return _id; }
			private set { _id = value; }
		} 
	}
}