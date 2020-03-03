using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MyWebsite.Helpers
{
	/// <summary>
	/// SHA512 Encryption algorithm
	/// </summary>
	public static class Encryptor
	{
		/// <summary>
		/// Computes string hash string using SHA512 encryption
		/// </summary>
		/// <param name="plainText">String which hash is to be computed</param>
		/// <param name="saltBytes">Salt bytes array (for hash verification)</param>
		/// <returns>Encrypted hash string</returns>
		public static string ComputeHash(string plainText, byte[] saltBytes = null)
		{
			// Generate salt in it's not provided.
			if (saltBytes == null)
			{
				saltBytes = new byte[new Random().Next(4, 8)];

				// Initialize a random number generator.
				using RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

				// Fill the salt with cryptographically strong byte values.
				rng.GetNonZeroBytes(saltBytes);
			}

			// Initializing array, which will hold plain text and salt.
			List<byte> plainBytes = new List<byte>();

			plainBytes.AddRange(Encoding.UTF8.GetBytes(plainText));
			plainBytes.AddRange(saltBytes);

			// Initialize hashing algorithm class.
			using HashAlgorithm encryptor = new SHA512Managed();

			// Create array which will hold hash and original salt bytes.
			List<byte> hash = new List<byte>();

			// Compute hash value of our plain text with appended salt.
			hash.AddRange(encryptor.ComputeHash(plainBytes.ToArray()));
			hash.AddRange(saltBytes);

			// Convert result into a base64-encoded string.
			return Convert.ToBase64String(hash.ToArray());
		}

		/// <summary>
		/// Verifies that <paramref name="hash"/> belongs to <paramref name="plainText"/> string
		/// </summary>
		/// <param name="plainText">Oringinal text string</param>
		/// <param name="hash">SHA512 encrypted hash string</param>
		/// <returns><see cref="true"/> if hash belong to <paramref name="plainText"/> otherwise returns <see cref="false"/></returns>
		public static bool VerifyHash(string plainText, string hash)
		{
			// Convert base64-encoded hash value into a byte array.
			byte[] hashBytes = Convert.FromBase64String(hash);

			// Make sure that the specified hash value is long enough.
			if (hashBytes.Length < 64)
				return false;

			// Allocate array to hold original salt bytes retrieved from hash.
			byte[] saltBytes = new byte[hashBytes.Length - 64];

			// Copy salt from the end of the hash to the new array.
			for (int i = 0; i < saltBytes.Length; i++)
				saltBytes[i] = hashBytes[64 + i];

			// Compute a new hash string.
			string expectedHashString = ComputeHash(plainText, saltBytes);

			// If the computed hash matches the specified hash,
			// the plain text value must be correct.
			return hash == expectedHashString;
		}
	}
}