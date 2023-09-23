using System.Collections.Generic;

namespace Tactile.TactileMatch3Challenge.Model {
    
    public class Board : IBoard {
        
        private Piece[,] boardState;
        private readonly IPieceSpawner pieceSpawner;

        public static Board Create(int[,] definition, IPieceSpawner pieceSpawner) {
            return new Board(definition, pieceSpawner);
        }
        
        public int Width {
            get { return boardState.GetLength(0); }
        }
        
        public int Height {
            get { return boardState.GetLength(1); }
        }
        
        public Board(int[,] definition, IPieceSpawner pieceSpawner) {
            
            this.pieceSpawner = pieceSpawner;

            var transposed = ArrayUtility.TransposeArray(definition);
            CreatePieces(transposed);
            
        }

        private void CreatePieces(int[,] array) {
            
            var defWidth = array.GetLength(0);
            var defHeight = array.GetLength(1);
            
            boardState = new Piece[defWidth,defHeight];
            
            for (int y = 0; y < defHeight; y++) {
                for (int x = 0; x < defWidth; x++) {
                    CreatePiece(array[x,y], x, y);
                }
            }
        }
        
        public Piece CreatePiece(int pieceType, int x, int y) { 
            var piece = new Piece(){type = pieceType};
            boardState[x, y] = piece;
            return piece;
        }
        
        public int[,] GetBoardStateAsArrayWithTypes() {
            
            var result = new int[Width, Height];
            
            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {
                    var p = boardState[x,y];
                    result[x, y] = p != null ? p.type : -1;
                }
            }

            return ArrayUtility.TransposeArray(result);
        }

        public ResolveResult Resolve(int x, int y) {
	        var swapped = FindAndRemoveConnectedAt(x, y);
            var changed = MoveAndCreatePiecesUntilFull();
	        return new ResolveResult(changed, swapped);
        }

        public Piece GetAt(int x, int y) {
            return boardState[x, y];
        }
        
        public IEnumerable<PiecePosition> IteratePieces() {
            for (var y = 0; y < Height; y++) {
                for (var x = 0; x < Width; x++) {
                    yield return new PiecePosition() {
                        piece = boardState[x, y],
                        pos = new BoardPos(x, y)
                    };
                }
            }
        }

        public void MovePiece(int fromX, int fromY, int toX, int toY) {
            boardState[toX, toY] = boardState[fromX, fromY];
            boardState[fromX, fromY] = null;
        }
        
        public bool IsWithinBounds(int x, int y) {
            
            if (x < Width && y < Height && x >= 0 && y >= 0) {
                return true;
            }
            return false;
        } 
        
        public void RemovePieceAt(int x, int y) {
            boardState[x, y] = null;
        }
        
        public bool TryGetPiecePos(Piece piece, out int px, out int py) {
               for (int y = 0; y < Height; y++) {
                   for (int x = 0; x < Width; x++) {
                       if (boardState[x, y] == piece) {
                           px = x;
                           py = y;
                           return true;
                       }
                   }
               }

               px = -1;
               py = -1;
               return false;
        }
        
        public List<Piece> GetConnected(int x, int y) {
            var start = GetAt(x, y);
            return SearchForConnected(start, new List<Piece>());
        }

        private List<Piece> SearchForConnected(Piece piece, List<Piece> searched) {
            int x, y;
            if (!TryGetPiecePos(piece, out x, out y)) {
                return searched;
            }

            searched.Add(piece);
            var neighbors = GetNeighbors(x,y);
            
            if (neighbors.Length == 0) {
                return searched;
            }

            for (int i = 0; i < neighbors.Length; i++) {
                
                var neighbor = neighbors[i];
                if (!searched.Contains(neighbor) && neighbor.type.Equals(piece.type)) {
                    SearchForConnected(neighbor, searched);
                }
            }

            return searched;
        }
        
        public Piece[] GetNeighbors(int x, int y) {

            var neighbors = new List<Piece>(4);
            
            neighbors = AddNeighbor(x - 1, y, neighbors); // Left
            neighbors = AddNeighbor(x, y - 1, neighbors); // Top
            neighbors = AddNeighbor(x + 1, y, neighbors); // Right
            neighbors = AddNeighbor(x, y + 1, neighbors); // Bottom

            return neighbors.ToArray();
        }
        
        private List<Piece> AddNeighbor(int x, int y, List<Piece> neighbors) {
            if (!IsWithinBounds(x, y)) return neighbors;
            
            neighbors.Add(GetAt(x,y));
            return neighbors;
        }
        
        public List<Piece> FindAndRemoveConnectedAt(int x, int y) {

			var connections = GetConnected(x, y);
			if (connections.Count > 1) {
				return RemovePieces(connections);
			}

            return new List<Piece>();
		}

		public Dictionary<Piece, ChangeInfo> MoveAndCreatePiecesUntilFull() {
			
			var result = new Dictionary<Piece, ChangeInfo>();
			
			int resolveStep = 0;
			bool moreToResolve = true;
			
			while (moreToResolve) {
				moreToResolve = MovePiecesOneDownIfAble(result);
				moreToResolve |= CreatePiecesAtTop(result, resolveStep);
				resolveStep++;
			}

			return result;
		}

		private List<Piece> RemovePieces(List<Piece> connections) {
            var result = new List<Piece>();

			foreach (var piece in connections) {
                if (TryGetPiecePos(piece, out int x, out int y))
                {
                    RemovePieceAt(x, y);
                    result.Add(piece);
                }
            }

            return result;
		}
		
		private bool CreatePiecesAtTop(Dictionary<Piece, ChangeInfo> created, int resolveStep) {
			var createdAnyPieces = false;
			var y = 0;
			for (int x = 0; x < Width; x++) {
				if (GetAt(x, y) == null) { 
					var piece = CreatePiece(pieceSpawner.CreateBasicPiece(), x,y);
					createdAnyPieces = true;
                    
					created[piece] = new ChangeInfo(){
						CreationTime = resolveStep,
						WasCreated = true,
						ToPos = new BoardPos(x,y),
						FromPos = new BoardPos(x,y-1)
					};
				}
			}

			return createdAnyPieces;
		}

		private bool MovePiecesOneDownIfAble(Dictionary<Piece, ChangeInfo> moved) {
			
			bool movedAny = false;
			
			for (int y = Height - 1; y >= 1; y--) {
				for (int x = 0; x < Width; x++) {
					
					var dest = GetAt(x, y);
					if (dest != null) {
						continue;
					}
					
					var pieceToMove = GetAt(x, y - 1);
					if (pieceToMove == null) {
						continue;
					}

					var fromX = x;
					var fromY = y - 1;
					MovePiece(fromX,fromY, x, y);
					movedAny = true;
					
					if(!moved.ContainsKey(pieceToMove)) {
						moved[pieceToMove] = new ChangeInfo{
                            FromPos = new BoardPos(fromX,fromY)
                        };
					};
					moved[pieceToMove].ToPos = new BoardPos(x,y);
					
				}
			}

			return movedAny;
		}
    }
}