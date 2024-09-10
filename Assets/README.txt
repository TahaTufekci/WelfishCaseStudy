3D TOP-DOWN SHOOTER

Overview
This game is a third-person shooter project that I designed. As a player, we try to kill enemies to progress through levels. While designing this game, my goal was to provide an immersive and thrilling combat experience.

Design Choices
1. Player Movement and Interaction
I designed the player movement by combining CharacterController and Rigidbody. This ensures that movement is both precise and smooth while retaining physical interactions. I used the HandleCameraSwitch function to allow the player to switch between normal and aiming camera modes, which enhances precision during combat.

Camera System:
The camera system was designed to allow the player to freely explore the environment and switch to a more precise mode when needed.
Camera transitions are managed through the GameManager, keeping the game state machine consistent and manageable.

2. Combat Mechanics
The core of the combat mechanics is based on shooting and enemy AI. The player can shoot at enemies while in aiming mode, which triggers a specific shooting animation. The shooting system responds quickly, and bullets are instantiated with a specific lifespan to optimize performance.

Enemy AI:
Enemies use NavMeshAgent to navigate towards the player and attack when within a certain range.
The design includes showing damage taken and playing a death sound effect to provide immediate feedback to the player.

3. Audio System
The audio system in the game plays a critical role in enhancing immersion. I implemented sound effects for actions like shooting, enemy attacks, and enemy deaths using AudioSource. By using multiple sound clips for each enemy, I aimed to make the combat experience more dynamic.

Audio Management:
To ensure the death sound plays fully, I triggered the sound effect before the destruction process. This design choice ensures that the sound is not cut off by the destruction of the enemy object.

4. Health Management
I designed a health system for both the player and enemies, visualized through a slider UI component. This visual representation allows players to easily track their health status and make more informed decisions during gameplay.

Player Health:
The enemy reduces the player's health when within a certain distance. This mechanic encourages players to manage distance and avoid overwhelming situations.

Enemy Health:
Enemies have a specific amount of health that decreases with each hit. When health reaches zero, the enemy dies after playing the death sound.

Challenges Faced
1. Audio Management
Ensuring that the enemy's death sound played fully before the object was destroyed was challenging. Initially, the sound would get cut off, but I resolved this by playing the sound before initiating the destruction process. This ensured the sound played completely.

2. Animation and State Management
Synchronizing animations with player actions and game states was quite challenging. I managed this by controlling aiming and shooting states through the GameManager, allowing me to control animations based on user input.

3. Enemy AI Behavior
Developing a responsive and challenging enemy AI was quite demanding. The enemies needed to approach the player and attack at a certain distance. I fine-tuned the NavMeshAgent parameters and attack range to achieve a balance that offers a fair yet challenging experience.

Conclusion
This game development process was both challenging and educational for me. By carefully making design choices and conducting iterative testing, I transformed the game into a cohesive and immersive experience. The final product is shaped by meticulously designed mechanics, polished visual and auditory elements, and a strong focus on player interaction.