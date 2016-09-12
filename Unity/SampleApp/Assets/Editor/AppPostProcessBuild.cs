using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;

public class AppPostProcessBuild {
	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget target, string path) {
		Debug.Log ("OnPostprocessBuild : " + path);

		var rootPath = "";

		if (target == BuildTarget.Android) {
			rootPath = path.Replace ("/Export", "");
		}
		else if (target == BuildTarget.iOS) {
			rootPath = path.Replace ("/Export/SampleApp", "");
		}

		RunBuildScript (rootPath);
	}

	/// <summary>
	/// ビルドスクリプトを実行する
	/// </summary>
	/// <param name="rootPath">Root path.</param>
	private static void RunBuildScript(string rootPath) {
		var appName = PlayerSettings.productName;
		var scriptPath = rootPath + "/Script";
		var commands = new string[] {
			"cd " + scriptPath,
			"sh build.sh " + rootPath + " " + appName,
		};

		// Terminalを起動してシェルスクリプトを実行する
		// 直接シェルスクリプトを実行することもできるが進捗状態が出力されないのでTerminal上で実行させる。
		var process = new System.Diagnostics.Process ();
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.RedirectStandardOutput = true;
		// AppleScriptを実行する
		process.StartInfo.FileName = "osascript";
		// [memo]
		//  LaunchTerminalWithCommand.scpt
		//  ターミナルを起動してコマンドを実行するAppleScript（何故か直接ターミナルを起動できないので書いた）
		process.StartInfo.Arguments = scriptPath + "/LaunchTerminalWithCommand.scpt " + CatTarminalCommands(commands);
		process.Start ();

		// 終了を待つ
		string strOutput = process.StandardOutput.ReadToEnd (); 
		process.WaitForExit ();
		UnityEngine.Debug.Log (strOutput);
	}

	/// <summary>
	/// コマンドの文字列を連結
	/// </summary>
	/// <returns>The tarminal commands string.</returns>
	/// <param name="commands">Commands.</param>
	public static string CatTarminalCommands(string[] commands) {
		string cmdString = "'";
		foreach (string command in commands) {
			if (!commands.GetValue (0).Equals(command)) {
				cmdString += " && ";
			}
			cmdString += command;
		}
		cmdString += "'";
		return cmdString;
	}
}
