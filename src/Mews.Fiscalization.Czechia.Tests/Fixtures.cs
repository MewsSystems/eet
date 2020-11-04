using System;
using System.IO;
using System.Reflection;

namespace Mews.Eet.Tests
{
    public class Fixtures
    {
        public static readonly string TaxId1 = Environment.GetEnvironmentVariable("tax_id_1") ?? "INSERT_TAX_ID_1";
        public static readonly string TaxId2 = Environment.GetEnvironmentVariable("tax_id_2") ?? "INSERT_TAX_ID_2";
        public static readonly string TaxId3 = Environment.GetEnvironmentVariable("tax_id_3") ?? "INSERT_TAX_ID_3";
        public static readonly string CertificateData1 = Environment.GetEnvironmentVariable("certificate_data_1") ?? "INSERT_CERTIFICATE_DATA_1";
        public static readonly string CertificateData2 = Environment.GetEnvironmentVariable("certificate_data_2") ?? "INSERT_CERTIFICATE_DATA_2";
        public static readonly string CertificateData3 = Environment.GetEnvironmentVariable("certificate_data_3") ?? "INSERT_CERTIFICATE_DATA_3";
        public static readonly string CertificatePassword = Environment.GetEnvironmentVariable("certificate_password") ?? "INSERT_CERTIFICATE_PASSWORD";

        public static TaxPayerFixture First = new TaxPayerFixture
        {
            TaxId = TaxId1,
            PremisesId = 1,
            CertificatePassword = CertificatePassword,
            CertificateData = Convert.FromBase64String(CertificateData1)
        };

        public static TaxPayerFixture Second = new TaxPayerFixture
        {
            TaxId = TaxId2,
            PremisesId = 1,
            CertificatePassword = CertificatePassword,
            CertificateData = Convert.FromBase64String(CertificateData2)
        };

        public static TaxPayerFixture Third = new TaxPayerFixture
        {
            TaxId = TaxId3,
            PremisesId = 1,
            CertificatePassword = CertificatePassword,
            CertificateData = Convert.FromBase64String(CertificateData3)
        };
    }

    public class TaxPayerFixture
    {
        public string TaxId { get; set; }
        public int PremisesId { get; set; }
        public string CertificatePassword { get; set; }
        public byte[] CertificateData { get; set; }
    }
}
