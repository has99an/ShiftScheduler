using ShiftSchedulerAPI.DataAccess;
using ShiftSchedulerAPI.DTO;
using ShiftSchedulerAPI.Models;
using ShiftSchedulerAPI.Converters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShiftSchedulerAPI.BusinessLogicLayer
{
    public class ShiftLogic : IShiftLogic
    {
        private readonly IShiftAccess _shiftAccess;
        private readonly IEmployeeLogic _employeeLogic;

        public ShiftLogic(IShiftAccess shiftAccess, IEmployeeLogic employeeLogic)
        {
            _shiftAccess = shiftAccess;
            _employeeLogic = employeeLogic;
        }

        public async Task<List<ShiftDTO>> GetAllShifts()
        {
            try
            {
                List<Shift> shifts = await Task.Run(() => _shiftAccess.GetAllShifts());
                var shiftDtos = new List<ShiftDTO>();

                foreach (var shift in shifts)
                {
                    string employeeFullName = shift.EmployeeID.HasValue
                        ? await _employeeLogic.GetEmployeeFullNameById(shift.EmployeeID.Value)
                        : "Open";

                    shiftDtos.Add(ShiftConverter.ToDTO(shift, employeeFullName));
                }

                return shiftDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all shifts: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ShiftDTO>> GetFixedShifts()
        {
            try
            {
                List<Shift> shifts = await Task.Run(() => _shiftAccess.GetFixedShifts());
                var shiftDtos = new List<ShiftDTO>();

                foreach (var shift in shifts)
                {
                    string employeeFullName = shift.EmployeeID.HasValue
                        ? await _employeeLogic.GetEmployeeFullNameById(shift.EmployeeID.Value)
                        : "Open";

                    shiftDtos.Add(ShiftConverter.ToDTO(shift, employeeFullName));
                }

                return shiftDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting fixed shifts: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ShiftDTO>> GetOpenShifts()
        {
            try
            {
                List<Shift> shifts = await Task.Run(() => _shiftAccess.GetOpenShifts());
                var shiftDtos = new List<ShiftDTO>();

                foreach (var shift in shifts)
                {
                    shiftDtos.Add(ShiftConverter.ToDTO(shift, "Open"));
                }

                return shiftDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting open shifts: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ShiftDTO>> GetShiftsByEmployeeId(int employeeId)
        {
            try
            {
                List<Shift> shifts = await Task.Run(() => _shiftAccess.GetShiftsByEmployeeId(employeeId));
                var shiftDtos = new List<ShiftDTO>();

                foreach (var shift in shifts)
                {
                    string employeeFullName = shift.EmployeeID.HasValue
                        ? await _employeeLogic.GetEmployeeFullNameById(shift.EmployeeID.Value)
                        : "Open";

                    shiftDtos.Add(ShiftConverter.ToDTO(shift, employeeFullName));
                }

                return shiftDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting shifts by employee ID: {ex.Message}");
                throw;
            }
        }

        public async Task<ShiftDTO> GetShiftById(int shiftId)
        {
            try
            {
                Shift shift = await Task.Run(() => _shiftAccess.GetShiftById(shiftId));
                string employeeFullName = shift.EmployeeID.HasValue
                    ? await _employeeLogic.GetEmployeeFullNameById(shift.EmployeeID.Value)
                    : "Open";

                return ShiftConverter.ToDTO(shift, employeeFullName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting shift by ID: {ex.Message}");
                throw;
            }
        }

        public async Task<int> AddShift(ShiftDTO newShift)
        {
            try
            {
                Shift shift = ShiftConverter.ToModel(newShift);
                int newShiftID = await Task.Run(() => _shiftAccess.AddShift(shift));
                return newShiftID;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding shift: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateShift(ShiftDTO updatedShift)
        {
            try
            {
                Shift shift = ShiftConverter.ToModel(updatedShift);
                await Task.Run(() => _shiftAccess.UpdateShift(shift));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating shift: {ex.Message}");
                throw;
            }
        }

        public async Task RemoveShift(int shiftId)
        {
            try
            {
                await Task.Run(() => _shiftAccess.DeleteShift(shiftId));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing shift: {ex.Message}");
                throw;
            }
        }
    }
}
