using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class FeedbackModel
    {
        [Key]
        public long FeedbackId { get; set; }
        public long UserId { get; set; }
        public long BookId { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
    }
}
