Start scene file: StartMenu

How to play:
Navigate left and right with the arrow keys or a and d.
Press the space bar to jump and the s key to roll.
Roll under the bars and jump over the blocks.
Collect the coins to unlock the next level at the end of the road.

Technology requirements to observe:
Level progression: collect required # of coins in Level 1 to load Level 2 scene (handled in PlayerHit.cs)
Level 2 increased difficulty: faster obstacle speed and different obstacle arrangements than Level 1
Pause and End Menus: test pause functionality mid-run and review end screen when game over
Character animations: watch Bulldog and Buzz character models and animations during gameplay
Background and environmental objects: note background and decorative objects while running

Known problem areas:
Level 2 is very difficult to complete.
Buzz animations need to be refined, especially the ducking animation which is currently hard to see/use.
More levels need to be added.

Assets Implemented:
	Liam Smith - Pause Menu, End Menu, All music and sound effects.
	Lauren Schmidt -  Background objects, Bulldog model and animation, Buzz model
	Kate - Level 2 scene (duplicated and modified from Level 1 with different obstacles and increased speed)
	Joseph - Start Menu, Bulldog NavMesh AI, Bulldog chase/celebrate states
	Brandon - Ground, Movement (Run, jump, roll, strafe), Obstacle Prefabs, Interaction/ Slowdown system, Coin-gated walls


C# Files and Contributors:
	BulldogAI.cs - Joseph Hanna
	EndMenu.cs - Liam Smith
	ExitButton.cs - Joseph Hanna
	FollowCam.cs - Brandon Kim
	InfiniteGround.cs - Brandon Kim
	Obstacle.cs - Lauren Schmidt
	PauseManager.cs - Liam Smith
	PlayerHit.cs - Liam Smith, Kate Jeong, Joseph Hanna, Brandon Kim
	ReturnToMenuButton.cs - Liam Smith
	RunnerController.cs - Liam Smith, Brandon Kim
	StartButton.cs - Liam Smith, Joseph Hanna
	StartMenu.cs - Liam Smith, Joseph Hanna
	CoinGate.cs - Brandon Kim

