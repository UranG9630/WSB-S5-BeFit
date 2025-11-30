using BeFit.Models;
using System.ComponentModel.DataAnnotations;

namespace BeFit.DTOs
{
    public class SessionDTO
    {
        public int Id { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime Start { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        [IsAfter("Start")]
        public DateTime End { get; set; }

        public SessionDTO() { }
        public SessionDTO(Session session) 
        {
            Id = session.Id;
            Start = session.Start;
            End = session.End;
        }
    }
}
