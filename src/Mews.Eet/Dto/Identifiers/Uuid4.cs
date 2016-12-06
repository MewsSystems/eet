using System;

namespace Mews.Eet.Dto.Identifiers
{
    /// <summary>
    /// The UUID used by EET should comply with UUID v4.0.
    /// </summary>
    public sealed class Uuid4 : StringIdentifier
    {
        public Uuid4(Guid guid)
            : this(guid.ToString())
        {
        }

        public Uuid4(string value)
            : base(value, Patterns.Uuid4)
        {
        }
    }
}
