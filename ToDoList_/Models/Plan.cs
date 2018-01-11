using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoList_.Models
{
    public class Plan
    {
        public int PlanId { get; set; }

        [Required(ErrorMessage = "Please enter some description.")]
        public string Description { get; set; }

        [DisplayName("Status")]
        public bool Done { get; set; }

        public IdentityUser IdentityUser { get; set; }
    }
}