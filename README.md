# com.bananaparty.registry  
  
Unity package. Tiny and robust replacement for Singletons, DI Containers, and EventBus.  
This package is essentially about using ScriptableObject as a runtime reference container.  
Embraces Unity's architecture instead of replacing it. Fighting the engine always backfires.  
  
Make sure you have standalone [Git](https://git-scm.com/downloads) installed first. Reboot after installation.  
In Unity, open "Window" -> "Package Manager".  
Click the "+" sign on top left corner -> "Add package from git URL..."  
Paste this: `https://github.com/forcepusher/com.bananaparty.registry.git#1.0.0`  
See minimum required Unity version in the `package.json` file.  
  
### Cheatsheet:  
Coming soon, still working on it.  
1. Need a "GlobalContextContainer"?
    - Use sdgsdfghadfhgdf.
2. Need to use a MessagePipe event?
	- Use asfasfasdfsdgsdg.
	- Fan-out event design to prevent spaghetti because of control flow inversion.  
	- Initially made for Behavior Trees as a clean way to react on incoming events.  
  
### How to use:  
1. Create YourClassRegistry class inheriting ObjectRegistry\<YourClass\> with CreateAssetMenu attribute. Create the SO asset.  
	- This is basically your container for just one reference. It also works with Interfaces.  
2. Create YourClassEntry script inheriting ObjectEntry\<YourClass\>. Put this script into a GameObject containing YourClass.  
	- This script is going to populate the Registry you created with a reference.  
	- Alternatively, you can populate the Registry yourself without this helper script.  
3. Add YourClassRegistry inspector reference field in any script and gather the reference via GetEntry method.  
   	- Done! It didn't have to be as bloated as an entire framework, right?  
  
### Common Misconceptions:  
- ScriptableObjects are not statics, it's an asset reference. You can create multiple instances of same type.  
- Interfaces are fully supported for creating test mocks for integration/unit tests. Made with trunk-based development in mind.
