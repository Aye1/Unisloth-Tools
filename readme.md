# Readme
This package contains all tools which can be re-used on several projects. Feel free to import only what is useful to you.
## Tab Navigation
This element is composed of two scripts: TabGroup and TabButton.
When a TabButton is clicked, its associated GameObject is displayed, and all GameObjects associated to the TabButtons in the same TabGroup are hidden.
The TabGroup controls the visual of the TabButtons, you just have to give it the sprites for the Idle, Hovered and Selected states. Just had an Image child to the TabButtons which will be the icon displayed, which won't change whatever the state.
## Localization
This module manages the localization of the game from a simple csv. Note that the csv must be in a Resources folder.
Remember to set the locale at the initialization. This locale may (and probably should) be saved in the PlayerPrefs.
The LocalizationManager needs a list of bindings between the locales in your csv and the SystemLanguage. These bindings are defined in a LocalesBindingScriptableObject.
## Dialogs
This module creates a dialog box with old-fashioned text display (letter-by-letter).
## Alea
Simple helper to easily get random numbers
