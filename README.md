# Match3Challenge

Assignment 1 - Add The Yellow Piece
So here the solution is pretty straightforward. 
1. Add sprite to PieceTypeDatabase. 
2. Move PieceTypeDatabase to Boot class and pass it to boardRenderer constructor as a dependency.
3. Add to PieceSpawner maxPiceCount form PieceTypeDatabase.

Assignment 2 - Fix A Bug
The reason of bug is compering: neighbor.type == piece.type in Board class (SearchForConnected).
Also Piece class is used in Dictionary as a key. Which is fine now because it’s class. But it might be a problem if we replaced it with struct. So for safety reasons it would be better to add uId private field. Increase static int nextUid. In constructor add its value to uId. And override Equals and get GetHashCode methods.

Assignment 3 - Add Animation To The Game Board
Use result of Resolve method in Board.cs. Next we can extend VisualPiece.cs. F.e. 
1. To create AnimatedVisualPiece inherited from VisualPiece. And use it everywhere in BoardRenderer.cs. 
2. Create AnimatedVisualPiece.cs component in addition to VisualPiece.cs. To implement animation in it.
3. Create Animator.cs. In which handles the whole animation stuff. 

I will go in direction of combination last 2.

Assignment 4 - Add Power Piece
1. Unify removing, creation and moving tiles.
2. In Board’s Resolve method make pipeline which mutates result.
3. Moved all game logic to new Strategies. And Board now is only cares about boardState.

Assignment 5 - Add Winning/Losing Conditions
1. To add Level.cs responsible for turns and Goals.
2. After every resolve ask Level.cs for conditions.
3. Add Win/Lose conditions to Level.cs.
