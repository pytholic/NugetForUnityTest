# NugetForUnity
[NugetForUnity](https://github.com/GlitchEnzo/NuGetForUnity)  is a package management system that makes it easier to install required third-party packages inside Unity along with their dependencies. To test **NugetForUnity**, I created this rather sample Unity project which uses an ML model to run inference using the OnnxRuntime library.

## Project Environment
The project environment consists of a simple `scene` in which we create an empty `model` object and attach a `script`file to it. The `script` consists of a bunch of code that loads the ML model and input images, performs inference, and returns the output predictions.

## Installation
To install the `NugetForUnity` package, head over to [link](https://github.com/GlitchEnzo/NuGetForUnity/releases) and download the `NuGetForUnity.3.0.2.unitypackage` file (or any other version if you want). After downloading the package file, return to your Unity project, go to **Assets -> Import Package -> Custom Package** and load the downlaoded `NuGetForUnity.3.0.2.unitypackage` file.

After you finish the installation, there should be a `NuGet` option in the top menu. If you don't see any option, restart your Unity project.

## Installing Packages
To install the required packages using NuGet package manager, click on `NuGet` from the top menu and then select `Manage Nuget Packages`. Now you can see and browse through all the available packages. To install a package, type the name of the required package in the search bar. For this project, I installed the following packages.

* Microsoft.Onnx.Runtime
* Microsoft.Onnx.Runtime.Managed
* SixLabors.ImageSharp

Note that you also need to install **Microsoft.Onnx.Runtime.Managed** along with **Microsoft.Onnx.Runtime** package. `NugetForUnity` will automatically install the required dependencies as well for these packages. It will create a separate `Packages` folder inside your `Assets` folder, that will contain all the packages inatalled via `NugetForUnity` manager.


## Additional Setup
Now for the case of most of the libraries, you just need to install through NuGet manager, and then the libraries will start working directly. However, I found that in the case of native shared libraries i.e. **Microsoft.Onnx.Runtime** in my case, we have to do some additional steps.

1) After you have installed the **Microsoft.Onnx.Runtime** package, head over to **Assets -> Packages -> Microsoft.ML.OnnxRuntime.1.9.0** folder on your local explorer. Note that your folder name can be different depending on the version you are using. Locate `Microsoft.ML.OnnxRuntime.1.9.0.nupkg` inside the folder.
2) This step will be different depending on your OS.
  * If you are using **Windows**, rename the `Microsoft.ML.OnnxRuntime.1.9.0.nupkg` file to `Microsoft.ML.OnnxRuntime.1.9.0.zip` and extract it. Go to **runtimes -> win-x64 -> native** and copy all the `.dll` files. Note that you have to choose the right OS depending on your system. I am using win-x64 based system so I chose that.
  * If you are using **linux-based** system (Ubuntu in my case)


## Cleanup
## Final Comments
