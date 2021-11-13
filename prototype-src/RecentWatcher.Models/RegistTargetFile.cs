using System;

namespace elf.RecentWatcher
{
	public class RegistTargetFile
	{
		public DateTime? RealAccessDateTime { get; set; } = null;

		public DateTime? AccessTime
		{
			get
			{
				if (this.RealAccessDateTime.HasValue)
				{
					return new DateTime(this.RealAccessDateTime.Value.Year,
										this.RealAccessDateTime.Value.Month,
										this.RealAccessDateTime.Value.Day,
										this.RealAccessDateTime.Value.Hour,
										this.RealAccessDateTime.Value.Minute,
										this.RealAccessDateTime.Value.Second);
				}
				else
				{
					return null;
				}
			}
		}

		public string FilePath { get; set; } = string.Empty;

		public RegistTargetFile(DateTime? accessTime, string filePath)
		{
			this.RealAccessDateTime = accessTime;
			this.FilePath = filePath;
		}
	}
}
