[![Contributors][contributors-shield]][contributors-url]
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](https://opensource.org/licenses/MIT)


<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#sprint-progress">Sprint Progress</a></li>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li>
      <a href="#usage">Usage</a>
      <ul>
        <li><a href="keyboard-controls">Keyboard Controls</a></li>
      </ul>
    </li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

## About the Project
This 2D game is a spinoff of a portion of the original *Legend of Zelda* game with some additional features.
This project was completed over the course of the AU21 semester for the CSE 3902 course at Ohio State.
My team of five other classmates and I worked in an Agile workflow and kept track of our sprint planning on
[ZenHub](https://www.zenhub.com/). Our goal was to use different design patterns to create the first dungeon
along with implementing unique features. 

Unique Features:
* Multiplayer
* Item Shop
* Players and enemies drop items

### Sprint Progress
* Sprint 1
    * Creation of game objects (Link, Enemies, Items, NPCs, Blocks) without collisions
    * Only one instance of each object type with controls to cycle through each object
    * Basic keyboard input (movment controls and leaving game)
* Sprint 2
    * Collisions and collision handling implemented, causing state transitions and position changes when necessary
    * Create start room that contains all items and enemies to test all possible collisions
    * Add all individual "rooms" each with its own subset of objects, blocks, and enemies stored in an XML file
* Sprint 3
    * Implement smooth room switch transitions using scrolling techniques instead of instant room switch
    * Add sound when different actions are performed as well as the background music
    * Add the HUD and inventory with scrolling when called for a smooth transition
    * Continue to add and update collision instances
* Sprint 4
    * Update game structure and controls to support multiplayer (theoretically an infinite amount of players can be supported)
    * Implement the item shop and currency system, enemies must drop currency, players can also drop their inventory items
    * Clean up rooms, fix bugs, and add different aspects like secret rooms

### Built With
* [Visual Studio](https://visualstudio.microsoft.com/)
* [C# Monogame](https://www.monogame.net/)
* [Microsoft XNA](https://www.microsoft.com/en-us/download/details.aspx?id=20914)



## Getting Started

### Installation
1. Download [Visual Studio](https://visualstudio.microsoft.com/)
2. Install the Monogame extension for Visual Studio and follow [these steps](https://docs.monogame.net/articles/getting_started/1_setting_up_your_development_environment_windows.html)
3. Clone the repo
    ```sh
      git clone https://github.com/schlys/Zelda-Project.git
    ```



## Usage
XML sheets are used for much of the data in the game. These sheets can be swapped out to create new levels, new rooms, 
change Sprite appearance for game objects, change sounds, use different controls, create new collisions or update existing,
and change certain positionings of items and projectiles. The sheets are stores in the `XMLData` folder and must follow the
existing format if they are changed.

### Keyboard Controls
* **Game Controls**
    * **`R`**: reset game to start screen, can be pressed at any time
    * **`Q`**: exits the game and closes the window, can be pressed at any time
* **Start Screen**
    * **`1`**: sets mode to single player
    * **`2`**: sets mode to multiplayer, only two player is supported
    * **`L`**: plays the back story of *Legend of Zelda*, can be exited by pressing **`R`**
    * **`X`**: starts the game
* **Gameplay**
    * **`Space`**: pauses the game and goes to pause menu, press **`Space`** again to continue
    * **`I`**: goes to inventory, press **`I`** again to go back to gameplay
    * **`X`**: exits the item shop
* **Player 1**
    * **`W`**, **`A`**, **`S`**, **`D`**: moves Link up, down, left, and right
    * **`1`**: uses first inventory item
    * **`2`**: uses second inventory item
    * **`G`**: drop first item
    * Item Selection
        * **`W`**, **`A`**, **`S`**, **`D`**: changes selected item
        * **`1`**: item to replace is first item
        * **`2`**: item to replace is second item
        * **`CapsLock`**: replace item with selected item
    * Item Shop
        * **`1`**: buys first item in the shop
        * **`2`**: buys second item in the shop
        * **`3`**: buys third item in the shop
* **Player 2**
    * **`Up`**, **`Down`**, **`Right`**, **`Left`**: moves Link up, down, left, and right
    * **`9`**: uses first inventroy item
    * **`0`**: uses second inventory item
    * **`H`**: drop second item
    * Item Selection
        * **`Up`**, **`Down`**, **`Left`**, **`Right`**: changes selected item
        * **`9`**: item to replace is first item
        * **`0`**: item to replace is second item
        * **`Enter`**: replace item with selected item
    * Item Shop
        * **`8`**: buys first item in the shop
        * **`9`**: buys second item in the shop
        * **`0`**: buys third item in the shop


## License
Distributed under the MIT License. See `LICENSE` for more information.



## Contact
Sam Chlystek - samchlystek20@gmail.com

Project Link: [https://github.com/schlys/Zelda-Game](https://github.com/schlys/Zelda-Game)



## Acknowledgments
* Creators: Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh
* [ZenHub](https://www.zenhub.com/)
* [Sprites](https://www.spriters-resource.com/nes/legendofzelda/) by Mister Mike
* [SoundEffects](https://www.ZeldaSounds.com) by HelpTheWretched
* [Background Music](https://archive.org/details/the-legend-of-zelda-nes-soundtrack) by LEMONADE
* [README Template](https://github.com/othneildrew/Best-README-Template)



[contributors-shield]: https://img.shields.io/github/contributors/schlys/Zelda-Game.svg?style=for-the-badge
[contributors-url]: https://github.com/schlys/Zelda-Game/graphs/contributors

