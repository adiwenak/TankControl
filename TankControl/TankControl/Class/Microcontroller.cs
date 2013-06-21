using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOXA_CSharp_MXIO;
using System.Diagnostics;

namespace TankControl.Class
{

    public class Microcontroller
    {

        private static Int32[] hConnection = new Int32[1];
        private byte[] byteWriteCoils = new byte[2];
        private static UInt32 Timeout = TankControl.Properties.Settings.Default.MoxTimeout;
        private static UInt16 Port = TankControl.Properties.Settings.Default.MoxPort;
        private static string IPAddr = TankControl.Properties.Settings.Default.MoxAddress;
        private static string Password = "";
        private static Microcontroller singleton;

        public static Microcontroller Singleton
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new Microcontroller();
                }

                return singleton;
            }
        }

        // For error code
        private static void CheckError(int iRet, string szFunctionName)
        {
            string szErrMsg = "MXIO_OK";

            if (iRet != MXIO_CS.MXIO_OK)
            {

                switch (iRet)
                {
                    case MXIO_CS.ILLEGAL_FUNCTION:
                        szErrMsg = "ILLEGAL_FUNCTION";
                        break;
                    case MXIO_CS.ILLEGAL_DATA_ADDRESS:
                        szErrMsg = "ILLEGAL_DATA_ADDRESS";
                        break;
                    case MXIO_CS.ILLEGAL_DATA_VALUE:
                        szErrMsg = "ILLEGAL_DATA_VALUE";
                        break;
                    case MXIO_CS.SLAVE_DEVICE_FAILURE:
                        szErrMsg = "SLAVE_DEVICE_FAILURE";
                        break;
                    case MXIO_CS.SLAVE_DEVICE_BUSY:
                        szErrMsg = "SLAVE_DEVICE_BUSY";
                        break;
                    case MXIO_CS.EIO_TIME_OUT:
                        szErrMsg = "EIO_TIME_OUT";
                        break;
                    case MXIO_CS.EIO_INIT_SOCKETS_FAIL:
                        szErrMsg = "EIO_INIT_SOCKETS_FAIL";
                        break;
                    case MXIO_CS.EIO_CREATING_SOCKET_ERROR:
                        szErrMsg = "EIO_CREATING_SOCKET_ERROR";
                        break;
                    case MXIO_CS.EIO_RESPONSE_BAD:
                        szErrMsg = "EIO_RESPONSE_BAD";
                        break;
                    case MXIO_CS.EIO_SOCKET_DISCONNECT:
                        szErrMsg = "EIO_SOCKET_DISCONNECT";
                        break;
                    case MXIO_CS.PROTOCOL_TYPE_ERROR:
                        szErrMsg = "PROTOCOL_TYPE_ERROR";
                        break;
                    case MXIO_CS.SIO_OPEN_FAIL:
                        szErrMsg = "SIO_OPEN_FAIL";
                        break;
                    case MXIO_CS.SIO_TIME_OUT:
                        szErrMsg = "SIO_TIME_OUT";
                        break;
                    case MXIO_CS.SIO_CLOSE_FAIL:
                        szErrMsg = "SIO_CLOSE_FAIL";
                        break;
                    case MXIO_CS.SIO_PURGE_COMM_FAIL:
                        szErrMsg = "SIO_PURGE_COMM_FAIL";
                        break;
                    case MXIO_CS.SIO_FLUSH_FILE_BUFFERS_FAIL:
                        szErrMsg = "SIO_FLUSH_FILE_BUFFERS_FAIL";
                        break;
                    case MXIO_CS.SIO_GET_COMM_STATE_FAIL:
                        szErrMsg = "SIO_GET_COMM_STATE_FAIL";
                        break;
                    case MXIO_CS.SIO_SET_COMM_STATE_FAIL:
                        szErrMsg = "SIO_SET_COMM_STATE_FAIL";
                        break;
                    case MXIO_CS.SIO_SETUP_COMM_FAIL:
                        szErrMsg = "SIO_SETUP_COMM_FAIL";
                        break;
                    case MXIO_CS.SIO_SET_COMM_TIME_OUT_FAIL:
                        szErrMsg = "SIO_SET_COMM_TIME_OUT_FAIL";
                        break;
                    case MXIO_CS.SIO_CLEAR_COMM_FAIL:
                        szErrMsg = "SIO_CLEAR_COMM_FAIL";
                        break;
                    case MXIO_CS.SIO_RESPONSE_BAD:
                        szErrMsg = "SIO_RESPONSE_BAD";
                        break;
                    case MXIO_CS.SIO_TRANSMISSION_MODE_ERROR:
                        szErrMsg = "SIO_TRANSMISSION_MODE_ERROR";
                        break;
                    case MXIO_CS.PRODUCT_NOT_SUPPORT:
                        szErrMsg = "PRODUCT_NOT_SUPPORT";
                        break;
                    case MXIO_CS.HANDLE_ERROR:
                        szErrMsg = "HANDLE_ERROR";
                        break;
                    case MXIO_CS.SLOT_OUT_OF_RANGE:
                        szErrMsg = "SLOT_OUT_OF_RANGE";
                        break;
                    case MXIO_CS.CHANNEL_OUT_OF_RANGE:
                        szErrMsg = "CHANNEL_OUT_OF_RANGE";
                        break;
                    case MXIO_CS.COIL_TYPE_ERROR:
                        szErrMsg = "COIL_TYPE_ERROR";
                        break;
                    case MXIO_CS.REGISTER_TYPE_ERROR:
                        szErrMsg = "REGISTER_TYPE_ERROR";
                        break;
                    case MXIO_CS.FUNCTION_NOT_SUPPORT:
                        szErrMsg = "FUNCTION_NOT_SUPPORT";
                        break;
                    case MXIO_CS.OUTPUT_VALUE_OUT_OF_RANGE:
                        szErrMsg = "OUTPUT_VALUE_OUT_OF_RANGE";
                        break;
                    case MXIO_CS.INPUT_VALUE_OUT_OF_RANGE:
                        szErrMsg = "INPUT_VALUE_OUT_OF_RANGE";
                        break;
                }

                Debug.WriteLine("Function \"{0}\" execution Fail. Error Message : {1}\n", szFunctionName, szErrMsg);

                if (iRet == MXIO_CS.EIO_TIME_OUT || iRet == MXIO_CS.HANDLE_ERROR)
                {
                    //To terminates use of the socket
                    MXIO_CS.MXEIO_Exit();
                }
            }
        }
        
        // initialize the connection, it will return true if success
        public bool Connect()
        {
            bool success = false;

            int returnValues;

            // Initialize
            returnValues = MXIO_CS.MXEIO_Init();
            Debug.WriteLine("MXIO Initialize Return Code {0}", returnValues);

            if (returnValues == 0)
            {
                // Connect to device
                Debug.WriteLine("MXEIO_E1K_Connect IP={0}, Timeout={1}, Password={2}", IPAddr, Timeout, Password);
                returnValues = MXIO_CS.MXEIO_E1K_Connect(System.Text.Encoding.UTF8.GetBytes(IPAddr), Port, Timeout, hConnection, System.Text.Encoding.UTF8.GetBytes(Password));
                CheckError(returnValues, "MXEIO_E1K_Connect");
                if (returnValues == MXIO_CS.MXIO_OK)
                {
                    Debug.WriteLine("IO logic connected");
                    success = true;
                }
            }

            return success;
        }

        public bool Disconnect()
        {
            int returnValue;
            bool success = false;

            returnValue = MXIO_CS.MXEIO_Disconnect(hConnection[0]);
            CheckError(returnValue, "MXEIO_Disconnect");
            if (returnValue == MXIO_CS.MXIO_OK)
            {
                Console.WriteLine("MXEIO_Disconnect return {0}", returnValue);
                success = true;
            }

            return success;
        }

        // check connection to device
        public bool IsConnected()
        {
            int returnValue;
            bool success = false;
            byte[] bytCheckStatus = new byte[1];
            returnValue = MXIO_CS.MXEIO_CheckConnection(hConnection[0], Timeout, bytCheckStatus);
            CheckError(returnValue, "MXEIO_CheckConnection");
            if (returnValue == MXIO_CS.MXIO_OK)
            {
                switch (bytCheckStatus[0])
                {
                    case MXIO_CS.CHECK_CONNECTION_OK:
                        Debug.WriteLine("MXEIO_CheckConnection: Check connection ok => {0}", bytCheckStatus[0]);
                        success = true;
                        break;
                    case MXIO_CS.CHECK_CONNECTION_FAIL:
                        Debug.WriteLine("MXEIO_CheckConnection: Check connection fail => {0}", bytCheckStatus[0]);
                        break;
                    case MXIO_CS.CHECK_CONNECTION_TIME_OUT:
                        Debug.WriteLine("MXEIO_CheckConnection: Check connection time out => {0}", bytCheckStatus[0]);
                        break;
                    default:
                        Debug.WriteLine("MXEIO_CheckConnection: Check connection status unknown => {0}", bytCheckStatus[0]);
                        break;
                }
            }

            return success;
        }

        public bool OnDigitalOutput(UInt16 location)
        {
            bool success = false;
            if (TankControl.Properties.Settings.Default.SystemTest == 0)
            {
                int ret;
                byteWriteCoils[0] = 0x01;

                ret = MXIO_CS.MXIO_WriteCoils(hConnection[0], location, 1, byteWriteCoils);
                CheckError(ret, "MXIO_WriteCoils");
                if (ret == MXIO_CS.EIO_SOCKET_DISCONNECT)
                {
                    if (this.Connect())
                    {
                        this.OnDigitalOutput(location);
                    }
                }

                if (ret == MXIO_CS.MXIO_OK)
                {
                    Debug.WriteLine("MXIO_WriteCoils Values:0x{0:X} , 0x{0:X}", byteWriteCoils[0], byteWriteCoils[1]);
                    success = true;
                }
            }

            return success;
        }

        public bool OffDigitalOutput(UInt16 location)
        {
            
            bool success = false;
            if (TankControl.Properties.Settings.Default.SystemTest == 0)
            {
                int ret;
                byteWriteCoils[0] = 0x00;

                ret = MXIO_CS.MXIO_WriteCoils(hConnection[0], location, 1, byteWriteCoils);
                CheckError(ret, "MXIO_WriteCoils");

                if (ret == MXIO_CS.EIO_SOCKET_DISCONNECT)
                {
                    if (this.Connect())
                    {
                        this.OffDigitalOutput(location);
                    }
                }

                if (ret == MXIO_CS.MXIO_OK)
                {
                    Debug.WriteLine("MXIO_WriteCoils Values:0x{0:X}, 0x{0:X}", byteWriteCoils[0], byteWriteCoils[1]);
                    success = true;
                }
            }

            return success;
        }
    }
}
