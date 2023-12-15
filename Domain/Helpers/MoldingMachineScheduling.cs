﻿using InjectionMachineModule.Domain.Models;

namespace InjectionMachineModule.Domain.Helpers;

public static class MoldingMachineScheduling
{
    public static MoldingMachineSchedulingResult ScheduleWorkOrders(List<WorkOrder> workOrders, List<MoldingMachine> moldingMachines)
    {
        var stationSchedules = moldingMachines
            .Select(x => new MoldingMachineSchedule(x))
            .ToList();

        var moldSchedules = moldingMachines
            .SelectMany(x => x.PossibleMolds)
            .Distinct()
            .Select(x => new MoldSchedule(x))
            .ToList();

        foreach (var workOrder in workOrders)
        {
            MoldingMachineSchedule? bestMachineSchedule = null;
            MoldSchedule? bestMoldSchedule = null;
            DateTime bestStartTime;
            bestStartTime = DateTime.MaxValue;

            foreach (var machine in workOrder.AvailableMachines)
            {
                var machineSchedule = stationSchedules.First(x => x.MoldingMachine == machine);

                foreach (var mold in workOrder.AvailableMolds.Where(x => machine.PossibleMolds.Contains(x)))
                {
                    var moldSchedule = moldSchedules.First(x => x.Mold == mold);

                    if (moldSchedule is null || machineSchedule is null)
                    {
                        throw new InvalidOperationException("Mold or machine not available");
                    }

                    var moldBestStartTime = GetEarliestAvailableStartTime(workOrder, machineSchedule, moldSchedule);

                    if (moldBestStartTime < bestStartTime)
                    {
                        bestStartTime = moldBestStartTime;
                        bestMachineSchedule = machineSchedule;
                        bestMoldSchedule = moldSchedule;
                    }
                }
            }

            workOrder.Mold = bestMoldSchedule!.Mold;
            workOrder.StartTime = bestStartTime;
            workOrder.EndTime = workOrder.StartTime.Value.Add(workOrder.Duration!.Value);
            workOrder.MoldingMachine = bestMachineSchedule!.MoldingMachine;

            bestMachineSchedule!.WorkOrders.Add(workOrder);
            bestMoldSchedule!.WorkOrders.Add(workOrder);
        }

        return new MoldingMachineSchedulingResult(workOrders);
    }

    public static DateTime GetEarliestAvailableStartTime(WorkOrder workOrder, MoldingMachineSchedule stationSchedule, MoldSchedule moldSchedule)
    {
        var moldWorkOrders = moldSchedule.WorkOrders;
        var machineWorkOrders = stationSchedule.WorkOrders;

        var possibleStartTimes = new List<DateTime>
        {
            workOrder.AvailableTime
        };
        possibleStartTimes.AddRange(machineWorkOrders.Select(x => x.EndTime!.Value));
        possibleStartTimes.AddRange(moldWorkOrders.Select(x => x.EndTime!.Value));
        possibleStartTimes = possibleStartTimes.OrderBy(x => x).ToList();

        foreach (var startTime in possibleStartTimes)
        {
            var duration = moldSchedule.Mold.Cycle * workOrder.Quantity;
            var endTime = startTime.Add(duration);
            if (!machineWorkOrders.Exists(x => WorkOrder.CheckConflict(startTime, endTime, x.StartTime!.Value, x.EndTime!.Value)) &&
                !moldWorkOrders.Exists(x => WorkOrder.CheckConflict(startTime, endTime, x.StartTime!.Value, x.EndTime!.Value)))
            {
                return startTime;
            }
        }

        return possibleStartTimes.Max();
    }
}