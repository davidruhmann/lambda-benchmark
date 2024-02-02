#!/bin/bash

if [ $1 = "x86_64" ]; then
    ARCH="x64"
    IMAGE_ARCH="amd64"
    PLATFORM="linux/amd64"
elif [ $1 = "arm64" ]; then
    ARCH="arm64"
    IMAGE_ARCH="arm64v8"
    PLATFORM="linux/arm64/v8"
fi

path=$(sed -n 's/path: "\(.*\)"/\1/p' manifest.yml)
zip="${path}_${ARCH}.zip"

rm ${zip} 2> /dev/null

docker build . --platform=${PLATFORM} --build-arg ARCH=${ARCH} --build-arg IMAGE_ARCH=${IMAGE_ARCH} -t mbwilding/dotnet8al2023
dockerId=$(docker create mbwilding/dotnet8al2023)
docker cp $dockerId:/code.zip ${zip}
