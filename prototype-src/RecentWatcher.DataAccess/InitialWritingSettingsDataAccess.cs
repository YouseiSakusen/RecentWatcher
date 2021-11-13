using elf.Repositoris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elf.RecentWatcher
{
	public class InitialWritingSettingsDataAccess : SqliteDataAccessBase
	{
		public override List<T> GetItems<T>(T condition)
		{
			throw new NotImplementedException();
		}

		public InitialWritingSettingsDataAccess(string connectString) : base(connectString) { }
	}
}
