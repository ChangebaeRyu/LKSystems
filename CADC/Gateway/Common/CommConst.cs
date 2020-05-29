using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gateway
{
	class CommConst
	{
		public const string WORK_CODE = "0000010";		// 사업장 코드
		public const string CHIM_CODE = "0001";			// 굴뚝코드

		// 측정항목
		public const string MSR_DIS = "EFx";			// 배출시설 전류 + '0' ~ '9'
		public const string MSR_PRE = "PFx";            // 방지시설 전류 + '0' ~ '9'
		public const string MSR_DIF = "PFS";            // 압력(차압) + '0' ~ '9'
		public const string MSR_TMP = "TMP";            // 배출시설 전류 + '0' ~ '9'

		public const string CMD_TDAT = "TDAT";			// 자료전송
		public const string CMD_PDUM = "PDUM";			// 저장자료 요청
		public const string CMD_TDUM = "TDUM";			// 저장자료 응답
		public const string CMD_TFDT = "TFDT";			// 미전송 자료 자동전송
		public const string CMD_PSEP = "PSEP";			// 암호 변경 지시
		public const string CMD_TVER = "TVER";			// 기동시 게이트웨이 정보(버전, 해쉬코드) 전송
		public const string CMD_PUPG = "PUPG";			// 업그레이드 지시 전송
		public const string CMD_TUPG = "TUPG";			// 업그레이드 결과 전송
		public const string CMD_TCNG = "TCNG";			// 설정값 변경 항목 자동 전송
		public const string CMD_PVER = "PVER";			// 버전정보(버전, 해쉬코드) 조회 요청
		public const string CMD_DVER = "DVER";          // 버전정보(버전, 해쉬코드) 조회 응답
	}
}
