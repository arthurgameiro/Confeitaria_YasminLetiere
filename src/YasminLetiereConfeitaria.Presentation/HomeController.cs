using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YasminLetiereConfeitaria.Domain.Interfaces;
using YasminLetiereConfeitaria.Presentation.Models;

namespace YasminLetiereConfeitaria.Presentation
{
    public class HomeController(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        ISazonalidadeRepository sazonalidadeRepository,
        IFeiraGastronomicaRepository feiraGastronomicaRepository) : Controller
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly ISazonalidadeRepository _sazonalidadeRepository = sazonalidadeRepository;
        private readonly IFeiraGastronomicaRepository _feiraGastronomicaRepository = feiraGastronomicaRepository;

        public async Task<IActionResult> Index()
        {
            var feiras = await _feiraGastronomicaRepository.GetProximasAtivasAsync();
            ViewBag.Feiras = feiras;
            return View();
        }

        public async Task<IActionResult> Cardapio()
        {
            var products = await _productRepository.GetFixedMenuAsync();
            var categories = await _categoryRepository.GetOrderedAsync();

            ViewBag.Categories = categories;
            return View(products);
        }

        public async Task<IActionResult> Sazonal(string? season = null)
        {
            var seasonsList = (await _sazonalidadeRepository.GetAllAsync())
                .OrderBy(s => s.DataInicio)
                .ToList();

            if (string.IsNullOrWhiteSpace(season))
            {
                var now = DateTime.UtcNow;
                var activeNow = seasonsList.FirstOrDefault(s => s.Ativo && now >= s.DataInicio && now <= s.DataFim);
                season = activeNow?.Nome ?? seasonsList.FirstOrDefault()?.Nome ?? "Páscoa";
            }

            var products = await _productRepository.GetBySeasonalTagAsync(season);

            // Valida se a sazonalidade está ativa ou expirou
            var sazonalidade = seasonsList.FirstOrDefault(s => string.Equals(s.Nome, season, StringComparison.OrdinalIgnoreCase));
            bool isExpired = false;
            string expiredMessage = "As encomendas para este período estão encerradas no momento.";

            if (sazonalidade != null)
            {
                var now = DateTime.UtcNow;
                if (!sazonalidade.Ativo || now < sazonalidade.DataInicio || now > sazonalidade.DataFim)
                {
                    isExpired = true;
                    expiredMessage = sazonalidade.MensagemExpirada;
                }
            }
            else
            {
                isExpired = true;
            }

            ViewBag.ActiveSeason = season;
            ViewBag.IsExpired = isExpired;
            ViewBag.ExpiredMessage = expiredMessage;
            ViewBag.Seasons = seasonsList;

            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
