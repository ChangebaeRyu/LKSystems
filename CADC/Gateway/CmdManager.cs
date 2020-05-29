namespace Gateway
{
	class CmdManager
    {
        private string Cmd;
        public bool result;
        public CmdManager(string msg)
        {
            Cmd = msg.Length >= 4 ? msg.Substring(0, 4) : null;
            if (Cmd == null)
            {
                // TODO : Send NAK
                result = false;
                return;
            }
            switch (Cmd)
            {
                case "TDAT":
                    // 자료 전송
                    // TDUM 응답 : 저장자료 응답
                    result = CmdTDAT();
                    break;
                case "PDUM":
                    // 저장자료 요청
                    // TDUM 응답 : 저장자료 응답
                    result = CmdPDUM();
                    break;
                case "TDUM":
                    result = CmdTDUM();
                    break;
                case "PSEP":
                    // 암호 변경 지시
                    result = CmdPSEP();
                    break;
                case "TCNG":
                    result = CmdTCNG();
                    break;
                case "TVER":
                    result = CmdTVER();
                    break;
                case "PUPG":
                    // 업그레이드 지시
                    // TUPG 응답 : 업드레이드 결과 전송
                    result = CmdPUPG();
                    break;
                case "TUPG":
                    result = CmdTUPG();
                    break;
                case "PVER":
                    // 버전정보(버전, 해쉬코드) 조회 요청
                    result = CmdPVER();
                    break;
                case "DVER":
                    result = CmdDVER();
                    break;
                default:
                    break;
            }
        }

        #region 자료 전송(TDAT)
        private bool CmdTDAT()
        {
            bool ret = true;

            return ret;
        }
        #endregion

        #region 저장자료 요청(PDUM)
        private bool CmdPDUM()
        {
            bool ret = true;

            return ret;
        }
        #endregion

        #region 자료 전송(TDUM)
        private bool CmdTDUM()
        {
            bool ret = true;

            return ret;
        }
        #endregion

        #region 미전송된 자료 자동전송(TFDT)
        private bool CmdTFDT()
        {
            bool ret = true;

            return ret;
        }
        #endregion

        #region 암호 변경 지시(PSEP)
        private bool CmdPSEP()
        {
            bool ret = true;

            return ret;
        }
        #endregion

        #region 설정값 변경 항목 자동 전송(TCNG)
        private bool CmdTCNG()
        {
            bool ret = true;

            return ret;
        }
        #endregion

        #region 기동시 게이트웨이 정보(버전, 해쉬코드) 전송 일1회 전송(TVER)
        private bool CmdTVER()
        {
            bool ret = true;

            return ret;
        }
        #endregion

        #region 업그레이드 지시 전송(PUPG)
        private bool CmdPUPG()
        {
            bool ret = true;
            //AutoUpdate autoUpdate = new AutoUpdate();
            //autoUpdate.CompareVersions();

            return ret;
        }
        #endregion

        #region 업그레이드 결과 전송(TUPG)
        private bool CmdTUPG()
        {
            bool ret = true;

            return ret;
        }
        #endregion

        #region 버전정보(버전, 해쉬코드) 조회 요청(PVER)
        private bool CmdPVER()
        {
            bool ret = true;

            return ret;
        }
        #endregion

        #region 버전정보(버전, 해쉬코드) 조회 응답(DVER)
        private bool CmdDVER()
        {
            bool ret = true;

            return ret;
        }
        #endregion

    }
}
