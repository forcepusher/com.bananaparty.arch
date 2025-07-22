# com.bananaparty.arch  
  
Unity package. Tiny and robust replacement for Singletons, DI Containers, and EventBus.  
This package uses ScriptableObjects as runtime reference holders to manage object references.  
Embraces Unity's architecture instead of replacing it. Fighting the engine always backfires.  
  
### Installation:  
Make sure you have standalone [Git](https://git-scm.com/downloads) installed first. Reboot after installation.  
In Unity, open "Window" -> "Package Manager".  
Click the "+" sign on top left corner -> "Add package from git URL..."  
Paste this: `https://github.com/forcepusher/com.bananaparty.arch.git#1.0.0`  
See minimum required Unity version in the `package.json` file.  
  
### Extended Architecture Manual:  
[Russian](https://github.com/forcepusher/Obsidian/blob/master/Arch/com.bananaparty.arch.docs.ru.md)  
[English (Auto Translate)](https://github-com.translate.goog/forcepusher/Obsidian/blob/master/Arch/com.bananaparty.arch.docs.ru.md?_x_tr_sl=ru&_x_tr_tl=en&_x_tr_hl=en&_x_tr_pto=wapp)  
  
### Cheatsheet:  
1. Need "FindObjectOfType"? Use the ReferenceAsset.  
	- First create a new class inheriting ReferenceAsset\<YourClass\> to create a ScriptableObject reference holder.  
	- Create a new class inheriting ReferenceSource\<YourClass\>, it's a script for populating the reference holder.  
	- Add the ReferenceSource script besides your component, create ReferenceAsset SO and put it in the script.  
	- Create an Inspector reference to your ReferenceAsset from the MonoBehavior script that requires it.  
2. Need "FindObjectOfTypeAll"? Use the ReferenceListAsset.  
	- Follow the same steps as with FindObjectOfType, but use a ReferenceListAsset\<YourClass\>.  
3. Need "SceneContext Container"? Use the ReferenceAsset.  
	- Follow the same steps as with FindObjectOfType.  
4. Need "GlobalContext Container"? Use the GlobalPrefabAsset.  
    - Create the GlobalPrefabAsset SO and put a prefab with scripts and/or singletons you need inside it.  
	- Then follow FindObjectOfType steps within a prefab to reference the global context.  
5. Need to use a "MessagePipe Event" or want an "EventBus"? Use the EventHubAsset.  
	- Create a class inheriting EventHubAsset\<YourEventPayloadClass\> to create an EventHub ScriptableObject.  
	- Reference it in your MonoBehavior scripts just like you would with ReferenceAsset.  
	- Subscribe to it to receive events to the EventQueue the subscription returns.  
	- Read EventQueue manually in an Update. This [Fan-out design](https://en.wikipedia.org/wiki/Fan-out_(software)) prevents control flow inversion spaghetti. Thus simplifies debugging and gives opportunity to use events in Behavior Trees and multuthreading.  
6. Bonus: Use a SceneReference for drag-and-drop integration of the Unity Scene asset.  
  
### Common Misconceptions:  
- ScriptableObjects are not static; they are asset references. You can create multiple instances of the same type.  
- Interfaces are fully supported for creating Unit/Integration Test mocks. Just use \<IYourInterface\> instead of \<YourClass\>.
  
## Development Repo and Sample Project:  
See at https://github.com/forcepusher/BananaArchDevRepo/tree/master/Assets/Source
