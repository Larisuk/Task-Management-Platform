﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task_Management_Platform.Models
{
    public class Task
    {
        [Required]
        public int TaskId {get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DataFin { get; set; }

        //foreign key
        //un task apartine unei echipe
        //public virtual Team Team { get; set; }

        //un task poate avea unul sau mai multe comentarii
        public virtual ICollection<Comment> Comments { get; set; }
    }
}