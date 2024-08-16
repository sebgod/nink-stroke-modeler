ink-stroke-modeler wrapper for .NET

WIP:

```
bazel build --copt=-D_USE_MATH_DEFINES --cxxopt=/std:c++20 --incompatible_enable_cc_toolchain_resolution --platforms=//:arm64_windows-clang-cl --extra_toolchains=@local_config_cc//:cc-toolchain-x64_windows-clang-cl --extra_execution_platforms=//:arm64_windows-clang-cl --toolchain_resolution_debug=.* //:all
```