#!/bin/sh
exec docker run -e USER="$(id -u)" -u="$(id -u):$(id -g)" \
   -v $PWD:/src/$PWD \
   -v /tmp/build_output:/tmp/build_output \
   -w /src/$PWD -it gcr.io/bazel-public/bazel:latest \
   --output_user_root=/tmp/build_output \
   build --config=gcc //:all