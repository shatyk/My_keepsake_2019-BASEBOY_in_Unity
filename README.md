A demo version of my mobile game, developed using the Unity engine.
I consider the main achievement of this demo project to be the development of an algorithm responsible for spawning objects that fly toward the player.

The complexity of this algorithm is due to several factors:
1. The camera gradually moves away from the player, so the spawn points are always changing.
2. The objects flying toward the player must always arrive in such a way that the player is guaranteed to deflect them, meaning they cannot arrive simultaneously (taking the animation duration into account).
3. The objects must spawn at random intervals and fly at random speeds.
4. To adjust the game’s difficulty, the algorithm must allow customization of the following parameters: cooldown between spawns, flight speed, and the degree of randomness for these characteristics, while considering the previous conditions.

Perhaps I’ll finish it someday, but for now, I’m leaving it here as a memory.

![baseboy](https://github.com/user-attachments/assets/c931840b-6053-4367-9f77-d39a3ae0235d)
