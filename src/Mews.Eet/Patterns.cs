namespace Mews.Eet
{
    public static class Patterns
    {
        public const string BillNumber = "^[0-9a-zA-Z.,:;/#-_ ]{1,25}$";

        public const string RegistryIdentifier = "^[0-9a-zA-Z.,:;/#-_]{1,20}$";

        public const string SecurityCode = "^([0-9a-fA-F]{8}-){4}[0-9a-fA-F]{8}$";

        public const string TaxIdentifier = "^CZ[0-9]{8,10}$";

        public const string UUID = "^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[1-5][0-9a-fA-F]{3}-[89abAB][0-9a-fAF]{3}-[0-9a-fA-F]{12}$";
    }
}
