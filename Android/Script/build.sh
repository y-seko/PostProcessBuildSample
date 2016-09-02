#!/bin/sh
ROOT_PATH=$1

MANIFEST_PATH=$ROOT_PATH"/Export/SampleApp"
GRADLE_PATH=$ROOT_PATH"/App"

# AndroidManifest.xmlを修正する
cd $MANIFEST_PATH
sed -i -e "s/android:debuggable=\"[a-z]*\"//g" AndroidManifest.xml

# build
cd $GRADLE_PATH
./gradlew clean
./gradlew assembleDebug