using System;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;

namespace SpadeStat
{
	/// <summary>
	/// Summary description for Utils.
	/// </summary>
	public class Utils
	{
		private static byte[] key		= {223, 182, 132, 102, 57, 36, 69, 227, 203, 108, 119, 214, 187, 215, 50, 30, 245, 14, 120, 28, 85, 65, 33, 4, 114, 110, 205, 18, 15, 117, 146, 230};
		private static byte[] vector		= {194, 135, 97, 62, 49, 243, 12, 89, 224, 240, 170, 111, 253, 236, 147, 71};

		public static string EncryptStream(MemoryStream plainStream)
		{
			// Initialize cryptography provider:
			SymmetricAlgorithm crypton = SymmetricAlgorithm.Create(); 
			crypton.Key = key;
			crypton.IV = vector;

			// Convert plain stream to byte array:
			byte[] plainArray = plainStream.ToArray();
			
			// Encrypt the plain data array into stream
			MemoryStream encryptedStream = new MemoryStream();
			CryptoStream encStream = new CryptoStream(encryptedStream, crypton.CreateEncryptor(), CryptoStreamMode.Write);
            encStream.Write(plainArray, 0, plainArray.Length);
			encStream.Close();
            
			// Get resulting encrypted array:
			byte[] encryptedArray = encryptedStream.ToArray();
		
			// Convert encrypted array into Base64 string
			string result = System.Convert.ToBase64String(encryptedArray, 0, encryptedArray.Length);

			return result;
		}

		public static MemoryStream DecryptStream(string encodedString)
		{
			// Initialize cryptography provider:
			SymmetricAlgorithm crypton = SymmetricAlgorithm.Create(); 
			crypton.Key = key;
			crypton.IV = vector;

			// Convert base64 encoded string into byte array:
			byte[] encryptedArray = System.Convert.FromBase64String(encodedString);

			// Decrypt the encrypted data array into another array:
			MemoryStream decryptedStream = new MemoryStream();
			CryptoStream decStream = new CryptoStream(decryptedStream, crypton.CreateDecryptor(), CryptoStreamMode.Write);
			decStream.Write(encryptedArray, 0, encryptedArray.Length);
			decStream.FlushFinalBlock();

			decryptedStream.Position = 0;

			return decryptedStream;
		}
	}
}

