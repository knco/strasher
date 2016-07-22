# Strasher

## Introduction

A number of tools are available for generating checksums, however, if a user wants to save the checksums of the file or set of files for future verification, they would need to save it to a separate file or data-store since appending it to the file would alter the file data and defeat the original purpose of wanting to maintain the checksums. The problem this poses for many users is one of maintaining the checksum file. For instance, as files get added to or removed from a file set, the user would need to implement some form of version control around this file which contains the list of file checksums. Another example is that the user might move a file or even rename it, however, this could result in an out of sync checksums list which is maintained separately from the file(s). The Strasher system attempts to overcome these issues by utilizing features and functionalities provided by the filesystem - [file forks] (https://en.wikipedia.org/wiki/Fork_%28file_system%29), [extended file attributes] (https://en.wikipedia.org/wiki/Extended_file_attributes), and etc. - in order to decentralize the checksums list and maintain it along with each file. Currently, Strasher supports the NTFS filesystem, and utilizes NTFS Alternate Data Streams (ADS) to achieve its goal.

## Planned Enhancements

* Develop functionality to enable storing of additional file information (i.e. Modified Date) in file Strashes
* Create a Graphical User Interface and/or File Property Sheet Shell Extension for Strasher
* Data serialization to JSON and/or XML formats in addition to (or to replace) the current custom Strash serialization format
* Build a Windows Service to conduct periodic Strash generation, verification, and discrepancy notification for a user defined path
* Implement functionality to run the application using Command Line Arguments and Options
* Provide user with ability to backup file Strashes to a single file (Strash, JSON, and/or XML format) or to a data store
* In line with the above feature, add functionality granting user the ability to restore file Strashes from a backup
* Add functionality for reviewing, altering and/or deleting all other - other meaning non-Strash - Alternate Data Streams
* Create Unit Tests, also ideally conduct Benchmarking and Performance Testing
* Supply Comments in the Code; Add XML Documentation Comments
* Generate Software Documentation, preferably include Charts and Diagrams
* Implement functionality to enable Multithreaded file processing
* Equip application with functionality enabling the user to restore file dates based on the most recent and/or a user selected Strash
* Utilize the Strash store mechanism to also store File Recovery Data (possibly implementing an approach inspired by the Parchive system and the data it stores in par files)
* Analyze using Extended File Attributes (as opposed to file Forks) as a means for storing file Strashes in order to enable support for FAT32 filesystems
