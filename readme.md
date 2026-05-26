# Hashit 
## CLI to get hash codes for a file or group of files 
### Examples: 
* hashit file.nam
* hashit file*.txt 
* hashit file*.txt -h sha256 
* hashit file*.txt -h sha256 -- json 



## Also an experiment in AI coding 
* Ultimately I will be trying different AIS with the same rules. Comparing the difference might be fun.  

* Here are the rules I give each AI for creation of the program 
*  
I want a CLI program to calculate and show hash of files.  
The command line parameters are a filename, filename with wildcards for multiple files, a folder name.
Optionally the command line parameters might ask for a specific hash response, if not requested then show all the hash results the program supports. 
The output will list each filename and the hash or all of the hashes depending on the CLI parameters. 
This program should be c#.  Each hash routine should be in a separate cs file accessed by the main program. 
This program can expect the user to have installed .NET 8 or .net 9 
The resulting EXE should be a single EXE without expect extra DLLs. 
Start with providing these hash codes: MD5, SHA-1, SHA-256, SHA-512, CRC32 and CRC64


### (1) is COPilot Pro from Microsoft Edge, this was the AI process
* 15 minutes to write the instructions
* 30 minutes to get a clean compile from it's code 
* 2 hours trying to get a single EXE result instead of requiring a DLL.
* Have not succeeded in get a build from visual Studio that does not require a DLL, but I can do it with command line 
** dotnet publish HashIt.csproj -c Release -p:PublishSingleFile=true -p:SelfContained=false -p:PublishTrimmed=false -o ./publish_test_no_rid
* the AI request: 
*

* It created code then asked me if I wanted output option of json or csv.  I said BOTH, and it updated the code. 

### (2) CoPilot Pro inside of Visual Studio 
* 30 minutes trying find a way to create a .NET 8 with VS 2022 community 
* Failed to accomplish this 


### (3) CoPilot Pro Inside of VS Code 
* I gave it the same instructions
* It spent 10 minutes generating code then asking me to accept their code. 
* 1 Compiler error, which it fixed as soon as I pointed out the error. 
* Build process is manual with dotnet commands it provided 
* It created the, now standard, exe with a small DLL which is required. 
* Offer extra options to decode but I asked it to add in the jason and CSV output which produced 
* I asked for dotnet command to create a single EXE which resulted in a 73MB exe file as opposed to the 550KB exe + 20KB DLL
*  

## next steps 
### create text cases to validate the hash results and file access 
### fix the build to create a single EXE output 
### test on Linux with .NET8 option for Linux 
### see what happens of .NET8 is not installed 
### Add some other hash routines 
### create an option to text existing hash with a file 




