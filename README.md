# State Machine Prototype

A Unity 2D prototype focused on learning and experimenting with State Machine architecture for player controllers and enemy behavior.

The main goal of this project was to understand how a complex gameplay system can be divided into independent states and how those states can transition based on gameplay conditions.

This project is a learning prototype rather than a complete game.

## Play the Prototype

The prototype is available as a Web Build on itch.io.

**[Play the Prototype on itch.io](https://dotokafu.itch.io/state-machine-prototype)**

The Web Build allows you to try the prototype directly in your browser without downloading the project.

> **Note:** This is a prototype created for experimenting with State Machine architecture. It is not intended to be a complete game.

## State Machine

![State Machine Diagram](Documentation/FSM.png)

The project contains two main state machines:

- Player State Machine
- Enemy State Machine

### Player State Machine

The player controller is divided into several states based on the player's current behavior.

#### Grounded States

- Idle State
- Move State

#### Air States

- In Air State
- Wall Slide State

#### Ability States

- Jump State
- Wall Jump State
- Attack State
- Attack 2 State
- Air Attack State

#### Other States

- Damage State
- Dead State

### Enemy State Machine

The prototype also contains a separate state machine for enemy behavior.

Current enemy states include:

- Idle State
- Move State
- Charge State
- Attack State
- Player Detected State
- Dodge State
- Player In Range State
- Attack Player State

## What I Learned

### State Machine Architecture

- Understanding the basic structure of a Finite State Machine
- Creating individual states for different behaviors
- Managing the current state
- Switching between states based on gameplay conditions
- Organizing states into logical groups
- Separating behavior into smaller, more manageable classes

### State Transitions

- Creating conditions for state transitions
- Handling transitions between grounded and airborne states
- Handling wall-related transitions
- Triggering ability states from player input
- Understanding how multiple transition conditions can interact

### Player Movement

- Grounded movement
- Air movement
- Jumping
- Wall jumping
- Wall sliding
- Ground and wall detection
- Working with Rigidbody2D physics

### Abilities & Combat

- Creating separate states for abilities
- Separating movement states from ability states
- Implementing jump and wall jump behavior
- Creating separate attack states
- Handling different ground and air attacks
- Connecting attack states with animations

### Enemy State Machine

- Applying the State Machine pattern to enemy behavior
- Creating states for different enemy behaviors
- Handling player detection
- Creating attack and dodge states
- Designing transitions based on gameplay conditions

### Input Handling

- Separating player input from individual state logic
- Using input conditions to trigger ability states
- Managing actions such as jumping and attacking

### Animation Integration

- Connecting state behavior with Unity's Animator
- Updating animation parameters based on the current state
- Handling different animations for movement, jumping, attacking and taking damage

### Debugging State Machines

- Debugging unexpected state transitions
- Finding which conditions cause a state to activate
- Understanding how different states interact with each other
- Debugging timing-related behavior such as coyote time
- Identifying problems caused by multiple transition conditions

## Technologies

- Unity
- C#
- Unity Animator
- Unity 2D Physics
- State Pattern
- Finite State Machine

## Project Goals

The main goals of this prototype were:

- Learn how to implement a State Machine in Unity
- Understand state transitions
- Build a more modular player controller
- Experiment with ability and combat states
- Create an enemy state machine
- Improve my understanding of gameplay architecture

## Project Status

This project is a completed learning prototype.

The main focus was experimenting with State Machine architecture rather than creating a complete game.
