FROM gcr.io/bazel-public/bazel:latest

WORKDIR /

COPY . pkg/
RUN cd pkg/ && bazel build //:all