using System.Collections.Generic;
using System.Threading.Tasks;
using ShiftSchedulerWebApp.Models;
using ShiftSchedulerWebApp.ServiceLayer;

namespace ShiftSchedulerWebApp.BusinessLayer
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftAccess _shiftAccess;

        public ShiftService(IShiftAccess shiftAccess)
        {
            _shiftAccess = shiftAccess;
        }

        public async Task<List<Shift>?> GetShifts()
        {
            return await _shiftAccess.GetShifts();
        }

        public async Task<Shift?> GetShiftById(int shiftId)
        {
            return await _shiftAccess.GetShiftById(shiftId);
        }

        public async Task<bool> CreateShift(Shift shift)
        {
            return await _shiftAccess.CreateShift(shift);
        }

        public async Task<bool> UpdateShift(Shift shift)
        {
            return await _shiftAccess.UpdateShift(shift);
        }

        public async Task<bool> DeleteShift(int shiftId)
        {
            return await _shiftAccess.DeleteShift(shiftId);
        }
    }
}
