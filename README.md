# Hunter-Vs-Prey
It’s a game where you play as the Hunter, aiming to catch the Prey before it collects all the randomly spawned targets. The environment includes agents, targets, and ray sensors for vision. Rewards and penalties apply to both the Prey and the Hunter. Python 3.8.10 is used for development. 
This project is a game where you play as the Hunter with the goal of catching the Prey in the shortest amount of turns. The game environment contains two agents: the Prey and the Hunter. Here are the key features:
Objective:
The Prey’s task is to collect all the randomly spawned targets in the environment.
The Hunter’s task is to catch the Prey before the Prey collects all the targets.
Environment Design:
When you press play, the environment starts, and the agent (Prey), Hunter, and targets are spawned randomly on the environment ground.
Both the agent and the Hunter are equipped with a ray sensor, which acts like their eyes.
Agent Rewards and Penalties:
Agent (Prey):
+10 when it collects a target.
-15 if it hits a wall.
-15 if the Hunter catches it.
+5 if the agent collects all targets.
Hunter:
+10 when it catches the agent.
-15 if it hits a wall.
-15 if the agent collects all targets.
