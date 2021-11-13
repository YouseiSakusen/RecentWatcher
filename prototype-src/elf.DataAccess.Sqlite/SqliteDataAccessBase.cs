using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elf.Repositoris
{
	public abstract class SqliteDataAccessBase : IRepository
	{
		public string ConnectString { get; } = string.Empty;

		public abstract List<T> GetItems<T>(T condition) where T : class;

		public SqliteDataAccessBase(string connectString)
		{
			this.ConnectString = connectString;
		}
	}
}
