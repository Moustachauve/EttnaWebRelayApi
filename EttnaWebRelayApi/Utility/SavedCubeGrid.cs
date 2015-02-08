using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EttnaWebRelayApi.Utility
{
	public class SavedCubeGrid
	{
		public SortedList<ulong, FileInfo> m_versionList;

		public SavedCubeGrid()
		{
			m_versionList = new SortedList<ulong, FileInfo>();
		}
	}
}
