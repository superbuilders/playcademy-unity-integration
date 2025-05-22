# Playcademy Manifest Exporter

This Unity package automatically generates a `playcademy.manifest.json` file for WebGL builds. It also zips the build output and removes the loose manifest file from the original build directory.

## Features

- Auto-generates `playcademy.manifest.json`.
- Includes Unity version and game name in the manifest.
- Zips the WebGL build into a `_playcademy.zip` file.
- Cleans up the original `playcademy.manifest.json` from the build output folder.

## Installation

Install via the Unity Package Manager using the Git URL of the repository where this package is hosted.

## Usage

Once installed, the post-processor will automatically run after every WebGL build.
