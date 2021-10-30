using CreditCard.API.Infrastructure;
using CreditCard.API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCard.API.Service
{
    public class CreditCardService
        : ICreditCardService
    {
        private readonly CreditCardContext _creditCardContext;

        public CreditCardService(CreditCardContext creditCardContext)
        {
            _creditCardContext = creditCardContext;
        }

        public async Task Add(CreditCardViewModel creditCardViewModel)
        {
            Model.CreditCard creditCard = new(
                Guid.NewGuid(),
                creditCardViewModel.Name,
                true,
                creditCardViewModel.Number,
                creditCardViewModel.VerificationCode,
                creditCardViewModel.ExpirationDate,
                creditCardViewModel.Flag);

            await _creditCardContext.CreditCards.AddAsync(creditCard);
            await _creditCardContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Model.CreditCard>> Get()
        {
            return await _creditCardContext.CreditCards
                .ToListAsync();
        }

        public async Task<Model.CreditCard> GetById(Guid id)
        {
            var creditCard = await _creditCardContext.CreditCards.SingleOrDefaultAsync(ci => ci.Id == id);
            return creditCard;
        }

        public async Task Update(Guid id, CreditCardViewModel creditCardViewModel)
        {
            try
            {
                var creditCard = await _creditCardContext.CreditCards.AsNoTracking().SingleOrDefaultAsync(i => i.Id == id);

                if (creditCard == null)
                {
                    new Exception("Creditcard not found");
                }

                Model.CreditCard creditCardUpdate = new(
                id,
                creditCardViewModel.Name,
                creditCardViewModel.Active,
                creditCardViewModel.Number,
                creditCardViewModel.VerificationCode,
                creditCardViewModel.ExpirationDate,
                creditCardViewModel.Flag);

                creditCard = creditCardUpdate;

                _creditCardContext.CreditCards.Update(creditCard);

                await _creditCardContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                var creditCard = _creditCardContext.CreditCards.SingleOrDefault(x => x.Id == id);

                if (creditCard == null)
                {
                    new Exception("creditCard Not Found");
                }

                _creditCardContext.CreditCards.Remove(creditCard);

                await _creditCardContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
