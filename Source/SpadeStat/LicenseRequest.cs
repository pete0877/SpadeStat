using System;

namespace SpadeStat
{
	/// <summary>
	/// Summary description for RegRequest.
	/// </summary>
	[Serializable]
	public class LicenseRequest
	{
		public string CPUID;

		public LicenseRequest()
		{			
			CPUID = "";
		}		
	}
}
