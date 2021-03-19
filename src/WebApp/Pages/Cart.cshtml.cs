﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using WebApp.ApiCollection.Interfaces;
using WebApp.Models;

namespace AspnetRunBasics
{
    public class CartModel : PageModel
    {
        private readonly IBasketApi _basketApi;

        public CartModel(IBasketApi basketApi)
        {
            _basketApi = basketApi;
        }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var userName = "swn";
            Cart = await _basketApi.GetBasket(userName);

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            var userName = "swn";
            var basket = await _basketApi.GetBasket(userName);

            var item = basket.Items.Single(s => s.ProductId == productId);
            basket.Items.Remove(item);

            var basketUpdated = await _basketApi.UpdateBasket(basket);

            return RedirectToPage();
        }
    }
}