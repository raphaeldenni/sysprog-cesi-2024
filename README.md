# System Programmation CESI Project

Little school project to show main disk storage usage in C# .NET named DiskChecker

- [1.0 (CLI)](https://github.com/raphaeldenni/sysprog-cesi-2024/releases/tag/v1.0.0)
- [2.0 (GUI + CLI)](https://github.com/raphaeldenni/sysprog-cesi-2024/releases/tag/v2.0.0)

## Documentation

### Installation

Go to [Releases](https://github.com/raphaeldenni/sysprog-cesi-2024/releases/) and check the last version. Just download the executable or zip corresponding to your OS and launch it. You can also clone the repo and build the executable from source.

### Diagrams

![Git](/images/git-workflow-v1.png)

### CLI

#### Usage

```sh
DiskChecker.exe [DISK-LETTER] [NUMBER-OF-SECONDS-FOR-EACH-CHECK]
```

Example:

```sh
DiskChecker.exe c 2
```

#### Structure

![Use case v1](/images/use-case-v1.png)

![Sequencies v1](/images/sequency-v1.png)

![Classes v1](/images/classes-v1.png)

### GUI (versions 2.0)

#### Usage

![DiskCheckerGUI](/images/disk-checker.png)

1. Disk Selection area :
 - List all available disks
 - Select one disk by clicking on it

2. Iterations seconds :
 - Default value to avoid problems
 - Write a number for the interval between to disk check

3. Run button :
 - Run the disk checker

4. Stop button :
 - Stop the disk checker

5. Display area :
 - Live log file entries

#### Structure

![Use case v2](/images/use-case-v2.png)

![Sequencies v2](/images/sequency-v2.png)

![Classes v2](/images/classes-v2.png)