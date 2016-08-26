/*****************************************************************************************************
Unity Game Build Script
Author: Wil Taylor
*****************************************************************************************************/
#tool "nuget:?package=GitVersion.CommandLine"
var GameName = "StandardTest";

var RepoRootFolder = MakeAbsolute(Directory(".")); 
var ReleaseFolder = RepoRootFolder + "/Release";
var BuildFolder = RepoRootFolder + "/Build";
var SourceFolder = RepoRootFolder +"/Src";
var ToolsFolder = RepoRootFolder + "/tools";
var AssetsFolder = RepoRootFolder + "/Assets";

var UnityPath = "C:\\Program Files\\Unity\\Editor\\Unity.exe";
var DropboxPath = EnvironmentVariable("DROPBOXPATH");
var PublishPath = DropboxPath + "/Public/Ludum Dare/Test";


//Build Script Parameters
var version = GitVersion(new GitVersionSettings{UpdateAssemblyInfo = false}); //This updates all AssemblyInfo files automatically.
var target = Argument("target", "Default");

Task("Default")
    .IsDependentOn("Package");

Task("Restore")
    .IsDependentOn("File.Version")
    .IsDependentOn("Folder.Restore");

Task("Clean")
    .IsDependentOn("Unity.Clean");

Task("Build")
    .IsDependentOn("File.Version")
    .IsDependentOn("Unity.Build");

Task("Test");

Task("Package")
    .IsDependentOn("Unity.Package");

Task("Publish")
    .IsDependentOn("Unity.Publish");

/*****************************************************************************************************
Version Class
*****************************************************************************************************/
Task("File.Version")
    .Does(() => {
        var vs = "public static class VersionInfo\r\n";
        vs += "{\r\n";
        vs += "    public const string Version = \"" + version.SemVer + "\"\r\n";
        vs += "    public const string InfoVersion = \"" + version.InformationalVersion + "\"\r\n";
        vs += "}\r\n";

        System.IO.File.WriteAllText(AssetsFolder + "/Scripts/Version.cs", vs);
    });

/*****************************************************************************************************
Folder structure
*****************************************************************************************************/
Task("Folder.Restore")
    .Does(() => {
        CreateDirectory(ReleaseFolder);
        CreateDirectory(BuildFolder);
        CreateDirectory(AssetsFolder + "/Packages");
        CreateDirectory(AssetsFolder + "/Prefabs");
        CreateDirectory(AssetsFolder + "/Scenes");
        CreateDirectory(AssetsFolder + "/Scripts");
        CreateDirectory(AssetsFolder + "/Sprites");
        CreateDirectory(AssetsFolder + "/Sounds");
        CreateDirectory(AssetsFolder + "/ScriptObjects");
    });

/*****************************************************************************************************
Unity Project
*****************************************************************************************************/
Task("Unity.Clean")
    .IsDependentOn("Unity.Clean.Windowsx64")
    .IsDependentOn("Unity.Clean.Windowsx86")
    .IsDependentOn("Unity.Clean.Linux")
    .IsDependentOn("Unity.Clean.MacOS");

Task("Unity.Restore");

Task("Unity.Build")
    .IsDependentOn("Unity.Build.Windowsx64")
    .IsDependentOn("Unity.Build.Windowsx86")
    .IsDependentOn("Unity.Build.Linux")
    .IsDependentOn("Unity.Build.MacOS");

Task("Unity.Test");

Task("Unity.Package")
    .IsDependentOn("Unity.Package.Windowsx64")
    .IsDependentOn("Unity.Package.Windowsx86")
    .IsDependentOn("Unity.Package.Linux")
    .IsDependentOn("Unity.Package.MacOS");

Task("Unity.Publish")
    .IsDependentOn("Unity.Publish.Windowsx64")
    .IsDependentOn("Unity.Publish.Windowsx86")
    .IsDependentOn("Unity.Publish.Linux")
    .IsDependentOn("Unity.Publish.MacOS");

Task("Unity.Clean.Windowsx64")
    .Does(() => CleanDirectory(BuildFolder + "/Windowsx64"));

Task("Unity.Clean.Windowsx86")
    .Does(() => CleanDirectory(BuildFolder + "/Windowsx86"));

Task("Unity.Clean.Linux")
    .Does(() => CleanDirectory(BuildFolder + "/Linux"));

Task("Unity.Clean.MacOS")
    .Does(() => CleanDirectory(BuildFolder + "/MacOS"));

