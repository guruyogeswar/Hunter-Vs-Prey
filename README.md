# Hunter Vs Prey AI

This project is a game where you play as the Hunter with the goal of catching the Prey in the shortest amount of turns. The game environment contains two agents: the Prey and the Hunter.

## Key Features

### Objective
- **Prey’s task**: Collect all the randomly spawned targets in the environment.
- **Hunter’s task**: Catch the Prey before the Prey collects all the targets.

### Environment Design
- Upon pressing play, the environment starts, and the agent (Prey), Hunter, and targets are spawned randomly on the ground.
- Both the agent and the Hunter are equipped with a ray sensor, which acts like their eyes.

### Agent Rewards and Penalties
- **Agent (Prey)**:
  - **+10** when it collects a target.
  - **-15** if it hits a wall.
  - **-15** if the Hunter catches it.
  - **+5** if the agent collects all targets.
- **Hunter**:
  - **+10** when it catches the agent.
  - **-15** if it hits a wall.
  - **-15** if the agent collects all targets.
