using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiver.Api.Crud.Models.Reviews
{
    public class ReviewInputModel
    {
        public int Id { get; set; }
        public string Reviewer { get; set; }
        public string Comments { get; set; }
        public int MovieId { get; set; }
    }
}
