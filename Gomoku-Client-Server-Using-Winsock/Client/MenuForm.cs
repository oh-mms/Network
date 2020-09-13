
// 이 프로그램은 제가 작성한 코드 및 로직이 아닙니다.
// 나동빈(GitHub : ndb796)님의 강의에서 클론 코딩을 통해 연습한 자료입니다.
// 개인적인 TIL 연습물, 참고 자료용 등의 목적으로 커밋된 자료입니다.

using System;
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
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void singlePlayButton_Click(object sender, EventArgs e)
        {
            Hide();
            
            SinglePlayForm singlePlayForm = new SinglePlayForm();

            // 폼이 닫히면 이벤트 핸들러로 등록된 childForm_Closed를 실행시킴. 즉, 현재 Menu Form을 Show() 해줌.
            singlePlayForm.FormClosed += new FormClosedEventHandler(childForm_Closed);
            
            singlePlayForm.Show();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        void childForm_Closed(object sender, FormClosedEventArgs e)
        {
            Show();
        }

        private void multiPlayButton_Click(object sender, EventArgs e)
        {
            Hide();

            MultiPlayForm multiPlayForm = new MultiPlayForm();

            // 폼이 닫히면 이벤트 핸들러로 등록된 childForm_Closed를 실행시킴. 즉, 현재 Menu Form을 Show() 해줌.
            multiPlayForm.FormClosed += new FormClosedEventHandler(childForm_Closed);

            multiPlayForm.Show();
        }
    }
}
