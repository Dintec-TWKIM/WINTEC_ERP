using System;
using System.IO;

namespace DX
{
	public class 파일정보
	{
		readonly FileInfo fi;

		public 파일정보(string 파일이름)
		{
			fi = new FileInfo(파일이름);
		}

		public string 이름 => fi.Name;

		public string 이름_경로포함 => fi.FullName;

		public string 확장자 => fi.Extension;

		public string 확장자_점제외 => fi.Extension.Replace(".", "");

		/// <summary>
		/// 현재 파일의 크기(바이트)
		/// </summary>
		public long 크기 => fi.Length;

		public DateTime 수정한날짜 => fi.LastWriteTime;
	}
}
