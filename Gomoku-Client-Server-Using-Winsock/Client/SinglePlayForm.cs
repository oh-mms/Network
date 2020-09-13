
// 이 프로그램은 제가 작성한 코드 및 로직이 아닙니다.
// 나동빈(GitHub : ndb796)님의 강의에서 클론 코딩을 통해 연습한 자료입니다.
// 개인적인 TIL 연습물, 참고 자료용 등의 목적으로 커밋된 자료입니다.

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class SinglePlayForm : Form
    {
        private const int rectSize = 33;             // 오목판의 셀 크기
        private const int edgeCount = 15;       // 오목판의 선 갯수

        private enum Horse { none = 0, BLACK, WHITE };      // 오목판이 비어있다면 none 값을 가짐.
        private Horse[,] board = new Horse[edgeCount, edgeCount];
        private Horse nowPlayer = Horse.BLACK;

        private bool playing = false;       // 게임이 진행중인지 확인.

        public SinglePlayForm()
        {
            InitializeComponent();
        }

        private bool judge()    // 승리 판정 함수
        {
            for (int i = 0; i < edgeCount - 4; i++)     // 가로
                for (int j = 0; j < edgeCount; j++)
                    if (board[i, j]       == nowPlayer  &&
                        board[i + 1, j] == nowPlayer  &&
                        board[i + 2, j] == nowPlayer  &&
                        board[i + 3, j] == nowPlayer  &&
                        board[i + 4, j] == nowPlayer)
                        return true;

            for (int i = 0; i < edgeCount; i++)     // 세로
                for (int j = 4; j < edgeCount; j++)
                    if (board[i, j]      == nowPlayer  &&
                        board[i, j - 1] == nowPlayer  &&
                        board[i, j - 2] == nowPlayer  &&
                        board[i, j - 3] == nowPlayer  &&
                        board[i, j - 4] == nowPlayer)
                        return true;

            for (int i = 0; i < edgeCount - 4; i++)     // Y = X 직선
                for (int j = 0; j < edgeCount - 4; j++)
                    if (board[i, j]              == nowPlayer  &&
                        board[i + 1, j + 1] == nowPlayer  &&
                        board[i + 2, j + 2] == nowPlayer  &&
                        board[i + 3, j + 3] == nowPlayer  &&
                        board[i + 4, j + 4] == nowPlayer)
                        return true;

            for (int i = 4; i < edgeCount; i++)     // Y = -X 직선
                for (int j = 0; j < edgeCount - 4; j++)
                    if (board[i, j]            == nowPlayer  &&
                        board[i - 1, j + 1] == nowPlayer  &&
                        board[i - 2, j + 2] == nowPlayer  &&
                        board[i - 3, j + 3] == nowPlayer  &&
                        board[i - 4, j + 4] == nowPlayer)
                        return true;

            return false;
        }

        private void refresh()
        {
            this.boardPicture.Refresh();    // Picture인 boardPicture에 렌더된 이미지를 모두 지우고 다시 Paint()를 수행.
            for (int i = 0; i < edgeCount; i++)
                for (int j = 0; j < edgeCount; j++)
                    board[i, j] = Horse.none;
        }

        private void boardPicture_MouseDown(object sender, MouseEventArgs e)
        {
            if (!playing)
            {
                MessageBox.Show("게임을 실행해주세요.");
                return;
            }

            Graphics g = this.boardPicture.CreateGraphics();

            // boardPicture 기준으로 마우스의 좌표가 생성되므로, 셀 크기만큼 나누어 오목판의 교점 좌표를 도출.
            int x = e.X / rectSize;
            int y = e.Y / rectSize;

            if (x < 0 || y < 0 || x >= edgeCount || y >= edgeCount)
            {
                MessageBox.Show("테두리를 벗어날 수 없습니다.");
                return;
            }

            if (board[x, y] != Horse.none) return;
            board[x, y] = nowPlayer;

            if (nowPlayer == Horse.BLACK)
            {
                SolidBrush brush = new SolidBrush(Color.Black);
                g.FillEllipse(brush, x * rectSize, y * rectSize, rectSize, rectSize);
            }
            else {
                SolidBrush brush = new SolidBrush(Color.White);
                g.FillEllipse(brush, x * rectSize, y * rectSize, rectSize, rectSize);
            }

            // 게임이 끝나는지 판정.
            if (judge())
            {
                status.Text = nowPlayer.ToString() + "플레이어가 승리했습니다!";
                playing = false;
                playButton.Text = "게임시작";
            }
            else
            {
                nowPlayer = ((nowPlayer == Horse.BLACK) ? Horse.WHITE : Horse.BLACK);
                status.Text = nowPlayer.ToString() + " 플레이어의 차례입니다.";
            }
        }

        private void boardPicture_Paint(object sender, PaintEventArgs e)
        {
            Graphics gp = e.Graphics;
            Color lineColor = Color.Black;
            Pen p = new Pen(lineColor, 2);

            // 오목판 테두리를 그린다.
            gp.DrawLine(p, rectSize / 2, rectSize / 2, rectSize / 2, rectSize * edgeCount - rectSize / 2);
            gp.DrawLine(p, rectSize / 2, rectSize / 2, rectSize * edgeCount - rectSize / 2, rectSize / 2);
            gp.DrawLine(p, rectSize / 2, rectSize * edgeCount - rectSize / 2, rectSize * edgeCount - rectSize / 2, rectSize * edgeCount - rectSize / 2);
            gp.DrawLine(p, rectSize * edgeCount - rectSize / 2, rectSize / 2, rectSize * edgeCount - rectSize / 2, rectSize * edgeCount - rectSize / 2);

            // 내부 선을 그려준다.
            p = new Pen(lineColor, 1);
            for (int i = rectSize + rectSize / 2; i < rectSize * edgeCount - rectSize / 2; i += rectSize)
            {
                gp.DrawLine(p, rectSize / 2, i, rectSize * edgeCount - rectSize / 2, i);
                gp.DrawLine(p, i, rectSize / 2 , i, rectSize * edgeCount - rectSize / 2);
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (!playing)
            {
                refresh();
                playing = true;
                playButton.Text = "재시작";
                status.Text = nowPlayer.ToString() + " 플레이어의 차례입니다.";
            }
            else
            {
                refresh();
                status.Text = "게임이 재시작되었습니다.";
            }
        }
    }
}
