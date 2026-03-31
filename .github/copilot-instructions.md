# GitHub Copilot Instructions for Plant Growth Sync (Continued) Mod

## Mod Overview and Purpose
The **Plant Growth Sync (Continued)** mod for RimWorld is designed to enhance how crops grow in your colony's fields. The mod synchronizes crop growth across your designated growing zones, aiming to have them mature simultaneously. This ensures a more predictable and manageable harvest cycle, particularly useful when planning resources for your colony's survival.

## Key Features and Systems
- **Growth Synchronization**: Crops in your growing zones are averaged out in terms of growth percentage to mature at the same time, leading to a synchronized harvest.
- **Customization through Mod Settings**: Includes a settings menu that allows you to adjust specific parameters such as:
  - **Plant Growth Amount**: Dictates the degree to which each plant's growth is averaged.
  - **Growth Sync Time**: Configures how frequently the synchronization process occurs.
- **User Control**: Offers flexibility to tailor the synchronization features according to your gameplay style.

## Coding Patterns and Conventions
- The mod uses typical C# conventions and structures relevant to RimWorld modding:
  - Classes and methods are laid out to align with RimWorld's modding practices.
  - Public classes such as `MapComponent_GrowthSync`, `PGSMod`, and `PGSModSettings` are employed to handle mod initialization and synchronization logic.

## XML Integration
- XML is not directly described in the summary, but consider leveraging XML files for:
  - Defining default settings.
  - Integrating new plant types or modifying existing ones to be compatible with your synchronization logic.
  - Localization data to accommodate custom text that appears in-game or in mod settings.

## Harmony Patching
- Utilize Harmony patches to override or extend base game methods, ensuring:
  - Non-invasive compatibility with RimWorld updates and other mods.
  - Clean implementation of your synchronization logic without altering core game files.

## Suggestions for Copilot
- **Code Documentation**: Ensure that all public methods and classes are well-documented to aid in future development and support.
  - Use XML comments (`///`) to outline the purpose and parameters of methods.
- **Modular Code Structure**: Encourage splitting logic into smaller, reusable methods to facilitate easier maintenance and enhancement.
- **Logging and Error Handling**: Integrate robust logging to assist with troubleshooting and user support.
- **Testing and Debugging**: Encourage testing with only this mod active initially to isolate issues, then introduce other mods to ensure compatibility.
- **User Contributions**: Provide pathways for community contributions by suggesting places in the code where enhancements or bug fixes can be applied.

By adhering to these conventions and utilizing these systems, you will create a mod that is not only effective but also maintainable and accessible for future development and community involvement.
