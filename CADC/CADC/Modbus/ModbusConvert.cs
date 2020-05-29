using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CADC.Modbus
{
    public class ModbusConvert
    {
        public string m_modbusStatus;

        #region Constructor / Deconstructor
        public ModbusConvert()
        {
        }
        ~ModbusConvert()
        {
        }
        #endregion

        #region CRC Computation
        private void GetCRC(byte[] message, ref byte[] CRC)
        {
            //Function expects a modbus message of any length as well as a 2 byte CRC array in which to 
            //return the CRC values:

            ushort CRCFull = 0xFFFF;
            byte CRCHigh = 0xFF, CRCLow = 0xFF;
            char CRCLSB;

            for (int i = 0; i < (message.Length) - 2; i++)
            {
                CRCFull = (ushort)(CRCFull ^ message[i]);

                for (int j = 0; j < 8; j++)
                {
                    CRCLSB = (char)(CRCFull & 0x0001);
                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);

                    if (CRCLSB == 1)
                        CRCFull = (ushort)(CRCFull ^ 0xA001);
                }
            }
            CRC[1] = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
            CRC[0] = CRCLow = (byte)(CRCFull & 0xFF);
        }
        #endregion

        #region Build Message
        private void BuildMessage(byte address, byte type, ushort start, ushort length_or_value, ref byte[] message)
        {
            //Array to receive CRC bytes:
            byte[] CRC = new byte[2];

            message[0] = address;
            message[1] = type;
            message[2] = (byte)(start >> 8);
            message[3] = (byte)start;
            message[4] = (byte)(length_or_value >> 8);
            message[5] = (byte)length_or_value;

            GetCRC(message, ref CRC);
            message[message.Length - 2] = CRC[0];
            message[message.Length - 1] = CRC[1];
        }
        #endregion

        #region Check Response
        private bool CheckResponse(byte[] response)
        {
            //Perform a basic CRC check:
            byte[] CRC = new byte[2];
            GetCRC(response, ref CRC);
            if (CRC[0] == response[response.Length - 2] && CRC[1] == response[response.Length - 1])
                return true;
            else
                return false;
        }
        #endregion


        #region Convert Read Registers
        //public string MakeFc3(int Slave, ushort StartAddr, ushort Length)
        //{
        //    byte[] message = new byte[8];
        //    BuildMessage(System.Convert.ToByte(Slave), (byte)3, StartAddr, Length, ref message);
        //    return BitConverter.ToString(message).Replace("-", "");
        //}
        public void MakeFc3(int Slave, ushort StartAddr, ushort Length, ref byte[] message)
        {
            //byte[] message = new byte[8];
            try
            {
                BuildMessage(System.Convert.ToByte(Slave), (byte)3, StartAddr, Length, ref message);
            }
            catch
            {

            }
            //return BitConverter.ToString(message).Replace("-", "");
        }

        public void MakeWordWrite(int Slave, ushort StartAddr, ushort Value, ref byte[] message)
        {
            BuildMessage(System.Convert.ToByte(Slave), (byte)6, StartAddr, Value, ref message);
        }

        public int ConvertModbus(byte[] response, ref short[] values)
        {
            m_modbusStatus = "";
            int ret = -1;
            if (CheckResponse(response))
            {
                //Return requested register values:
                ret = response.Length - 5;
                for (int i = 0; i < (response.Length - 5) / 2; i++)
                {
                    values[i] = response[2 * i + 3];
                    values[i] <<= 8;
                    values[i] += response[2 * i + 4];
                }
                m_modbusStatus = "Read successful";
                return ret;
            }
            else
            {
                m_modbusStatus = "CRC error";
                return ret;
            }
        }
        #endregion

    }
}
