using System.Collections.Generic;
using System.Linq;
using ToDoList_.Models;

namespace ToDoList_.Repository
{
    public class ToDoListRepository
    {

        public List<Plan> GetAllPlans(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var done = context.Plans.Where(p => p.IdentityUser.Id == userId).ToList();
                return done;
            }
        }
        public List<Plan> GetAllDone(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var done =context.Plans.Where(p => (p.Done == true) && (p.IdentityUser.Id == userId)).ToList();
                return done;
            }
        }
        public List<Plan> GetAllNotDone(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var done = (from all in context.Plans
                            where (all.Done == false) && (all.IdentityUser.Id == userId)
                            select all).ToList();
                return done;
            }
        }

        public List<Plan> SaveAllAfterLogin(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var withNullId = (from all in context.Plans
                            where (all.IdentityUser.Id == null)
                            select all).ToList();
                foreach (Plan item in withNullId)
                {
                    item.IdentityUser.Id = userId;
                }

                return withNullId;
            }
        }
    }
}