Task("Unity.Build.Windowsx64")
    .IsDependentOn("Unity.Clean.Windowsx64")
    .Does(() => StartProcess(UnityPath, string.Format("-batchmode -quit -buildWindows64Player \"{0}/Windowsx64/{1}x64-{2}.exe\"", BuildFolder, GameName, version.SemVer)));

Task("Unity.Build.Windowsx86")
    .IsDependentOn("Unity.Clean.Windowsx86")
    .Does(() => StartProcess(UnityPath, string.Format("-batchmode -quit -buildWindowsPlayer  \"{0}/Windowsx86/{1}x86-{2}.exe\"", BuildFolder, GameName, version.SemVer)));

Task("Unity.Build.Linux")
    .IsDependentOn("Unity.Clean.Linux")
    .Does(() => StartProcess(UnityPath, string.Format("-batchmode -quit -buildLinuxUniversalPlayer  \"{0}/Linux/{1}-{2}\"", BuildFolder, GameName, version.SemVer)));

Task("Unity.Build.MacOS")
   .IsDependentOn("Unity.Clean.MacOS")
   .Does(() => StartProcess(UnityPath, string.Format("-batchmode -quit -buildOSXUniversalPlayer  \"{0}/MacOS/{1}-{2}\"", BuildFolder, GameName, version.SemVer)));

Task("Unity.Package.Windowsx64")
    .IsDependentOn("Unity.Build.Windowsx64")
    .Does(() => Zip(BuildFolder + "/Windowsx64", ReleaseFolder + String.Format("/Win64-{0}-{1}.zip", GameName, version.SemVer)));

Task("Unity.Package.Windowsx86")
    .IsDependentOn("Unity.Build.Windowsx86")
    .Does(() => Zip(BuildFolder + "/Windowsx86", ReleaseFolder + String.Format("/Win86-{0}-{1}.zip", GameName, version.SemVer)));

Task("Unity.Package.Linux")
    .IsDependentOn("Unity.Build.Linux")
    .Does(() => Zip(BuildFolder + "/Linux", ReleaseFolder + String.Format("/Linux-{0}-{1}.zip", GameName, version.SemVer)));

Task("Unity.Package.MacOS")
    .IsDependentOn("Unity.Build.MacOS")
    .Does(() => Zip(BuildFolder + "/MacOS", ReleaseFolder + String.Format("/MacOS-{0}-{1}.zip", GameName, version.SemVer)));

Task("Unity.Publish.CreateFolder")
    .Does(() => CleanDirectory(PublishPath + "/" + version.SemVer));

Task("Unity.Publish.Windowsx64")
    .IsDependentOn("Unity.Publish.CreateFolder")
    .IsDependentOn("Unity.Package.Windowsx64")
    .Does(() => CopyFile(ReleaseFolder + String.Format("/Win64-{0}-{1}.zip", GameName, version.SemVer), PublishPath + "/" + version.SemVer + String.Format("/Win64-{0}-{1}.zip", GameName, version.SemVer)));

Task("Unity.Publish.Windowsx86")
    .IsDependentOn("Unity.Publish.CreateFolder")
    .IsDependentOn("Unity.Package.Windowsx86")
    .Does(() => CopyFile(ReleaseFolder + String.Format("/Win86-{0}-{1}.zip", GameName, version.SemVer), PublishPath + "/" + version.SemVer + String.Format("/Win86-{0}-{1}.zip", GameName, version.SemVer)));

Task("Unity.Publish.Linux")
    .IsDependentOn("Unity.Publish.CreateFolder")
    .IsDependentOn("Unity.Package.Linux")
    .Does(() => CopyFile(ReleaseFolder + String.Format("/Linux-{0}-{1}.zip", GameName, version.SemVer), PublishPath + "/" + version.SemVer + String.Format("/Linux-{0}-{1}.zip", GameName, version.SemVer)));

Task("Unity.Publish.MacOS")
    .IsDependentOn("Unity.Publish.CreateFolder")
    .IsDependentOn("Unity.Package.MacOS")
    .Does(() => CopyFile(ReleaseFolder + String.Format("/MacOS-{0}-{1}.zip", GameName, version.SemVer), PublishPath + "/" + version.SemVer + String.Format("/MacOS-{0}-{1}.zip", GameName, version.SemVer)));

/*****************************************************************************************************
End of script
*****************************************************************************************************/
RunTarget(target);