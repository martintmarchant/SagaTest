using System;

namespace Saga.Common.Model.Transaction
{
    // note: The models are generally kept outside of the main solution, to maximize reuse - (maybe used by the calling app?).
    //       i would normally consider having sperate assemblies for diffent key areas e.g Models.Transactions
    public class TransactionDetail
    {
        public Guid TransactionNumber { get; set; }
        public int? NumberOfPassengers { get; set; }
        public string Domain { get; set; }
        public string AgentId { get; set; } /* martinm 11/11/2018 - todo: check datatype, maybe Guid, Int? etc... */
        public string ReferrerUrl { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string UserId { get; set; }
        public string SelectedCurrency { get; set; }
        public string ReservationSystem { get; set; }
    }
}
