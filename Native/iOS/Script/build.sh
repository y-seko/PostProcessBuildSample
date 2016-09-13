#!/bin/sh
for arg in $@
do
	if [[ "$arg" =~ ^rootPath= ]]; then
		rootPath=`echo "$arg" | sed 's/rootPath=//'`
	fi
	if [[ "$arg" =~ ^appName= ]]; then
		appName=`echo "$arg" | sed 's/appName=//'`
	fi
	if [[ "$arg" =~ ^codeSignIdentity= ]]; then
		codeSignIdentity=`echo "$arg" | sed 's/codeSignIdentity=//'`
	fi
	if [[ "$arg" =~ ^profileId= ]]; then
		profileId=`echo "$arg" | sed 's/profileId=//'`
	fi
	if [[ "$arg" =~ ^profileName= ]]; then
		profileName=`echo "$arg" | sed 's/profileName=//'`
	fi
done

PROJECT_PATH=$rootPath"/Export/"$appName
OUTPUT_PATH=$rootPath"/Output"
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
xcodebuild -scheme Unity-iPhone archive -archivePath $ARCHIVE_PATH"/Unity-iPhone" CODE_SIGN_IDENTITY=$codeSignIdentity PROVISIONING_PROFILE=$profileId
xcodebuild -exportArchive -exportFormat ipa -archivePath $ARCHIVE_PATH"/Unity-iPhone.xcarchive" -exportPath $IPA_PATH"/Unity-iPhone.ipa" -exportProvisioningProfile $profileName