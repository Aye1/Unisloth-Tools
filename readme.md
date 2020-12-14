# Readme
This package contains all tools which can be re-used on several projects. Feel free to import only what is useful to you.
## Tab Navigation
This element is composed of two scripts: TabGroup and TabButton.
When a TabButton is clicked, its associated GameObject is displayed, and all GameObjects associated to the TabButtons in the same TabGroup are hidden.
The TabGroup controls the visual of the TabButtons, you just have to give it the sprites for the Idle, Hovered and Selected states. Just had an Image child to the TabButtons which will be the icon displayed, which won't change whatever the state.

## Localization
### Localization v1
This module manages the localization of the game from a simple csv. Note that the csv must be in a Resources folder.
Remember to set the locale at the initialization. This locale may (and probably should) be saved in the PlayerPrefs.
The LocalizationManager needs a list of bindings between the locales in your csv and the SystemLanguage. These bindings are defined in a LocalesBindingScriptableObject.

### Localization v2
The new localization package is now directly integrated in Unity. You can use a LocalizationManager object to manage all your translations, both in editor and play mode.
All translations are stored in a TranslationsList ScriptableObject (Create/Unisloth/Localization/Translation List).
Some bugs may appear, due to the fact that translations are dynamically updated both in editor and play mode.

## Dialogs
This module creates a dialog box with old-fashioned text display (letter-by-letter).
## Alea
Simple helper to easily get random numbers
## Array2D
Base class to use bidimensionnal arrays and convert them from one format to another.
