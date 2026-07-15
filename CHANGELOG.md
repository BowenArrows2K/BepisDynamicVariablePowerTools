# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Planned]

- Context Menu drop actions

## [Unreleased]

### Added

- Change text display on Protoflux Drives to show Variable names

### Changes

- Refactored rename dynamic variable sources, thank you [IHaveAName2653](https://github.com/IHaveAName2653)

## [1.0.4]

### Added

- Change text display on Protoflux Value sources to show Variable name
- Setting to toggle to change to text display on Value sources

## [1.0.3]

### Fixed

- TryGetLinkedSpace now gets DynamicVariableSpace.
- Open Inspector and Open Worker Inspector for Space now generates correctly. 

## [1.0.2]

### Added

- System.Reflections value refs for build compatibility
- Github Actions

### Removed

- Remora.Resonite.SDK no longer used

## [1.0.1]

### Changes

- New Icon

## [1.0.0]

### Added

- Rename Linked Dynamic Variable functionality.
- Rename Linked Variable from Dynamic Variable Space functionality.
- DebugInfo Output Linked Variables on Dynamic Variable Space functionality.
- DebugInfo Output Dynamic Variable Component Hierarchy on Dynamic Variable Space functionality.
- DebugInfo Pop-Out Text Display functionality.
- Mod_Enabled Setting this toggles the main UI Generation injection.
- ChangeProtoFluxStringInputs Setting this toggles the adjustment of String Inputs connected to Write/ReadDynamicVariable ProtoFlux nodes via the path connection.
- DebugInfo_Enabled setting toggles the DebugInfo Output UI generation on DynamicVariableSpaces.
- DebugInfo_LinkedVars setting that toggles the generation of the Output Variable Definitions buttonon DynamicVariableSpaces.
- DebugInfo_CompHierarchy setting that toggles the generation of the Output Component Hierarchy button on DynamicVariableSpaces.
- DebugInfo_OutputInPopoutUI setting that toggles between the Pop-out Text Display and Output StringField UI generation methods.