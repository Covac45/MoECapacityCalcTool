using MoECapacityCalc.DomainEntities.Datastructs.CapacityStructs;

namespace MoECapacityCalc.Utilities.AggregatedCapacityCalcServices.MoECapacityCalcServices
{
    public class MoECapacityCalcService
    {
        public MoECapacityCalcService() { }

        public CapacityStruct ConductMoECapacityAssement(CapacityStruct hmoeCapacityStruct, CapacityStruct vmoeCapacityStruct)
        {
            if (hmoeCapacityStruct.Capacity < vmoeCapacityStruct.Capacity)
            {
                return new CapacityStruct()
                {
                    Id = hmoeCapacityStruct.Id,
                    Capacity = hmoeCapacityStruct.Capacity,
                    CapacityNote = hmoeCapacityStruct.CapacityNote
                };
            }
            else
            {
                return new CapacityStruct()
                {
                    Id = vmoeCapacityStruct.Id,
                    Capacity = vmoeCapacityStruct.Capacity,
                    CapacityNote = vmoeCapacityStruct.CapacityNote
                };
            }
        }
    }
}
