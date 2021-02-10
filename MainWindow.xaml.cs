using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    public partial class MainWindow : Window 
    {
        GameBoard gameBoard = new GameBoard();
        GameLogic gameLogic = new GameLogic();
        HumanPlayer humanPlayer = new HumanPlayer();
        AiPlayer aiPlayer = new AiPlayer();

        

        public MainWindow()
        {
            // init and center window
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            //set human player as first player
            humanPlayer.turn = true;
            gameLogic.sign = humanPlayer.sign;
        }

        

        public async void TogglePlayers()
        {
            if (humanPlayer.turn)
            {
                humanPlayer.turn = false;
                aiPlayer.turn = true;
                gameLogic.sign = aiPlayer.sign;
                await Task.Delay(300);
                PutSignOnBoard(aiPlayer.ExecuteAiTurn(gameBoard.fields));
            }
            else
            {
                aiPlayer.turn = false;
                humanPlayer.turn = true;
                gameLogic.sign = humanPlayer.sign;
            }
            
        }

        private void ShowWinningRow(int[] winningRow)
        {
            Flash(winningRow);
            for (int i = 0; i < winningRow.GetLength(0); i++)
            {
                string btnName = "Button" + (winningRow[i]+1).ToString();   //Adding 1 to the every winning field index as the array starts with 0
                
                Button btn = (Button)GameBoardGrid.FindName(btnName);

                btn.Background = Brushes.LightGreen;
            }
        }

        async void Flash(int[] winningRow)
        {
            string btn1Name = "Button" + (winningRow[0] + 1).ToString();
            string btn2Name = "Button" + (winningRow[1] + 1).ToString();
            string btn3Name = "Button" + (winningRow[2] + 1).ToString();

            Button btn1 = (Button)GameBoardGrid.FindName(btn1Name);
            Button btn2 = (Button)GameBoardGrid.FindName(btn2Name);
            Button btn3 = (Button)GameBoardGrid.FindName(btn3Name);

            for (int i = 0; i < 20; i++)
            {
                if (i%2 == 0)
                {
                    btn1.Background = Brushes.White;
                    btn2.Background = Brushes.White;
                    btn3.Background = Brushes.White;

                }
                else
                {
                    btn1.Background = Brushes.LightGreen;
                    btn2.Background = Brushes.LightGreen;
                    btn3.Background = Brushes.LightGreen;
                }
                await Task.Delay(50);
            }
        }

        public void PutSignOnBoard(string btnName)
        {
            Field field = gameBoard.fields.Find(item => item.btnName == btnName);
            field.occupied = true;
            field.occupiedBy = gameLogic.sign;

            Button btn = (Button)GameBoardGrid.FindName(btnName);
            btn.Content = gameLogic.sign;


            if (gameLogic.FullRowFound(gameBoard.fields))
                ShowWinningRow(gameLogic.winningRow);

            else if (!gameLogic.Draw(gameBoard.fields))
                TogglePlayers();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (!GameLogic.gameOver)
            {
                if (humanPlayer.turn && gameLogic.MoveAllowed(btn.Name, gameBoard.fields, gameLogic.sign))
                    PutSignOnBoard(btn.Name);
            }
        }

        private void New_Game_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < gameBoard.fields.Count; i++)
            {
                gameBoard.fields[i].occupied = false;
                gameBoard.fields[i].occupiedBy = "";
                Button btn = (Button)GameBoardGrid.FindName(gameBoard.fields[i].btnName);
                btn.Content = "";
                btn.Background = Brushes.LightGray;
            }

            humanPlayer.turn = true;
            aiPlayer.turn = false;
            gameLogic.sign = humanPlayer.sign;

            if (GameLogic.userStartedRound)
            {
                aiPlayer.turn = true;
                humanPlayer.turn = false;
                gameLogic.sign = aiPlayer.sign;
                GameLogic.userStartedRound = false;
                PutSignOnBoard(aiPlayer.ExecuteAiTurn(gameBoard.fields));
            }
            else
            {
                GameLogic.userStartedRound = true;
                humanPlayer.turn = true;
                aiPlayer.turn = false;
                gameLogic.sign = humanPlayer.sign;
            }

            GameLogic.gameOver = false;


        }
    }
}
