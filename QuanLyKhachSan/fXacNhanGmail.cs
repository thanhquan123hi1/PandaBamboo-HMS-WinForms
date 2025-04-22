using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessAccessLayer;
using System.Web.UI.WebControls;

namespace QuanLyKhachSan
{
    public partial class fXacNhanGmail : Form
    {
        public fXacNhanGmail()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string email = tBEmail.Text;

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            UserService  dangNhap= new UserService();

            if (!dangNhap.KiemTraMail(email))
            {
                MessageBox.Show("Tài khoản email không khả dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Bật TLS 1.2 (hoặc cao hơn)

            Random rand = new Random();
            string otp = rand.Next(100000, 999999).ToString();


            string senderEmail = "vuminhduc42705002@gmail.com";
            string receiverEmail = email.Trim();
            string appPassword = "lozqvrfspbcbxoqv"; // Nhớ đổi lại App Password


            MailMessage mail = new MailMessage();
            mail.To.Add(receiverEmail);
            mail.From = new MailAddress(senderEmail);
            mail.Subject = receiverEmail;
            mail.Body = otp;

            SmtpClient stmp = new SmtpClient("smtp.gmail.com");
            stmp.EnableSsl = true;
            stmp.Port = 587;
            stmp.DeliveryMethod = SmtpDeliveryMethod.Network;
            stmp.Credentials = new NetworkCredential(senderEmail, appPassword);

            try
            {
                stmp.Send(mail);
                MessageBox.Show("TC", "Email", MessageBoxButtons.OK, MessageBoxIcon.Information);

                fXacNhanOTP form3 = new fXacNhanOTP();
                form3.otpCode = otp;
                form3.gmail = email;
                form3.Show();  // Mở Form2
                this.Close();   // Ẩn Form1

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            fLogin fr1 = new fLogin();
            fr1.Show();
            this.Close();
        }
    }
}
