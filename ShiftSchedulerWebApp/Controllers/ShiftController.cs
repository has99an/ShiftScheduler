using Microsoft.AspNetCore.Mvc;
using ShiftSchedulerWebApp.BusinessLayer;
using ShiftSchedulerWebApp.Models;

namespace ShiftSchedulerWebApp.Controllers
{
    public class ShiftController : Controller
    {
        private readonly IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        public async Task<IActionResult> Index()
        {
            var shifts = await _shiftService.GetShifts();
            return View(shifts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var shift = await _shiftService.GetShiftById(id);
            if (shift == null) return NotFound();
            return View(shift);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Shift shift)
        {
            if (ModelState.IsValid)
            {
                await _shiftService.CreateShift(shift);
                return RedirectToAction(nameof(Index));
            }
            return View(shift);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var shift = await _shiftService.GetShiftById(id);
            if (shift == null) return NotFound();
            return View(shift);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Shift shift)
        {
            if (ModelState.IsValid)
            {
                await _shiftService.UpdateShift(shift);
                return RedirectToAction(nameof(Index));
            }
            return View(shift);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var shift = await _shiftService.GetShiftById(id);
            if (shift == null) return NotFound();

            await _shiftService.DeleteShift(id);
            return NoContent(); 
        }


    }
}
