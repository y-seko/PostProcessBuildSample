#!/bin/sh
for arg in $@
do
	if [[ "$arg" =~ ^rootPath= ]]; then
		rootPath=`echo "$arg" | sed 's/rootPath=//'`
	fi
	if [[ "$arg" =~ ^appName= ]]; then
		appName=`echo "$arg" | sed 's/appName=//'`
	fi
done

MANIFEST_PATH=$rootPath"/Export/"$appName
GRADLE_PATH=$rootPath"/App"

# AndroidManifest.xmlを修正する
cd $MANIFEST_PATH
sed -i -e "s/android:debuggable=\"[a-z]*\"//g" AndroidManifest.xml

# build
cd $GRADLE_PATH
./gradlew clean
./gradlew assembleDebug