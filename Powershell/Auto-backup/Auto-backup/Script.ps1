# 
# The purpose of this script is to mirror the changes made to a directory on another existing directory
# It will try to avoid copying a file unless it can't be find in any subdirectories
#

if (Get-PSDrive -Name f) {
	exit 1
} else {
	$driveD = Get-ChildItem -path D:\Other\Torrent\Anime -File -recurse | Sort name | Select-Object Name
	$driveE = Get-ChildItem -path E:\Anime -File -Recurse -Name | Sort name | Select-Object Name
	$driveF = Get-ChildItem -path F:\ -file -Recurse -Name | Sort name | Select-Object Name
	exit 0
}

function backup {

}

#(Compare-Object -ReferenceObject (Get-ChildItem -path F:\ -File -recurse | Sort name | Select-Object Name) -DifferenceObject (Get-ChildItem -path D:\Other\Torrent\Anime -File -recurse | Sort name | Select-Object Name).InputObject