using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShiftSchedulerWebApp.BusinessLayer;
using ShiftSchedulerWebApp.Models;
using ShiftSchedulerWebApp.ViewModels;

namespace ShiftSchedulerWebApp.Controllers
{
    public class ShiftController : Controller
    {
        private readonly IShiftService _shiftService;
        private readonly IEmployeeService _employeeService;

        public ShiftController(IShiftService shiftService, IEmployeeService employeeService)
        {
            _shiftService = shiftService;
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var shifts = await _shiftService.GetShifts();

            return View(shifts);
        }

        [HttpGet]
        public async Task<IActionResult> GetShiftsForCalendar()
        {
            var shifts = await _shiftService.GetShifts();
            var events = shifts.Select(shift => new
            {
                title = $"{shift.EmployeeFullName} - {shift.StartTime:hh\\:mm} to {shift.EndTime:hh\\:mm}",
                start = shift.Date.ToString("yyyy-MM-dd") + "T" + shift.StartTime.ToString(@"hh\:mm"),
                end = shift.Date.ToString("yyyy-MM-dd") + "T" + shift.EndTime.ToString(@"hh\:mm"),
                type = shift.EmployeeID.HasValue ? "Fixed" : "Open" 
            });

            return Json(events);
        }


        public async Task<IActionResult> Details(int id)
        {
            var shift = await _shiftService.GetShiftById(id);
            if (shift == null) return NotFound();
            return View(shift);
        }

        public async Task<IActionResult> Create()
        {
            var employees = await _employeeService.GetEmployees();

            var employeeSelectList = employees.Select(e => new
            {
                EmployeeID = e.EmployeeID,
                FullName = $"{e.FirstName} {e.LastName}" 
            });

            ViewBag.Employees = new SelectList(employeeSelectList, "EmployeeID", "FullName");

            return View(new Shift());
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

            var employees = await _employeeService.GetEmployees();
            if (employees == null) return NotFound();

            var employeeSelectList = employees.Select(e => new
            {
                EmployeeID = e.EmployeeID,
                FullName = $"{e.FirstName} {e.LastName}"
            });

            ViewBag.Employees = new SelectList(employeeSelectList, "EmployeeID", "FullName", shift.EmployeeID);

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

            ViewBag.Employees = new SelectList(await _employeeService.GetEmployees(), "EmployeeID", "FirstName");
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
