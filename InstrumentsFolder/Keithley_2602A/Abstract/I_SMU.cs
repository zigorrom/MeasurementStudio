using System;
using System.Collections.Generic;
using System.Text;

namespace SMU
{
    public enum SourceMode { Voltage, Current }
	/// <summary>
	/// Basic SMU interface
	/// </summary>
	public interface I_SMU
	{
        /// <summary>
        /// Initializes the device
        /// </summary>
        /// <returns>true, if the device was initialized and false otherwise</returns>
        bool InitDevice();
        /// <summary>
        /// Switches the device ON
        /// </summary>
        /// <returns></returns>
        void SwitchON();
        /// <summary>
        /// Switches the device OFF
        /// </summary>
        /// <returns></returns>
        void SwitchOFF();
        /// <summary>
        /// Sets appropriate voltage value limit
        /// </summary>
        /// <param name="Value">Value to be set to the device</param>
        /// <returns>true, if the value was set to the device and false otherwise</returns>
        bool SetVoltageLimit(double Value);
        /// <summary>
        /// Sets appropriate current value limit
        /// </summary>
        /// <param name="Value">Value to be set to the device</param>
        /// <returns>true, if the value was set to the device and false otherwise</returns>
        bool SetCurrentLimit(double Value);
		/// <summary>
		/// Sets appropriate voltage value
		/// </summary>
		/// <param name="Value">Value to be set to the device</param>
		/// <returns>true, if the value was set to the device and false otherwise</returns>
		bool SetSourceVoltage(double Value);
		/// <summary>
		/// Sets appropriate current value
		/// </summary>
		/// <param name="Value">Value to be set to the device</param>
		/// <returns>true, if the value was set to the device and false otherwise</returns>
		bool SetSourceCurrent(double Value);
		/// <summary>
		/// Measures voltage value
		/// </summary>
		/// <returns>Measured voltage value</returns>
		double MeasureVoltage(int NumberOfAverages, double TimeDelay);
		/// <summary>
		/// Measures current value
		/// </summary>
		/// <returns>Measured current value</returns>
        double MeasureCurrent(int NumberOfAverages, double TimeDelay);
        /// <summary>
        /// Measures resistance
        /// </summary>
        /// <param name="NumberOfAverages"></param>
        /// <param name="TimeDelay"></param>
        /// <returns></returns>
        double MeasureResistance(double valueThroughTheStrusture, int NumberOfAverages, double TimeDelay, SourceMode sourceMode);
        /// <summary>
        /// Measures power
        /// </summary>
        /// <param name="NumberOfAverages"></param>
        /// <param name="TimeDelay"></param>
        /// <returns></returns>
        double MeasurePower(double valueThroughTheStrusture, int NumberOfAverages, double TimeDelay, SourceMode sourceMode);
	}
}