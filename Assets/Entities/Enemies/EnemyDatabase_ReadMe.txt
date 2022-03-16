//TDK443   
Enemies 'n Junk Readme

Making a new baddy 
A new enemy prefab needs a script that inharits the EnemyController script
This is located in Assets/Entities/Enemies/Scripts/EnemyController.cs
This also inharits the IEnemyControl interface which is vital for spawning, saving, and loading the Enemies
You are welcome to override any of the EnemyController.cs functions to meet your needs, so long as the interface is intact.


Making a new Behavior Entry
The Behavior Entries exist in the Assets/Entities/Enemies/Database file location
To make a new one R-Click->Create->Enemy Data-> Enemy Behavior Controller.
From here you can add in your stats for the Enemy, as well as your existing prefab for it.

On the bottom you'll notice the "Enemy Behaviors" section. Each of those can have a behavior file dragged in
that will change how the Enemy response to various triggers. The Normal Set of behaviors does nothing of interest.

Making a new Behavior from existing SO's
R-Click->Create->Enemy Data-> Enemy Action -> Whatever suits you.
This creates an empty version of the scripted action that may need to have a few variables configured before adding to a Behavior Entry

Scripting new Behaviors
Need to incorperate the base Behavior SO so that the controller can add and interact with it.
Here is an example of an ON HIT behavior that does nothing.
------------------------------------------------------------------------------------

[CreateAssetMenu(fileName = "OnHit Normal", menuName = "Enemy Data/Enemy Action/OnHit Normal")]
public class NormalEnemyOnHitSO : EnemyOnHitSO
{
    public override float onHit(float damage, GameObject source, GameObject thisObject)
    {
        return damage;
    }
}
------------------------------------------------------------------------------------
Note that the main call on it needs an override to replace the original function, and that the FILENAME and MENUNAME need to be updated 
accordingly so that you can make a new SO.

The "thisObject" pointer is passed forward to every behavior so that it's more pliable with whatever you need it to do.