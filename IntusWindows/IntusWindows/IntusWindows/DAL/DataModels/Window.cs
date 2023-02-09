using IntusWindows.DAL.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntusWindows.DAL.DataModels
{
    public class Window
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int QualityOfWindows { get; set; }
        public int TotalSubElements { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
