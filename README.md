[![Contributors][contributors-shield]][contributors-url]
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](https://opensource.org/licenses/MIT)


<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

## About the Project

### Built With
* [Visual Studio](https://visualstudio.microsoft.com/)
* [C# Monogame](https://www.monogame.net/)



## Getting Started

### Installation




## Usage




## License
Distributed under the MIT License. See `LICENSE` for more information.



## Contact
Sam Chlystek - samchlystek20@gmail.com

Project Link: [https://github.com/schlys/Zelda-Game](https://github.com/schlys/Zelda-Game)



## Acknowledgments
dafasdfsadfds

<!--
# Project
Names: Mulan Blum, Sam Chlystek, Jake Haskins, Chaeun Hong, Elise Kosmides and Andy Kroh. 

Course: CSE 3902 

# Project Planning 
We used ZenHub to track our planning and progress. You can find it below: 

https://app.zenhub.com/workspaces/3902-dream-team-613ffa4f2ca7870010eda966/board 

# Description 
This program is a replication of the legend of Zelda. 

Sprites sourced from https://www.spriters-resource.com/nes/legendofzelda/
Credit: Mister Mike

SoundEffects sourced from https://www.ZeldaSounds.com
Credit: HelpTheWretched

Background music sourced from https://archive.org/details/the-legend-of-zelda-nes-soundtrack
Credit: LEMONADE

# Sprint 2 
Submitted on 10/2/21

This was the intial creation of the basics of the Zelda game. We created Link, Items, Enemys, NPCs, and Blocks. The screen shows a single Link, Block, Enemy, and Item. The controls for the game at this time are as follow: 

Player controls
- Arrow and "wasd" keys should move Link and change his facing direction.
- The 'z' and 'n' key should cause Link to attack using his sword.
- Number keys (1, 2, 3, etc.) should be used to have Link use a different item 
- Use 'e' to cause Link to become damaged.

Block/obstacle controls
- Use keys "t" and "y" to cycle between which block is currently being shown (i.e. think of the obstacles as being in a list where the game's current obstacle is being drawn, "t" switches to the previous item and "y" switches to the next)

Item controls
- Use keys "u" and "i" to cycle between which item is currently being shown (i.e. think of the items as being in a list where the game's current item is being drawn, "u" switches to the previous item and "i" switches to the next)
- Items should move and animate as they do in the final game, but should not interact with any other objects

Enemy/NPC (other character) controls
- Use keys "o" and "p" to cycle between which enemy or npc is currently being shown (i.e. think of these characters as being in a list where the game's current character is being drawn, "o" switches to the previous item and "p" switches to the next) characters should move, animate, fire projectiles, etc. as they do in the final game. 

Other controls
- Use 'q' to quit and 'r' to reset the program back to its initial state.



# Sprint 3 
Submitted on 10/23/21

The continuation of the creation of the basics of the Zelda game. We created Link, Items, Enemys, Blocks, Rooms, and Collisions. 
- continue to develop more core features of the 2D game framework
- Implement collision handling for all types of collisions that can occur, causing state transitions or position changes when necessary.
- Individual "rooms"  of the dungeon each with its own subset of objects, blocks, and enemies. Stored this information in an xml file.
- Create an artificial level that contains an instance of Link and all types of objects that are found in your first dungeon. 
- Start room contains all enemies and items



The controls for the game at this time are as follow: 
Player controls
- Arrow and "wasd" keys should move Link and change his facing direction.
- The 'z' and 'n' key should cause Link to attack using his sword.
- Number keys (1, 2, 3, etc.) should be used to have Link use a different item 
- Use 'e' to cause Link to become damaged.

Block/obstacle controls
- Use keys "t" and "y" to cycle between which block is currently being shown (i.e. think of the obstacles as being in a list where the game's current obstacle is being drawn, "t" switches to the previous item and "y" switches to the next)

Item controls
- Use keys "u" and "i" to cycle between which item is currently being shown (i.e. think of the items as being in a list where the game's current item is being drawn, "u" switches to the previous item and "i" switches to the next)
- Items should move and animate as they do in the final game, but should not interact with any other objects

Enemy/NPC (other character) controls
- Use keys "o" and "p" to cycle between which enemy or npc is currently being shown (i.e. think of these characters as being in a list where the game's current character is being drawn, "o" switches to the previous item and "p" switches to the next) characters should move, animate, fire projectiles, etc. as they do in the final game. 

Room Switching controls 
- Left click to go to left room
- Right click to go to right room


Other controls
- Use 'q' to quit and 'r' to reset the program back to its initial state.





# Sprint 4

Submitted on 11/12/21

The continuation of the creation of the basics of the Zelda game. We created Link, Items, Enemys, Blocks, Rooms, and Collisions.

continue to develop more core features of the 2D game framework
Implement collision handling for all types of collisions that can occur, causing state transitions or position changes when necessary.
Individual "rooms" of the dungeon each with its own subset of objects, blocks, and enemies. Stored this information in an xml file.
Create an artificial level that contains an instance of Link and all types of objects that are found in your first dungeon.
Start room contains all enemies and items
Game states for start, pause, and end. 
Contains HUD
The controls for the game at this time are as follow: Player controls

Arrow and "up, down, left, right" keys should move Link and change his facing direction.
The 'z' and 'n' key should cause Link to attack using his sword.
Number keys (1, 2, 3, etc.) should be used to have Link use a different item
Use 'e' to cause Link to become damaged.
Block/obstacle controls

Use keys "t" and "y" to cycle between which block is currently being shown (i.e. think of the obstacles as being in a list where the game's current obstacle is being drawn, "t" switches to the previous item and "y" switches to the next)
Item controls

Use keys "u" and "i" to cycle between which item is currently being shown (i.e. think of the items as being in a list where the game's current item is being drawn, "u" switches to the previous item and "i" switches to the next)
Items should move and animate as they do in the final game, but should not interact with any other objects
Enemy/NPC (other character) controls

Use keys "o" and "p" to cycle between which enemy or npc is currently being shown (i.e. think of these characters as being in a list where the game's current character is being drawn, "o" switches to the previous item and "p" switches to the next) characters should move, animate, fire projectiles, etc. as they do in the final game.
Room Switching controls
Walk through door to switch room
Left click to go to left room
Right click to go to right room
Other controls
use space bar to pause
Use 'q' to quit and 'r' to reset the program back to its initial state.

# Sprint 5 

Submitted on 12/5/2021 

This was the final submission for the project. We extended out previous game to include multiplayer, a store, and item dropping. Two players can play at the same time, each with their own window. The windows reflect the unique color of the player to indicate which belongs to which player. When a player collides with the Old Man, a store window opens and they can purchase items for rupees. Enemies now drop items when they die. Link can drop items using the 'g' or 'h' key. 

Previous game functionality remains. 
-->

[contributors-shield]: https://img.shields.io/github/contributors/schlys/Zelda-Game.svg?style=for-the-badge
[contributors-url]: https://github.com/schlys/Zelda-Game/graphs/contributors

