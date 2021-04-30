## Introduction
  * standard admissible algorithm for cooperative pathfinding problems has a branching factor that is exponential in number of agents
  * LRA\*
  * HCA\*
  * other attempts establish a direction for every grid position and encourage or require each agent to move in that direction at every step. These methods reduce the chance of computing conflicting path by creating analog of traffic laws for agent to follow. But they are suboptimal and suffer from deadlock.

## The Standard Admissible Algorithm

## Operator Decomposition
  * in the standard algorithm, every operator advances a state by one timestep and change the position of every agent. We propose a new representation of the state space in which each timestep is divided so that agents are considered one at a time and a state requires n operator to advance to next timestep. In this representation, a state not only contain position for every agent, but also contains up to one move assignment for every agent.
  * an operator in this representation consist of assigning a move to the next unassigned agent in a fixed order
  * we call this technique A\* with operator decomposition (OD) because the standard operators are decomposed into a series of operators
