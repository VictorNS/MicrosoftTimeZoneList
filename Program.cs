using System;

namespace TimeZoneList
{
	class Program
	{
		private const string outputFile = @"C:\Temp\TimeZoneList.txt";

		static void Main()
		{
			var timezones = TimeZoneInfo.GetSystemTimeZones();
			var date1 = new DateTime(2015, 1, 15);
			var date2 = new DateTime(2015, 7, 15);

			using (var file = new System.IO.StreamWriter(outputFile, false))
			{
				file.WriteLine(string.Format("{0,-62} {1,-32} {2,-32} {3,-32} {4,-15} {5,-20} {6,-20}", "Display Name", "Id", "Standard Name", "Daylight Name", "Supports DST", "UTC Standard Offset", "UTC Daylight Offset"));
				file.WriteLine();

				foreach (var timezone in timezones)
				{
					var o1 = timezone.GetUtcOffset(date1);
					var o2 = timezone.GetUtcOffset(date2);
					var o1String = " 00:00";
					var o2String = " 00:00";

					if (o1 < TimeSpan.Zero)
						o1String = o1.ToString(@"\-hh\:mm");
					if (o1 > TimeSpan.Zero)
						o1String = o1.ToString(@"\+hh\:mm");
					if (o2 < TimeSpan.Zero)
						o2String = o2.ToString(@"\-hh\:mm");
					if (o2 > TimeSpan.Zero)
						o2String = o2.ToString(@"\+hh\:mm");

					file.WriteLine(string.Format("{0,-62} {1,-32} {2,-32} {3,-32} {4,-15} {5,-20} {6,-20}",
													timezone.DisplayName,
													timezone.Id,
													timezone.StandardName,
													timezone.DaylightName,
													timezone.SupportsDaylightSavingTime ? "Yes" : "No",
													o1String,
													o2String));
				}
			}

			Console.Write($"The result is in {outputFile}");
		}
	}
}
