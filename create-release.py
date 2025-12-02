from pathlib import Path
import shutil
import os
import json

# Config
dotnet_version = "net10.0"
platform = "win-x64"
archive_extension = "zip"

# Get release name
with open("./version.json", "r") as file:
    release_name = json.load(file).get("version")

# Paths
root = str(Path(os.path.realpath()).parent)
folder_to_zip = f"{root}/bin/Release/{dotnet_version}/{platform}/publish"
zip_to_create = f"{root}/{release_name}"
file_to_not_zip = f"{folder_to_zip}/Notes.save"
temp_path_for_file_to_not_zip = f"{root}/temp/Notes.save"



os.system(f"cd {root} && dotnet publish -c release -r {platform} --self-contained")
if Path("temp").exists() == False: os.mkdir("temp")



if Path(folder_to_zip).exists():
    if Path(file_to_not_zip).exists(): shutil.move(file_to_not_zip, temp_path_for_file_to_not_zip)

    shutil.make_archive(zip_to_create, archive_extension, folder_to_zip)

    if Path(temp_path_for_file_to_not_zip).exists(): shutil.move(temp_path_for_file_to_not_zip, file_to_not_zip)

    print(f"Successfully created release at \"{zip_to_create}.{archive_extension}\""
        + "\nMake sure you have incremented the version from version.json, before running this script.")


else:
    print(f"Failed to create release."
            + f"\nThe folder \"{folder_to_zip}\" could not be archived because it doesn't exist."
            + "\nMaybe the automatic build command was incorrect."
        )