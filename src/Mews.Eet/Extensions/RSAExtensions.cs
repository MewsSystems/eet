using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Mews.Eet.Extensions
{
	public static class RSAExtensions
	{
		public static RSACryptoServiceProvider GetPrivateKeyRsaCryptoServiceProvider(this X509Certificate2 certificate, bool useMachineKeyStore)
		{
			// Get params from private key
			var rsa = certificate.GetRSAPrivateKey();
			var rsaParameters = rsa.ExportParameters(true);

			var csp = new RSACryptoServiceProvider(rsa.KeySize, new CspParameters()
			{
				ProviderName = "Microsoft Enhanced RSA and AES Cryptographic Provider",
				Flags = useMachineKeyStore ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags
			});
			csp.ImportParameters(rsaParameters);

			return csp;
		}
	}
}
