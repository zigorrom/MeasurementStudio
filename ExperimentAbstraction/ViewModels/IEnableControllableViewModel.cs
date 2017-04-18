using System;
namespace ExperimentAbstraction
{
    public interface IEnableControllableViewModel
    {
        bool GlobalIsEnabled { get; set; }
    }
}
