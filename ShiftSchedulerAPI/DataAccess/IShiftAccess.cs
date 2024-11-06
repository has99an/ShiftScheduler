using ShiftSchedulerAPI.Models;
using System.Collections.Generic;

namespace ShiftSchedulerAPI.DataAccess
{
    public interface IShiftAccess
    {
        Shift GetShiftById(int id);
        List<Shift> GetAllShifts();
        string GetEmployeeFullNameById(int employeeId);
        List<Shift> GetShiftsByEmployeeId(int employeeId);
        int AddShift(Shift shift);
        void UpdateShift(Shift shift);
        void DeleteShift(int id);
    }
}
