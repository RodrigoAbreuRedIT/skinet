using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    public class BasketController : BaseApiController {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        public BasketController(IBasketRepository basketRepository, IMapper mapper) {
            this._basketRepository = basketRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketID(string id){
            var basket = await this._basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket){
            var customerBasket = this._mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var updateBasket = await this._basketRepository.UpdateBasketAsync(customerBasket);

            return Ok(updateBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id){
            await this._basketRepository.DeleteBasketAsync(id);
        }
    }
}