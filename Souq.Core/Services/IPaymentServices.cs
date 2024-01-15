using Souq.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.Core.Services
{
    public interface IPaymentServices
    {

        //Signature For one Method



        Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId);


    }
}
