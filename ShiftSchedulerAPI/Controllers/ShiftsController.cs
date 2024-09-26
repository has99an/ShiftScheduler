using Microsoft.AspNetCore.Mvc;
using ShiftSchedulerAPI.DataAccess;
using ShiftSchedulerAPI.Models;
using System.Collections.Generic;

namespace ShiftSchedulerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftsController : ControllerBase
    {
        private readonly IShiftAccess _shiftAccess;

        public ShiftsController(IShiftAccess shiftAccess)
        {
            _shiftAccess = shiftAccess;
        }

        [HttpGet]
        public ActionResult<List<Shift>> Get()
        {
            return _shiftAccess.GetAllShifts();
        }

        [HttpGet("{id}")]
        public ActionResult<Shift> Get(int id)
        {
            var shift = _shiftAccess.GetShiftById(id);
            if (shift == null)
            {
                return NotFound();
            }
            return shift;
        }

        [HttpPost]
        public ActionResult<int> Post([FromBody] Shift shift)
        {
            return _shiftAccess.AddShift(shift);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Shift shift)
        {
            _shiftAccess.UpdateShift(shift);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _shiftAccess.DeleteShift(id);
            return NoContent();
        }
    }
}
