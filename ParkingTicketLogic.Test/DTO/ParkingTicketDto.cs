using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingTicketLogic.Test.DTO
{
    public class ParkingTicketDto
    {
        /// <summary>
        /// Unique Identifier
        /// </summary>
        public Guid TicketID { get; set; }

        /// <summary>
        /// State where the ticket occured
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// What did they do?
        /// </summary>
        public string Offense { get; set; }

        /// <summary>
        /// When did this ticket happen?
        /// </summary>
        public DateTime DateOfOffense { get; set; }
        /// <summary>
        /// How much is this ticket for?
        /// </summary>
        public int Fine { get; set; }
    }
}
