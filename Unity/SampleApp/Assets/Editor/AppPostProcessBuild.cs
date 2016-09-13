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

	private static void RunBuildScript(string rootPath) {
		var scriptPath = rootPath + "/Script";

		var process = new System.Diagnostics.Process ();
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.FileName = "sh";
		process.StartInfo.Arguments = scriptPath + "/build.sh " + rootPath;
		process.Start ();

		// 終了を待つ
		string strOutput = process.StandardOutput.ReadToEnd (); 
		process.WaitForExit ();
		UnityEngine.Debug.Log (strOutput);
	}
}
