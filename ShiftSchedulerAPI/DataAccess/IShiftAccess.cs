using ShiftSchedulerAPI.Models;
using System.Collections.Generic;

namespace ShiftSchedulerAPI.DataAccess
{
    public interface IShiftAccess
    {
        Shift GetShiftById(int id);
        List<Shift> GetAllShifts();
        List<Shift> GetFixedShifts();
        List<Shift> GetOpenShifts();
        List<Shift> GetShiftsByEmployeeId(int employeeId);
        int AddShift(Shift shift);
        void UpdateShift(Shift shift);
        void DeleteShift(int id);
    }
}
