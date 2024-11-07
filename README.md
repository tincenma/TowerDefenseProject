# Tower Defense Game

## Overview
This is a Tower Defense game built in Unity. The project features a variety of design patterns including Singleton, Factory Method, Decorator, Observer, State, and Facade, which contribute to maintaining the architecture of the code and enhancing the game's modularity and scalability.

## Features
- **Multiple Levels:** Players progress through a series of increasingly challenging levels.
- **Turret System:** Players can build, upgrade, and sell turrets to defend their territory.
- **Wave System:** Enemies spawn in waves, with different types of enemies to challenge the player.
- **Game States:** The game includes states for the menu, playing, and game-over scenarios.
- **Design Patterns:** The game uses several design patterns to improve maintainability.

## Design Patterns Implemented

### Creational Patterns
- **Singleton**: Used for managing instances of classes like `GameManager` and `BuildManager`. Ensures a single point of access.
- **Factory Method**: Utilized for creating turret instances, allowing easy extension of turret types.

### Structural Patterns
- **Decorator**: Used for adding functionalities to turrets. For instance, adding upgrades to a turret without modifying the base turret code.
- **Facade**: The `GameFacade` class provides a simplified interface to complex subsystems (e.g., `WaveSpawner`, `BuildManager`, `GameManager`), improving usability and reducing coupling between different parts of the code.

### Behavioral Patterns
- **Observer**: The `WaveSpawner` notifies the `GameManager` when specific events occur, such as all enemies being defeated in a wave.
- **State**: The game manages different states (`MenuState`, `PlayingState`, `GameOverState`) to handle the different phases of gameplay. This enhances clarity and control flow management.

## Project Structure
- **Scripts**: Contains all the source code for game mechanics.
  - **Controllers**: Contains core controllers like `GameManager`, `BuildManager`, `PauseMenu`, etc.
  - **Models**: Includes `IGameState` interface and different game states (`MenuState`, `GameOverState`, `PlayingState`), and also contains the `Enemy` and `Turret` classes.
  - **Views**: Handles UI elements like `SceneFader`.
  - **Utilities**: Contains helper scripts like `Waypoints`.
- **Prefabs**: Stores reusable game objects, including turrets and enemies.
- **Scenes**: Contains different levels and main menu.

## Installation and Setup
1. Clone the repository from GitHub:
   ```bash
   git clone <repository-url>
   ```
2. Open the project in Unity (2022.3.12f1 or higher).
3. Press **Play** in the Unity editor to test the game.

## How to Play
- Use **WASD** or move the mouse to the edges of the screen to move the camera.
- **Left Click** on a node to build a turret.
- Click **Retry** from the Pause Menu to restart the level.
- Win by surviving all enemy waves.

## Version Control and Collaboration
- Use Git to track changes in the project.
- Ensure commits are descriptive and reflect substantial changes to maintain clear version history.

## Assumptions and Limitations
- The game currently has 5 waves of enemies in each level.
- The camera control is limited to WASD keys and screen edges.
- Only basic enemy types are implemented, with room for further expansion.

## Authors and Acknowledgments
- Developed by **Yessirkegen Bexultan**.
- Special thanks to those who contributed to the implementation of design patterns, which helped in improving the overall code quality.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
