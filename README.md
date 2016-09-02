# PostProcessBuildSample
PostProcessBuildを使用した自動ビルドのサンプルです。

## フォルダ構成
    [Android] --- Android関連のフォルダ
       [App] --- AndroidStudioでインポートしたプロジェクト。このプロジェクトをgradleでビルドする。  
       [Export] --- Unityからエクスポートしたプロジェクトの格納先  
       [Script] --- スクリプト格納フォルダ  
    [Unity]--- Unityのプロジェクト格納フォルダ

## 最初に
ローカル環境のパスが通っていないのでクローン直後にgradleでビルドしてもコケます。  
まずAndroidStudioでAndroid/Appを開いてください。  
開いた後、自動でビルドが走りますがエラーが出ます。※1  

※1  
build.gradleでソースの参照先をExportの方に変更しているが、  
クローン直後はプロジェクトが出力されていないため。  

## ビルド
1. UnityのBuild Settingsを開く  
2. AndroidにSwith Platform  
3. 「Google Android Project」にチェックを入れる  
4. Export
5. 「Android/App/app/build/outputs/apk」にapkファイルが出力されます。
