# HADotNet

This is a fork from qJake's great [HADotNet API library](https://github.com/qJake/HADotNet). 
It circumvents the RestSharp bug that exists at the time of writing, by adding a curl fallback mechanism.

You can enable curl by flagging the third initialise parameter to true (default false). 
[CliWrap](https://github.com/Tyrrrz/CliWrap) is used to execute curl. Add the nuget to your project, or manually add the library.

Please make sure curl is present on your system, and it's location has been added to PATH. You can download curl for windows [here](https://curl.se/windows/). 
A simple manual to add directories to your PATH variable is available [here](https://helpdeskgeek.com/windows-10/add-windows-path-environment-variable/). 