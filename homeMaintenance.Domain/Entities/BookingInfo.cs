using System.Numerics;

namespace homeMaintenance.Domain.Entities
{
    public class BookingInfo
    {
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public BigInteger Amount { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

    }
}
