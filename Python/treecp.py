import argparse
import os

#todo Finish the argument parser thingy
def parse():
    parser = argparse.ArgumentParser(prog="TreeCopy",
                                     usage="Mirrors the tree of a directory on an another directory",
                                     description="The script will try to find existing files based on name "
                                                 "comparison and will look in subdirectories",
                                     epilog="License: Revised BSD license",
                                     prefix_chars="-",
                                     add_help="true")
    parser.add_argument(dest=1, help="")
    parser.add_argument("-f", "--force", action="store_false", help="pass this argument with -w or --overwrite to "
                                                                    "prevent asking the action to take for each files")


#parse()
#parser.add_argument()

#copy("", "")
root = os.walk("D:/Downloads/ISO")
for abc, dfe, ghi in root:
    print abc, dfe
