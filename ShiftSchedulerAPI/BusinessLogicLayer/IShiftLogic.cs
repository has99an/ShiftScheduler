using ShiftSchedulerAPI.DTO;


namespace ShiftSchedulerAPI.BusinessLogicLayer
{
    public interface IShiftLogic
    {
        Task<List<ShiftDTO>> GetAllShifts();
        Task<ShiftDTO> GetShiftById(int shiftId);
        Task<int> AddShift(ShiftDTO shift);
        Task UpdateShift(ShiftDTO shift);
        Task RemoveShift(int shiftId);
    }
}