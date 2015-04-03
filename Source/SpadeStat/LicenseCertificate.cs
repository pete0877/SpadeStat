using System;

namespace SpadeStat
{
	/// <summary>
	/// Summary description for LicenseCertificate.
	/// </summary>
	[Serializable]
	public class LicenseCertificate
	{
		public string CPUID;

		public LicenseCertificate()
		{
			CPUID = "";
		}
	}
}
