using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elf.Repositoris
{
	public interface IRepository
	{
		public List<T> GetItems<T>(T condition) where T : class;
	}
}
