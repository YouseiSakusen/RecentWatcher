﻿using System.Diagnostics;
using System.Text;

namespace elf.Windows.Libraries;

/// <summary>WindowsScriptingHostライブラリを表します。</summary>
public static class WindowsScriptingHosts
{
	/// <summary>ショートカットファイルから元ファイルのフルパスを取得します。</summary>
	/// <param name="shortcutFilePath">
	/// <para>ショートカットファイル(*.lnk)のフルパスを表す文字列。</para>
	/// <para>実際のショートカットファイルが存在しなくても構いません。</para>
	/// </param>
	/// <returns>
	/// <para>ショートカットファイルから取得した元ファイルのパスを表す文字列。</para>
	/// <para>ショートカットファイルが存在しない場合は空文字が返ります。</para>
	/// <para>取得した元ファイルが存在するかはチェックしません。</para>
	/// </returns>
	public static string GetSourcePathFromShortcutFile(string shortcutFilePath)
	{
#pragma warning disable CA1416 // プラットフォームの互換性を検証
		var wshType = Type.GetTypeFromProgID("WScript.Shell");
#pragma warning restore CA1416 // プラットフォームの互換性を検証
		if (wshType == null)
			return string.Empty;

		dynamic shell = Activator.CreateInstance(wshType)!;

		var s_jisBytes = Encoding.GetEncoding("shift_jis").GetBytes(shortcutFilePath);
		var s_jisString = Encoding.GetEncoding("shift_jis").GetString(s_jisBytes);
		Debug.WriteLine(s_jisString);

		return shell.CreateShortcut(s_jisString).TargetPath;
		//return shell.CreateShortcut(shortcutFilePath).TargetPath;
	}

	static WindowsScriptingHosts()
		=> Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
}