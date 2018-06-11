using System;
using System.Threading;
using System.Windows.Forms;

namespace Microsoft.SAPSK.ContosoTours
{
    public sealed class Message
    {
        private static readonly string _standardTitle =
            "Main Event";

        private Message()
        {
        }

        public static void AssignErrorHandlers()
        {
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(
                    Message.UnhandledExceptionHandler);

            Application.ThreadException +=
                new ThreadExceptionEventHandler(
                    Message.ThreadExceptionHandler);
        }

        public static void HandleError(
            string message,
            string messageExtra,
            string mesageToDisplay)
        {
            try
            {
                DisplayError(message, messageExtra);
            }
            catch (Exception) // HandleError should not show exception
            {
            }
        }

        public static void HandleError(
            Exception ex,
            string mesageToDisplay)
        {
            try
            {
                HandleError(
                    ex.Message,
                    ex.ToString(),
                    mesageToDisplay);
            }

            catch (Exception) // HandleError should not show exception
            {
            }
        }

        public static void HandleError(Exception ex)
        {
            try
            {
                HandleError(ex, ex.Message);
            }

            catch (Exception) // HandleError should not show exception
            {
            }
        }

        public static void UnhandledExceptionHandler(
            Object sender,
            UnhandledExceptionEventArgs e)
        {
            try
            {
                // CLR allows non-Exception classes to be thrown
                // so in theory this could be an int, string or decimal
                if (e.ExceptionObject is Exception)
                {
                    HandleError((Exception)e.ExceptionObject);
                }
            }
            catch
            {
                // do not let this method throw an exception
            }
        }

        public static void ThreadExceptionHandler(
              Object sender,
              ThreadExceptionEventArgs e)
        {
            try
            {
                HandleError(e.Exception);
            }
            catch
            {
                // do not let this method throw an exception
            }
        }

        public static DialogResult AskQuestion(string messageText)
        {
            return MessageBox.Show(
                messageText,
                "Question?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
        }

        public static void DisplayMessage(string messageText)
        {
            MessageBox.Show(
                messageText,
                _standardTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Hand,
                MessageBoxDefaultButton.Button1);
        }

        public static DialogResult DeleteMessage(string objectToDelete)
        {
            return MessageBox.Show(
                "Are you sure you wanted to delete: " + objectToDelete + "?",
                "Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
        }

        public static void DisplayInfo(string infoMessage)
        {
            MessageBox.Show(
                infoMessage,
                _standardTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Asterisk,
                MessageBoxDefaultButton.Button1);
        }

        public static void DisplayWarning(string warningMessage)
        {
            MessageBox.Show(
                warningMessage,
                _standardTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);
        }

        public static void SAPDisplayError(IWin32Window owner, string message)
        {
            SAPErrorForm sapErrorForm = new SAPErrorForm(message);
            sapErrorForm.ShowDialog(owner);
            sapErrorForm.Dispose();
        }

        public static void DisplayError(string message, string messageExtra)
        {
            ErrorForm errorForm = new ErrorForm(message, messageExtra);
            errorForm.ShowDialog();
            errorForm.Dispose();
        }
    }

}