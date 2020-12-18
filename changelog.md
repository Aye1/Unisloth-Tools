# Changelog
All notable changes to this package are documented in this file. Please keep it updated.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/) and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.1.2] - 18-12-2020
### Added
- New attribute [TranslationKey] usable on a string property, which creates a dropdown with all available keys in the inspector

## [1.1.1] - 15-12-2020
### Changed
- Put both localization packages in namespaces to avoid conflicts
- Changed first localization package asmdef names

## [1.1.0] - 14-12-2020
### Added
- Array2D: base class to store 2D arrays and get them in different formats (WIP)
- New Localization Manager: store and edit translations directly in Unity

### Changed
- Starting to change the asmdef names to have a unique naming convention (Array2D and LocalizationV2 at the moment)

## [1.0.2] - 08-12-2020
### Added
- You can know visualize all translations in your CSV in the Unity Editor (menu Tools/Localization/Open Window)

### Changed
- Decoupling the CSV parsing and the localization management
- List of available locales (and their mappings to SystemLanguage) is now defined in a scriptable object


## [1.0.1] - 07-12-2020
### Added
- Alea script to manage randomness easily (int only at the moment)

## [1.0.0] - 04-06-2020
### Added
- Finally understood how Packages work, let's make it a real release!
- .meta files are back (so that we can update correctly from the Package Manager)

## [0.2.1] - 04-06-2020
### Removed
- Removed some unnecessary warnings

## [0.2.0] - 04-06-2020
### Added
- Dialogs
- Localization Manager

## [0.1.0] - 03-06-2020
### Added
- The package in available on Git!
- Bump version

## [0.0.1] - 03-06-2020
### Added
- Creation of the package
- Added the Tab Panel

[1.1.2]: https://github.com/Aye1/Unisloth-Tools/compare/v1.1.1...v1.1.2
[1.1.1]: https://github.com/Aye1/Unisloth-Tools/compare/v1.1.0...v1.1.1
[1.1.0]: https://github.com/Aye1/Unisloth-Tools/compare/v1.0.2...v1.1.0
[1.0.2]: https://github.com/Aye1/Unisloth-Tools/compare/v1.0.1...v1.0.2
[1.0.1]: https://github.com/Aye1/Unisloth-Tools/compare/v1.0.0...v1.0.1
[1.0.0]: https://github.com/Aye1/Unisloth-Tools/compare/v0.2.1...v1.0.0
[0.2.1]: https://github.com/Aye1/Unisloth-Tools/compare/v0.2.0...v0.2.1
[0.2.0]: https://github.com/Aye1/Unisloth-Tools/compare/v0.1.0...v0.2.0
[0.1.0]: https://github.com/Aye1/Unisloth-Tools/compare/v0.0.1...v0.1.0
[0.0.1]: https://github.com/Aye1/Unisloth-Tools/releases/tag/v0.0.1
