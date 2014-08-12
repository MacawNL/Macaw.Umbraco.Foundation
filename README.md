#Macaw Umbraco Foundation
###Work in progress! 

Foundation library used at Macaw Netherlands. Some features are:  

- Standard Examine Search implementation  
- Paged result views  
- Frontend developers friendly dynamic models for Templates and Macro  
- Basic property editor converters 
- Testable controllers 
- "Distributed" file caching with MvcDonutCaching
- Several Html helpers like ToJson...

Currently build on top of Umbraco 7.1.4

##Installation
First create an empty website project and set the target framework to .net 4.5. Extract the Umbraco 7.0.1 files into the same directory (except the App_Code and Bin folders).
If not already done.

<code>  
PM> Install-Package Macaw.Umbraco.Foundation
</code>  
<code>  
PM> Install-Package Macaw.Umbraco.Foundation -IncludePrerelease
</code>

Currently only available as pre release version.


##Examples and documentation
Both the example project and the nuget package only use the Umbraco Core libraries. 
To run a website follow the instructions by ben-morris to setup and run your project. (In case of the example project don't overwrite any file)

http://www.ben-morris.com/using-umbraco-6-to-create-an-asp-net-mvc-4-web-applicatio  

- The example database username is "admin" and the password is "password".  
- The nuget package and the example project both use Autofac. See the implementation folder for more information and / or customization. 

