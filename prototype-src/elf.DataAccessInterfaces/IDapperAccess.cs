using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elf.Repositoris
{
	public interface IDapperAccess
	{
		public Task<DbConnection> GetAsyncConnection();

		public string DbConnectString { get; }
	}
}
