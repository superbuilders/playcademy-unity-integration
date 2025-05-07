# Playcademy Manifest Exporter

This Unity package automatically generates a `cademy.manifest.json` file for WebGL builds. It also zips the build output and removes the loose manifest file from the original build directory.

## Features

- Auto-generates `cademy.manifest.json`.
- Includes Unity version and game name in the manifest.
- Zips the WebGL build into a `_playcademy_export.zip` file.
- Cleans up the original `cademy.manifest.json` from the build output folder.

## Installation

Install via the Unity Package Manager using the Git URL of the repository where this package is hosted.

## Usage

Once installed, the post-processor will automatically run after every WebGL build.
