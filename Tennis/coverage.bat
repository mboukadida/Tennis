Tennis\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user "-filter:+[Tennis]* -[*Test]*" "-target:Tennis\packages\NUnit.ConsoleRunner.3.7.0\tools\nunit3-console.exe" "-targetargs:Tennis\Tennis.Tests\bin\Debug\Tennis.Tests.dll"

Tennis\packages\ReportGenerator.3.0.2\tools\ReportGenerator.exe "-reports:TestResults\results.xml" "-targetdir:.\coverage"

pause