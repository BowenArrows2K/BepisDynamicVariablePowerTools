# Bepis Dynamic Variable Power Tools
[![Build](https://github.com/BowenArrows2K/BepisDynamicVariablePowerTools/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/BowenArrows2K/BepisDynamicVariablePowerTools/actions/workflows/build.yml)


[![Thunderstore Badge](https://modding.resonite.net/assets/available-on-thunderstore.svg)](https://thunderstore.io/c/resonite/p/BowenArrows/BepisDynamicVariablePowerTools/)


A [Resonite](https://resonite.com/) mod that adds a variety of powerful functions to dynamic variables and their spaces.
This mod is a port of and heavily inspired by [Dynamic Variable Power Tools](https://github.com/ResoniteModdingGroup/DynamicVariablePowerTools)

## Installation (Manual)
1. Install [BepisLoader](https://github.com/ResoniteModding/BepisLoader) for Resonite.
2. Download the latest release ZIP file (e.g., `BowenArrows-BepisDynamicVariablePowerTools-1.X.X.zip`) from the [Releases](https://github.com/BowenArrows2K/BepisDynamicVariablePowerTools/releases) page.
3. Extract the ZIP and copy the `plugins` folder to your BepInEx folder in your Resonite installation directory:
   - **Default location:** `C:\Program Files (x86)\Steam\steamapps\common\Resonite\BepInEx\`
4. Start the game. If you want to verify that the mod is working you can check your BepInEx logs.

## Features

- Expanded Component UI for Dynamic Variable Spaces and all Dynamic Variable Types
	- Dynamic Variable Space:
		- Rename UI to apply a new space name to all linked dynamic variable under the variable space.
		- Debug Info generation buttons:
			- Output Variable Definitions: This outputs a list of all defined variables under a space.
			- Output Component Hierarchy: This outputs a space tree of all linked dynamic variable components under the dynamic variable space.
	- Dynamic Variables:
		- Linked Space Name display with OpenInspector and OpenWorkInspector buttons (Similar to SyncRef fields.)
		- Rename UI to apply a new name to all linked Dynamic Variable components.
- Protoflux Node_UI generation:
	- Source: If a dynamic variable field is used as a source in ProtoFlux the ui will include the dynamic variable name.

[Open Changlog](/CHANGELOG.md)