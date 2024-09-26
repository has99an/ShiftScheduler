using Microsoft.AspNetCore.Mvc;
using ShiftSchedulerAPI.BusinessLogicLayer;
using ShiftSchedulerAPI.DTO;


namespace ShiftSchedulerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftsController : ControllerBase
    {
        private readonly IShiftLogic _shiftLogic;

        public ShiftsController(IShiftLogic shiftLogic)
        {
            _shiftLogic = shiftLogic;
        }

        [HttpGet]
        public async Task<ActionResult<List<ShiftDTO>>> Get()
        {
            var shifts = await _shiftLogic.GetAllShifts();
            return Ok(shifts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShiftDTO>> Get(int id)
        {
            var shift = await _shiftLogic.GetShiftById(id);
            if (shift == null)
            {
                return NotFound();
            }
            return Ok(shift);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] ShiftDTO shiftDto)
        {
            int newShiftId = await _shiftLogic.AddShift(shiftDto);
            return CreatedAtAction(nameof(Get), new { id = newShiftId }, newShiftId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShift(int id, [FromBody] ShiftDTO shiftDto)
        {
            if (id != shiftDto.ShiftID)
            {
                return BadRequest();
            }

            await _shiftLogic.UpdateShift(shiftDto);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _shiftLogic.RemoveShift(id);
            return NoContent();
        }
    }
}
