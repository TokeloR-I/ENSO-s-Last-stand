# ENSO-s-Last-stand
RPG game project
This script provides a solid foundation for your 2D side-scroller RPG player. To use it, you'll need to:

Create a new C# Script in your Unity project (e.g., PlayerController) and copy-paste the code into it.

Attach the PlayerController script to your player GameObject in Unity.

Add a Rigidbody2D component to your player GameObject (set its Body Type to Dynamic).

Add a Collider2D component (e.g., BoxCollider2D or CapsuleCollider2D) to your player.

Create an empty GameObject as a child of your player, name it GroundCheck, and drag it into the Ground Check slot in the Inspector of your PlayerController. Position it slightly below your player's feet.

Create another empty GameObject as a child, name it AttackPoint, and drag it into the Attack Point slot. Position it where you want your player's attacks to originate (e.g., slightly in front of the player).

Set up your groundLayer and enemyLayers. In Unity, go to Edit > Project Settings > Tags and Layers. You can define new layers there (e.g., "Ground", "Enemy") and then assign those layers to your ground and enemy GameObjects, and select them in the PlayerController script's Inspector.

Create an Animator component and set up basic animations like "Idle", "Run", "Jump", "Attack", and "Die" if you want visual feedback. Connect the parameters Speed, IsGrounded, Jump, Attack, and Die as used in the script.

Create a simple Enemy.cs script (as commented at the bottom of the provided code) and attach it to your enemy GameObjects.
