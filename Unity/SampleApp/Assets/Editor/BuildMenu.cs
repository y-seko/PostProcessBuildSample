using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class BuildMenu {

	[MenuItem("Build/Build")]
	public static void ShowWindow() {
		EditorWindow window = EditorWindow.GetWindow (typeof(BuildMenuWindow));
		window.Show ();
	}
}

public class BuildMenuWindow : EditorWindow {

	private string exportPath = "";

	void OnGUI() {

		EditorGUILayout.BeginHorizontal ();
		{
			exportPath = EditorGUILayout.TextField ("Export Path", exportPath);
			if (GUILayout.Button ("…", GUILayout.Width(20))) {
				exportPath = EditorUtility.OpenFolderPanel ("出力先を選択してください", exportPath, "");
			}
		}
		EditorGUILayout.EndHorizontal ();

		if (GUILayout.Button("Build", GUILayout.Width(70))) {
			BuildAndroid (exportPath);
		}
	}

	/// <summary>
	/// Androidビルドを実行する
	/// </summary>
	/// <param name="exportPath">Export path.</param>
	private void BuildAndroid(string exportPath) {
		var levels = GetBuildScenes ();
		BuildTarget target = BuildTarget.Android;
		BuildOptions options = BuildOptions.AcceptExternalModificationsToPlayer;
		BuildPipeline.BuildPlayer (levels, exportPath, target, options);
	}

	/// <summary>
	/// ビルドに含めるシーンの配列を取得する
	/// </summary>
	/// <returns>The build scenes.</returns>
	private string[] GetBuildScenes() {
		List<string> sceneList = new List<string>();
		EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
		foreach (EditorBuildSettingsScene scene in scenes) {
			if (scene.enabled) {
				sceneList.Add (scene.path);
			}
		}
		return sceneList.ToArray();
	}
}
