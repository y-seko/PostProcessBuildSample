﻿using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using System.Collections.Generic;

public class AppPostProcessBuild {
	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget target, string path) {
		Debug.Log ("OnPostprocessBuild : " + path);

		var rootPath = "";
		var platformPath = "";
		var args = "";

		if (target == BuildTarget.Android) {
			rootPath = path.Replace ("/Android/Export", "");
			platformPath = rootPath + "/Android";
			args = CatArguments (new Dictionary<string, string> () {
				{"rootPath", platformPath},
				{"appName", PlayerSettings.productName},
			});
		}
		else if (target == BuildTarget.iOS) {
			rootPath = path.Replace ("/iOS/Export/SampleApp", "");
			platformPath = rootPath + "/iOS";
			args = CatArguments (new Dictionary<string, string> () {
				{"rootPath", platformPath},
				{"appName", PlayerSettings.productName},
				{"codeSignIdentity", "XXXXXXXX"},
				{"profileId", "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"},
				{"profileName", "xxxxxxxxxxxxxxx"},
			});
		}

		RunBuildScript (rootPath, platformPath, args);
	}

	/// <summary>
	/// ビルドスクリプトを実行する
	/// </summary>
	/// <param name="rootPath">Root path.</param>
	private static void RunBuildScript(string rootPath, string platformPath, string args) {
		var commonScriptPath = rootPath + "/Script";
		var platformScriptPath = platformPath + "/Script";
		var commands = new string[] {
			"cd " + platformScriptPath,
			"sh build.sh" + args,
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
		process.StartInfo.Arguments = commonScriptPath + "/LaunchTerminalWithCommand.scpt " + CatTarminalCommands(commands);
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

	public static string CatArguments(Dictionary<string, string> args) {
		string argsString = "";
		foreach (string key in args.Keys) {
			argsString += " " + key + "=" + args [key];
		}
		return argsString;
	}
}
