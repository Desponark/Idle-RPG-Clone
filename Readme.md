# Basics
Game Engine: Godot 4.1 mono  
Language used: C#  
IDE used: VS Code  
Status: Working & Finished requirements (but not finished everything planned)  

# Folder Structure
- Assets				- All graphical assets used
- Resources				- Pre-created custom resources
	- Abilities	
	- Attributes	
	- Items		
	- Passives
	- Loot.tres	
- Scenes
	- Enemies			- All enemy scenes
	- HUD				- ALL UI related scenes
	- Player			- Player scene
- Scripts
	- Agents			- Contains all agent related code
		- Enemies
		- Player
	- HUD				- All UI classes
	- Resources			- Mirrors Resources folder containing their classes
		- Abilities
		- Attributes
		- Items
		- Passives
		- Loot.cs
- Gamemaster.cs				- Main script that manages the game
- Gamemaster.tscn			- Main scene
- SaveData.tres				- Gets created upon death, keeps some info for next run

# The Most important scenes for editing the game are:
- Gamemaster
- Player
- Enemy	
	- All enemies inherit from this
- HUD.tscn
	- Contains the entire HUD (except small hp bars those are in enemy and player scene)

# Main Classes
- Gamemaster.cs  
	Handles the game itself, spawning enemies, moving enemies and the parallax background and updating game statistics.
- Agent.cs  
	This class contains almost all code needed for players and enemies to work. It handles stats, abilities, passives, whether an agent died, fighting, attribute increases, and exp and level gains.
- Stats.cs  
	Handles attributes (which are dynamically added via editor) and all derived stats. Only consists of fields and properties. Handles changing stats via getters.
- Enemy.cs  
	Inherits from agent, exp worth, Loot array & has dropLoot logic. Needs to be in a “Move” group and implement the Move function in order to be moved by the gamemaster.
- Player.cs  
	Inherits from agents and only has Inventory. Could be removed if Inventory was added to Agent.cs.
- Inventory.cs  
	Handles adding, removing and getting items.
- Ability.cs  
	An ability is something the player unlocks and then can use for an immediate effect. Can be upgraded to be stronger via RP.  
	Implements basic fields and functionalities for an ability in general. Is intended as a blueprint for creating new abilities by inheriting it and overriding / adding as needed. See Firebolt.cs for example.
- Attribute.cs  
	Attributes serve as a base to increase derived stats. Can be increased via AP.  
	Contains everything needed for creating basic attributes. Is intended as a blueprint. BaseValue is only ever modified by itself and Value can be modified by others. See Strength.cs for example derivative.
- Items.cs  
	Abstract base class for all items that can be used. Not intended to be used by itself. See HpPotion.cs for a derivative.
- Loot.cs  
	Holder for items in relation to a drop chance.
- Passives  
	Passives serve as unlock and upgradeable special effects. They are levelled up with RP.  
	Passives are split into 3 classes. Not completely finished implementation.
	- PassiveAttribute.cs  
		Attribute passives simply increase the selected attributes by an extra amount. Has an array of attributes that tells it which attributes it should modify.
	- PassiveEffect.cs  
		Effect passives trigger whenever an agent receives damage and can modify the resulting damage. Has an enum that tells it what to do if triggered.
	- PassiveProc.cs  
		Proc passives trigger whenever an agent attacks another agent. Has an enum that tells it what to do if triggered.
- SaveData.cs  
	Handles saving of player RP, Gamestatistics and the last 10 log messages so scene can simply be reloaded on player death.
- Logger.cs  
	- Handles writing Log messages to a RichtTextLabel displayed in the HUD. For convenience it is used via a static method. Like this Logger.Log(“text”).  
	- Internally it uses a static event to message itself the new message, adding it to the RichtTextLabel. Because it is impossible to get the raw text back from a RichtTextLabel there is a field that also saves all added text.  
	- Look in the Godot documentation for BBcode in order to see how messages can be formatted.
- Gamestatistics.cs  
	- Simply stores game statistics.


# Adding new content
## Enemies
In order to create a new Enemy:  
1. Top left -> Scene -> New Inherited Scene -> select Enemy.tscn
2. Adjust scene as wanted (use red sprite as orientation; remove its texture when no longer needed to hide)
3. Adjust stats as wanted on the Stats Node
4. It is even possible to give an enemy abilities, passives or attributes
	a. Attribute scaling and level scaling for enemies where planned but not implemented so far
5. If wanted add Loot
6. Save
7. When done go to the Gamemaster scene and find the EnemyScenes array and add the newly created enemy

## Attributes
In order to create a new Attribute:
1. simply create a new class that inherits from Attribute
2. override one or more of Attribute’s virtual methods to manipulate an agent's stats as wanted.
3. create a resource of the new attribute
4. In the player scene find the Stats node and add the new Attribute into the Attributes array.

## Inventory & Items & Potions
In order to create a new Item:
1. Create a new class that inherits from Item
2. Implement the Used event
3. Implement the Execute function with what your item should do when used
4. Create a  resource of the new item type
5. Optional
	a. In the player scene find the Inventory node and add the new item to have it at the start of the game
	b. Or create a new Loot resource and add your item there
		i. Don't forget to add this loot resource onto an enemy scene’s Loot array

## Loot
In order to create new Loot:
1. Simply create a new Loot resource
2. Add items into it and adjust Dropchance
3. Attach Loot resource to an enemy Loot array

## Abilities
In order to create a new ability:
1. Create a new class that inherits from Ability
2. Implement custom execute function that does what you want your ability to do
3. Create a resource from the new ability
4. In the player scene add the new resource to the player ability array

## PassiveAttributes
In order to create a new Attribute passive:
1. Create a new PassiveAttribute resource
2. Adjust the exported fields as needed
3. Add attribute resources that should be modified
4. In player scene add passive into the correct array

## PassiveEffects
In order to create a new Effect passive:
1. In the PassiveEffect class 
	a. Create a new enum entry
	b. Implement custom code that does what you need it to in the switch
2. Create a new PassiveEffect resource
3. Adjust the exported fields as needed & don't forget to select your new enum
4. In the player scene add the resource to the correct array

## PassiveProc
In order to create a new Proc passive:
1. In the PassiveProc class 
	a. Create a new enum entry
	b. Implement custom code that does what you need it to in the switch
2. Create a new PassiveProc resource
3. Adjust the exported fields as needed & don't forget to select your new enum
4. In the player scene add the resource to the correct array
