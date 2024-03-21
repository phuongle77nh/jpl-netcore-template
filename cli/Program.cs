using System.Diagnostics;

if (args.Length == 0)
{
    Console.WriteLine("insufficient params.");
    return;
}

string firstArg = args[0];
if (firstArg == "install" || firstArg == "i" || firstArg == "update" || firstArg == "u")
{
    await InstallTemplates();
    return;
}

if (firstArg == "api")
{
    if (args.Length != 3)
    {
        Console.WriteLine("invalid command. use something like : rs api new <projectname>");
        return;
    }

    string command = args[1];
    string projectName = args[2];
    if (command == "n" || command == "new")
    {
        await BootstrapWebApiSolution(projectName);
    }

    return;
}

async Task InstallTemplates()
{
    WriteSuccessMessage("installing rs dotnet webapi template...");
    var apiPsi = new ProcessStartInfo
    {
        FileName = "dotnet",
        Arguments = "new install Jpl.MicroService.Boilerplate"
    };
    using var apiProc = Process.Start(apiPsi)!;
    await apiProc.WaitForExitAsync();

    WriteSuccessMessage("installed the required templates.");
    Console.WriteLine("get started by using : rs <type> new <projectname>.");
    Console.WriteLine("note : <type> can be api, wasm.");
}

async Task BootstrapWebApiSolution(string projectName)
{
    Console.WriteLine($"bootstraping dotnet webapi project for you at \"./{projectName}\"...");
    var psi = new ProcessStartInfo
    {
        FileName = "dotnet",
        Arguments = $"new rs-microservice -n {projectName} -o {projectName} -v=q"
    };
    using var proc = Process.Start(psi)!;
    await proc.WaitForExitAsync();
    WriteSuccessMessage($"rs-microservice project {projectName} successfully created.");
    WriteSuccessMessage("application ready! build something amazing!");
}

void WriteSuccessMessage(string message)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(message);
    Console.ResetColor();
}