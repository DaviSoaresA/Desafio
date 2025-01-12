using Microsoft.AspNetCore.Mvc;
using SGFP.Application.DTOs;
using SGFP.Domain.Services;

namespace SGFP.Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinanceController : ControllerBase
    {
        private readonly FinanceService _financeService;

        public FinanceController(FinanceService financeService)
        {
            _financeService = financeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _financeService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _financeService.GetByIdAsync(id));
            }
            catch (HttpRequestException nfe)
            {
                return NotFound(nfe.Message);
            } 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]FinanceDTO financeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _financeService.AddAsync(financeDTO);
                return Created();
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] FinanceDTO financeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _financeService.UpdateAsync(id, financeDTO);
                return Ok();
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (HttpRequestException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _financeService.DeleteAsync(id);
                return NoContent();
            }
            catch (HttpRequestException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
