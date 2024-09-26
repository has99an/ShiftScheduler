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

        public ShiftLogic(IShiftAccess shiftAccess)
        {
            _shiftAccess = shiftAccess;
        }

        public async Task<List<ShiftDTO>> GetAllShifts()
        {
            try
            {
                List<Shift> shifts = await Task.Run(() => _shiftAccess.GetAllShifts());
                return ShiftConverter.ToDTOCollection(shifts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all shifts: {ex.Message}");
                throw;
            }
        }

        public async Task<ShiftDTO> GetShiftById(int shiftId)
        {
            try
            {
                Shift shift = await Task.Run(() => _shiftAccess.GetShiftById(shiftId));
                return ShiftConverter.ToDTO(shift);
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