using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Str769
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        private  TaskScheduler m_syncContextTaskScheduler; // readonly
        //public Form1()
        //{
        //    m_syncContextTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

        //    Text = "Synchronization Context Task Scheduler Demo";
        //    Visible = true; Width = 600; Height = 100;
        //}
        //private readonly TaskScheduler m_syncContextTaskScheduler
        //    = TaskScheduler.FromCurrentSynchronizationContext();

        private CancellationTokenSource m_cts;
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (m_cts != null)
            {
                m_cts.Cancel();
                m_cts = null;
            }
            else
            {
                Text = "Operation running";
                m_cts = new CancellationTokenSource();


                Task<Int32> t = Task.Run(() => Sum(m_cts.Token, 20_000), m_cts.Token);

                t.ContinueWith(task => Text = "Result: " + task.Result,
                    CancellationToken.None,
                    TaskContinuationOptions.OnlyOnRanToCompletion,
                    m_syncContextTaskScheduler);

                t.ContinueWith(task => Text = "Operation canceled",
                    CancellationToken.None, TaskContinuationOptions.OnlyOnCanceled,
                    m_syncContextTaskScheduler);

                t.ContinueWith(task => Text = "Operation faulted",
                    CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted,
                    m_syncContextTaskScheduler);
            }
            base.OnMouseClick(e);
        }

        private static Int32 Sum(CancellationToken ct, Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; n--)
            {
                ct.ThrowIfCancellationRequested();
                checked { sum += n; }
            }
            return sum;
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Form1";

            m_syncContextTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Text = "Synchronization Context Task Scheduler Demo";
            Visible = true; Width = 600; Height = 100;
        }

        #endregion
    }
}

