using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YasminLetiereConfeitaria.Domain.Entities;
using YasminLetiereConfeitaria.Domain.Interfaces;

namespace YasminLetiereConfeitaria.Presentation
{
    [Authorize]
    public partial class AdminController(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        ISazonalidadeRepository sazonalidadeRepository,
        IFeiraGastronomicaRepository feiraGastronomicaRepository,
        IRedeSocialRepository redeSocialRepository,
        IConfiguracaoSistemaRepository configuracaoRepository,
        IWebHostEnvironment webHostEnvironment) : Controller
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly ISazonalidadeRepository _sazonalidadeRepository = sazonalidadeRepository;
        private readonly IFeiraGastronomicaRepository _feiraGastronomicaRepository = feiraGastronomicaRepository;
        private readonly IRedeSocialRepository _redeSocialRepository = redeSocialRepository;
        private readonly IConfiguracaoSistemaRepository _configuracaoRepository = configuracaoRepository;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            ViewBag.Categories = await _categoryRepository.GetOrderedAsync();
            ViewBag.Seasons = (await _sazonalidadeRepository.GetAllAsync())
                .OrderBy(s => s.DataInicio)
                .ToList();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryRepository.GetOrderedAsync();
            ViewBag.Seasons = (await _sazonalidadeRepository.GetAllAsync())
                .OrderBy(s => s.DataInicio)
                .ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, string description, decimal price, string imageUrl, Guid categoryId, string seasonalTag, IFormFile? imageFile)
        {
            if (string.IsNullOrWhiteSpace(name) || price < 0 || categoryId == Guid.Empty)
            {
                ModelState.AddModelError("", "Por favor, preencha os dados obrigatórios corretamente.");
                ViewBag.Categories = await _categoryRepository.GetOrderedAsync();
                ViewBag.Seasons = (await _sazonalidadeRepository.GetAllAsync())
                    .OrderBy(s => s.DataInicio)
                    .ToList();
                return View();
            }

            if (imageFile?.Length > 0)
            {
                var formattedName = FormatFileName(name, imageFile.FileName);
                var savePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", formattedName);
                await using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                imageUrl = "/images/" + formattedName;
            }

            if (string.IsNullOrWhiteSpace(imageUrl) || imageUrl == "/images/placeholder.jpg")
            {
                imageUrl = "/images/sem_foto.jpg";
            }

            var product = new Product(name, description, price, imageUrl, categoryId, string.IsNullOrWhiteSpace(seasonalTag) ? null : seasonalTag);
            await _productRepository.AddAsync(product);

            TempData["AdminMessage"] = "Produto cadastrado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _categoryRepository.GetOrderedAsync();
            ViewBag.Seasons = (await _sazonalidadeRepository.GetAllAsync())
                .OrderBy(s => s.DataInicio)
                .ToList();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, string name, string description, decimal price, string imageUrl, Guid categoryId, string seasonalTag, bool isAvailable, IFormFile? imageFile)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(name) || price < 0 || categoryId == Guid.Empty)
            {
                ModelState.AddModelError("", "Por favor, preencha os dados obrigatórios corretamente.");
                ViewBag.Categories = await _categoryRepository.GetOrderedAsync();
                ViewBag.Seasons = (await _sazonalidadeRepository.GetAllAsync())
                    .OrderBy(s => s.DataInicio)
                    .ToList();
                return View(product);
            }

            if (imageFile?.Length > 0)
            {
                var formattedName = FormatFileName(name, imageFile.FileName);
                var savePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", formattedName);
                await using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                imageUrl = "/images/" + formattedName;
            }

            if (string.IsNullOrWhiteSpace(imageUrl) || imageUrl == "/images/placeholder.jpg")
            {
                imageUrl = "/images/sem_foto.jpg";
            }

            product.Update(name, description, price, imageUrl, categoryId, string.IsNullOrWhiteSpace(seasonalTag) ? null : seasonalTag);
            product.SetAvailability(isAvailable);

            await _productRepository.UpdateAsync(product);

            TempData["AdminMessage"] = "Produto atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleAvailability(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            bool wasAvailable = product.IsAvailable;
            product.SetAvailability(!product.IsAvailable);
            await _productRepository.UpdateAsync(product);

            TempData["AdminMessage"] = wasAvailable
                ? "Produto inativado com sucesso!"
                : "Produto ativado com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteAsync(id);
            TempData["AdminMessage"] = "Produto removido com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Sazonalidades()
        {
            var sazonalidades = await _sazonalidadeRepository.GetAllAsync();
            return View(sazonalidades.OrderBy(s => s.DataInicio));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSazonalidade(Guid id, DateTime dataInicio, DateTime dataFim, string mensagemExpirada, string? icone)
        {
            var saz = await _sazonalidadeRepository.GetByIdAsync(id);
            if (saz == null)
            {
                return NotFound();
            }

            saz.AtualizarPeriodo(dataInicio, dataFim, mensagemExpirada, icone);
            await _sazonalidadeRepository.UpdateAsync(saz);

            TempData["AdminMessage"] = $"Sazonalidade '{saz.Nome}' actualizada com sucesso!";
            return RedirectToAction(nameof(Sazonalidades));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleSazonalidadeAtiva(Guid id, bool? ativo = null)
        {
            var saz = await _sazonalidadeRepository.GetByIdAsync(id);
            if (saz == null)
            {
                return NotFound();
            }

            bool novoEstado = ativo ?? !saz.Ativo;
            saz.SetAtivo(novoEstado);
            await _sazonalidadeRepository.UpdateAsync(saz);

            // Se a temporada foi inativada, inativar todos os seus produtos vinculados
            if (!novoEstado)
            {
                var products = await _productRepository.GetBySeasonalTagAsync(saz.Nome);
                foreach (var product in products)
                {
                    if (product.IsAvailable)
                    {
                        product.SetAvailability(false);
                        await _productRepository.UpdateAsync(product);
                    }
                }
            }

            TempData["AdminMessage"] = novoEstado
                ? $"Temporada '{saz.Nome}' ativada com sucesso!"
                : $"Temporada '{saz.Nome}' inativada com sucesso!";

            return RedirectToAction(nameof(Sazonalidades));
        }

        [HttpGet]
        public async Task<IActionResult> EditSazonalidade(Guid id)
        {
            var saz = await _sazonalidadeRepository.GetByIdAsync(id);
            if (saz == null)
            {
                return NotFound();
            }
            return View(saz);
        }

        [HttpGet]
        public IActionResult CreateSazonalidade()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSazonalidade(string nome, DateTime dataInicio, DateTime dataFim, string mensagemExpirada, string? icone)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(mensagemExpirada))
            {
                TempData["AdminError"] = "Preencha todos os campos obrigatórios.";
                return View();
            }

            var saz = new Sazonalidade(
                nome.Trim(),
                dataInicio,
                dataFim,
                mensagemExpirada.Trim(),
                icone?.Trim()
            );

            await _sazonalidadeRepository.AddAsync(saz);

            TempData["AdminMessage"] = $"Temporada '{saz.Nome}' criada com sucesso!";
            return RedirectToAction(nameof(Sazonalidades));
        }

        [GeneratedRegex(@"\s+")]
        private static partial Regex SpaceRegex();

        [GeneratedRegex("[^a-z0-9_]")]
        private static partial Regex CleanNameRegex();

        [HttpPost]
        public async Task<IActionResult> MoveProduct(Guid id, Guid categoryId, string? seasonalTag)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Update(product.Name, product.Description, product.Price, product.ImageUrl, categoryId, seasonalTag);
            await _productRepository.UpdateAsync(product);

            TempData["AdminMessage"] = $"Produto '{product.Name}' movido com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private static string FormatFileName(string productName, string originalFileName)
        {
            var extension = Path.GetExtension(originalFileName).ToLower();
            if (string.IsNullOrEmpty(extension)) extension = ".jpg";

            var cleanName = productName.ToLower();
            // Substitui espaços por underline
            cleanName = SpaceRegex().Replace(cleanName, "_");
            // Remove caracteres que não sejam letras, números ou underline
            cleanName = CleanNameRegex().Replace(cleanName, "");
            return $"{cleanName}{extension}";
        }

        [HttpGet]
        public async Task<IActionResult> Feiras()
        {
            var feiras = await _feiraGastronomicaRepository.GetAllAsync();
            return View(feiras.OrderBy(f => f.DataHora));
        }

        [HttpGet]
        public IActionResult CreateFeira()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeira(string nome, string local, DateTime dataHora, string? descricao)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(local) || dataHora == DateTime.MinValue)
            {
                TempData["AdminError"] = "Por favor, preencha todos os campos obrigatórios.";
                return View();
            }

            var feira = new FeiraGastronomica(
                nome.Trim(),
                local.Trim(),
                dataHora,
                descricao?.Trim()
            );

            await _feiraGastronomicaRepository.AddAsync(feira);

            TempData["AdminMessage"] = $"Feira '{feira.Nome}' cadastrada com sucesso!";
            return RedirectToAction(nameof(Feiras));
        }

        [HttpGet]
        public async Task<IActionResult> EditFeira(Guid id)
        {
            var feira = await _feiraGastronomicaRepository.GetByIdAsync(id);
            if (feira == null)
            {
                return NotFound();
            }
            return View(feira);
        }

        [HttpPost]
        public async Task<IActionResult> EditFeira(Guid id, string nome, string local, DateTime dataHora, string? descricao)
        {
            var feira = await _feiraGastronomicaRepository.GetByIdAsync(id);
            if (feira == null)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(local) || dataHora == DateTime.MinValue)
            {
                TempData["AdminError"] = "Por favor, preencha todos os campos obrigatórios.";
                return View(feira);
            }

            feira.Atualizar(nome.Trim(), local.Trim(), dataHora, descricao?.Trim());
            await _feiraGastronomicaRepository.UpdateAsync(feira);

            TempData["AdminMessage"] = $"Feira '{feira.Nome}' atualizada com sucesso!";
            return RedirectToAction(nameof(Feiras));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleFeiraAtiva(Guid id)
        {
            var feira = await _feiraGastronomicaRepository.GetByIdAsync(id);
            if (feira == null)
            {
                return NotFound();
            }

            feira.SetAtivo(!feira.Ativo);
            await _feiraGastronomicaRepository.UpdateAsync(feira);

            TempData["AdminMessage"] = feira.Ativo
                ? $"Feira '{feira.Nome}' ativada com sucesso!"
                : $"Feira '{feira.Nome}' inativada com sucesso!";

            return RedirectToAction(nameof(Feiras));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFeira(Guid id)
        {
            var feira = await _feiraGastronomicaRepository.GetByIdAsync(id);
            if (feira == null)
            {
                return NotFound();
            }

            await _feiraGastronomicaRepository.DeleteAsync(id);
            TempData["AdminMessage"] = $"Feira '{feira.Nome}' removida com sucesso!";
            return RedirectToAction(nameof(Feiras));
        }

        // ─── CONFIGURAÇÕES ───────────────────────────────────────────────────────

        [HttpGet]
        public async Task<IActionResult> Configuracoes()
        {
            ViewBag.CatalogoSazonalAtivo = await _configuracaoRepository.GetBoolAsync("catalogo_sazonal_ativo", true);
            ViewBag.RedesSociais = (await _redeSocialRepository.GetAllAsync())
                .OrderBy(r => r.Ordem).ThenBy(r => r.Nome).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SalvarCatalogoSazonal(bool ativo)
        {
            await _configuracaoRepository.SetAsync("catalogo_sazonal_ativo", ativo ? "true" : "false");
            TempData["AdminMessage"] = ativo
                ? "Cardápio sazonal ativado no portal."
                : "Cardápio sazonal desativado no portal.";
            return RedirectToAction(nameof(Configuracoes));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRedeSocial(string nome, string url, string icone, int ordem)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(icone))
            {
                TempData["AdminError"] = "Preencha todos os campos obrigatórios da rede social.";
                return RedirectToAction(nameof(Configuracoes));
            }

            var rede = new RedeSocial(nome.Trim(), url.Trim(), icone.Trim(), ordem);
            await _redeSocialRepository.AddAsync(rede);

            TempData["AdminMessage"] = $"Rede social '{rede.Nome}' adicionada com sucesso!";
            return RedirectToAction(nameof(Configuracoes));
        }

        [HttpPost]
        public async Task<IActionResult> EditRedeSocial(Guid id, string nome, string url, string icone, int ordem)
        {
            var rede = await _redeSocialRepository.GetByIdAsync(id);
            if (rede == null) return NotFound();

            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(icone))
            {
                TempData["AdminError"] = "Preencha todos os campos obrigatórios da rede social.";
                return RedirectToAction(nameof(Configuracoes));
            }

            rede.Atualizar(nome.Trim(), url.Trim(), icone.Trim(), ordem);
            await _redeSocialRepository.UpdateAsync(rede);

            TempData["AdminMessage"] = $"Rede social '{rede.Nome}' atualizada com sucesso!";
            return RedirectToAction(nameof(Configuracoes));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleRedeSocial(Guid id)
        {
            var rede = await _redeSocialRepository.GetByIdAsync(id);
            if (rede == null) return NotFound();

            rede.SetAtivo(!rede.Ativo);
            await _redeSocialRepository.UpdateAsync(rede);

            TempData["AdminMessage"] = rede.Ativo
                ? $"'{rede.Nome}' ativada no portal."
                : $"'{rede.Nome}' desativada no portal.";
            return RedirectToAction(nameof(Configuracoes));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRedeSocial(Guid id)
        {
            var rede = await _redeSocialRepository.GetByIdAsync(id);
            if (rede == null) return NotFound();

            await _redeSocialRepository.DeleteAsync(id);
            TempData["AdminMessage"] = $"Rede social '{rede.Nome}' removida com sucesso!";
            return RedirectToAction(nameof(Configuracoes));
        }
    }
}
