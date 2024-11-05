using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly EmpleadoData _empleadoData;
        public EmpleadoController(EmpleadoData empleadoData)
        {
            _empleadoData = empleadoData;
        }

        [HttpPost]
        [Route("GetEmpleados")]
        public async Task<IActionResult> Lista([FromBody] string filtro)
        {
            List<Empleado> Lista = await _empleadoData.GetEmpleados(filtro);
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        [HttpPost]
        [Route("AddEmpleado")]
        public async Task<IActionResult> AddEmpleado([FromBody] Empleado objeto)
        { 

            var respuesta = await _empleadoData.AddEmpleado(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpPost]
        [Route("UpdateEmpleado")]
        public async Task<IActionResult> UpdateEmpleado([FromBody] Empleado objeto)
        {
            var respuesta = await _empleadoData.UpdateEmpleado(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpPost]
        [Route("DeleteEmpleado")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            var respuesta = await _empleadoData.DeleteEmpleado(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}
