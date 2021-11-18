using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace elf.Windows.Libraries.ShellLinks
{
	[CLSCompliant(false), StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode)]
	public struct WIN32_FIND_DATAW
	{
		public uint dwFileAttributes;
		public FILETIME ftCreationTime;
		public FILETIME ftLastAccessTime;
		public FILETIME ftLastWriteTime;
		public uint nFileSizeHigh;
		public uint nFileSizeLow;
		public uint dwReserved0;
		public uint dwReserved1;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = IShellLinkW.MAX_PATH)]
		public string cFileName;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
		public string cAlternateFileName;
	}

	[CLSCompliant(false), Flags]
	public enum SLGP_FLAGS : uint
	{
		SHORTPATH = 1,
		UNCPRIORITY = 2,
		RAWPATH = 4,
	}

	public enum SW : int
	{
		HIDE = 0,
		NORMAL = 1,
		SHOWNORMAL = 1,
		SHOWMINIMIZED = 2,
		MAXIMIZE = 3,
		SHOWMAXIMIZED = 3,
		SHOWNOACTIVATE = 4,
		SHOW = 5,
		MINIMIZE = 6,
		SHOWMINNOACTIVE = 7,
		SHOWNA = 8,
		RESTORE = 9,
		SHOWDEFAULT = 10,
		FORCEMINIMIZE = 11,
	}

	/// <summary>Flags determining how the links with missing targets are resolved.</summary>
	[CLSCompliant(false), Flags]
	public enum SLR_FLAGS : uint
	{
		/// <summary>
		/// Do not display a dialog box if the link cannot be resolved. 
		/// When SLR_NO_UI is set, a time-out value that specifies the 
		/// maximum amount of time to be spent resolving the link can 
		/// be specified in milliseconds. The function returns if the 
		/// link cannot be resolved within the time-out duration. 
		/// If the timeout is not set, the time-out duration will be 
		/// set to the default value of 3,000 milliseconds (3 seconds). 
		/// </summary>
		NO_UI = 1,
		/// <summary>
		/// Allow any match during resolution.  Has no effect
		/// on ME/2000 or above, use the other flags instead.
		/// </summary>
		ANY_MATCH = 2,
		/// <summary>
		/// If the link object has changed, update its path and list 
		/// of identifiers. If SLR_UPDATE is set, you do not need to 
		/// call IPersistFile::IsDirty to determine whether or not 
		/// the link object has changed. 
		/// </summary>
		UPDATE = 4,
		/// <summary>Do not update the link information.</summary>
		NOUPDATE = 8,
		/// <summary>Do not execute the search heuristics.</summary>
		NOSEARCH = 16,
		/// <summary>Do not use distributed link tracking.</summary>
		NOTRACK = 32,
		/// <summary>
		/// Disable distributed link tracking. By default, 
		/// distributed link tracking tracks removable media 
		/// across multiple devices based on the volume name. 
		/// It also uses the UNC path to track remote file 
		/// systems whose drive letter has changed. Setting 
		/// SLR_NOLINKINFO disables both types of tracking.
		/// </summary>
		NOLINKINFO = 64,
		/// <summary>Call the Microsoft Windows Installer.</summary>
		INVOKE_MSI = 128,

		/// <summary>
		/// Not documented in SDK.  Assume same as SLR_NO_UI but 
		/// intended for applications without a hWnd.
		/// </summary>
		UI_WITH_MSG_PUMP = 0x101,
	}
}
