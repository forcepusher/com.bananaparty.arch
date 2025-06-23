# com.bananaparty.registry  
  
Unity package. Replacement for Singletons, DI Containers, and EventBus.  
  
Make sure you have standalone [Git](https://git-scm.com/downloads) installed first. Reboot after installation.  
In Unity, open "Window" -> "Package Manager".  
Click the "+" sign on top left corner -> "Add package from git URL..."  
Paste this: `https://github.com/forcepusher/com.bananaparty.registry.git#1.0.0`  
See minimum required Unity version in the `package.json` file.  
  
### Key notes:  
1. Tiny replacement for Zenject/VContainer, and MessagePipe.  
	- No overhead what so ever. Seven small scripts.  
2. Embraces Unity's architecture instead of replacing it.  
	- Fighting the engine design only makes things worse.  
3. Fan-out event design.  
	- Read events as they arrive without inverting the control flow.  
 	- Behavior Tree compatible and way less spaghetti.  
  
### How to use:  
1. Create YourClassRegistry class inheriting ObjectRegistry\<YourClass\> with CreateAssetMenu attribute. Create the SO asset.  
	- This is basically your container for just one reference. It also works with Interfaces.  
2. Create YourClassEntry script inheriting ObjectEntry\<YourClass\>. Put this script into a GameObject containing YourClass.  
	- This script is going to populate the Registry you created with a reference.  
	- Alternatively, you can populate the Registry yourself without this helper script.  
3. Add YourClassRegistry inspector reference field in any script and gather the reference via GetEntry method.  
   	- No more buggy static Singleton instances (however this package supports and endorses it, except the static part).  
   	- No more laggy DI Container framework bloatware.  
	- No more slow FindObjectOfType or FindObjectOfTypeAll.
  
### TL;DR:  
1. This package is essentially about putting the references you need into a ScriptableObject and use them across the project.  
	- ScriptableObjects are not statics, it's just an asset reference.  
 	- You can create multiple instances of same type and it's prefectly usable in integration tests.  
  	- Again, Interfaces are fully supported for creating test mocks.
