using IT008_UIT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace IT008_UIT.Utils
{
    public class LoadDBcontextHelper
    {
        public static async Task<List<Customer>> LoadAll()
        {
            using (var context = new dbGymContext())
            {
                await Task.Delay(2000);

                List<Customer> list = await context.Customers.ToListAsync();

                return list;
            }
                
            
        }
    }
}
