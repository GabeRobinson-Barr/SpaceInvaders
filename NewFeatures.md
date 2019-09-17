# SpaceInvaders
CIS 568 HW 2

New Features for Advanced Gameplay

Mystery ships that cross the top of the screen will sometimes drop powerups (small white balls) that you should try to collect.
If you collect powerups it changes the weapon equipped from normal to rapidfire then to beam and finally to rapid beam. Dying will reset your weapon to the default mode and getting a powerup while in rapid beam mode doesn't do anything.

Default - press once to fire a missile
Rapid Fire - firing rate increased, hold the button to continuously fire
Beam - press once to fire a laser beam that wipes out everything in its path
Rapid Beam - this combines rapidfire and beam powerups. Hold the button to continuously fire beams


Mobile Mode
I've included the mobile build for Android in the AndroidBuild folder.
The controls are as follows
Tap close to the left side of the screen - Move Left
Tap close to the right side of the screen - Move Right
Tap the bottom half of the screen - Fire
Tap using 3 fingers - Change Camera

Notes -
Moving left or right takes precedence over shooting.
The game can only be played in landscape mode on Android, I had trouble implementing a screen adjustment feature for when the game initializes in portrait. If the game initalizes in portrait quit the game, turn the phone or tablet to landscape mode, and restart the game.
