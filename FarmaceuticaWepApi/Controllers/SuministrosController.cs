using FarmaceuticaBack.dominio;
using FarmaceuticaBack.negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaceuticaWepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuministrosController : ControllerBase
    {
        private IAplicacion aplicacion;
        public SuministrosController()
        {
            aplicacion = new Aplicacion();
        }
        [HttpPost("InsertarSuministro")]
        public bool InsertarSuministro(Suministro suministro)
        {
            return aplicacion.Insert(suministro);
        }
        [HttpPut("ActualizarSuministro")]
        public bool ActualizarSuministro(Suministro suministro)
        {
            return aplicacion.Update(suministro);
        }
        //[HttpPut("{suministro:Suministro}")]
        //public IActionResult UpdateSuministro(Suministro suministro)
        //{
        //    if (aplicacion.Update(suministro))
        //    {
        //        return Ok(true);
        //    }
        //    else
        //    {
        //        return Ok(false);
        //    }
        //}
        [HttpDelete("{id:int}")]
        public IActionResult DeleteSuministro(int id)
        {
            if (aplicacion.Delete(id))
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
        [HttpGet("TiposSuministros")]
        public IActionResult GetTiposSuministros()
        {
            return Ok(aplicacion.TipoSuministros());
        }
        [HttpGet("suministros")]
        public IActionResult GetSuministros()
        {
            return Ok(aplicacion.Suministros());
        }
    }
}
