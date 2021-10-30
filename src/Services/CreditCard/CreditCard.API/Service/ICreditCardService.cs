using CreditCard.API.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditCard.API.Service
{
    public interface ICreditCardService
    {
        Task Add(CreditCardViewModel creditCardViewModel);
        Task<Model.CreditCard> GetById(Guid id);
        Task<IEnumerable<Model.CreditCard>> Get();
        Task Update(Guid id, CreditCardViewModel creditCardViewModel);
        Task Delete(Guid id);
    }
}
