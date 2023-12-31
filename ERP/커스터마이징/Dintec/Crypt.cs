﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Dintec
{
	public class Crypt
	{
		// AES 256 암호화
		public static string Encrypt(string inputText, string password)
		{
			RijndaelManaged RijndaelCipher = new RijndaelManaged();

			// 입력받은 문자열을 바이트 배열로 변환  
			byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(inputText);

			// 딕셔너리 공격을 대비해서 키를 더 풀기 어렵게 만들기 위해서   
			// Salt를 사용한다.  
			byte[] Salt = Encoding.ASCII.GetBytes(password.Length.ToString());

			PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(password, Salt);

			// Create a encryptor from the existing SecretKey bytes.  
			// encryptor 객체를 SecretKey로부터 만든다.  
			// Secret Key에는 32바이트  
			// Initialization Vector로 16바이트를 사용  
			ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

			MemoryStream memoryStream = new MemoryStream();

			// CryptoStream객체를 암호화된 데이터를 쓰기 위한 용도로 선언  
			CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

			cryptoStream.Write(PlainText, 0, PlainText.Length);

			cryptoStream.FlushFinalBlock();

			byte[] CipherBytes = memoryStream.ToArray();

			memoryStream.Close();
			cryptoStream.Close();

			string EncryptedData = Convert.ToBase64String(CipherBytes);

			return EncryptedData;
		}

		// AES 256 복호화  
		public static string Decrypt(string inputText, string password)
		{
			RijndaelManaged RijndaelCipher = new RijndaelManaged();

			byte[] EncryptedData = Convert.FromBase64String(inputText);
			byte[] Salt = Encoding.ASCII.GetBytes(password.Length.ToString());

			PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(password, Salt);

			// Decryptor 객체를 만든다.  
			ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

			MemoryStream memoryStream = new MemoryStream(EncryptedData);

			// 데이터 읽기 용도의 cryptoStream객체  
			CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

			// 복호화된 데이터를 담을 바이트 배열을 선언한다.  
			byte[] PlainText = new byte[EncryptedData.Length];

			int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);

			memoryStream.Close();
			cryptoStream.Close();

			string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);

			return DecryptedData;
		}
	}
}
