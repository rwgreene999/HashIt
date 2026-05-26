# Hashit 
## CLI to get hash codes for a file or group of files 
### Examples: 
* hashit file.nam
* hashit file*.txt 
* hashit file*.txt -h sha256 
* hashit file*.txt -h sha256 -- json 
##Also an experiment in AI coding 
### this was the AI process: 
* 15 minutes to write the instructions
* 30 minutes to get a clean compile from it's code 
* 2 hours trying to get a single EXE result instead of requiring a DLL.
* Have not succedded in get a build from visual Studio that does not require a DLL, but I can do it with command line 
** dotnet publish HashIt.csproj -c Release -p:PublishSingleFile=true -p:SelfContained=false -p:PublishTrimmed=false -o ./publish_test_no_rid
* the AI request: 
*
I want a CLI program to calculate and show hash of files.  
The command line parameters are a filename, filename with wildcards for multiple files, a folder name.
Optionally the command line parameters might ask for a specific hash response, if not requested then show all the hash results the program supports. 
The output will list each filename and the hash or all of the hashes depending on the CLI parameters. 
This program should be c#.  Each hash routine should be in a separate cs file accessed by the main program. 
This program can expect the user to have installed .NET 8 or .net 9 
The resulting EXE should be a single EXE without expect extra DLLs. 
Start with providing these hash codes: MD5, SHA-1, SHA-256, SHA-512, CRC32 and CRC64
*
* It created code then asked me if I wanted output option of json or csv.  I said BOTH, and it updated the code. 
<<<<<<< Updated upstream
=======

### (2) CoPilot Pro inside of Visual Studio 
* 30 minutes trying find a way to create a .NET 8 with VS 2022 community 
* Failed to accomplish this 


### (3) CoPilot Pro Inside of VS Code (see hashit2 )
* I gave it the same instructions
* It spent 10 minutes generating code then asking me to accept their code. 
* 1 Compiler error, which it fixed as soon as I pointed out the error. 
* Build process is manual with dotnet commands it provided 
* It created the, now standard, exe with a small DLL which is required. 
* Offer extra options to decode but I asked it to add in the jason and CSV output which produced 
* I asked for dotnet command to create a single EXE which resulted in a 73MB exe file as opposed to the 550KB exe + 20KB DLL
*  Build command for 73MB standalone EXE: 
> dotnet publish -c Release -r win-x64 --self-contained false -p:PublishSingleFile=true
* build command for the exe+dll solution 
> dotnet build
* _or use the BAT files I just discovered_


### (4) CharGPT (I forgot the results) 

### (5) gemini 
* I gave it the same instructions
* Code compiled correctly first pass 
* link process failed.  
* I could followup on the link process but got side tracked 



## Differences between CoPilot in different environments. 
* Oddly difference code between these two versions
For example, Option (1) (CoPilot from Edge) created this: 
```
    public override string ComputeHash(Stream stream)
    {
        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(stream);
        return Convert.ToHexString(hash);
    }
```
While option (3) produced 
```
    public string ComputeHash(string filePath)
    {
        using var sha256 = SHA256.Create();
        using var stream = File.OpenRead(filePath);
        var hash = sha256.ComputeHash(stream);
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }
```
*  Option (3), CoPilot in VVS Code also created a readme.md with instructions for building the code. 
* Also Option (3) created BAT files to build various versions of the Of the program which I didn't even notice until I was writing this document.
### I have to admin that Option (3) produced better results 









>>>>>>> Stashed changes
## next steps 
### fix the build to create a single EXE output 
### test on Linux with .NET8 option for Linux 
### see what happens of .NET8 is not installed 
### Add some other hash routines 
### create an option to text existing hash with a file 



