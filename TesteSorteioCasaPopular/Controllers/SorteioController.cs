using Domain.Aplication;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TesteSorteioCasaPopular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SorteioController : ControllerBase
    {
        private IFamiliaService _familiaService;
        public SorteioController(IFamiliaService familiaService)
        {
            _familiaService = familiaService;
        }

        [HttpGet]
        public IActionResult SortearFamilia()
        {
            try
            {
                var sorteio = _familiaService.SortearFamilia();
                return Ok(sorteio);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
