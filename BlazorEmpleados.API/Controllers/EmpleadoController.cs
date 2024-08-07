﻿using BlazorEmpleados.BLL.Interface;
using BlazorEmpleados.DTOs.Empleado;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEmpleados.API.Controllers
{
   [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;
        private readonly ILogger<EmpleadoController> _logger;
        public EmpleadoController(IEmpleadoService empleadoService, ILogger<EmpleadoController> logger)
        {
            _empleadoService = empleadoService;
            _logger = logger;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<EmpleadoResponseDTO>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Se invoca al Endpoint GetAll");
                var result = await _empleadoService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, "Error al obtener datos en GetAll");
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Create")]
        public async Task<ActionResult<EmpleadoResponseDTO>> Create(EmpleadoCreateRequestDTO empleado)
        {
            try
            {
                _logger.LogInformation("Se invoca al Endpoint Create");
                var result = await _empleadoService.Create(empleado);
                if (result == null)
                    return BadRequest();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, "Error al crear nuevo empleado");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetByNombre/{nombre}")]
        public async Task<ActionResult<List<EmpleadoResponseDTO>>> GetByNombre(string nombre)
        {
            try
            {
                _logger.LogInformation("Se invoca al Endpoint GetByNombre");
                var result = await _empleadoService.GetByNombre(nombre);
                if (result.Count == 0 || result == null)
                {
                    return NotFound("No hay empleados con ese nombre");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, "Error al obtener empleado por nombre");
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetByNroDocumento/{nroDocumento}")]
        public async Task<ActionResult<EmpleadoResponseDTO>> GetByNroDocumento(string nroDocumento)
        {
            try
            {
                _logger.LogInformation("Se invoca al Endpoint GetByNroDocumento");
                var result = await _empleadoService.GetByNroDocumento(nroDocumento);
                if (result == null)
                {
                    return NotFound("No hay empleados con ese nombre");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, "Error al obtener empleado por nro documento");
                return NotFound(ex.Message);
            }
        }
        [HttpPut("Update/{nroDocumento}")]
        public async Task<ActionResult<EmpleadoResponseDTO>> Update(string nroDocumento, [FromBody] EmpleadoUpdateRequestDTO empleado)
        {
            try
            {
                _logger.LogInformation("Se invoca al Endpoint Update");
                var result = await _empleadoService.Update(empleado, nroDocumento);
                if (result == null)
                    return NotFound("No se encontró el empleado");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, "Error al editar empleado");
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("Delete/{nroDocumento}")]
        public async Task<ActionResult<bool>> Delete(string nroDocumento)
        {
            try
            {
                _logger.LogInformation("Se invoca al Endpoint Delete");
                var result = await _empleadoService.Delete(nroDocumento);
                if (result == false)
                    return NotFound(false);
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, "Error al eliminar empleado");
                return NotFound(ex.Message);
            }
        }
    }
}
