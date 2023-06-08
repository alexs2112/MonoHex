# MonoHex
* A simple 4X strategy game on a small hex map using the MonoGame library.

![Screenshot](https://i.imgur.com/1pNMCwq.png)

# Design
* The key is simple deterministic combat and a condensed amount of content to prevent feature creep and untested complexity.

### **Main Idea**
* The game is run over a series of turns. Each turn has two phases: *Empire* and *Military*.
* Broadly, the *Empire* phase handles resource production and constructing Buildings and Units. The *Military* phase handles moving units around the map and resolving combat, done on an initiative system where faster units move before slower units regardless of the empire they belong to.

### **Empire Phase**
**Empire Upkeep**:
* First any constructed Buildings.
    * Constructed buildings also produce Gold as taxes. Buildings that are in territory that is separated from the Capital do not produce taxes.
    * Buildings that produce resources will produce extra if they have a Villager assigned to work them.
* Buildings with units or upgrades in their build queue will work on producing those things. 
    * Villagers on a tile containing a building under construction will work on the building instead of producing resources
* Then existing units will consume Food. Military units are also paid their wages in Gold
    * If you do not have enough Food and Gold, all of it is consumed and you suffer an empire-wide debuff on units and buildings depending on what is missing.

**Empire Management**:
* After the empire upkeep, the player can select any of their constructed buildings to add units or upgrades to their build queue, spending resources to do so.
* Villagers can be selected in this phase. This allows you to move them and use them to start a new construction.
    * Villagers can only move within friendly territory.

**Resources**:
* Food: Used to build units and feed them. Being unable to feed your units causes an empire-wide debuff
* Material: Used by Villagers to construct buildings
* Gold: Used to pay military units. Produced by buildings in the form of taxes. Used to buy other resources and mercenaries in the Marketplace.
* Crystal: Used to upgrade individual buildings or units
* Population Cap: Restricts the number of units that can be built, this is increased by building houses

**World Tiles**:
* Plains: Can construct Farms to produce Food
* Forest: Can construct a Lodge to produce both Material and little Food
* Rocks: Can construct a Mine to produce Material
* Crystal: Can construct a Mine to produce Crystal
* Mountains: Units cannot enter this tile

### **Military Phase**
* **Unit Stats**: Health, Armor, Damage, Speed, Initiative, Range
    * When Health hits 0 the unit dies
    * Armor is virtually health but it regenerates in the upkeep phase if they are within friendly territory
    * Damage reduces enemy Armor and Health
    * Speed determines how many tiles a unit can move
    * Initiative determines which unit is moved first in the Military phase. This is increased by the units speed and if they are in friendly territory
        * Need a good way to resolve ties, ideally ties shouldn't be happening very often
    * Range determines the attack range
* Unit stats should be kept relatively low for fast combat
* Each tile can hold up to one unit. To attack another unit, you must be in range.
    * Combat is deterministic, the attacker deals damage to the defender. If the attacker kills the defender, they move into their tile.
