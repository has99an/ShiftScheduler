using System.Collections.Generic;
using System.Threading.Tasks;
using ShiftSchedulerWebApp.Models;

namespace ShiftSchedulerWebApp.ServiceLayer
{
    public interface IShiftAccess
    {
        Task<List<Shift>?> GetShifts();
        Task<Shift?> GetShiftById(int shiftId);
        Task<bool> CreateShift(Shift shift);
        Task<bool> UpdateShift(Shift shift);
        Task<bool> DeleteShift(int shiftId);
    }
}
