#!/bin/sh
ROOT_PATH=$1
APP_NAME=$2

PROJECT_PATH=$ROOT_PATH"/Export/"$APP_NAME
OUTPUT_PATH=$ROOT_PATH"/Output"
ARCHIVE_PATH=$OUTPUT_PATH"/xcarchive"
IPA_PATH=$OUTPUT_PATH"/ipa"

# delete
if [ -e $OUTPUT_PATH ];
	then rm -rf $OUTPUT_PATH;
fi

# create directory
mkdir $OUTPUT_PATH
mkdir $ARCHIVE_PATH
mkdir $IPA_PATH

# build
cd $PROJECT_PATH
xcodebuild clean -project Unity-iPhone.xcodeproj
xcodebuild -scheme Unity-iPhone archive -archivePath $ARCHIVE_PATH"/Unity-iPhone" CODE_SIGN_IDENTITY='XXXXXXXXXXXXXXXXX' PROVISIONING_PROFILE='XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX'
xcodebuild -exportArchive -exportFormat ipa -archivePath $ARCHIVE_PATH"/Unity-iPhone.xcarchive" -exportPath $IPA_PATH"/Unity-iPhone.ipa" -exportProvisioningProfile "xxxxxxxxxxxxxxxxx"