.\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user "-filter:+[Tennis]* -[*Test]*" "-target:.\packages\NUnit.ConsoleRunner.3.7.0\tools\nunit3-console.exe" "-targetargs:/noshadow .\Tennis.Tests\bin\Debug\Tennis.Tests.dll"

.\packages\ReportGenerator.3.0.2\tools\ReportGenerator.exe "-reports:results.xml" "-targetdir:.\coverage"

pause