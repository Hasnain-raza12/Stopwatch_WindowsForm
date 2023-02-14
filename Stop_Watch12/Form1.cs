using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stop_Watch12
{
    public partial class Form1 : Form
    {
        // declare some attributes
        int Hour;
        int Minutes;
        int Seconds;
      //  bool stop = true;
        private Thread stopwatchThread;
        public Form1()
        {
            InitializeComponent();
            //Initializing attribute
            Hour = 0;
            Minutes = 0;
            Seconds = 0;
            Stop.Enabled = false;
            Reset.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Start_Click(object sender, EventArgs e)
        {

            Reset.Enabled = true;
            Stop.Enabled = true;
            stopwatchThread = new Thread(DoStart);
            stopwatchThread.IsBackground = true; // BackGround Thread
            stopwatchThread.Start(); //stopwatch Starting Thread
            Start.Enabled = false;

        }

       // Append zero so it gonna look better
        private string appendZero(double str)
        {
            try   
            {
                if (str <= 9)
                {
                    return "0" + str;
                }
                else
                {
                    return str.ToString();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        // Define Delegates
        private delegate void DisplaySecondDelegate(int i);
        private delegate void DisplayMinutesDelegate(int minutes);
        private delegate void DisplayHousrDelegate(int i);
        private delegate void EnableButtonDelegate();

        //
        private void EnableButton()
        {
            Start.Enabled = true;
        }   
        private void DisplaySecond(int i)
        {
            lblSeconds.Text = appendZero(i);
        }
        private void DisplayMinute(int Minutes)
        {
            lblMinute.Text = appendZero(Minutes);
        }
        private void DisplayHour(int i)
        {
            lblHour.Text = appendZero(i);
        }

        //DoStart Method
        private void DoStart()
        {
            
            try
            {
                for (int i= Seconds; i >=0; i++)
                {
                    lblSeconds.Invoke(new DisplaySecondDelegate(DisplaySecond), i);
                    lblMinute.Invoke(new DisplayMinutesDelegate(DisplayMinute), Minutes);
                    lblHour.Invoke(new DisplayHousrDelegate(DisplayHour),Hour);

                    Thread.Sleep(1000);
                    
                    if (i > 59)
                    {

                        Minutes++;
                        i = Seconds;
                        if (Minutes > 59)
                        {
                            Hour++;
                            Minutes=0;
                        }
                    }
                   
                }
                
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            //Invoking doReset Method
            doReset();

        }

        private void doReset()
        {
            try
            {
                //Enable remaining start Button
                Start.Invoke(new EnableButtonDelegate(EnableButton));
                Reset.Enabled = false;
                Stop.Enabled = false;

                // Reset to zero
                Minutes = 0;
                Seconds = 0;
                Hour = 0;

                //for UI
                lblHour.Text = 0.ToString();
                lblMinute.Text = 0.ToString();
                lblSeconds.Text = 0.ToString();
                stopwatchThread.Abort();
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
       

        private void Stop_Click(object sender, EventArgs e)
        {

            // Invoking doStop() Method
            doStop();
          
           
        }

        private void doStop()
        {

            try
            {
                if (Stop.Text == "Stop")
                {
                    Stop.Text = "Resume";

                 
                    stopwatchThread.Suspend();
                    Reset.Enabled = false;
                }
                else
                {
                    Stop.Text = "Stop";
                    stopwatchThread.Resume();
                    Reset.Enabled = true;

                }
            }
            catch (Exception e)
            {

                throw e;
            }

            
        }

    }
}
