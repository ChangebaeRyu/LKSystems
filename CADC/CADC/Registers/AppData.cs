using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CADC.Registers
{
    public enum DCType
    {
        DC_NONE = 0,
        DC_SINGLE = 1,
        DC_MULTI = 2
    }
    public enum RwRegister
    {
        VOLT_HDR = 42001,
        VOLT_VAL,
        VOLT_SUB,
        CURR_HDR,
        CURR_VAL,
        CURR_SUB,
        PH_HDR,
        PH_VAL,
        PH_SUB,
        TEMP_HDR = 42010,
        TEMP_VAL,
        TEMP_SUB,
        FB_HDR,
        FB_VAL,
        DAM_HDR,
		DAM_VAL,
		IOT_HDR,
		IOT_VAL,
        LMT_DIF1_VAL,
        LMT_DIF2_VAL = 42020,
        LMT_DIF3_VAL,
        LMT_DIF4_VAL,
		BLANK8,
		BLANK9,
        DIF1_HDR,
        DIF1_VAL,
        DIF1_SUB,
        DIF2_HDR,
        DIF2_VAL,
        DIF2_SUB = 42030,
        DIF3_HDR,
        DIF3_VAL,
        DIF3_SUB,
        DIF4_HDR,
        DIF4_VAL,
        DIF4_SUB,
        SP_HDR,
        SP_VAL,
        SP_SUB,
        START_VAL = 42040,
        DOOR_VAL,
        CONTROL_VAL,
        AIR_COUNT_VAL,      // AIR Count
        STATUS_VAL,			// STATUS
        LMT_VOLT_VAL,
		LMT_CURR_VAL,
		LMT_PH_VAL,
		LMT_TEMP_VAL,
		LMT_FAN_VAL,
		BLANK10 = 42050,
		LMT_SP_VAL,
		FAN1_HDR,
		FAN1_VAL1,
		FAN1_VAL2,
		FAN1_VAL3,
		FAN1_VAL4,
		FAN1_VAL5,
		FAN1_VAL6,
		FAN1_SUB,
		FAN2_HDR = 42060,
		FAN2_VAL1,
		FAN2_VAL2,
		FAN2_VAL3,
		FAN2_VAL4,
		FAN2_VAL5,
		FAN2_VAL6,
		FAN2_SUB,
		FAN3_HDR,
		FAN3_VAL1,
		FAN3_VAL2 = 42070,
		FAN3_VAL3,
		FAN3_VAL4,
		FAN3_VAL5,
		FAN3_VAL6,
		FAN3_SUB,
		FAN4_HDR,
		FAN4_VAL1,
		FAN4_VAL2,
		FAN4_VAL3,
		FAN4_VAL4 = 42080,
		FAN4_VAL5,
		FAN4_VAL6,
		FAN4_SUB,
		SPARE1
	}

	class AppData
    {
        public bool isTest = false;
        public StringBuilder strTest;

        public ROData[] roData;
        public RWData[] rwData;

        private static readonly Lazy<AppData> instance = new Lazy<AppData>(() => new AppData());

        public DateTime m_lastConnTime;

        public static AppData Instence
        {
            get
            {
                return instance.Value;
            }
        }

        private AppData() 
        {
            InitData();
        }

        public void InitData(bool bArrayInit = true)
        {
            int i = 0;
            if(bArrayInit)
            {
                roData = new ROData[4];
                rwData = new RWData[4];
            }

            for (i = 0; i < 4; i++) roData[i] = new ROData();
            for (i = 0; i < 4; i++) rwData[i] = new RWData();
            strTest = new StringBuilder();
        }

    }
}
