using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;

public class AppPostProcessBuild {
	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget target, string path) {
		if (target == BuildTarget.Android) {

			UnityEngine.Debug.Log ("OnPostprocessBuild:" + path);

			var rootPath = path.Replace ("Export", "");
			var scriptPath = rootPath + "/Script";

			// 直接graldeコマンド叩いてもAndroidManifest.xmlの問題でビルドコケるので、
			// シェルスクリプト書いてそれを実行する
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
}
