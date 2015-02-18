using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices
{
    /*     Interface to implement basic work with device     */

    public interface IExperimentalDevice
    {
        /// <summary>
        /// Initializing the experimental device
        /// </summary>
        /// <returns>True, if initialization succeed, false otherwise</returns>
        bool InitDevice();

        /// <summary>
        /// Sends command request to the device
        /// </summary>
        /// <param name="RequestString"></param>
        /// <returns>True, if the request writting succeed, false otherwise</returns>
        bool SendCommandRequest(string RequestString);

        /// <summary>
        /// Recieves the device answer
        /// </summary>
        /// <returns>Device answer</returns>
        string ReceiveDeviceAnswer();

        /// <summary>
        /// Sends query request to the device and receives answer
        /// </summary>
        /// <param name="Query">Query request</param>
        /// <returns>Device answer</returns>
        string RequestQuery(string Query);
    }
}
