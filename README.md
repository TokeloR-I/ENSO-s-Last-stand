# ENSO's Last Stand

**2D Side-Scroller RPG Game Project**

This script provides a solid foundation for your 2D side-scroller RPG player in Unity.

---

## Setup Instructions

Follow these steps to integrate the player script into your Unity project:

1. **Create the Script**
   - Create a new **C# Script** in your Unity project (e.g., `PlayerController`).
   - Copy and paste the provided code into the script.

2. **Attach to Player**
   - Attach the `PlayerController` script to your **Player GameObject**.

3. **Add Required Components**
   - **Rigidbody2D** component (set **Body Type** to `Dynamic`).
   - **Collider2D** component (e.g., `BoxCollider2D` or `CapsuleCollider2D`).

4. **Ground Check Setup**
   - Create an **empty GameObject** as a child of the player.
   - Name it `GroundCheck`.
   - Drag it into the **Ground Check** slot in the PlayerController Inspector.
   - Position it slightly below the player's feet.

5. **Attack Point Setup**
   - Create another **empty GameObject** as a child of the player.
   - Name it `AttackPoint`.
   - Drag it into the **Attack Point** slot.
   - Position it where attacks should originate (e.g., slightly in front of the player).

6. **Layer Configuration**
   - Go to **Edit > Project Settings > Tags and Layers**.
   - Define new layers (e.g., `"Ground"`, `"Enemy"`).
   - Assign these layers to relevant GameObjects.
   - Select them in the PlayerController script's Inspector.

7. **Animator Setup**
   - Create an **Animator** component.
   - Set up animations for:
     - `Idle`
     - `Run`
     - `Jump`
     - `Attack`
     - `Die`
   - Connect the following parameters as used in the script:
     - `Speed`
     - `IsGrounded`
     - `Jump`
     - `Attack`
     - `Die`

8. **Enemy Setup**
   - Create a simple `Enemy.cs` script (refer to the comments in the provided code).
   - Attach it to your enemy GameObjects.

---

## Notes
- Ensure your `groundLayer` and `enemyLayers` are set correctly to detect collisions and attacks.
- Position your `GroundCheck` and `AttackPoint` carefully for accurate detection.

---

## Example Folder Structure
