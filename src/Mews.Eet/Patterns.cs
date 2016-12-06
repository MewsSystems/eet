using System.Text.RegularExpressions;

namespace Mews.Eet
{
    public class Patterns
    {
        public static readonly Regex BillNumber = new Regex("^[0-9a-zA-Z.,:;/#-_ ]{1,25}$");

        public static readonly Regex RegistryIdentifier = new Regex("^[0-9a-zA-Z.,:;/#-_]{1,20}$");

        public static readonly Regex TaxIdentifier = new Regex("^CZ[0-9]{8,10}$");

        public static readonly Regex Uuid4 = new Regex("^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[1-5][0-9a-fA-F]{3}-[89abAB][0-9a-fAF]{3}-[0-9a-fA-F]{12}$");
    }
}
