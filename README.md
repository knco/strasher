# Strasher

## Introduction

A number of tools are available for generating checksums, however, if a user wants to save the checksums of the file or set of files for future verification, they would need to save it to a separate file or data-store since appending it to the file would alter the file data and defeat the original purpose of wanting to maintain the checksums. The problem this poses for many users is one of maintaining the checksum file. For instance, as files get added to or removed from a file set, the user would need to implement some form of version control around this file which contains the list of file checksums. Another example is that the user might move a file or even rename it, however, this could result in an out of sync checksums list which is maintained separately from the file(s). The Strasher system attempts to overcome these issues by utilizing features and functionalities provided by the filesystem - [file forks] (https://en.wikipedia.org/wiki/Fork_%28file_system%29), [extended file attributes] (https://en.wikipedia.org/wiki/Extended_file_attributes), and etc. - in order to decentralize the checksums list and maintain it along with each file. Currently, Strasher supports the NTFS filesystem, and utilizes NTFS Alternate Data Streams (ADS) to achieve its goal.

## Planned Enhancements

